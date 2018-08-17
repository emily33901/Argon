#pragma once

#include "../steam/steam_api.h"

#pragma pack(push, 4)
struct MappedTestStruct {
    uint8  a; // 0
    uint32 b; // 4
    uint16 c; // 8
    uint8  d; // 10
    uint64 e; // 12
};

enum class MappedTestEnum {
    size = sizeof(MappedTestStruct),
};

#pragma pack(pop)

class IMappedTest001 {
public:
    // a and b should be 4 and 5 respectively
    // c should be a + b
    virtual int  PointerTest(int *a, int *b, int *c)       = 0;
    virtual int  BufferTest(char *buffer, int size)        = 0;
    virtual void TestStruct(MappedTestStruct *s, int size) = 0;
};
