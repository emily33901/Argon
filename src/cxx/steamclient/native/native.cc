#include "native.hh"

// This file bootstraps the .net core runtime to the unmanaged process
// Refer to http://yizhang82.me/hosting-coreclr for how to load .net core

#ifdef _MSC_VER
#include <Windows.h>
#include <direct.h>

static char *realpath(const char *path, char *resolved) {
    GetFullPathName(path, MAX_PATH, resolved, nullptr);
    return resolved;
}
#else
#include <dirent.h>
#include <dlfcn.h>
#include <stdlib.h>
#include <string.h>
#include <sys/stat.h>
#include <sys/types.h>

#define MAX_PATH 260
#define __stdcall
#endif

#include <algorithm>
#include <set>
#include <string>

// Stolen from coreclr hosts
void add_files_from_directory_to_tpa_list(const char *directory, std::string &tpaList) {
#ifdef _MSC_VER
    const char *tpaExtensions[] = {
        "*.ni.dll", // Probe for .ni.dll first so that it's preferred if ni and il coexist in the same dir
        "*.dll",
        "*.ni.exe",
        "*.exe",
        "*.ni.winmd",
        "*.winmd",
    };
#else
    const char *const tpaExtensions[] = {
        ".ni.dll", // Probe for .ni.dll first so that it's preferred if ni and il coexist in the same dir
        ".dll",
        ".ni.exe",
        ".exe",
    };

    DIR *dir = opendir(directory);
    if (dir == nullptr) {
        return;
    }
#endif

    std::set<std::string> addedAssemblies;

    // Walk the directory for each extension separately so that we first get files with .ni.dll extension,
    // then files with .dll extension, etc.
    for (int extIndex = 0; extIndex < sizeof(tpaExtensions) / sizeof(tpaExtensions[0]); extIndex++) {
        const char *ext       = tpaExtensions[extIndex];
        int         extLength = strlen(ext);

#ifdef _MSC_VER
        std::string query;
        query.assign(directory);
        query.append(ext);

        WIN32_FIND_DATA data;
        HANDLE          findHandle = FindFirstFileA(query.c_str(), &data);

        if (findHandle != INVALID_HANDLE_VALUE) {
            do {
                if (!(data.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY)) {
                    // It seems that CoreCLR doesn't always use the first instance of an assembly on the TPA list (ni's may be preferred
                    // over il, even if they appear later). So, only include the first instance of a simple assembly name to allow
                    // users the opportunity to override Framework assemblies by placing dlls in %CORE_LIBRARIES%

                    // ToLower for case-insensitive comparisons
                    std::string filename;
                    filename.assign(data.cFileName);
                    std::transform(filename.begin(), filename.end(), filename.begin(), ::tolower);

                    // Check if the extension matches the one we are looking for
                    // +1 becuase windows querys have the wildcard
                    int extPos = filename.length() - extLength + 1;

                    std::string filenameWithoutExt(filename.substr(0, extPos));

                    // Make sure if we have an assembly with multiple extensions present,
                    // we insert only one version of it.
                    if (addedAssemblies.find(filenameWithoutExt) == addedAssemblies.end()) {
                        addedAssemblies.insert(filenameWithoutExt);

                        tpaList.append(directory);
                        //tpaList.append("/");
                        tpaList.append(filename);
                        tpaList.append(";");
                    }
                }
            } while (0 != FindNextFile(findHandle, &data));

            FindClose(findHandle);
        }

#else
        struct dirent *entry;

        // For all entries in the directory
        while ((entry = readdir(dir)) != nullptr) {
            // We are interested in files only
            switch (entry->d_type) {
            case DT_REG:
                break;

                // Handle symlinks and file systems that do not support d_type
            case DT_LNK:
            case DT_UNKNOWN: {
                std::string fullFilename;

                fullFilename.append(directory);
                fullFilename.append("/");
                fullFilename.append(entry->d_name);

                struct stat sb;
                if (stat(fullFilename.c_str(), &sb) == -1) {
                    continue;
                }

                if (!S_ISREG(sb.st_mode)) {
                    continue;
                }
            } break;

            default:
                continue;
            }

            std::string filename(entry->d_name);

            // Check if the extension matches the one we are looking for
            int extPos = filename.length() - extLength;
            if ((extPos <= 0) || (filename.compare(extPos, extLength, ext) != 0)) {
                continue;
            }

            std::string filenameWithoutExt(filename.substr(0, extPos));

            // Make sure if we have an assembly with multiple extensions present,
            // we insert only one version of it.
            if (addedAssemblies.find(filenameWithoutExt) == addedAssemblies.end()) {
                addedAssemblies.insert(filenameWithoutExt);

                tpaList.append(directory);
                tpaList.append("/");
                tpaList.append(filename);
                tpaList.append(":");
            }
        }

        // Rewind the directory stream to be able to iterate over it for the next extension
        rewinddir(dir);
#endif
    }

#ifndef _MSC_VER
    closedir(dir);
#endif
}

