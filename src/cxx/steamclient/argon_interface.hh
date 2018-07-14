#pragma once

// Interfaces that are exposed from the Argon
// This must be kept up to date with the c# version of this interface otherwise
// there will be problems!

// Look at InterfaceArgonCore for information about what each function does
class ArgonCore {
public:
    virtual void *create_interface(int pipe, const char *name) = 0;
    virtual void *create_interface_no_pipe(const char *name)   = 0;
    virtual bool  get_callback(int pipe, void *msg)            = 0;
    virtual void  free_last_callback(int pipe)                 = 0;
};

extern ArgonCore *argon;
