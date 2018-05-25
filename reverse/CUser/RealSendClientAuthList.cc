int __cdecl CBaseUser::RealSendClientAuthList(int this)
{
    int     v1;  // edi@1
    int     v2;  // esi@1
    int     v3;  // eax@1
    int     v4;  // ecx@1
    int     v5;  // eax@1
    int     v6;  // eax@1
    int     v7;  // eax@1
    int     v8;  // edx@2
    int     v9;  // edi@4
    int     v10; // ecx@4
    int     v11; // eax@4
    int     v12; // ebx@5
    int     v13; // eax@8
    int     v14; // esi@9
    int     v15; // eax@9
    int     v16; // ecx@9
    int     v17; // ST34_4@9
    int     v18; // edx@9
    int     v19; // edx@9
    int     v20; // edx@9
    int     v21; // edx@9
    int     v22; // esi@9
    int     v23; // eax@9
    int     v24; // edi@10
    int     v25; // esi@10
    int     v26; // edx@12
    _DWORD *v27; // eax@19
    int     v28; // esi@22
    _DWORD *v29; // ebx@23
    _DWORD *v31; // [sp+24h] [bp-44h]@2
    int     v32; // [sp+28h] [bp-40h]@9
    int     v33; // [sp+2Ch] [bp-3Ch]@9
    int     v34; // [sp+34h] [bp-34h]@6
    int     v35; // [sp+34h] [bp-34h]@9
    char    v36; // [sp+38h] [bp-30h]@1
    int     v37; // [sp+54h] [bp-14h]@1
    int     v38; // [sp+58h] [bp-10h]@23

    v1 = this;
    CUtlMemoryBase::Purge((CUtlMemoryBase *)(this + 5364), *(_DWORD *)(this + 5380), 1);
    CProtoBufMsg<CMsgClientAuthList>::CProtoBufMsg(&v36, 5432);
    v2 = v37;

    // Get the total number of game connect tokens that we have
    v3 = *(_DWORD *)(this + 5420);
    v4 = *(_DWORD *)(v37 + 8);
    *(_DWORD *)(v37 + 8) |= 1u;

    *(_DWORD *)(v2 + 16)     = v3;
    v5                       = *(_DWORD *)(this + 5428);
    *(_DWORD *)(v2 + 8)      = v4 | 3;
    *(_DWORD *)(v2 + 20)     = v5;
    *(_DWORD *)(this + 5428) = 0;
    v6                       = *(_DWORD *)(this + 5460);
    *(_DWORD *)(v2 + 8) |= 4u;
    *(_DWORD *)(v2 + 40)     = v6;
    v7                       = *(_DWORD *)(this + 5464) + 1;
    *(_DWORD *)(this + 5464) = v7;
    *(_BYTE *)(v2 + 8) |= 0x20u;
    *(_DWORD *)(v2 + 44) = v7;
    if (*(_DWORD *)(this + 5380) <= 0)
    {
        v27 = (_DWORD *)(this + 5400);
    }
    else
    {
        v31 = (_DWORD *)(this + 5400);
        v8  = 0;
        while (1)
        {
            v9  = *(_DWORD *)(v1 + 5368);
            v10 = *(_DWORD *)(v2 + 28);
            v11 = *(_DWORD *)(v2 + 32);
            if (v10 >= v11)
            {
                v34 = v8;
                if (v11 == *(_DWORD *)(v2 + 36))
                    google::protobuf::internal::RepeatedPtrFieldBase::Reserve(
                        (google::protobuf::internal::RepeatedPtrFieldBase *)(v2 + 24),
                        v11 + 1);
                v12 = google::protobuf::internal::GenericTypeHandler<CMsgAuthTicket>::New();
                ++*(_DWORD *)(v2 + 32);
                v13                                         = *(_DWORD *)(v2 + 28);
                *(_DWORD *)(v2 + 28)                        = v13 + 1;
                *(_DWORD *)(*(_DWORD *)(v2 + 24) + 4 * v13) = v12;
                v8                                          = v34;
            }
            else
            {
                *(_DWORD *)(v2 + 28) = v10 + 1;
                v12                  = *(_DWORD *)(*(_DWORD *)(v2 + 24) + 4 * v10);
            }
            v35 = v8;
            v14 = 92 * v8;
            v32 = 92 * v8;
            v33 = v9;
            v15 = *(_DWORD *)(v9 + 92 * v8);
            v16 = *(_DWORD *)((char *)&loc_200E34 + v12 - 2100780);
            *(_DWORD *)((char *)&loc_200E34 + v12 - 2100780) |= 1u;
            *(_DWORD *)((char *)&loc_200E40 + v12 - 2100784) = v15;
            *(_DWORD *)((char *)&loc_200E44 + v12 - 2100784) = *(_DWORD *)(v9 + 92 * v8 + 4);
            *(_QWORD *)((char *)&loc_200E47 + v12 - 2100783) = *(_QWORD *)(v9 + 92 * v8 + 8);
            v17                                              = *(_DWORD *)(v9 + 92 * v8 + 16);
            v18                                              = *(_DWORD *)(v9 + 92 * v8 + 20);
            *(_DWORD *)((char *)&loc_200E34 + v12 - 2100780) = v16 | 0xF;
            *(_DWORD *)((char *)&loc_200E54 + v12 - 2100784) = v18;
            *(_DWORD *)((char *)&loc_200E4F + v12 - 2100783) = v17;
            v19                                              = *(_DWORD *)(v9 + v14 + 24);
            *(_DWORD *)((char *)&loc_200E34 + v12 - 2100780) = v16 | 0x1F;
            *(_DWORD *)((char *)&loc_200E57 + v12 - 2100783) = v19;
            v20                                              = *(_DWORD *)(v9 + v14 + 84);
            *(_DWORD *)((char *)&loc_200E34 + v12 - 2100780) = v16 | 0x3F;
            *(_DWORD *)((char *)&loc_200E5A + v12 - 2100782) = v20;
            v21                                              = *(_DWORD *)(v9 + v14 + 36);
            v22                                              = *(_DWORD *)(v9 + v14 + 80);
            *(_DWORD *)((char *)&loc_200E34 + v12 - 2100780) = v16 | 0x7F;
            v23                                              = *(_DWORD *)((char *)&loc_200E5A + v12 - 2100778);
            if (v23 == *(_DWORD *)*(&dword_E5C228 + 525196))
            {
                v24                                              = v22;
                v25                                              = v21;
                v23                                              = operator new(0xCu);
                *(_DWORD *)(v23 + 4)                             = 0;
                *(_DWORD *)v23                                   = 0;
                *(_DWORD *)(v23 + 8)                             = 0;
                *(_DWORD *)((char *)&loc_200E5A + v12 - 2100778) = v23;
                v21                                              = v25;
                v22                                              = v24;
            }
            std::__1::basic_string<char, std::__1::char_traits<char>, std::__1::allocator<char>>::assign(v23, v21, v22);
            if (*v31 <= 0)
                goto LABEL_17;
            v26 = 0;
            while (*(_DWORD *)(*(_DWORD *)(this + 5388) + 4 * v26) != ((unsigned int)&unk_FFFFFF & *(_DWORD *)(v33 + v32 + 16)))
            {
                if (++v26 >= *v31)
                    goto LABEL_17;
            }
            if (v26 == -1)
            LABEL_17:
                AssertMsgImplementation(
                    "Assertion Failed: m_vecAuthAppIds.Find( clientAuthInstance.m_GameID.AppID() ) != m_vecAuthAppIds.InvalidIndex()",
                    0,
                    "/Users/buildbot/buildslave/steam_rel_client_osx/build/src/clientdll/baseuser.cpp",
                    1380,
                    0);
            v8 = v35 + 1;
            v1 = this;
            if (v35 + 1 >= *(_DWORD *)(this + 5380))
                break;
            v2 = v37;
        }
        v27 = (_DWORD *)(this + 5400);
    }
    if (*v27 > 0)
    {
        v28 = 0;
        do
        {
            v29 = v27;
            v38 = *(_DWORD *)(*(_DWORD *)(v1 + 5388) + 4 * v28);
            google::protobuf::RepeatedField<unsigned int>::Add(v37 + 48, &v38);
            ++v28;
            v27 = v29;
        } while (v28 < *v29);
    }
    CCMInterface::BSendMsgToCM(v1 + 36, &v36);
    if (!*(_DWORD *)(v1 + 5380))
        CCMInterface::AdjustConnectionPriority((CCMInterface *)(v1 + 36), 0, 1);
    return CProtoBufMsg<CMsgClientAuthList>::~CProtoBufMsg(&v36);
}