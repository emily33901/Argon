#include "ArgonInterface.hh"

// This file exports all functions which steamclient.dll exports

#ifdef _MSC_VER
#define EXPORT extern "C" __declspec(dllexport)
#define STEAM_CALL __cdecl
#else
#define EXPORT
#define STEAM_CALL
#endif

using HSteamPipe   = unsigned int;
using HSteamUser   = unsigned int;
using EAccountType = unsigned int;

class ISteamClient {
public:
    // Creates a communication pipe to the Steam client
    virtual HSteamPipe CreateSteamPipe() = 0;

    // Releases a previously created communications pipe
    virtual bool BReleaseSteamPipe(HSteamPipe hSteamPipe) = 0;

    // connects to an existing global user, failing if none exists
    // used by the game to coordinate with the steamUI
    virtual HSteamUser ConnectToGlobalUser(HSteamPipe hSteamPipe) = 0;

    // used by game servers, create a steam user that won't be shared with anyone else
    virtual HSteamUser CreateLocalUser(HSteamPipe *phSteamPipe, EAccountType eAccountType) = 0;

    // removes an allocated user
    virtual void ReleaseUser(HSteamPipe hSteamPipe, HSteamUser hUser) = 0;

    // retrieves the ISteamUser interface associated with the handle
    virtual void *GetISteamUser(HSteamUser hSteamUser, HSteamPipe hSteamPipe, const char *pchVersion) = 0;

    // retrieves the ISteamGameServer interface associated with the handle
    virtual void *GetISteamGameServer(HSteamUser hSteamUser, HSteamPipe hSteamPipe, const char *pchVersion) = 0;

    // set the local IP and Port to bind to
    // this must be set before CreateLocalUser()
    virtual void SetLocalIPBinding(int unIP, short usPort) = 0;

    // returns the ISteamFriends interface
    virtual void *GetISteamFriends(HSteamUser hSteamUser, HSteamPipe hSteamPipe, const char *pchVersion) = 0;

    // returns the ISteamUtils interface
    virtual void *GetISteamUtils(HSteamPipe hSteamPipe, const char *pchVersion) = 0;

    // returns the ISteamMatchmaking interface
    virtual void *GetISteamMatchmaking(HSteamUser hSteamUser, HSteamPipe hSteamPipe, const char *pchVersion) = 0;

    // returns the ISteamMatchmakingServers interface
    virtual void *GetISteamMatchmakingServers(HSteamUser hSteamUser, HSteamPipe hSteamPipe, const char *pchVersion) = 0;

    // returns the a generic interface
    virtual void *GetISteamGenericInterface(HSteamUser hSteamUser, HSteamPipe hSteamPipe, const char *pchVersion) = 0;

    // returns the ISteamUserStats interface
    virtual void *GetISteamUserStats(HSteamUser hSteamUser, HSteamPipe hSteamPipe, const char *pchVersion) = 0;

    // returns the ISteamGameServerStats interface
    virtual void *GetISteamGameServerStats(HSteamUser hSteamuser, HSteamPipe hSteamPipe, const char *pchVersion) = 0;

    // returns apps interface
    virtual void *GetISteamApps(HSteamUser hSteamUser, HSteamPipe hSteamPipe, const char *pchVersion) = 0;

    // networking
    virtual void *GetISteamNetworking(HSteamUser hSteamUser, HSteamPipe hSteamPipe, const char *pchVersion) = 0;

    // remote storage
    virtual void *GetISteamRemoteStorage(HSteamUser hSteamuser, HSteamPipe hSteamPipe, const char *pchVersion) = 0;

    // user screenshots
    virtual void *GetISteamScreenshots(HSteamUser hSteamuser, HSteamPipe hSteamPipe, const char *pchVersion) = 0;

    // this needs to be called every frame to process matchmaking results
    // redundant if you're already calling SteamAPI_RunCallbacks()
    virtual void RunFrame() = 0;

