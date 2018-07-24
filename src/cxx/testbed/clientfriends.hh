#pragma once

#include "../steam/steam_api.h"

class IClientFriends001 {
public:
    // returns the local players name - guaranteed to not be NULL.
    virtual const char *GetPersonaName() = 0;

    // sets the player name, stores it on the server and publishes the changes to all friends who are online
    virtual void           SetPersonaName(const char *pchPersonaName)                       = 0;
    virtual SteamAPICall_t SetPersonaNameEx(const char *pchPersonaName, bool bSendCallback) = 0;

    virtual bool IsPersonaNameSet() = 0;

    // gets the friend status of the current user
    virtual EPersonaState GetPersonaState() = 0;
    // sets the status, communicates to server, tells all friends
    virtual void SetPersonaState(EPersonaState ePersonaState) = 0;
};

struct FriendChatMsg_t {
    enum { k_iCallback = k_iClientFriendsCallbacks + 5 };

    CSteamID m_ulFriendID; // other participant in the msg
    CSteamID m_ulSenderID; // steamID of the friend who has sent this message
    uint8    m_eChatEntryType;
    uint8    m_bLimitedAccount;
    uint32   m_iChatID; // chat id
};