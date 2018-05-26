int __cdecl CUser::BaseGetAuthTicket(int a1, CUtlBuffer *a2, int a3)
{
    CUtlBuffer *v3;                         // esi@1
    CUtlBuffer *v4;                         // eax@1
    int         v5;                         // edi@2
    int         v6;                         // esi@3
    void(__cdecl * v8)(int, int, int, int); // [sp+18h] [bp-10h]@3

    v3 = a2;
    v4 = CUser::LoadAppOwnershipTicketFromDisk(a1, a3 & 0xFFFFFF);
    if (v4)
    {
        v5 = *((_DWORD *)v4 + 5);
        CUtlBuffer::PutUnsignedInt(a2, *((_DWORD *)v4 + 5));
        if (v5)
        {
            v8 = *(void(__cdecl **)(int, int, int, int))(*(_DWORD *)a1 + 428);
            v6 = 0;
            if ((unsigned __int8)CUtlBuffer::CheckPut(a2, v5))
                v6 = *((_DWORD *)a2 + 5) + *((_DWORD *)a2 + 1);
            v8(a1, a3 & 0xFFFFFF, v6, v5);
            v3 = a2;
            CUtlBuffer::SeekPut(a2, 1, v5);
        }
    }
    else
    {
        CUtlBuffer::PutUnsignedInt(a2, 0);
    }
    return *((_DWORD *)v3 + 5);
}