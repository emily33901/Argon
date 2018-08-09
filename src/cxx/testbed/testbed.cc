#include <cassert>
#include <cstdio>

#ifdef _MSC_VER
#include <Windows.h>
void sleep(int seconds) {
    return Sleep(seconds * 1000);
}
#else
#include <dlfcn.h>
#include <unistd.h>
#endif

#include "../steam/steam_api.h"

// Stolen from open-steamworks
const char *CSteamID::Render() const {
    const int k_cBufLen = 37;
    const int k_cBufs   = 4;
    char *    pchBuf;

    static char rgchBuf[k_cBufs][k_cBufLen];
    static int  nBuf = 0;

    pchBuf = rgchBuf[nBuf++];
    nBuf %= k_cBufs;

    switch (m_steamid.m_comp.m_EAccountType) {
    case k_EAccountTypeAnonGameServer:
        sprintf(pchBuf, "[A:%u:%u:%u]", m_steamid.m_comp.m_EUniverse, m_steamid.m_comp.m_unAccountID, m_steamid.m_comp.m_unAccountInstance);
        break;
    case k_EAccountTypeGameServer:
        sprintf(pchBuf, "[G:%u:%u]", m_steamid.m_comp.m_EUniverse, m_steamid.m_comp.m_unAccountID);
        break;
    case k_EAccountTypeMultiseat:
        sprintf(pchBuf, "[M:%u:%u:%u]", m_steamid.m_comp.m_EUniverse, m_steamid.m_comp.m_unAccountID, m_steamid.m_comp.m_unAccountInstance);
        break;
    case k_EAccountTypePending:
        sprintf(pchBuf, "[P:%u:%u]", m_steamid.m_comp.m_EUniverse, m_steamid.m_comp.m_unAccountID);
        break;
    case k_EAccountTypeContentServer:
        sprintf(pchBuf, "[C:%u:%u]", m_steamid.m_comp.m_EUniverse, m_steamid.m_comp.m_unAccountID);
        break;
    case k_EAccountTypeClan:
        sprintf(pchBuf, "[g:%u:%u]", m_steamid.m_comp.m_EUniverse, m_steamid.m_comp.m_unAccountID);
        break;
    case k_EAccountTypeChat:
        switch (m_steamid.m_comp.m_unAccountInstance & ~k_EChatAccountInstanceMask) {
        case k_EChatInstanceFlagClan:
            sprintf(pchBuf, "[c:%u:%u]", m_steamid.m_comp.m_EUniverse, m_steamid.m_comp.m_unAccountID);
            break;
        case k_EChatInstanceFlagLobby:
            sprintf(pchBuf, "[L:%u:%u]", m_steamid.m_comp.m_EUniverse, m_steamid.m_comp.m_unAccountID);
            break;
        default:
            sprintf(pchBuf, "[T:%u:%u]", m_steamid.m_comp.m_EUniverse, m_steamid.m_comp.m_unAccountID);
            break;
        }
        break;
    case k_EAccountTypeInvalid:
        sprintf(pchBuf, "[I:%u:%u]", m_steamid.m_comp.m_EUniverse, m_steamid.m_comp.m_unAccountID);
        break;
    case k_EAccountTypeIndividual:
        sprintf(pchBuf, "[U:%u:%u]", m_steamid.m_comp.m_EUniverse, m_steamid.m_comp.m_unAccountID);
        break;
    default:
        sprintf(pchBuf, "[i:%u:%u]", m_steamid.m_comp.m_EUniverse, m_steamid.m_comp.m_unAccountID);
        break;
    }

    return pchBuf;
}

#include "clientengine.hh"
#include "clientfriends.hh"
#include "clientuser.hh"
#include "mappedtest.hh"

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

    if (handle == nullptr) {
        printf("Unable to load path\n");
        return 1;
    }

    auto create_interface_proc = (CreateInterfaceFn)GetProcAddress(handle, "CreateInterface");
    auto get_next_callback     = (GetCallbackFn)GetProcAddress(handle, "Steam_BGetCallback");
    auto free_last_callback    = (FreeCallbackFn)GetProcAddress(handle, "Steam_FreeLastCallback");
