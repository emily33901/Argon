size_t __cdecl CUser::FillWithToken(void *this, CUtlBuffer *a2, bool a3)
{
    int         v3; // esi@2
    const void *v4; // eax@2
    size_t      v5; // esi@2
    signed int  v6; // eax@5

    if (*((_DWORD *)this + 1355))
    {
        v3 = *((_DWORD *)this + 1352);
        CUtlBuffer::PutUnsignedInt(a2, *(_DWORD *)(v3 + 16));
        v4 = *(const void **)(v3 + 4);
        v5 = *(_DWORD *)(v3 + 16);
        CUtlBuffer::Put(a2, v4, v5);
    }
    else
    {
        CUtlBuffer::PutUnsignedInt(a2, 4u);
        CUtlBuffer::PutUnsignedInt(a2, 0);
        v5 = 4;
    }
    if (a3)
    {
        v6 = *((_DWORD *)this + 1355);
        if (v6 < 2)
        {
            if (v6 == 1)
                ++*(_DWORD *)(*((_DWORD *)this + 1352) + 20);
        }
        else
        {
            CUtlMemoryBase::~CUtlMemoryBase(*((CUtlMemoryBase **)this + 1352));
            CUtlVector<CConnectionToken, CUtlMemory<CConnectionToken>>::ShiftElementsLeft((char *)this + 5404, 0, 1);
            --*((_DWORD *)this + 1355);
        }
    }
    return v5;
}