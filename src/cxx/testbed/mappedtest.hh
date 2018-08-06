#pragma once

class IMappedTest001 {
public:
    // a and b should be 4 and 5 respectively
    // c should be a + b
    virtual void PointerTest(int *a, int *b, int *c) = 0;
    virtual int  BufferTest(char *buffer, int size)  = 0;
};