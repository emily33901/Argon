int __cdecl CBaseUser::CheckTicketHistory(int this, unsigned __int8 *a2, unsigned int *a3)
{
    int v3;     // eax@1
    int v4;     // ecx@1
    int v5;     // edx@2
    int v6;     // edi@2
    int result; // eax@7
    int v8;     // [sp+Ch] [bp-Ch]@1

    v3 = CRC32_ProcessSingleBuffer(a2, a3);
    v8 = v3;
    v4 = *(_DWORD *)(this + 5452);
    if (v4 <= 0)
        goto LABEL_15;
    v5 = *(_DWORD *)(this + 5440);
    v6 = 0;
    while (*(_DWORD *)(v5 + 4 * v6) != v3)
    {
        if (++v6 >= v4)
            goto LABEL_8;
    }
    if (v6 != -1)
    {
        LOBYTE(result) = 0;
        return (unsigned __int8)result;
    }
LABEL_8:
    if (v4 > 99)
    {
        *(_DWORD *)(v5 + 4 * *(_DWORD *)(this + 5456)) = v3;
        *(_DWORD *)(this + 5456)                       = (*(_DWORD *)(this + 5456) + 1) % *(_DWORD *)(this + 5452);
    }
    else
    {
    LABEL_15:
        CUtlVector<unsigned int, CUtlMemory<unsigned int>>::InsertBefore((CUtlMemoryBase *)(this + 5436), v4, (int)&v8);
    }
    LOBYTE(result) = 1;
    return (unsigned __int8)result;
}