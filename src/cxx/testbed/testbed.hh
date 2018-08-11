#pragma once

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

#include "clientengine.hh"
#include "clientfriends.hh"
#include "clientuser.hh"
#include "mappedtest.hh"

using CreateInterfaceFn = void *(*)(const char *);
using GetCallbackFn     = bool (*)(unsigned int pipe, CallbackMsg_t *callback);
using FreeCallbackFn    = bool (*)(unsigned int pipe);

using ISteamClient017  = ISteamClient;
using ISteamFriends015 = ISteamFriends;

// Global steam objects
namespace steam {
extern CreateInterfaceFn create_interface;
extern GetCallbackFn     get_next_callback;
extern FreeCallbackFn    free_last_callback;

extern ISteamClient017 * client;
extern IClientEngine005 *engine;

extern HSteamPipe pipe_handle;
extern HSteamUser user_handle;

extern IClientUser001 *   user;
extern IClientFriends001 *client_friends;
extern ISteamFriends015 * steam_friends;

extern IMappedTest001 *mapped_test;

void handle_callback(const CallbackMsg_t &);
void process_callbacks();

using CallbackListener = void (*)(const void *buffer);

void add_listener(int id, CallbackListener func);
void remove_listener(CallbackListener func);

} // namespace steam