#else
    auto handle = dlopen(path, RTLD_NOW);

    if (handle == nullptr) {
        char *error = dlerror();
        printf("Unable to load path\n");
        printf("Error: %s\n", error);
        return 1;
    }

    auto create_interface_proc = (CreateInterfaceFn)dlsym(handle, "CreateInterface");
    auto get_next_callback     = (GetCallbackFn)dlsym(handle, "Steam_BGetCallback");
    auto free_last_callback    = (FreeCallbackFn)dlsym(handle, "Steam_FreeLastCallback");
#endif

    if (create_interface_proc != nullptr) {
        auto client_engine = (IClientEngine005 *)create_interface_proc("CLIENTENGINE_INTERFACE_VERSION005");
        auto steam_client  = (ISteamClient017 *)create_interface_proc("SteamClient017");

        if (steam_client != nullptr) {

            HSteamPipe pipe_handle; //    = client_engine->CreateSteamPipe();
            auto       user_handle = client_engine->CreateLocalUser(&pipe_handle, k_EAccountTypeIndividual);

            if (pipe_handle == 0 || user_handle == 0) {
                printf("Unable to get handles (%d, %d)\n", pipe_handle, user_handle);
                return 1;
            }

            {
                printf("\n\nMapped Tests!\n\n");
                // Use GetISteamUtils as it gets with pipe but no user
                auto mapped_test = (IMappedTest001 *)steam_client->GetISteamUtils(pipe_handle, "MappedTest001");

                int a = 3;
                int b = 3;
                int c = 10;

                mapped_test->PointerTest(&a, &b, &c);
                printf("PointerTest: Testing pointers across IPC\n");
                printf("Input (3, 3, 10)\n");
                printf("Expected (4, 5, 6) -> 15\n");
                printf("Actual (%d, %d, %d) -> 15\n", a, b, c);

                char buffer[1024];
                memset(buffer, 0, sizeof(buffer));
                strcpy(buffer, "OverrideMe");

                int bytes_wrote = mapped_test->BufferTest(buffer, 1024);

                printf("BufferTest: Testing buffers across IPC\n");
                printf("Input (\"OverrideMe\")\n");
                printf("Expected: (\"13 characters\") -> 13\n");
                printf("Actual (\"%s\") -> %d\n", buffer, bytes_wrote);

                MappedTestStruct s;
                memset(&s, 0x11, sizeof(MappedTestStruct));
                mapped_test->TestStruct(&s, sizeof(MappedTestStruct));

                printf("StructTest: Testing struct alignment in buffers\n");
                printf("%hhx %x %x %hhx %llx\n", s.a, s.b, s.c, s.d, s.e);

                printf("\n\nMapped tests finished\n\n");

                getc(stdin);
            }

            auto user           = (IClientUser001 *)client_engine->GetIClientUser(user_handle, pipe_handle, "CLIENTUSER_INTERFACE_VERSION001");
            auto client_friends = (IClientFriends001 *)client_engine->GetIClientFriends(user_handle, pipe_handle, "CLIENTFRIENDS_INTERFACE_VERSION001");

            auto friends = (ISteamFriends *)client_engine->GetIClientFriends(user_handle, pipe_handle, "SteamFriends015");

            if (user == nullptr) {
                printf("Unable to get user!\n");
                return 1;
            }

            if (friends == nullptr) {
                printf("Unable to get friends!\n");
                return 1;
            }

            char username[128];
            char password[128];

            printf("Enter your username: ");
            scanf("%s", username);

            printf("Enter your password: ");
            scanf("%s", password);

            user->LogOnWithPassword(username, password);

            // TODO: wait to see if 2factor is necessary

            CallbackMsg_t msg;
            while (true) {
                while (get_next_callback(pipe_handle, &msg)) {
                    switch (msg.m_iCallback) {
                    case SteamServersConnected_t::k_iCallback: {
                        printf("Connected!\n");
                        client_friends->SetPersonaState(EPersonaState::k_EPersonaStateOnline);
                    } break;
                    case SteamServerConnectFailure_t::k_iCallback: {
                        auto cb = (SteamServerConnectFailure_t *)msg.m_pubParam;

                        auto result = cb->m_eResult;

                        printf("Failed to connect (%d)\n", result);

                        switch (result) {
                        case EResult::k_EResultAccountLoginDeniedNeedTwoFactor: {
                            printf("Account needs 2fa!\n");

                            char twofactor[128];

                            printf("Enter 2fa: ");
                            scanf("%s", twofactor);

                            user->SetTwoFactorCode(twofactor);

                            user->LogOn({});
                        } break;
                        case EResult::k_EResultNoConnection: {
                            printf("No connection...\n");
                            printf("Retrying in 5 seconds\n");
                            sleep(5);
                            user->LogOnWithPassword(username, password);
                        } break;
                        case EResult::k_EResultInvalidPassword: {
                            printf("Invalid username / password...\n");

                            printf("Enter your username: ");
                            scanf("%s", username);

                            printf("Enter your password: ");
                            scanf("%s", password);

                            user->LogOnWithPassword(username, password);
                        } break;
                        default: {
                            printf("Unknown eresult!\n");
                        } break;
                        }
                    } break;
                    case PersonaStateChange_t::k_iCallback: {

                        auto cb       = (PersonaStateChange_t *)msg.m_pubParam;
                        auto user_id  = CSteamID(cb->m_ulSteamID);
                        auto user_acc = user_id.BIndividualAccount();

                        auto change = cb->m_nChangeFlags;

                        if (user_acc) {
                            printf("State change for user %s (%s)", user_id.Render(), friends->GetFriendPersonaName(user_id));
                        } else {
                            printf("State change for unknown %s", user_id.Render());
                        }

                        if (change & EPersonaChange::k_EPersonaChangeStatus) {
                            printf(" [state changed to %d]", friends->GetFriendPersonaState(user_id));
                        }

                        if (change & EPersonaChange::k_EPersonaChangeComeOnline) {
                            printf(" [Went offline]");
                        } else if (change & EPersonaChange::k_EPersonaChangeComeOnline) {
                            printf(" [Came online]");
                        }

                        printf("\n");
                    } break;
                    case FriendChatMsg_t::k_iCallback: {
                        auto cb = (FriendChatMsg_t *)msg.m_pubParam;

                        auto user_id = CSteamID(cb->m_ulSenderID);

                        printf("Message index is %d (%X)\n", cb->m_iChatID, cb->m_iChatID);
                        printf("Message is limited? %s\n", cb->m_bLimitedAccount ? "true" : "false");

                        // Ignore user is typing messages
                        if (cb->m_eChatEntryType == EChatEntryType::k_EChatEntryTypeTyping) {
                            printf("%s (%s) is typing (1)...\n", user_id.Render(), friends->GetFriendPersonaName(user_id));
                            break;
                        }

                        char message[2000];
                        memset(message, 0, sizeof(message));
                        EChatEntryType entry;

                        auto msg = friends->GetFriendMessage(user_id, cb->m_iChatID, message, sizeof(message), &entry);

                        if (entry == EChatEntryType::k_EChatEntryTypeTyping) {
                            printf("%s (%s) is typing (2)...\n", user_id.Render(), friends->GetFriendPersonaName(user_id));
                            break;
                        }

                        printf("Message from %s (%s) Length %d\n>>%s<<\n", user_id.Render(), friends->GetFriendPersonaName(user_id), msg, message);
                    } break;
                    default: {
                        printf("Unknown message from user %u: id: %d size: %u\n", msg.m_hSteamUser, msg.m_iCallback, msg.m_cubParam);
                    } break;
                    }

                    free_last_callback(pipe_handle);
                }

                sleep(1);
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

// #pragma pack(push, 4)
// struct packing_helper {
//     uint8  byte_a;
//     uint16 align_16;

//     uint8  byte_b;
//     uint32 align_32;

//     uint8  byte_c;
//     uint64 align_64;
// };

// enum class packing_test {
//     a = offsetof(packing_helper, align_16) - offsetof(packing_helper, byte_a),
//     b = offsetof(packing_helper, align_32) - offsetof(packing_helper, byte_b),
//     c = offsetof(packing_helper, align_64) - offsetof(packing_helper, byte_c),
// };
// #pragma pack(pop)
