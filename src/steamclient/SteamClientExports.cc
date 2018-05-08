#include "ArgonInterface.hh"

// This file exports all functions which steamclient.dll exports

#ifdef _MSC_VER
#define EXPORT extern "C" __declspec(dllexport)
#define STEAM_CALL __cdecl
#else
#define EXPORT
#define STEAM_CALL
#endif

EXPORT void *STEAM_CALL CreateInterface(const char *name) {
    return argon->create_interface(name);
}