    // returns the number of IPC calls made since the last time this function was called
    // Used for perf debugging so you can understand how many IPC calls your game makes per frame
    // Every IPC call is at minimum a thread context switch if not a process one so you want to rate
    // control how often you do them.
    virtual unsigned int GetIPCCallCount() = 0;

    // API warning handling
    // 'int' is the severity; 0 for msg, 1 for warning
    // 'const char *' is the text of the message
    // callbacks will occur directly after the API function is called that generated the warning or message
    virtual void SetWarningMessageHook(void *pFunction) = 0;

    // Trigger global shutdown for the DLL
    virtual bool BShutdownIfAllPipesClosed() = 0;

    // Expose HTTP interface
    virtual void *GetISteamHTTP(HSteamUser hSteamuser, HSteamPipe hSteamPipe, const char *pchVersion) = 0;

    // Exposes the ISteamUnifiedMessages interface
    virtual void *GetISteamUnifiedMessages(HSteamUser hSteamuser, HSteamPipe hSteamPipe, const char *pchVersion) = 0;

    // Exposes the ISteamController interface
    virtual void *GetISteamController(HSteamUser hSteamUser, HSteamPipe hSteamPipe, const char *pchVersion) = 0;

    // Exposes the ISteamUGC interface
    virtual void *GetISteamUGC(HSteamUser hSteamUser, HSteamPipe hSteamPipe, const char *pchVersion) = 0;

    // returns app list interface, only available on specially registered apps
    virtual void *GetISteamAppList(HSteamUser hSteamUser, HSteamPipe hSteamPipe, const char *pchVersion) = 0;

    // Music Player
    virtual void *GetISteamMusic(HSteamUser hSteamuser, HSteamPipe hSteamPipe, const char *pchVersion) = 0;

    // Music Player Remote
    virtual void *GetISteamMusicRemote(HSteamUser hSteamuser, HSteamPipe hSteamPipe, const char *pchVersion) = 0;

    // html page display
    virtual void *GetISteamHTMLSurface(HSteamUser hSteamuser, HSteamPipe hSteamPipe, const char *pchVersion) = 0;

    // Helper functions for internal Steam usage
    virtual void Set_SteamAPI_CPostAPIResultInProcess(void *)           = 0;
    virtual void Remove_SteamAPI_CPostAPIResultInProcess(void *)        = 0;
    virtual void Set_SteamAPI_CCheckCallbackRegisteredInProcess(void *) = 0;

    // inventory
    virtual void *GetISteamInventory(HSteamUser hSteamuser, HSteamPipe hSteamPipe, const char *pchVersion) = 0;

    // Video
    virtual void *GetISteamVideo(HSteamUser hSteamuser, HSteamPipe hSteamPipe, const char *pchVersion) = 0;
};

EXPORT void *STEAM_CALL CreateInterface(const char *name) {
    return argon->create_interface(name);
}

ISteamClient *       client = nullptr;
static ISteamClient *steam_client() {
    if (client == nullptr) {
        client = (ISteamClient *)argon->create_interface("SteamClient017");
    }

    return client;
}

// Wrappers for steamclient functions
EXPORT HSteamPipe STEAM_CALL Steam_CreateSteamPipe() {
    return steam_client()->CreateSteamPipe();
}

EXPORT bool STEAM_CALL Steam_BReleaseSteamPipe(HSteamPipe pipe) {
    return steam_client()->BReleaseSteamPipe(pipe);
}

EXPORT HSteamUser STEAM_CALL Steam_CreateLocalUser(HSteamPipe *pipe, unsigned account_type) {
    return steam_client()->CreateLocalUser(pipe, account_type);
}

EXPORT HSteamUser STEAM_CALL Steam_CreateGlobalUser(HSteamPipe pipe) {
    // this function uses the CSteamClient interface
    return 0;
}

