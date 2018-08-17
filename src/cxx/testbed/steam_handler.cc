#include "testbed.hh"

#include <algorithm>
#include <utility>
#include <vector>

// Global steam objects
namespace steam {
CreateInterfaceFn create_interface;
GetCallbackFn     get_next_callback;
FreeCallbackFn    free_last_callback;

ISteamClient017 * client;
IClientEngine005 *engine;

HSteamPipe pipe_handle;
HSteamUser user_handle;

IClientUser001 *   user;
IClientFriends001 *client_friends;
ISteamFriends015 * steam_friends;

IMappedTest001 *mapped_test;
} // namespace steam

std::vector<std::pair<int, steam::CallbackListener>> callback_listeners;

void steam::handle_callback(const CallbackMsg_t &c) {
    bool handled = false;

    for (auto &p : callback_listeners) {
        auto id       = p.first;
        auto listener = p.second;

        if (id == c.m_iCallback) {
            listener(c.m_pubParam);
            handled = true;
        }
    }

    if (!handled) printf("Unknown message from user %u: id: %d size: %u\n",
                         c.m_hSteamUser, c.m_iCallback, c.m_cubParam);
}

void steam::process_callbacks() {
    CallbackMsg_t c;

    while (steam::get_next_callback(steam::pipe_handle, &c)) {
        handle_callback(c);
        steam::free_last_callback(steam::pipe_handle);
    }
}

void steam::add_listener(int id, CallbackListener listener) {
    callback_listeners.push_back(std::make_pair(id, listener));
}

void steam::remove_listener(CallbackListener listener) {
    std::remove_if(callback_listeners.begin(), callback_listeners.end(),
                   [listener](auto &x) -> bool { return x.second == listener; });
}

// Stolen from open-steamworks
const char *CSteamID::Render() const {
    const int temp_buffer_len  = 37;
    const int temp_max_buffers = 4;

    static char temp_buffers[temp_max_buffers][temp_buffer_len];
    static int  current_buffer = 0;

    char *buffer = temp_buffers[current_buffer++];
    current_buffer %= temp_max_buffers;

    switch (m_steamid.m_comp.m_EAccountType) {
    case k_EAccountTypeAnonGameServer:
        sprintf(buffer, "[A:%u:%u:%u]", m_steamid.m_comp.m_EUniverse, m_steamid.m_comp.m_unAccountID, m_steamid.m_comp.m_unAccountInstance);
        break;
    case k_EAccountTypeGameServer:
        sprintf(buffer, "[G:%u:%u]", m_steamid.m_comp.m_EUniverse, m_steamid.m_comp.m_unAccountID);
        break;
    case k_EAccountTypeMultiseat:
        sprintf(buffer, "[M:%u:%u:%u]", m_steamid.m_comp.m_EUniverse, m_steamid.m_comp.m_unAccountID, m_steamid.m_comp.m_unAccountInstance);
        break;
    case k_EAccountTypePending:
        sprintf(buffer, "[P:%u:%u]", m_steamid.m_comp.m_EUniverse, m_steamid.m_comp.m_unAccountID);
        break;
    case k_EAccountTypeContentServer:
        sprintf(buffer, "[C:%u:%u]", m_steamid.m_comp.m_EUniverse, m_steamid.m_comp.m_unAccountID);
        break;
    case k_EAccountTypeClan:
        sprintf(buffer, "[g:%u:%u]", m_steamid.m_comp.m_EUniverse, m_steamid.m_comp.m_unAccountID);
        break;
    case k_EAccountTypeChat:
        switch (m_steamid.m_comp.m_unAccountInstance & ~k_EChatAccountInstanceMask) {
        case k_EChatInstanceFlagClan:
            sprintf(buffer, "[c:%u:%u]", m_steamid.m_comp.m_EUniverse, m_steamid.m_comp.m_unAccountID);
            break;
        case k_EChatInstanceFlagLobby:
            sprintf(buffer, "[L:%u:%u]", m_steamid.m_comp.m_EUniverse, m_steamid.m_comp.m_unAccountID);
            break;
        default:
            sprintf(buffer, "[T:%u:%u]", m_steamid.m_comp.m_EUniverse, m_steamid.m_comp.m_unAccountID);
            break;
        }
        break;
    case k_EAccountTypeInvalid:
        sprintf(buffer, "[I:%u:%u]", m_steamid.m_comp.m_EUniverse, m_steamid.m_comp.m_unAccountID);
        break;
    case k_EAccountTypeIndividual:
        sprintf(buffer, "[U:%u:%u]", m_steamid.m_comp.m_EUniverse, m_steamid.m_comp.m_unAccountID);
        break;
    default:
        sprintf(buffer, "[i:%u:%u]", m_steamid.m_comp.m_EUniverse, m_steamid.m_comp.m_unAccountID);
        break;
    }

    return buffer;
}
