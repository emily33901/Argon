#pragma once

// Interfaces that are exposed from the Argon
// This must be kept up to date with the c# version of this interface otherwise
// there will be problems!

// Look at Interfaces.InterfaceArgonCore for information about what each function does
class ArgonCore {
public:
    virtual void *create_interface(const char *name) = 0;
};

extern ArgonCore *argon;