// Functions exported from coreclr.dll or libcoreclr.so
using CoreCLR_InitialiseFn = int(__stdcall *)(
    const char *  exePath,
    const char *  appDomainFriendlyName,
    int           propertyCount,
    const char ** propertyKeys,
    const char ** propertyValues,
    void **       hostHandle,
    unsigned int *domainId);
CoreCLR_InitialiseFn coreclr_initialize = nullptr;

using CoreCLR_CreateDelegateFn = int(__stdcall *)(
    void *       hostHandle,
    unsigned int domainId,
    const char * entryPointAssemblyName,
    const char * entryPointTypeName,
    const char * entryPointMethodName,
    void **      delegate);
CoreCLR_CreateDelegateFn coreclr_create_delegate = nullptr;

// Handle to our coreclr instance.
static void *       coreclr_handle;
static unsigned int domain_id;

bool native_loader::load_runtime(const char *relative_location, const char *relative_dotnet_root) {

    // Convert these into absolute paths as necessary by dotnet
    char app_path[MAX_PATH];
    realpath(relative_location, app_path);

    char dotnet_root[MAX_PATH];
    realpath(relative_dotnet_root, dotnet_root);

    void *clr_lib_handle = nullptr;

    {
        // Attempt to load coreclr from the paths provided
#ifdef _MSC_VER
        auto clr_location = std::string(dotnet_root).append("\\coreclr.dll");
        clr_lib_handle    = LoadLibraryA(clr_location.c_str());
#else
        auto clr_location       = std::string(dotnet_root).append("/libcoreclr.so");
        clr_lib_handle          = dlopen(clr_location.c_str(), RTLD_NOW);
#endif

        if (clr_lib_handle == nullptr) {
            printf("Unable to find coreclr library\n");
#ifndef _MSC_VER
            printf("Error:\n%s\n", dlerror());
#endif
            return false;
        }
    }

    // Get the functions that we need out of the libraries
    {
#ifdef _MSC_VER
        coreclr_initialize      = (CoreCLR_InitialiseFn)GetProcAddress((HMODULE)clr_lib_handle, "coreclr_initialize");
        coreclr_create_delegate = (CoreCLR_CreateDelegateFn)GetProcAddress((HMODULE)clr_lib_handle, "coreclr_create_delegate");
#else
        coreclr_initialize      = (CoreCLR_InitialiseFn)dlsym(clr_lib_handle, "coreclr_initialize");
        coreclr_create_delegate = (CoreCLR_CreateDelegateFn)dlsym(clr_lib_handle, "coreclr_create_delegate");
#endif

        if (coreclr_initialize == nullptr || coreclr_create_delegate == nullptr) {
            printf("Unable to get functions from coreclr\n");
            return false;
        }
    }

    // Construct the trusted platform assemblies list
    std::string tpa_list;
    add_files_from_directory_to_tpa_list(dotnet_root, tpa_list);

    const char *property_keys[] = {
        "APP_PATHS",
        "TRUSTED_PLATFORM_ASSEMBLIES"};
    const char *property_values[] = {
        // APP_PATHS
        app_path,
        // TRUSTED_PLATFORM_ASSEMBLIES
        tpa_list.c_str()};

    // Finally attempt to load the clr
    int ret = coreclr_initialize(
        app_path,                                 // exePath
        "host",                                   // appDomainFriendlyName
        sizeof(property_values) / sizeof(char *), // propertyCount
        property_keys,                            // propertyKeys
        property_values,                          // propertyValues
        &coreclr_handle,                          // hostHandle
        &domain_id                                // domainId
    );

    if (!ret) return true;

    printf("Unable to load coreclr (HR: 0x%X)\n", ret);

    return false;
}

void *native_loader::create_delegate(const char *assembly_name, const char *type_name, const char *entry_point) {
    void *created_delegate = nullptr;
    int   ret              = coreclr_create_delegate(coreclr_handle, domain_id, assembly_name, type_name, entry_point, &created_delegate);

    if (!ret) return created_delegate;

    printf("Unable to create delegate (HR: 0x%X)\n", ret);

    return nullptr;
}