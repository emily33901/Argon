#include "testbed.hh"

#ifdef _MSC_VER
const char *steam_path = "steamclient.dll";
#else
const char *steam_path = "./libsteamclient.so";
#endif

#define check_null(var)             \
    if (!var) {                     \
        printf(#var " is null!\n"); \
        return false;               \
    }

bool load_steam_dll() {
#ifdef _MSC_VER
    auto handle = LoadLibrary(steam_path);

    if (handle == nullptr) {
        printf("Unable to load steam from steam_path\n");
        return false;
    }

    steam::create_interface   = (CreateInterfaceFn)GetProcAddress(handle, "CreateInterface");
    steam::get_next_callback  = (GetCallbackFn)GetProcAddress(handle, "Steam_BGetCallback");
    steam::free_last_callback = (FreeCallbackFn)GetProcAddress(handle, "Steam_FreeLastCallback");
#else
    auto handle = dlopen(steam_path, RTLD_NOW);

    if (handle == nullptr) {
        char *error = dlerror();
        printf("Unable to load steam from steam_path\n");
        printf("Error: %s\n", error);
        return false;
    }

    steam::create_interface   = (CreateInterfaceFn)dlsym(handle, "CreateInterface");
    steam::get_next_callback  = (GetCallbackFn)dlsym(handle, "Steam_BGetCallback");
    steam::free_last_callback = (FreeCallbackFn)dlsym(handle, "Steam_FreeLastCallback");
#endif

    check_null(steam::create_interface);
    check_null(steam::get_next_callback);
    check_null(steam::free_last_callback);

    return true;
}

bool get_steam_interfaces() {
    steam::engine = (IClientEngine005 *)steam::create_interface("CLIENTENGINE_INTERFACE_VERSION005");
    steam::client = (ISteamClient017 *)steam::create_interface("SteamClient017");

    check_null(steam::client);
    check_null(steam::engine);

    steam::pipe_handle = 0;
    steam::user_handle = 0;

    steam::user_handle = steam::engine->CreateLocalUser(&steam::pipe_handle, k_EAccountTypeIndividual);

    check_null(steam::pipe_handle);
    check_null(steam::user_handle);

    if (!steam::engine->IsValidHSteamUserPipe(steam::pipe_handle, steam::user_handle)) {
        printf("'valid' (non zero) user and pipe handle are not valid!\nThis is probably a bug.\n");
        return false;
    }

    steam::user = (IClientUser001 *)steam::engine->GetIClientUser(
        steam::user_handle,
        steam::pipe_handle,
        "CLIENTUSER_INTERFACE_VERSION001");

    steam::client_friends = (IClientFriends001 *)steam::engine->GetIClientFriends(
        steam::user_handle,
        steam::pipe_handle,
        "CLIENTFRIENDS_INTERFACE_VERSION001");

    steam::steam_friends = (ISteamFriends015 *)steam::client->GetISteamFriends(
        steam::user_handle,
        steam::pipe_handle,
        "SteamFriends015");

    check_null(steam::user);
    check_null(steam::steam_friends);
    check_null(steam::client_friends);

    return true;
}

#undef check_null

bool run_tests() {
    // We need a way of checking the output of a test
    // To see whether it succeeded or failed

    printf("\n\nMapped Tests!\n\n");

    // Use GetISteamUtils as it gets with pipe but no user
    auto mapped_test = (IMappedTest001 *)steam::client->GetISteamUtils(steam::pipe_handle, "MappedTest001");

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

    return true;
}

void login_to_steam() {
    char username[128];
    char password[128];

    printf("Enter your username: ");
    scanf("%s", username);

    printf("Enter your password: ");
    scanf("%s", password);

    steam::user->LogOnWithPassword(username, password);
}

class ListenerRegister {
public:
    static ListenerRegister *head;
    ListenerRegister *       next;

    steam::CallbackListener listener;
    int                     id;

    ListenerRegister(int id, steam::CallbackListener l) : id(id), listener(l) {
        next = head;
        head = this;
    }

    static void register_all() {
        for (auto l = head;
             l != nullptr;
             l = l->next) {
            steam::add_listener(l->id, l->listener);
        }
    }
};

ListenerRegister *ListenerRegister::head = nullptr;

#define listener(callback)                                                                                                   \
    void             on_##callback(const callback *);                                                                        \
    ListenerRegister register_##callback = ListenerRegister(callback::k_iCallback, (steam::CallbackListener)&on_##callback); \
    void             on_##callback(const callback *cb)

listener(SteamServerConnectFailure_t) {
    auto result = cb->m_eResult;

    printf("Failed to connect (%d)\n", result);

    switch (result) {
    case EResult::k_EResultAccountLoginDeniedNeedTwoFactor: {
        printf("Account needs 2fa!\n");

        char twofactor[128];

        printf("Enter 2fa: ");
        scanf("%s", twofactor);

        steam::user->SetTwoFactorCode(twofactor);

        steam::user->LogOn({});
    } break;
    case EResult::k_EResultNoConnection: {
        printf("No connection...\n");
        printf("Retrying in 5 seconds\n");
        sleep(5);

        // We already have login information stored LogOn will just reuse that
        steam::user->LogOn({});
    } break;
    case EResult::k_EResultInvalidPassword: {
        printf("Invalid username / password...\n");

        login_to_steam();
    } break;
    default: {
        printf("Unknown eresult!\n");
    } break;
    }
}

listener(SteamServersConnected_t) {
    printf("Connected!\n");

    printf("Setting state to online\n");
    steam::client_friends->SetPersonaState(EPersonaState::k_EPersonaStateOnline);
}

listener(PersonaStateChange_t) {
    auto user_id  = CSteamID(cb->m_ulSteamID);
    auto user_acc = user_id.BIndividualAccount();

    auto change = cb->m_nChangeFlags;

    if (user_acc) {
        printf("State change for user %s (%s)", user_id.Render(), steam::steam_friends->GetFriendPersonaName(user_id));
    } else {
        printf("State change for unknown %s", user_id.Render());
    }

    if (change & EPersonaChange::k_EPersonaChangeStatus) {
        printf(" [state changed to %d]", steam::steam_friends->GetFriendPersonaState(user_id));
    }
    if (change & EPersonaChange::k_EPersonaChangeComeOnline) {
        printf(" [Went offline]");
    }
    if (change & EPersonaChange::k_EPersonaChangeComeOnline) {
        printf(" [Came online]");
    }

    printf("\n");
}

listener(FriendChatMsg_t) {
    auto user_id = CSteamID(cb->m_ulSenderID);

    printf("Message index is %d (%X)\n", cb->m_iChatID, cb->m_iChatID);
    printf("Message is limited? %s\n", cb->m_bLimitedAccount ? "true" : "false");

    // Ignore user is typing messages
    if (cb->m_eChatEntryType == EChatEntryType::k_EChatEntryTypeTyping) {
        printf("%s (%s) is typing (1)...\n", user_id.Render(), steam::steam_friends->GetFriendPersonaName(user_id));
        return;
    }

    char message[2000];
    memset(message, 0, sizeof(message));
    EChatEntryType entry;

    auto msg = steam::steam_friends->GetFriendMessage(user_id, cb->m_iChatID, message, sizeof(message), &entry);

    if (entry == EChatEntryType::k_EChatEntryTypeTyping) {
        printf("%s (%s) is typing (2)...\n", user_id.Render(), steam::steam_friends->GetFriendPersonaName(user_id));
        return;
    }

    printf("Message from %s (%s) Length %d\n>>%s<<\n", user_id.Render(), steam::steam_friends->GetFriendPersonaName(user_id), msg, message);
}

#undef listener

int main(int argc, const char **argv) {
    if (!load_steam_dll()) {
        printf("Unable to load the steam dll from %s\n", steam_path);
        return 1;
    }

    if (!get_steam_interfaces()) {
        printf("Unable to get steam interfaces\n");
        return 1;
    }

    ListenerRegister::register_all();

    login_to_steam();

    while (true) {
        steam::process_callbacks();
        sleep(1);
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
