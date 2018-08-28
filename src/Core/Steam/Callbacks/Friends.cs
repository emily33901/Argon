using SteamKit2;
using Core;

using System.Runtime.InteropServices;

namespace Core.Steam.Callbacks
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct FriendSessionStateInfo
    {
        public uint m_uiOnlineSessionInstances;
        public byte m_uiPublishedToFriendsSessionInstance;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct PersonaStateChange
    {
        public const int callback_id = 304;
        public ulong m_ulSteamID;
        public SteamKit2.EClientPersonaStateFlag m_nChangeFlags;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct GameOverlayActivated
    {
        public const int callback_id = 331;
        public byte m_bActive;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct GameServerChangeRequested
    {
        public const int callback_id = 332;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string m_rgchServer;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string m_rgchPassword;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct GameLobbyJoinRequested
    {
        public const int callback_id = 333;
        public ulong m_steamIDLobby;
        public ulong m_steamIDFriend;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct AvatarImageLoaded
    {
        public const int callback_id = 334;
        public ulong m_steamID;
        public int m_iImage;
        public int m_iWide;
        public int m_iTall;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct ClanOfficerListResponse
    {
        public const int callback_id = 335;
        public ulong m_steamIDClan;
        public int m_cOfficers;
        public byte m_bSuccess;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct FriendRichPresenceUpdate
    {
        public const int callback_id = 336;
        public ulong m_steamIDFriend;
        public uint m_nAppID;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct GameRichPresenceJoinRequested
    {
        public const int callback_id = 337;
        public ulong m_steamIDFriend;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string m_rgchConnect;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct GameConnectedClanChatMsg
    {
        public const int callback_id = 338;
        public ulong m_steamIDClanChat;
        public ulong m_steamIDUser;
        public int m_iMessageID;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct GameConnectedChatJoin
    {
        public const int callback_id = 339;
        public ulong m_steamIDClanChat;
        public ulong m_steamIDUser;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct GameConnectedChatLeave
    {
        public const int callback_id = 340;
        public ulong m_steamIDClanChat;
        public ulong m_steamIDUser;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bKicked;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bDropped;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct DownloadClanActivityCountsResult
    {
        public const int callback_id = 341;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bSuccess;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct JoinClanChatRoomCompletionResult
    {
        public const int callback_id = 342;
        public ulong m_steamIDClanChat;
        public EChatRoomEnterResponse m_eChatRoomEnterResponse;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct GameConnectedFriendChatMsg
    {
        public const int callback_id = 343;
        public ulong m_steamIDUser;
        public int m_iMessageID;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct FriendsGetFollowerCount
    {
        public const int callback_id = 344;
        public EResult m_eResult;
        public ulong m_steamID;
        public int m_cCount;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct FriendsIsFollowing
    {
        public const int callback_id = 345;
        public EResult m_eResult;
        public ulong m_steamID;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bIsFollowing;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct FriendsEnumerateFollowingList
    {
        public const int callback_id = 346;
        public EResult m_eResult;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
        public ulong[] m_steamIDs;
        public int m_cSteamIDs;
        public int m_cTotalResults;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct SetPersonaNameResponse
    {
        public const int callback_id = 347;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bUnk1;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bUnk2;
        public EResult m_eResult;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct GameOverlayActivateRequested
    {
        public const int callback_id = 801;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string m_rgchDialog;
        public ulong m_steamIDTarget;
        public uint m_nAppID;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bWebPage;
        public uint m_eFlag;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct ClanEventReceived
    {
        public const int callback_id = 802;
        public ulong m_gidEvent;
        public ulong m_ulSteamIDClan;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string m_rgchTitle;
        public uint m_nStartTime;
        public uint m_GameID;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct FriendAdded
    {
        public const int callback_id = 803;
        public EResult m_eResult;
        public ulong m_ulSteamID;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct UserRequestingFriendship
    {
        public const int callback_id = 804;
        public ulong m_ulSteamID;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct FriendChatMsg
    {
        public const int callback_id = 805;
        public ulong m_ulFriendID;
        public ulong m_ulSenderID;
        public byte m_eChatEntryType;
        public byte m_bLimitedAccount;
        public uint m_iChatID;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct FriendInvited
    {
        public const int callback_id = 806;
        public EResult m_eResult;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct ChatRoomInvite
    {
        public const int callback_id = 807;
        public ulong m_ulSteamIDChat;
        public ulong m_ulSteamIDPatron;
        public ulong m_ulSteamIDFriendChat;
        public EChatRoomType m_EChatRoomType;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string m_rgchChatRoomName;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct ChatRoomEnter
    {
        public const int callback_id = 808;
        public ulong m_ulSteamIDChat;
        public EChatRoomType m_EChatRoomType;
        public ulong m_ulSteamIDOwner;
        public ulong m_ulSteamIDClan;
        public ulong m_ulSteamIDFriendChat;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bLocked;
        public uint m_rgfChatPermissions;
        public EChatRoomEnterResponse m_EChatRoomEnterResponse;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string m_rgchChatRoomName;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct ChatMemberStateChange
    {
        public const int callback_id = 809;
        public ulong m_ulSteamIDChat;
        public ulong m_ulSteamIDUserChanged;
        public EChatMemberStateChange m_rgfChatMemberStateChange;
        public ulong m_ulSteamIDMakingChange;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct ChatRoomMsg
    {
        public const int callback_id = 810;
        public ulong m_ulSteamIDChat;
        public ulong m_ulSteamIDUser;
        public byte m_eChatEntryType;
        public uint m_iChatID;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct ChatRoomDlgClose
    {
        public const int callback_id = 811;
        public ulong m_ulSteamIDChat;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct ChatRoomClosing
    {
        public const int callback_id = 812;
        public ulong m_ulSteamIDChat;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct ChatRoomKicking
    {
        public const int callback_id = 813;
        public ulong m_ulSteamIDChat;
        public ulong m_ulSteamIDAdmin;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct ChatRoomBanning
    {
        public const int callback_id = 814;
        public ulong m_ulSteamIDChat;
        public ulong m_ulSteamIDAdmin;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct ChatRoomCreate
    {
        public const int callback_id = 815;
        public EResult m_eResult;
        public ulong m_ulSteamIDChat;
        public ulong m_ulSteamIDFriendChat;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct OpenChatDialog
    {
        public const int callback_id = 816;
        public ulong m_ulSteamID;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct ChatRoomActionResult
    {
        public const int callback_id = 817;
        public ulong m_ulSteamIDChat;
        public ulong m_ulSteamIDUserActedOn;
        public EChatAction m_EChatAction;
        public EChatActionResult m_EChatActionResult;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct ChatRoomDlgSerialized
    {
        public const int callback_id = 818;
        public ulong m_ulSteamID;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct ClanInfoChanged
    {
        public const int callback_id = 819;
        public ulong m_ulSteamID;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bNameChanged;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bAvatarChanged;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bAccountInfoChanged;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct ChatMemberInfoChanged
    {
        public const int callback_id = 820;
        public ulong m_ulSteamIDChat;
        public ulong m_ulSteamIDUser;
        public uint m_rgfChatMemberPermissions;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct ChatRoomInfoChanged
    {
        public const int callback_id = 821;
        public ulong m_ulSteamIDChat;
        public uint m_rgfChatRoomDetails;
        public ulong m_ulSteamIDMakingChange;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct SteamRackBouncing
    {
        public const int callback_id = 822;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct ChatRoomSpeakChanged
    {
        public const int callback_id = 823;
        public ulong m_ulSteamIDChat;
        public ulong m_ulSteamIDUser;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bSpeaking;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct NotifyIncomingCall
    {
        public const int callback_id = 824;
        public int m_Handle;
        public ulong m_ulSteamID;
        public ulong m_ulSteamIDChat;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bIncoming;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct NotifyHangup
    {
        public const int callback_id = 825;
        public int m_Handle;
        public uint m_eVoiceResult;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct NotifyRequestResume
    {
        public const int callback_id = 826;
        public int m_Handle;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct NotifyChatRoomVoiceStateChanged
    {
        public const int callback_id = 827;
        public ulong m_steamChatRoom;
        public ulong m_steamUser;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct ChatRoomDlgUIChange
    {
        public const int callback_id = 828;
        public ulong m_SteamIDChat;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bShowAvatars;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bBeepOnNewMsg;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bShowSteamIDs;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bShowTimestampOnNewMsg;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct VoiceCallInitiated
    {
        public const int callback_id = 829;
        public ulong m_ulSteamIDUser;
        public ulong m_ulSteamIDFriend;
        public int m_hVoiceCall;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct FriendIgnored
    {
        public const int callback_id = 830;
        public EResult m_eResult;
        public ulong m_ulSteamID;
        public ulong m_ulSteamIDFriend;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bIgnored;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct VoiceInputDeviceChanged
    {
        public const int callback_id = 831;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct VoiceEnabledStateChanged
    {
        public const int callback_id = 832;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bVoiceEnabled;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct FriendsWhoPlayGameUpdate
    {
        public const int callback_id = 833;
        public GameID m_gameID;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct FriendProfileInfoResponse
    {
        public const int callback_id = 834;
        public ulong m_steamIDFriend;
        public EResult m_eResult;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct RichInviteReceived
    {
        public const int callback_id = 835;
        public ulong m_steamIDFriend;
        public uint m_nAppID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string m_rgchConnect;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct FriendsMenuChange
    {
        public const int callback_id = 836;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bShowAvatars;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bSortByName;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bShowOnlineFriendsOnly;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bShowUntaggedFriends;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct TradeInviteReceived
    {
        public const int callback_id = 837;
        public ulong m_steamIDPartner;
        public uint m_unTradeRequestID;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct TradeInviteResponse
    {
        public const int callback_id = 838;
        public ulong m_steamIDPartner;
        public uint m_eResponse;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct TradeStartSession
    {
        public const int callback_id = 839;
        public ulong m_steamIDPartner;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct TradeInviteCanceled
    {
        public const int callback_id = 840;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct GameUsingVoice
    {
        public const int callback_id = 841;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct FriendsGroupCreated
    {
        public const int callback_id = 842;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct FriendsGroupDeleted
    {
        public const int callback_id = 843;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct FriendsGroupRenamed
    {
        public const int callback_id = 844;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct FriendsGroupMemberAdded
    {
        public const int callback_id = 845;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct FriendsGroupMemberRemoved
    {
        public const int callback_id = 846;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct NameHistoryResponse
    {
        public const int callback_id = 847;
        public int m_cSuccessfulLookups;
        public int m_cFailedLookups;
    };
}