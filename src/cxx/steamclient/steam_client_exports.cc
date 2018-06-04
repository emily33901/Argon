#include "argon_interface.hh"

#include <stdio.h>

// This file exports all functions which steamclient.dll exports

#ifdef _MSC_VER
#define EXPORT extern "C" __declspec(dllexport)
#define STEAM_CALL __cdecl
#else
#define EXPORT extern "C"
#define STEAM_CALL
#endif

#include "../steam/steam_api.h"

EXPORT void *STEAM_CALL CreateInterface(const char *name) {
    return argon->create_interface_no_pipe(name);
}

ISteamClient *       client = nullptr;
static ISteamClient *steam_client() {
    if (client == nullptr) {
        client = (ISteamClient *)argon->create_interface_no_pipe("SteamClient017");
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

EXPORT HSteamUser STEAM_CALL Steam_CreateLocalUser(HSteamPipe *pipe, EAccountType account_type) {
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
EXPORT bool STEAM_CALL Steam_BLoggedOn(HSteamUser user, HSteamPipe pipe) { return false; }
EXPORT bool STEAM_CALL Steam_BConnected(HSteamUser user, HSteamPipe pipe) { return false; }
EXPORT void STEAM_CALL Steam_InitiateGameConnection(HSteamUser user,
                                                    HSteamPipe pipe, void *blob,
                                                    int max_blob, unsigned long long id,
                                                    int appid, int ip,
                                                    short port, bool secure) {}
EXPORT void STEAM_CALL Steam_TerminateGameConnection(HSteamUser user,
                                                     HSteamPipe pipe, int ip,
                                                     short port) {}

// Callback related
EXPORT bool STEAM_CALL Steam_BGetCallback(HSteamPipe pipe, void *callback) {
    return argon->get_callback(callback);
}
EXPORT void STEAM_CALL Steam_FreeLastCallback(HSteamPipe pipe) {
    return argon->free_last_callback();
}
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