EXPORT HSteamUser STEAM_CALL Steam_ConnectToGlobalUser(HSteamPipe pipe) {
    return steam_client()->ConnectToGlobalUser(pipe);
}

EXPORT void STEAM_CALL Steam_ReleaseUser(HSteamPipe pipe, HSteamUser user) {
    return steam_client()->ReleaseUser(pipe, user);
}

EXPORT void STEAM_CALL Steam_SetLocalIpBinding(int ip, short port) {
    return steam_client()->SetLocalIPBinding(ip, port);
}

// These call SteamUser related functions by creating a new interaface based on the user + pipe handles
EXPORT void STEAM_CALL Steam_LogOn(HSteamUser user, HSteamPipe pipe, unsigned long long id) {}
EXPORT void STEAM_CALL Steam_LogOff(HSteamUser user, HSteamPipe pipe) {}
EXPORT bool STEAM_CALL Steam_BLoggedOn(HSteamUser user, HSteamPipe pipe) {}
EXPORT bool STEAM_CALL Steam_BConnected(HSteamUser user, HSteamPipe pipe) {}
EXPORT void STEAM_CALL Steam_InitiateGameConnection(HSteamUser user,
                                                    HSteamPipe pipe, void *blob,
                                                    int max_blob, unsigned long long id,
                                                    int appid, int ip,
                                                    short port, bool secure) {}
EXPORT void STEAM_CALL Steam_TerminateGameConnection(HSteamUser user,
                                                     HSteamPipe pipe, int ip,
                                                     short port) {}

// Callback related
EXPORT void STEAM_CALL Steam_BGetCallback(HSteamPipe pipe, void *callback) {}
EXPORT void STEAM_CALL Steam_FreeLastCallback(HSteamPipe pipe) {}
EXPORT void STEAM_CALL Steam_GetAPICallResult(HSteamPipe pipe, unsigned hSteamAPICall,
                                              void *pCallback, int cubCallback,
                                              int iCallbackExpected, bool *pbFailed) {}

// TODO: find this signature properly
EXPORT void STEAM_CALL Steam_ReleaseThreadLocalMemory(int a1) {}

// TODO: implement these gameserver related functions
EXPORT void STEAM_CALL Steam_GSSendSteam2UserConnect() {}
EXPORT void STEAM_CALL Steam_GSSendSteam3UserConnect() {}
EXPORT void STEAM_CALL Steam_GSSendSteamUserDisconnect() {}
EXPORT void STEAM_CALL Steam_GSSendUserStatusResponse() {}
EXPORT void STEAM_CALL Steam_GSUpdateStatus() {}
EXPORT void STEAM_CALL Steam_GSSetServerType() {}
EXPORT void STEAM_CALL Steam_GSSetSpawnCount() {}
EXPORT void STEAM_CALL Steam_GSRemoveUserConnect() {}
EXPORT void STEAM_CALL Steam_GSGetSteam2GetEncryptionKeyToSendToNewClient() {}
EXPORT void STEAM_CALL Steam_GSLogOn() {}
EXPORT void STEAM_CALL Steam_GSLogOff() {}
EXPORT void STEAM_CALL Steam_GSBLoggedOn() {}
EXPORT void STEAM_CALL Steam_GetGSHandle() {}
EXPORT void STEAM_CALL Steam_GSBSecure() {}
EXPORT void STEAM_CALL Steam_GSGetSteamID() {}

EXPORT void STEAM_CALL Breakpad_SteamMiniDumpInit(int a, const char *b, const char *c) {}
EXPORT void STEAM_CALL Breakpad_SteamWriteMiniDumpUsingExceptionInfoWithBuildId(int a, int b) {}
EXPORT void STEAM_CALL Breakpad_SteamWriteMiniDumpSetComment(const char *message) {}
EXPORT void STEAM_CALL Breakpad_SteamSetSteamID(unsigned long long userid) {}
EXPORT void STEAM_CALL Breakpad_SteamSetAppID(int appid) {}
