#pragma once

#include "../steam/steam_api.h"

using IConCommandBaseAccessor = void *;

class IClientEngine005 {
public:
    virtual HSteamPipe CreateSteamPipe()                                                       = 0;
    virtual bool       BReleaseSteamPipe(HSteamPipe)                                           = 0;
    virtual int        CreateGlobalUser(HSteamPipe *)                                          = 0;
    virtual HSteamUser ConnectToGlobalUser(HSteamPipe)                                         = 0;
    virtual HSteamUser CreateLocalUser(HSteamPipe *, EAccountType)                             = 0;
    virtual int        CreatePipeToLocalUser(int, int *)                                       = 0;
    virtual int        ReleaseUser(int, int)                                                   = 0;
    virtual int        IsValidHSteamUserPipe(int, int)                                         = 0;
    virtual void *     GetIClientUser(int, int, char const *)                                  = 0;
    virtual void *     GetIClientGameServer(int, int, char const *)                            = 0;
    virtual int        SetLocalIPBinding(unsigned int, unsigned short)                         = 0;
    virtual int        GetUniverseName(EUniverse)                                              = 0;
    virtual void *     GetIClientFriends(int, int, char const *)                               = 0;
    virtual int        GetIClientUtils(int, char const *)                                      = 0;
    virtual int        GetIClientBilling(int, int, char const *)                               = 0;
    virtual int        GetIClientMatchmaking(int, int, char const *)                           = 0;
    virtual int        GetIClientApps(int, int, char const *)                                  = 0;
    virtual int        GetIClientMatchmakingServers(int, int, char const *)                    = 0;
    virtual int        GetIClientGameSearch(int, int, char const *)                            = 0;
    virtual int        RunFrame()                                                              = 0;
    virtual int        GetIPCCallCount()                                                       = 0;
    virtual int        GetIClientUserStats(int, int, char const *)                             = 0;
    virtual int        GetIClientGameServerStats(int, int, char const *)                       = 0;
    virtual int        GetIClientNetworking(int, int, char const *)                            = 0;
    virtual int        GetIClientRemoteStorage(int, int, char const *)                         = 0;
    virtual int        GetIClientScreenshots(int, int, char const *)                           = 0;
    virtual int        SetWarningMessageHook(void (*)(int, char const *))                      = 0;
    virtual int        GetIClientGameCoordinator(int, int, char const *)                       = 0;
    virtual int        SetOverlayNotificationPosition(ENotificationPosition)                   = 0;
    virtual int        SetOverlayNotificationInset(int, int)                                   = 0;
    virtual int        HookScreenshots(bool)                                                   = 0;
    virtual int        IsScreenshotsHooked()                                                   = 0;
    virtual int        IsOverlayEnabled()                                                      = 0;
    virtual int        GetAPICallResult(int, unsigned long long, void *, int, int, bool *)     = 0;
    virtual int        GetIClientProductBuilder(int, int, char const *)                        = 0;
    virtual int        GetIClientDepotBuilder(int, int, char const *)                          = 0;
    virtual int        GetIClientNetworkDeviceManager(int, char const *)                       = 0;
    virtual int        ConCommandInit(IConCommandBaseAccessor *)                               = 0;
    virtual int        GetIClientAppManager(int, int, char const *)                            = 0;
    virtual int        GetIClientConfigStore(int, int, char const *)                           = 0;
    virtual int        BOverlayNeedsPresent()                                                  = 0;
    virtual int        GetIClientGameStats(int, int, char const *)                             = 0;
    virtual int        GetIClientHTTP(int, int, char const *)                                  = 0;
    virtual int        BShutdownIfAllPipesClosed()                                             = 0;
    virtual int        GetIClientAudio(int, int, char const *)                                 = 0;
    virtual int        GetIClientMusic(int, int, char const *)                                 = 0;
    virtual int        GetIClientUnifiedMessages(int, int, char const *)                       = 0;
    virtual int        GetIClientController(int, int, char const *)                            = 0;
    virtual int        GetIClientParentalSettings(int, int, char const *)                      = 0;
    virtual int        GetIClientStreamLauncher(int, int, char const *)                        = 0;
    virtual int        GetIClientDeviceAuth(int, int, char const *)                            = 0;
    virtual int        GetIClientRemoteClientManager(int, char const *)                        = 0;
    virtual int        GetIClientStreamClient(int, int, char const *)                          = 0;
    virtual int        GetIClientShortcuts(int, int, char const *)                             = 0;
    virtual int        GetIClientUGC(int, int, char const *)                                   = 0;
    virtual int        GetIClientInventory(int, int, char const *)                             = 0;
    virtual int        GetIClientVR(int, char const *)                                         = 0;
    virtual int        GetIClientGameNotifications(int, int, char const *)                     = 0;
    virtual int        GetIClientHTMLSurface(int, int, char const *)                           = 0;
    virtual int        GetIClientVideo(int, int, char const *)                                 = 0;
    virtual int        GetIClientControllerSerialized(int, char const *)                       = 0;
    virtual int        GetIClientAppDisableUpdate(int, int, char const *)                      = 0;
    virtual int        Set_Client_API_CCheckCallbackRegisteredInProcess(unsigned int (*)(int)) = 0;
    virtual int        GetIClientBluetoothManager(int, char const *)                           = 0;
    virtual int        GetIClientSharedConnection(int, int, char const *)                      = 0;
    virtual int        GetIClientShader(int, int, char const *)                                = 0;
    virtual int        GetIClientNetworkingSocketsSerialized(int, int, char const *)           = 0;
    virtual int        destructor1()                                                           = 0;
    virtual int        GetIPCServerMap()                                                       = 0;
    virtual int        OnDebugTextArrived(char const *)                                        = 0;
    virtual int        OnThreadLocalRegistration()                                             = 0;
    virtual int        OnThreadBuffersOverLimit()                                              = 0;
};