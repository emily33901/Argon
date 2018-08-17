#include "listener.hh"

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
        scanf("%128s", twofactor);

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

        extern void login_to_steam();
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
