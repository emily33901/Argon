int __cdecl CUser::InternalUpdateClientGame(int a1, unsigned int a2, int a3, int a4, char a5, int a6, char *a7, char *a8, int a9, __int16 a10, char a11, void *a12, size_t a13, int a14, int a15, char a16, char a17, char a18)
{
    size_t     v18;                        // esi@1
    signed int v19;                        // esi@3
    char *     v20;                        // eax@4
    int        v21;                        // edi@6
    char       v22;                        // al@6
    int        v23;                        // edx@6
    int        v24;                        // ebx@6
    int        v25;                        // ecx@6
    char *     v26;                        // eax@10
    int        v27;                        // eax@11
    int        v28;                        // eax@29
    int        v29;                        // eax@29
    void(__cdecl * **v30)(_DWORD, _DWORD); // ST34_4@30
    int *          v31;                    // ecx@41
    size_t         v32;                    // esi@41
    char           v33;                    // dl@41
    bool           v34;                    // zf@50
    unsigned int * v35;                    // eax@52
    int            v36;                    // esi@55
    int            v37;                    // ST38_4@55
    int            v38;                    // edx@55
    char           v39;                    // bl@55
    int            v41;                    // [sp+1Ch] [bp-7Ch]@3
    int            v42;                    // [sp+20h] [bp-78h]@3
    CClientGame *  v43;                    // [sp+28h] [bp-70h]@6
    __int16        v44;                    // [sp+2Ch] [bp-6Ch]@3
    unsigned int   v45;                    // [sp+30h] [bp-68h]@3
    void *         v46;                    // [sp+34h] [bp-64h]@4
    int            v47;                    // [sp+38h] [bp-60h]@5
    int            v48;                    // [sp+3Ch] [bp-5Ch]@5
    unsigned int   v49;                    // [sp+40h] [bp-58h]@5
    char           v50;                    // [sp+44h] [bp-54h]@5
    int            v51;                    // [sp+48h] [bp-50h]@5
    int            v52;                    // [sp+4Ch] [bp-4Ch]@5
    int            v53;                    // [sp+50h] [bp-48h]@5
    int            v54;                    // [sp+54h] [bp-44h]@5
    int            v55;                    // [sp+58h] [bp-40h]@5
    int            v56;                    // [sp+5Ch] [bp-3Ch]@5
    __int16        v57;                    // [sp+60h] [bp-38h]@5
    __int64        v58;                    // [sp+68h] [bp-30h]@3
    CUtlMemoryBase v59;                    // [sp+70h] [bp-28h]@1
    size_t         v60;                    // [sp+80h] [bp-18h]@1
    int            v61;                    // [sp+84h] [bp-14h]@1

    v18 = a13;
    CUtlMemoryBase::CUtlMemoryBase(&v59, 1, 0, 0);
    v61 = 0;
    v60 = v18;
    if (a12)
    {
        CUtlMemoryBase::EnsureCapacity(&v59, v18);
        memcpy((void *)v59.memory, a12, v18);
    }
    v44 = a10;
    v45 = a5;
    v42 = a6;
    v58 = __PAIR__(a6, a5);
    v41 = (unsigned __int16)a10;
    v19 = CUser::InternalFindClientGame(a1, a5, a6, a9, a10);
    if (v19 == -1)
    {
        v47 = -1;
        v48 = 0;
        v49 = 0;
        v50 = 0;
        v52 = 0;
        v51 = 0;
        v54 = 0;
        v53 = 0;
        v56 = 0;
        v55 = 0;
        v57 = 0;
        CUser::CheckAppOwnership((CAppData *)(v45 & 0xFFFFFF), a1, v45 & 0xFFFFFF, (int)&v47);
        v19 = CUtlVector<CClientGame, CUtlMemory<CClientGame>>::InsertBefore(
            (CUtlMemoryBase *)(a1 + 6420),
            *(_DWORD *)(a1 + 6436));
        CClientGame::Init((CClientGame *)(*(_DWORD *)(a1 + 6424) + 1328 * v19), a2, v49);
        CClientGameCoordinator::EnableApp((CClientGameCoordinator *)((char *)&loc_5888E9 + a1 - 5798609), v45 & 0xFFFFFF);
        v20         = (char *)&locret_5893E9 + a1 - 5798609;
        LOBYTE(v46) = 1;
    }
    else
    {
        v20         = (char *)&locret_5893E9 + a1 - 5798609;
        LOBYTE(v46) = 0;
    }
    v21 = *(_DWORD *)v20;
    v22 = CGameID::IsValid((CGameID *)&a5);
    v24 = 1328 * v19;
    v25 = v21 + 1328 * v19;
    v43 = (CClientGame *)(v21 + 1328 * v19);
    if (v22)
    {
        v25 = v45 ^ *(_DWORD *)(v21 + v24 + 8);
        v23 = v25 | v42 ^ *(_DWORD *)(v21 + v24 + 12);
        if (*(_QWORD *)(v21 + v24 + 8) != __PAIR__(v42, v45) && (*(_DWORD *)(v21 + v24 + 8) & 0xFF000000 ^ 0x1000000 || v25 & 0xFFFFFF))
        {
            v26                                            = (char *)(v21 + v24 + 8);
            *(_DWORD *)v26                                 = v45;
            v23                                            = v42;
            *(_DWORD *)&v26[(_DWORD)&loc_587AD5 - 5798609] = v42;
            v25                                            = v45 & 0xFFFFFF ^ 0x301;
            if (v25)
            {
                *(_DWORD *)(v21 + v24 + 1284) = 0;
                v27                           = (*(int(__cdecl **)(int, unsigned int))(*(_DWORD *)a1 + 972))(a1, v45 & 0xFFFFFF);
                v46                           = &unk_10FB801;
                CUser::CheckLogAppDisableUpdatesStatus((CUser *)a1, v43, (CRTime *)(CRTime::sm_nTimeCur + v27));
            }
        }
    }
    if (a7 && v45 & 0xFF000000 && strcmp(a7, (const char *)(v21 + v24 + 64)))
    {
        LOBYTE(v46) = 1;
        V_strncpy(v21 + v24 + 64, a7, 64);
    }
    if (a8 && strcmp(a8, (const char *)(v21 + v24 + 128)))
        V_strncpy(v21 + v24 + 128, a8, 1024);
    *(_DWORD *)(v21 + v24 + 1240) = *(_DWORD *)((char *)&loc_58BC35 + a1 - 5798609);
    if (!*(_DWORD *)(v21 + v24 + 1244))
    {
        v25                           = a15;
        *(_DWORD *)(v21 + v24 + 1244) = a15;
    }
    if (a16)
        *(_BYTE *)(v21 + v24 + 29) = 1;
    if (a17)
        *(_BYTE *)(v21 + v24 + 30) = 1;
    if (a18)
        *(_BYTE *)(v21 + v24 + 31) = 1;
    if (a2)
    {
        if (*(_DWORD *)(v21 + v24 + 1156) != a2)
        {
            v28 = SteamService();
            v29 = (*(int(__cdecl **)(int))(*(_DWORD *)v28 + 40))(v28);
            if (v29)
            {
                v30 = (void(__cdecl ***)(_DWORD, _DWORD))v29;
                (*(void(__cdecl **)(int, _DWORD))(*(_DWORD *)v29 + 4))(v29, *(_DWORD *)(v21 + v24 + 1156));
                (**v30)(v30, a2);
            }
        }
    }
    if (a14)
        *(_DWORD *)(v21 + v24 + 60) = a14;
    if (a2 && ThreadGetCurrentProcessId(v25, v23) != a2)
    {
        CClientGame::AddTrackedPID(v43, a2);
        *(_DWORD *)(v21 + v24 + 1156) = a2;
        LOBYTE(v46)                   = 1;
    }
    if (!a9 && !v44 && !__PAIR__(a4, a3))
        goto LABEL_60;
    if (*(_QWORD *)v43 != __PAIR__(a4, a3))
    {
        *(_DWORD *)v43       = a3;
        *((_DWORD *)v43 + 1) = a4;
        LOBYTE(v46)          = 1;
    }
    v31 = (int *)(v21 + v24 + 16);
    v32 = a13;
    v33 = a11;
    if (*v31 != a9)
    {
        *v31        = a9;
        LOBYTE(v46) = 1;
    }
    if (*(_WORD *)(v21 + v24 + 20) != v41)
    {
        *(_WORD *)(v21 + v24 + 20) = v44;
        LOBYTE(v46)                = 1;
    }
    if (*(_BYTE *)(v21 + v24 + 28) != (unsigned __int8)v33)
    {
        *(_BYTE *)(v21 + v24 + 28) = v33;
        LOBYTE(v46)                = 1;
    }
    if (v60 != *(_DWORD *)(v21 + v24 + 52) || v60 && memcmp((const void *)v59.memory, *(const void **)(v21 + v24 + 40), v60))
    {
        v34                         = a12 == 0;
        *(_DWORD *)(v21 + v24 + 56) = 0;
        *(_DWORD *)(v21 + v24 + 52) = v32;
        if (!v34)
        {
            CUtlMemoryBase::EnsureCapacity((CUtlMemoryBase *)(v21 + v24 + 36), v32);
            memcpy(*(void **)(v21 + v24 + 40), a12, v32);
        }
        v35 = (unsigned int *)(v21 + v24 + 16);
    }
    else
    {
    LABEL_60:
        if (!(_BYTE)v46)
        {
            v39 = 0;
            goto LABEL_57;
        }
        v35 = (unsigned int *)(v21 + v24 + 16);
    }
    v36 = *(_DWORD *)(v21 + v24 + 8);
    v37 = *(_DWORD *)(v21 + v24 + 1156);
    PchRenderUnIP(*v35);
    v38 = *(_WORD *)(v21 + v24 + 20);
    v39 = 1;
    Msg("Game update: AppID %u \"%s\", ProcID %u, IP %s:%u\n");
LABEL_57:
    CUtlMemoryBase::~CUtlMemoryBase(&v59);
    return (unsigned __int8)v39;
}