#include <cassert>
#include <cstdio>

#ifdef _MSC_VER
#include <Windows.h>
#else
#include <dlfcn.h>
#include <unistd.h>
using DWORD = unsigned int;
#endif

using HSteamPipe   = DWORD;
using HSteamUser   = DWORD;
using EAccountType = DWORD;

class ISteamClient017 {
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
    virtual void SetLocalIPBinding(int unIP, int usPort) = 0;

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

struct CallbackMsg_t {
    HSteamUser     m_hSteamUser;
    int            m_iCallback;
    unsigned char *m_pubParam;
    int            m_cubParam;
};

using CreateInterfaceFn = void *(*)(const char *);
using GetCallbackFn     = bool (*)(unsigned int pipe, CallbackMsg_t *callback);
using FreeCallbackFn    = bool (*)(unsigned int pipe);

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
            auto user_handle = steam_client->CreateLocalUser(&pipe_handle, 4);
            auto user        = steam_client->GetISteamUser(user_handle, pipe_handle, "SteamUser019");

            if (user == nullptr) printf("Unable to get user!\n");

            CallbackMsg_t msg;

            while (true) {
                while (get_next_callback(pipe_handle, &msg)) {
                    printf("msg from user %lu: id: %d size: %u\n", msg.m_hSteamUser, msg.m_iCallback, msg.m_cubParam);

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
