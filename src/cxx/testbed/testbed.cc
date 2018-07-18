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

#include "clientengine.hh"
#include "clientuser.hh"

using CreateInterfaceFn = void *(*)(const char *);
using GetCallbackFn     = bool (*)(unsigned int pipe, CallbackMsg_t *callback);
using FreeCallbackFn    = bool (*)(unsigned int pipe);

using ISteamClient017 = ISteamClient;

int main(int argc, const char **argv) {
    if (argc == 1) {
        printf("Missing args\n");
        return 0;
    }

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
        auto client_engine = (IClientEngine005 *)create_interface_proc("CLIENTENGINE_INTERFACE_VERSION005");
        auto steam_client  = (ISteamClient017 *)create_interface_proc("SteamClient017");

        if (steam_client != nullptr) {
            HSteamPipe pipe_handle = client_engine->CreateSteamPipe();
            auto       user_handle = client_engine->CreateLocalUser(&pipe_handle, k_EAccountTypeIndividual);
            auto       user        = (IClientUser001 *)client_engine->GetIClientUser(user_handle, pipe_handle, "CLIENTUSER_INTERFACE_VERSION001");

            if (user == nullptr) {
                printf("Unable to get user!\n");
                return 1;
            }

            char username[128];
            char password[128];

            printf("Enter your username: ");
            scanf("%s", username);

            printf("Enter your password: ");
            scanf("%s", password);

            user->LogOnWithPassword(username, password);

            char twofactor[128];

            printf("Enter 2fa: ");
            scanf("%s", twofactor);

            user->SetTwoFactorCode(twofactor);

            user->LogOn({});

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
