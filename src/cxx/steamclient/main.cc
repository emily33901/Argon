// dllmain.cpp : Defines the entry point for the DLL application.
#ifdef _MSC_VER
#include <Windows.h>
#else
#include <dirent.h>
#include <dlfcn.h>
#include <string.h>

#endif

#include <stdio.h>

#include "native/native.hh"

#include "argon_interface.hh"

// some constants for where we expect to find argon
// and the tpas that go with
const char *dotnet_root = "argon";
const char *argon_root  = "argon";

void on_attach(void *param) {
    // Attempt to load the runtime
    if (!native_loader::load_runtime(argon_root, dotnet_root)) {
        printf("Failed to load .net core runtime. Aborting!\n");
        return;
    }

    // Attempt to get our ArgonCore interface from argon
    using RecieveArgonCore  = ArgonCore *(*)();
    auto recieve_argon_core = (RecieveArgonCore)native_loader::create_delegate("ArgonCore", "ArgonCore.Export", "RecieveArgonCore");

    if (recieve_argon_core == nullptr) {
        printf("Failed to create delegate to ArgonCore.Export.RecieveArgonCore. Arborting!\n");
        return;
    }

    argon = recieve_argon_core();
}

void on_detach() {
}

#ifdef _MSC_VER
BOOL APIENTRY DllMain(HMODULE hModule,
                      DWORD   ul_reason_for_call,
                      LPVOID  lpReserved) {
    switch (ul_reason_for_call) {
    case DLL_PROCESS_ATTACH:
        // TODO: ideally this should be a createprocess call but with regards to CppTestBed this cant be done.
        //CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)&on_attach, 0, 0, NULL);
        on_attach(nullptr);
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
