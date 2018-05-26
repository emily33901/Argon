int __cdecl CBaseUser::UpdateClientAuthListAndSend(int a1, int a2, int a3, int a4, void *a5, size_t a6, unsigned int *a7, int a8, int a9, int a10)
{
    int         v10;    // edi@1
    int         v11;    // ebx@2
    int         v12;    // esi@2
    int         v13;    // ecx@2
    _QWORD *    v14;    // eax@3
    int         v15;    // edx@3
    int         v16;    // edx@9
    int         v17;    // ST30_4@11
    int         v18;    // eax@11
    int         v19;    // ecx@11
    int         v20;    // edx@11
    int         v21;    // edi@11
    int         v22;    // eax@11
    int         v23;    // ecx@11
    int         v24;    // eax@11
    int         v25;    // ebx@11
    CUtlBuffer *v26;    // esi@11
    void *      v27;    // edx@11
    int         v28;    // eax@13
    int         v29;    // ecx@13
    int         v30;    // ecx@13
    int         v31;    // edx@14
    int         result; // eax@20
    bool        v33;    // zf@22
    signed int  v34;    // [sp+28h] [bp-20h]@2
    int         v35;    // [sp+30h] [bp-18h]@2
    int         v36;    // [sp+34h] [bp-14h]@2
    int         v37;    // [sp+38h] [bp-10h]@19

    v10 = a1;
    if (a2 != 1)
    {
        v34 = 524643;
        v13 = *(_DWORD *)(a1 + 5380);
        v11 = a3;
        v35 = a4;
        v12 = a8;
        v16 = a9;
    LABEL_11:
        v17 = v16;
        v18 = CUtlVector<CClientAuthInstance, CUtlMemory<CClientAuthInstance>>::InsertBefore(
            (CUtlMemoryBase *)(v10 + 5364),
            v13);
        v19                                                    = *(_DWORD *)(v10 + 5368);
        v20                                                    = v10;
        v21                                                    = 92 * v18;
        *(_DWORD *)(v19 + v21)                                 = a2;
        *(_DWORD *)((char *)&loc_200590 + v19 + v21 - 2098572) = -1;
        *(_DWORD *)((char *)&loc_20059F + v19 + v21 - 2098571) = v35;
        *(_DWORD *)((char *)&loc_20059C + v19 + v21 - 2098572) = v11;
        v22                                                    = *(_DWORD *)(v20 + 5368);
        *(_DWORD *)(v22 + v21 + 12)                            = v17;
        *(_DWORD *)(v22 + v21 + 8)                             = v12;
        v23                                                    = *(_DWORD *)(v20 + 5368);
        *(_DWORD *)((char *)&loc_2005A4 + v23 + v21 - 2098572) = *(_DWORD *)(*(_DWORD *)dword_E5CAAC[v34] + 232);
        v24                                                    = *(_DWORD *)(v20 + 5424) + 1;
        *(_DWORD *)(v20 + 5424)                                = v24;
        v25                                                    = v20;
        *(_DWORD *)((char *)&loc_2005A7 + v23 + v21 - 2098571) = v24;
        *(_DWORD *)((char *)&loc_2005BC + v23 + v21 - 2098572) = 0;
        *(_DWORD *)((char *)&loc_2005BC + v23 + v21 - 2098568) = 0;
        *((_BYTE *)&loc_2005CA + v23 + v21 - 2098572)          = 0;
        *(_DWORD *)((char *)&loc_2005C2 + v23 + v21 - 2098570) = -1;
        v26                                                    = (CUtlBuffer *)((char *)&loc_2005AC + v23 + v21 - 2098572);
        CUtlBuffer::AddNullTermination(v26);
        v27 = a5;
        if (a6)
        {
            CUtlBuffer::Put(v26, a5, a6);
            v27 = a5;
        }
        *(_DWORD *)(*(_DWORD *)(v25 + 5368) + v21 + 80)        = a7;
        v28                                                    = CRC32_ProcessSingleBuffer(v27, a7);
        v29                                                    = *(_DWORD *)(v25 + 5368);
        *(_DWORD *)((char *)&loc_2005E0 + v29 + v21 - 2098572) = v28;
        *((_BYTE *)&loc_2005E4 + v29 + v21 - 2098572)          = 0;
        *(_DWORD *)a10                                         = *(_DWORD *)((char *)&loc_2005A7 + v29 + v21 - 2098571);
        v30                                                    = *(_DWORD *)(v25 + 5400);
        if (v30 <= 0)
            goto LABEL_19;
        v31 = 0;
        while (*(_DWORD *)(*(_DWORD *)(v25 + 5388) + 4 * v31) != (a3 & (unsigned int)&unk_FFFFFF))
        {
            if (++v31 >= v30)
                goto LABEL_19;
        }
        if (v31 == -1)
        {
        LABEL_19:
            v37 = a3 & (unsigned int)&unk_FFFFFF;
            CUtlVector<unsigned int, CUtlMemory<unsigned int>>::AddToTail((CUtlMemoryBase *)(v25 + 5384), (int)&v37);
        }
        CCMInterface::ScheduleImmediateReconnect((CCMInterface *)(v25 + 36));
        CBaseUser::SendClientAuthList((CBaseUser *)v25, 1, 0);
        return 0;
    }
    v34 = 524643;
    v11 = a3;
    v35 = a4;
    v12 = a8;
    v13 = *(_DWORD *)(a1 + 5380);
    v36 = v13;
    if (v13 <= 0)
    {
    LABEL_9:
        v16 = a9;
        goto LABEL_11;
    }
    v14 = (_QWORD *)(*(_DWORD *)(a1 + 5368) + 16);
    v15 = 0;
    while ((*((_DWORD *)v14 - 4) | 2) != 3 || *v14 != __PAIR__(a4, a3) || __PAIR__(a9, a8) != __PAIR__(*((_DWORD *)v14 - 1), *((_DWORD *)v14 - 2)))
    {
        ++v15;
        v14 = (_QWORD *)((char *)v14 + 92);
        v13 = v36;
        if (v15 >= v36)
        {
            v10 = a1;
            goto LABEL_9;
        }
    }
    result = 2;
    v33    = v15 == -1;
    v10    = a1;
    v16    = a9;
    v13    = v36;
    if (v33)
        goto LABEL_11;
    return result;
}