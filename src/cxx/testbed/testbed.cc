#include <cassert>
#include <cstdio>

#ifdef _MSC_VER
#include <Windows.h>
#else
#include <dlfcn.h>
#include <unistd.h>
using DWORD = unsigned int;
#endif

#include "../steam/steam_api.h"

using CreateInterfaceFn = void *(*)(const char *);
using GetCallbackFn     = bool (*)(unsigned int pipe, CallbackMsg_t *callback);
using FreeCallbackFn    = bool (*)(unsigned int pipe);

using ISteamClient017 = ISteamClient;

int main(int argc, const char **argv) {
    if (argc == 1) return 0;

    auto path = argv[1];

#ifdef _MSC_VER
    auto handle = LoadLibrary(path);

    auto create_interface_proc = (CreateInterfaceFn)GetProcAddress(handle, "CreateInterface");
    auto get_next_callback     = (GetCallbackFn)GetProcAddress(handle, "Steam_BGetCallback");
    auto free_last_callback    = (FreeCallbackFn)GetProcAddress(handle, "Steam_FreeLastCallback");
#else
    auto handle = dlopen(path, RTLD_NOW);

    if (handle == nullptr) {
        char *error = dlerror();
        printf("Unable to load path\n");
        printf("Error: %s\n", error);
    }

    auto create_interface_proc = (CreateInterfaceFn)dlsym(handle, "CreateInterface");
    auto get_next_callback     = (GetCallbackFn)dlsym(handle, "Steam_BGetCallback");
    auto free_last_callback    = (FreeCallbackFn)dlsym(handle, "Steam_FreeLastCallback");
#endif

    if (create_interface_proc != nullptr) {
        auto steam_client = (ISteamClient017 *)create_interface_proc("SteamClient017");

        if (steam_client != nullptr) {
            auto pipe_handle = steam_client->CreateSteamPipe();
            auto user_handle = steam_client->CreateLocalUser(&pipe_handle, k_EAccountTypeIndividual);
            auto user        = steam_client->GetISteamUser(user_handle, pipe_handle, "SteamUser019");

            if (user == nullptr) printf("Unable to get user!\n");

            CallbackMsg_t msg;

            while (true) {
                while (get_next_callback(pipe_handle, &msg)) {
                    printf("msg from user %u: id: %d size: %u\n", msg.m_hSteamUser, msg.m_iCallback, msg.m_cubParam);

                    free_last_callback(pipe_handle);
                }
#ifdef _MSC_VER
                Sleep(1000);
#else
                sleep(1);
#endif
            }

        } else {
            printf("Unable to find SteamClient017\n");
        }
    } else {
        printf("Unable to find CreateInterface in %s\n", path);
    }

    getc(stdin);

    return 0;
}
