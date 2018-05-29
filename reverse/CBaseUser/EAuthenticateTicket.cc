signed int __cdecl CBaseUser::EAuthenticateTicket(_DWORD *a1, int a2, int a3, int a4, int a5, _DWORD *a6, int a7, int a8, unsigned int a9)
{
    signed int   v9;  // ebx@1
    unsigned int v10; // eax@3
    int          v11; // ebx@7
    unsigned int v12; // edx@10
    int          v13; // ST2C_4@10
    unsigned int v14; // edx@11
    int          v15; // eax@12
    signed int   v16; // edx@20
    int          v18; // eax@31
    int          v19; // edx@31
    int          v20; // [sp+1Ch] [bp-5Ch]@10
    unsigned int v21; // [sp+20h] [bp-58h]@10
    int          v22; // [sp+24h] [bp-54h]@10
    unsigned int v23; // [sp+34h] [bp-44h]@28
    char         v24; // [sp+38h] [bp-40h]@3
    int          v25; // [sp+3Ch] [bp-3Ch]@28
    int          v26; // [sp+48h] [bp-30h]@6
    int          v27; // [sp+50h] [bp-28h]@6

    v9 = 1;
    if (a4 && a5)
    {
        CUtlBuffer::CUtlBuffer(2097686);
        v10 = CUtlBuffer::GetUnsignedInt((CUtlBuffer *)&v24);
        if (v10 <= a5 - 4 && (v10 || ((a9 >> 20) & 0xF) - 3 <= 1))
        {
            CUtlBuffer::SeekGet(&v24, 1, v10);
            if ((unsigned int)(v27 - v26) >= 4)
            {
                v11 = CUtlBuffer::GetUnsignedInt((CUtlBuffer *)&v24);
                if (v11)
                {
                    if (v11 <= v27 - v26)
                    {
                        if (((a9 >> 20) & 0xF) - 3 > 1)
                        {
                            CUtlBuffer::SeekGet(&v24, 1, v11);
                        LABEL_19:
                            if ((unsigned int)(v27 - v26) >= 4)
                            {
                                *a6 = v26;
                                v16 = CUtlBuffer::GetUnsignedInt((CUtlBuffer *)&v24);
                                if (!v16)
                                {
                                    v9 = 1;
                                    if (((a9 >> 20) & 0xF) - 3 <= 1)
                                    {
                                        v9 = 0;
                                        if (a7)
                                        {
                                            *(_DWORD *)a7       = a8;
                                            *(_DWORD *)(a7 + 4) = a9;
                                        }
                                    }
                                    goto LABEL_23;
                                }
                                if (v16 <= v27 - v26)
                                {
                                    v23 = v16;
                                    v9  = 1;
                                    if ((unsigned __int8)CSteamEngine::BIsTicketSignatureValid(
                                            (CSteamEngine *)g_pSteamEngine,
                                            (const void *)(v25 + v26),
                                            v16))
                                    {
                                        v9 = 5;
                                        if (!(unsigned __int8)CSteamEngine::BIsTicketExpired(
                                                (CSteamEngine *)g_pSteamEngine,
                                                (const void *)(v26 + v25),
                                                v23))
                                        {
                                            v9 = 4;
                                            if ((unsigned __int8)CSteamEngine::BIsTicketForApp(
                                                    (CSteamEngine *)g_pSteamEngine,
                                                    (const void *)(v26 + v25),
                                                    v23,
                                                    a2 & (unsigned int)&unk_FFFFFF))
                                            {
                                                v18 = CSteamEngine::GetTicketSteamID(
                                                    (CSteamEngine *)g_pSteamEngine,
                                                    (const void *)(v26 + v25),
                                                    v23);
                                                if (a7)
                                                {
                                                    *(_DWORD *)a7                                   = v18;
                                                    *(_DWORD *)((char *)&loc_20021A + a7 - 2097686) = v19;
                                                }
                                                v9 = 0;
                                                CUtlBuffer::SeekGet(&v24, 1, v23);
                                            }
                                        }
                                    }
                                    goto LABEL_23;
                                }
                            }
                            goto LABEL_22;
                        }
                        CUtlBuffer::GetUnsignedInt((CUtlBuffer *)&v24);
                        CUtlBuffer::GetUnsignedInt((CUtlBuffer *)&v24);
                        v20 = CUtlBuffer::GetUnsignedInt64((CUtlBuffer *)&v24);
                        v21 = v12;
                        CUtlBuffer::GetUnsignedInt((CUtlBuffer *)&v24);
                        CUtlBuffer::GetUnsignedInt((CUtlBuffer *)&v24);
                        v13 = CUtlBuffer::GetUnsignedInt((CUtlBuffer *)&v24);
                        v22 = CUtlBuffer::GetUnsignedInt((CUtlBuffer *)&v24);
                        CUtlBuffer::SeekGet(&v24, 1, v11 - 32);
                        v9 = 4;
                        if (v13 != (a2 & (unsigned int)&unk_FFFFFF))
                        {
                        LABEL_23:
                            CUtlMemoryBase::~CUtlMemoryBase((CUtlMemoryBase *)&v24);
                            return v9;
                        }
                        (*(void(__cdecl **)(_DWORD *))((char *)&loc_20022A + *a1 - 2097686))(a1);
                        if (v22 == v14 >> 24)
                        {
                            v15 = (v21 >> 20) & 0xF;
                            if ((unsigned int)(v15 - 3) <= 1)
                            {
                                if (!v20 && (v15 == 4 || v15 == 10))
                                {
                                    v9 = 5;
                                    if (!(v21 & 0xFFFFF))
                                        goto LABEL_23;
                                }
                                goto LABEL_19;
                            }
                        }
                    }
                }
            }
        }
    LABEL_22:
        v9 = 1;
        goto LABEL_23;
    }
    return v9;
}