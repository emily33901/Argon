int __cdecl CUser::BeginAuthSession(CBaseUser *a1, void *a2, size_t a3, __int64 a4)
{
    int           v4;     // eax@1
    int           v5;     // esi@1
    int           result; // eax@4
    unsigned int  v7;     // eax@6
    __int64       v8;     // rcx@8
    int           v9;     // edi@8
    int           v10;    // [sp+4Ch] [bp-3Ch]@12
    __int64       v11;    // [sp+50h] [bp-38h]@4
    __int64       v12;    // [sp+58h] [bp-30h]@4
    unsigned int *v13;    // [sp+64h] [bp-24h]@4
    unsigned int  v14;    // [sp+68h] [bp-20h]@4
    unsigned int  v15;    // [sp+6Ch] [bp-1Ch]@4
    int           v16;    // [sp+70h] [bp-18h]@1
    int           v17;    // [sp+74h] [bp-14h]@1

    v4  = CSteamEngine::GetAppIDForCurrentPipe((CSteamEngine *)g_pSteamEngine);
    v5  = v4 & 0xFFFFFF;
    v16 = v4 & 0xFFFFFF;
    v17 = 0;
    if (!(unsigned __int8)CGameID::IsValid((CGameID *)&v16) && g_bAPIWarningEnabled)
        _APIWarning("BeginAuthSession called with invalid gameid ");
    v15    = 0;
    v14    = 0;
    v13    = 0;
    v12    = (unsigned int)v5;
    v11    = a4;
    result = CBaseUser::EAuthenticateTicket(a1, v5, 0, (int)a2, a3, &v13, (int)&v14, a4, HIDWORD(a4));
    if (!result)
    {
        if (v14 || (v7 = (v15 >> 20) & 0xF, v7 != 4) && v7 != 10)
        {
            result = 1;
            v9     = a4;
            v8     = a4 ^ v14;
        }
        else
        {
            HIDWORD(v8) = HIDWORD(a4);
            v9          = a4;
            if (!(v15 & 0xFFFFF))
                return CBaseUser::UpdateClientAuthListAndSend((int)a1, 1, v5, 0, a2, a3, v13, v9, SHIDWORD(v8), (int)&v10);
            result      = 1;
            LODWORD(v8) = a4 ^ v14;
        }
        if ((unsigned int)v8 | HIDWORD(v8) ^ v15)
            return result;
        return CBaseUser::UpdateClientAuthListAndSend((int)a1, 1, v5, 0, a2, a3, v13, v9, SHIDWORD(v8), (int)&v10);
    }
    return result;
}