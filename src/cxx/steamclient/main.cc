// dllmain.cpp : Defines the entry point for the DLL application.
#ifdef _MSC_VER
#include <Windows.h>
#include <direct.h>
#else
#include <dirent.h>
#include <dlfcn.h>
#include <stdlib.h>
#include <string.h>
#include <sys/stat.h>
#include <sys/types.h>

#define MAX_PATH 260

#endif

#include <algorithm>
#include <set>
#include <string>

#include "ArgonInterface.hh"

// This file bootstraps the .net core runtime to the unmanaged process
// It then calls out into ArgonCore to recieve an interface to the necessary
// functions from ArgonCore

const char *relative_dotnet_root = "argon";
const char *relative_app_path    = "argon";

#ifdef _MSC_VER
const char *relative_clr_location = "argon/coreclr.dll";
#else
const char *relative_clr_location = "./argon/libcoreclr.so";
#endif

#ifdef _MSC_VER
char *realpath(const char *path, char *resolved) {
    GetFullPathName(path, MAX_PATH, resolved, nullptr);
    return resolved;
}
#endif

#ifndef _MSC_VER
#define __stdcall
#endif

// Refer to http://yizhang82.me/hosting-coreclr file for how to load .net core

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
#endif

#ifdef _MSC_VER

#else
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

void *       coreclr_handle;
unsigned int domain_id;

// Does the heavy lifting of calling starting the clr and getting the necessary function pointers
void load_runtime() {
    char app_path[MAX_PATH];
    realpath(relative_app_path, app_path);

    char dotnet_root[MAX_PATH];
    realpath(relative_dotnet_root, dotnet_root);

    void *clr_handle;

#ifdef _MSC_VER
    clr_handle = LoadLibraryA(relative_clr_location);
#else
    clr_handle              = dlopen(relative_clr_location, RTLD_NOW);
#endif

    if (!clr_handle) {
        printf("Unable to find libcoreclr.so\n");
#ifndef _MSC_VER
        printf("Error:\n%s\n", dlerror());
#endif
        return;
    }

#ifdef _MSC_VER
    coreclr_initialize      = (CoreCLR_InitialiseFn)GetProcAddress((HMODULE)clr_handle, "coreclr_initialize");
    coreclr_create_delegate = (CoreCLR_CreateDelegateFn)GetProcAddress((HMODULE)clr_handle, "coreclr_create_delegate");
#else
    coreclr_initialize      = (CoreCLR_InitialiseFn)dlsym(clr_handle, "coreclr_initialize");
    coreclr_create_delegate = (CoreCLR_CreateDelegateFn)dlsym(clr_handle, "coreclr_create_delegate");
#endif

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

    // Attempt to initialise .net core runtime
    int ret = coreclr_initialize(
        app_path,                                 // exePath
        "host",                                   // appDomainFriendlyName
        sizeof(property_values) / sizeof(char *), // propertyCount
        property_keys,                            // propertyKeys
        property_values,                          // propertyValues
        &coreclr_handle,                          // hostHandle
        &domain_id                                // domainId
    );

    if (ret != 0) {
        printf("CoreCLR failed to load %d (0x%X)\n", ret, ret);
        return;
    } else {
        printf("CoreCLR loaded successfully\n");
    }
}

void on_attach(void *param) {
    load_runtime();

    // Call out to our dll for the second part of the bootstrap procedure
    using RecieveArgonCore = ArgonCore *(*)();
    RecieveArgonCore recieve_argon_core;

    int ret = coreclr_create_delegate(coreclr_handle, domain_id, "ArgonCore", "ArgonCore.Export", "RecieveArgonCore", (void **)&recieve_argon_core);

    if (ret == 0) {
        argon = recieve_argon_core();
    } else {
        printf("CoreCLR failed to create delegate %d (0x%X)\n", ret, ret);
        return;
    }
}

void on_detach() {
}

#ifdef _MSC_VER
BOOL APIENTRY DllMain(HMODULE hModule,
                      DWORD   ul_reason_for_call,
                      LPVOID  lpReserved) {
    switch (ul_reason_for_call) {
    case DLL_PROCESS_ATTACH:
        // TODO: ideally this should be a creatprocess call but with regards to CppTestBed this cant be done.
        on_attach(nullptr);
        //CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)&on_attach, 0, 0, NULL);
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}

#else

void __attribute__((constructor)) init() {
    on_attach(nullptr);
}

void __attribute__((destructor)) shutdown() {
    on_detach();
}

#endif
