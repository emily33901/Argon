#pragma once

// Interfaces that are exposed from the Argon
// This must be kept up to date with the c# version of this interface otherwise
// there will be problems!

// Look at InterfaceArgonCore for information about what each function does
class ArgonCore {
public:
    virtual void *create_interface(const char *name) = 0;
    virtual bool  get_callback(void *msg)            = 0;
    virtual void  free_last_callback()               = 0;
};

extern ArgonCore *argon;
