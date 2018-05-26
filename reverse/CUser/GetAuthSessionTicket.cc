int __cdecl CUser::GetAuthSessionTicket(int this, void *pBlob, int cbBlob, unsigned int *cbTotal)
{
    signed int       v4;  // edi@1
    int              v5;  // eax@1
    int              v6;  // esi@1
    int              v7;  // eax@4
    _QWORD *         v8;  // ecx@5
    signed int       v9;  // edx@5
    int              v10; // edi@5
    int              v11; // ebx@9
    signed int       v12; // ebx@13
    unsigned int     v13; // eax@14
    unsigned int     v14; // eax@14
    unsigned int     v15; // eax@14
    unsigned int     v16; // eax@14
    unsigned int *   v17; // edi@14
    CUtlBuffer *     v18; // eax@14
    unsigned __int8  v19; // al@17
    unsigned int     v20; // eax@19
    signed int       v22; // [sp+30h] [bp-78h]@13
    CCMInterface *   v23; // [sp+38h] [bp-70h]@13
    int              a10; // [sp+54h] [bp-54h]@25
    __int64          v25; // [sp+58h] [bp-50h]@16
    char             v26; // [sp+60h] [bp-48h]@10
    unsigned __int8 *v27; // [sp+64h] [bp-44h]@17
    unsigned int *   a7;  // [sp+74h] [bp-34h]@14
    int              v29; // [sp+90h] [bp-18h]@1
    int              v30; // [sp+94h] [bp-14h]@1

    v4  = 5802721;
    v5  = CSteamEngine::GetAppIDForCurrentPipe((CSteamEngine *)g_pSteamEngine);
    v6  = v5 & 0xFFFFFF;
    v29 = v5 & 0xFFFFFF;
    v30 = 0;
    if (!(unsigned __int8)CGameID::IsValid((CGameID *)&v29) && g_bAPIWarningEnabled)
        _APIWarning("GetAuthSessionTicket called with invalid gameid ");
    v7 = *(_DWORD *)(this + 5380);
    if (v7 <= 0)
        goto LABEL_32;
    v8  = (_QWORD *)(*(_DWORD *)(this + 5368) + 16);
    v9  = 0;
    v10 = 0;
    do
    {
        if (!*((_DWORD *)v8 - 4))
            v9 += *v8 == v6;
        ++v10;
        v8 = (_QWORD *)((char *)v8 + 92);
    } while (v10 < v7);
    v11 = 0;
    v4  = 5802721;
    if (v9 <= 499)
    {
    LABEL_32:
        CUtlBuffer::CUtlBuffer(&v26, 0, 0, 0);
        *cbTotal = 0;
        v11      = 0;
        if ((pBlob || !cbBlob) && cbBlob)
        {
            v22 = v4;
            v23 = (CCMInterface *)(this + 36);
            v12 = 1;
            do
            {
                CUser::FillWithToken((CUser *)this, (CUtlBuffer *)&v26, pBlob != 0);
                CUtlBuffer::PutUnsignedInt((CUtlBuffer *)&v26, 0x18u);
                CUtlBuffer::PutUnsignedInt((CUtlBuffer *)&v26, 1u);
                CUtlBuffer::PutUnsignedInt((CUtlBuffer *)&v26, 2u);
                v13 = CCMInterface::GetPublicIP(v23);
                CUtlBuffer::PutUnsignedInt((CUtlBuffer *)&v26, v13);
                v14 = CCMInterface::GetLocalIP(v23);
                CUtlBuffer::PutUnsignedInt((CUtlBuffer *)&v26, v14);
                v15 = Plat_MSTime();
                CUtlBuffer::PutUnsignedInt((CUtlBuffer *)&v26, v15);
                v16                      = *(_DWORD *)(this + 5432) + 1;
                *(_DWORD *)(this + 5432) = v16;
                CUtlBuffer::PutUnsignedInt((CUtlBuffer *)&v26, v16);
                v17 = a7;
                v18 = CUser::LoadAppOwnershipTicketFromDisk(this, v6);
                if (!v18 || !*((_DWORD *)v18 + 5))
                {
                    if (g_bAPIWarningEnabled)
                    {
                        _APIWarning((const char *)(v22 + 8493493));
                        v11 = 0;
                        goto LABEL_28;
                    }
                LABEL_24:
                    v11 = 0;
                    goto LABEL_28;
                }
                v25 = (unsigned int)v6;
                CUser::BaseGetAuthTicket((CUser *)this, (CUtlBuffer *)&v26, v6);
                *cbTotal = (unsigned int)a7;
                if (!pBlob)
                    goto LABEL_24;
                v19 = CBaseUser::CheckTicketHistory((CBaseUser *)this, v27, v17);
                if (v12 > 9)
                    break;
                ++v12;
            } while (v19 ^ 1);
            v20 = *cbTotal;
            v11 = 0;
            if ((signed int)*cbTotal <= cbBlob)
            {
                a10 = 0;
                if (CBaseUser::UpdateClientAuthListAndSend(this, 0, v6, 0, v27, v20, v17, 0, 0, (int)&a10))
                    AssertMsgImplementation(v22 + 8493622, 0, v22 + 8488836, 9807, 0);
                memcpy(pBlob, v27, *cbTotal);
                v11 = a10;
            }
            else if (g_bAPIWarningEnabled)
            {
                _APIWarning((const char *)(v22 + 8493560));
            }
        }
    LABEL_28:
        CUtlMemoryBase::~CUtlMemoryBase((CUtlMemoryBase *)&v26);
    }
    return v11;
}