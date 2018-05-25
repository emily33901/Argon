int __cdecl CUser::SendGameList(int this)
{
    int          v1;              // ebx@1
    void *       v2;              // esi@1
    int          v3;              // edi@2
    int          v4;              // eax@2
    int          v5;              // edx@3
    int          v6;              // ecx@5
    int          v7;              // eax@5
    int          v8;              // esi@6
    int          v9;              // eax@9
    int          v10;             // edx@9
    int          v11;             // eax@10
    int          v12;             // edx@10
    int          v13;             // ST64_4@10
    int          v14;             // ecx@10
    int          v15;             // ebx@10
    int          v16;             // ecx@10
    int          v17;             // edx@10
    int          v18;             // edi@10
    char *       v19;             // eax@10
    int          v20;             // ebx@11
    int          v21;             // eax@12
    int          v22;             // ecx@12
    int          v23;             // eax@12
    int          v24;             // eax@12
    unsigned int v25;             // ebx@14
    int          v26;             // eax@18
    int          v27;             // eax@19
    char *       v28;             // eax@20
    char *       v29;             // eax@22
    int(__cdecl * **v30)(_DWORD); // eax@24
    int          v31;             // ecx@24
    char *       v32;             // eax@27
    char *       v33;             // eax@32
    int          v34;             // eax@35
    signed int   v35;             // edx@35
    int          v36;             // edi@35
    int          v37;             // ecx@37
    char *       v38;             // eax@37
    int          v39;             // ecx@39
    int          v40;             // ebx@39
    signed int   v41;             // edi@39
    int          v42;             // edx@39
    int          v43;             // eax@40
    int          v44;             // esi@40
    char *       v45;             // eax@40
    int          v46;             // ST58_4@41
    int          v47;             // eax@43
    int          v48;             // ecx@45
    char *       v49;             // eax@45
    int          v50;             // eax@47
    int          v51;             // ecx@47
    int          v53;             // [sp+4Ch] [bp-2024Ch]@40
    int          v54;             // [sp+50h] [bp-20248h]@6
    unsigned int v55;             // [sp+54h] [bp-20244h]@12
    _DWORD *     v56;             // [sp+54h] [bp-20244h]@25
    int          v57;             // [sp+58h] [bp-20240h]@10
    signed int   v58;             // [sp+5Ch] [bp-2023Ch]@3
    int          v59;             // [sp+64h] [bp-20234h]@25
    char         v60;             // [sp+68h] [bp-20230h]@2
    int          v61;             // [sp+84h] [bp-20214h]@2
    char         v62;             // [sp+88h] [bp-20210h]@25
    char         v63;             // [sp+10088h] [bp-10210h]@25
    char         v64;             // [sp+20088h] [bp-210h]@17
    char         v65;             // [sp+20188h] [bp-110h]@14
    int          v66;             // [sp+20288h] [bp-10h]@1

    v1  = this;
    v2  = &__stack_chk_guard;
    v66 = __stack_chk_guard;
    CUtlMemoryBase::Purge((CUtlMemoryBase *)(this + 6420), *(_DWORD *)(this + 6436), 1);
    if ((unsigned __int8)(*(int(__cdecl **)(int))((char *)&loc_57236E + *(_DWORD *)v1 - 5710670))(v1))
    {
        CProtoBufMsg<CMsgClientGamesPlayed>::CProtoBufMsg(&v60, 5410);
        v3 = v61;
        v4 = *(_DWORD *)(this + 164);
        *(_DWORD *)(v3 + 8) |= 2u;
        *(_DWORD *)(v3 + 32) = v4;
        if (*(_DWORD *)(this + 6436) > 0)
        {
            v5  = 0;
            v58 = 1300;
            while (1)
            {
                v6 = *(_DWORD *)(v3 + 20);
                v7 = *(_DWORD *)(v3 + 24);
                if (v6 >= v7)
                {
                    v54 = v5;
                    if (v7 == *(_DWORD *)(v3 + 28))
                        google::protobuf::internal::RepeatedPtrFieldBase::Reserve(
                            (google::protobuf::internal::RepeatedPtrFieldBase *)(v3 + 16),
                            v7 + 1);
                    v9 = google::protobuf::internal::GenericTypeHandler<CMsgClientGamesPlayed_GamePlayed>::New();
                    ++*(_DWORD *)(v3 + 24);
                    v10                                         = *(_DWORD *)(v3 + 20);
                    *(_DWORD *)(v3 + 20)                        = v10 + 1;
                    *(_DWORD *)(*(_DWORD *)(v3 + 16) + 4 * v10) = v9;
                    v8                                          = v9;
                }
                else
                {
                    v54                  = v5;
                    *(_DWORD *)(v3 + 20) = v6 + 1;
                    v8                   = *(_DWORD *)(*(_DWORD *)(v3 + 16) + 4 * v6);
                }
                v11                  = *(_DWORD *)(v1 + 6424);
                v57                  = v11;
                v12                  = *(_DWORD *)(v8 + 8);
                *(_QWORD *)(v8 + 16) = *(_QWORD *)(&byte_571E3F[v11 - 5710675] + v58);
                v13                  = *(_DWORD *)(&byte_571E47[v11 - 5710675] + v58);
                v14                  = *(_DWORD *)(&byte_571E4B[v11 - 5710675] + v58);
                *(_DWORD *)(v8 + 8)  = v12 | 3;
                *(_DWORD *)(v8 + 28) = v14;
                *(_DWORD *)(v8 + 24) = v13;
                *(_DWORD *)(v8 + 32) = *(_DWORD *)(&byte_571E4F[v11 - 5710675] + v58);
                *(_DWORD *)(v8 + 36) = *(_WORD *)(&byte_571E53[v11 - 5710675] + v58);
                LOBYTE(v14)          = *(&byte_571E5B[v11 - 5710675] + v58);
                v15                  = v12;
                *(_DWORD *)(v8 + 8)  = v12 | 0x1F;
                *(_BYTE *)(v8 + 48)  = v14;
                v16                  = *(_DWORD *)((char *)&loc_5722C2 + v11 + v58 - 5710674);
                *(_DWORD *)(v8 + 8)  = v12 | 0x11F;
                *(_DWORD *)(v8 + 52) = v16;
                v17                  = *(_DWORD *)(&byte_571E67[v11 - 5710675] + v58);
                v18                  = *(int *)((char *)&dword_571E73 + v11 + v58 - 5710675);
                *(_DWORD *)(v8 + 8)  = v15 | 0x13F;
                v19                  = *(char **)(v8 + 40);
                if (v19 == (char *)google::protobuf::internal::empty_string_)
                {
                    v20                                            = v17;
                    v19                                            = (char *)operator new(0xCu);
                    *(_DWORD *)&v19[(_DWORD)&loc_572354 - 5710672] = 0;
                    *(_DWORD *)v19                                 = 0;
                    *(_DWORD *)&v19[(_DWORD)&loc_57235A - 5710674] = 0;
                    *(_DWORD *)(v8 + 40)                           = v19;
                    v17                                            = v20;
                }
                std::__1::basic_string<char, std::__1::char_traits<char>, std::__1::allocator<char>>::assign(v19, v17, v18);
                v21 = *(_DWORD *)(this + 16772);
                v22 = *(_DWORD *)(v8 + 8);
                *(_DWORD *)(v8 + 8) |= 0x200u;
                *(_DWORD *)(v8 + 60) = v21;
                *(_DWORD *)(v8 + 72) = *(_DWORD *)(v57 + v58 - 48);
                v23                  = *(_DWORD *)(v57 + v58 - 56);
                *(_DWORD *)(v8 + 8)  = v22 | 0x4A00;
                *(_DWORD *)(v8 + 76) = v23;
                v55                  = *(_DWORD *)(v57 + v58 - 1276) & 0xFFFFE0FF;
                v24                  = *(_DWORD *)(this + 16736);
                if (v24 == 1)
                {
                    v25 = v55 | 0x400;
                }
                else if (v24 == 2)
                {
                    CDbgFmtMsg::CDbgFmtMsg(
                        (CDbgFmtMsg *)&v65,
                        "Assertion Failed: %s",
                        "Playing a game from a client in k_EUIMode_Mobile?!");
                    AssertMsgImplementation(
                        &v65,
                        0,
                        "/Users/buildbot/buildslave/steam_rel_client_osx/build/src/clientdll/user.cpp",
                        10370,
                        0);
                    v25 = v55 | 0x200;
                }
                else
                {
                    v25 = *(_DWORD *)(v57 + v58 - 1276) & 0xFFFFE0FF;
                    if (v24 == 3)
                    {
                        CDbgFmtMsg::CDbgFmtMsg(
                            (CDbgFmtMsg *)&v64,
                            "Assertion Failed: %s",
                            "Playing a game from a client in k_EUIModeWeb?!");
                        AssertMsgImplementation(
                            &v64,
                            0,
                            "/Users/buildbot/buildslave/steam_rel_client_osx/build/src/clientdll/user.cpp",
                            10378,
                            0);
                        v25 = v55 | 0x100;
                    }
                }
                v26 = VRManager();
                if ((unsigned __int8)(*(int(__cdecl **)(int))(*(_DWORD *)v26 + 36))(v26))
                {
                    v27 = VRManager();
                    v25 |= 0x800u;
                    if ((unsigned __int8)(*(int(__cdecl **)(int))(*(_DWORD *)v27 + 28))(v27))
                    {
                        *(_BYTE *)(v8 + 9) |= 0x10u;
                        v28 = *(char **)(v8 + 68);
                        if (v28 == (char *)google::protobuf::internal::empty_string_)
                        {
                            v28                                            = (char *)operator new(0xCu);
                            *(_DWORD *)&v28[(_DWORD)&loc_572354 - 5710672] = 0;
                            *(_DWORD *)v28                                 = 0;
                            *(_DWORD *)&v28[(_DWORD)&loc_57235A - 5710674] = 0;
                            *(_DWORD *)(v8 + 68)                           = v28;
                        }
                        std::__1::basic_string<char, std::__1::char_traits<char>, std::__1::allocator<char>>::assign(v28, "(unknown)");
                        *(_BYTE *)(v8 + 9) |= 0x20u;
                        v29 = *(char **)(v8 + 80);
                        if (v29 == (char *)google::protobuf::internal::empty_string_)
                        {
                            v29                                            = (char *)operator new(0xCu);
                            *(_DWORD *)&v29[(_DWORD)&loc_572354 - 5710672] = 0;
                            *(_DWORD *)v29                                 = 0;
                            *(_DWORD *)&v29[(_DWORD)&loc_57235A - 5710674] = 0;
                            *(_DWORD *)(v8 + 80)                           = v29;
                        }
                        std::__1::basic_string<char, std::__1::char_traits<char>, std::__1::allocator<char>>::assign(v29, "(unknown)");
                        v30 = (int(__cdecl ***)(_DWORD))VRManager();
                        v31 = (**v30)(v30);
                        if (v31)
                        {
                            v56 = (_DWORD *)v31;
                            __bzero(&v63, (char *)&loc_FFFF + 1);
                            __bzero(&v62, (char *)&loc_FFFF + 1);
                            if ((*(int(__cdecl **)(_DWORD *, _DWORD, signed int, char *, char *, int *))((char *)&loc_5723BA + *v56 - 5710674))(
                                    v56,
                                    0,
                                    1001,
                                    &v63,
                                    (char *)&loc_FFFF + 1,
                                    &v59) &&
                                !v59)
                            {
                                *(_BYTE *)(v8 + 9) |= 0x20u;
                                v32 = *(char **)(v8 + 80);
                                if (v32 == (char *)google::protobuf::internal::empty_string_)
                                {
                                    v32                                            = (char *)operator new(0xCu);
                                    *(_DWORD *)&v32[(_DWORD)&loc_572354 - 5710672] = 0;
                                    *(_DWORD *)v32                                 = 0;
                                    *(_DWORD *)&v32[(_DWORD)&loc_57235A - 5710674] = 0;
                                    *(_DWORD *)(v8 + 80)                           = v32;
                                }
                                std::__1::basic_string<char, std::__1::char_traits<char>, std::__1::allocator<char>>::assign(v32, &v63);
                            }
                            if ((*(int(__cdecl **)(_DWORD *, _DWORD, signed int, char *, char *, int *))((char *)&loc_5723BA + *v56 - 5710674))(
                                    v56,
                                    0,
                                    1005,
                                    &v62,
                                    (char *)&loc_FFFF + 1,
                                    &v59) &&
                                !v59)
                            {
                                *(_BYTE *)(v8 + 9) |= 0x10u;
                                v33 = *(char **)(v8 + 68);
                                if (v33 == (char *)google::protobuf::internal::empty_string_)
                                {
                                    v33                                            = (char *)operator new(0xCu);
                                    *(_DWORD *)&v33[(_DWORD)&loc_572354 - 5710672] = 0;
                                    *(_DWORD *)v33                                 = 0;
                                    *(_DWORD *)&v33[(_DWORD)&loc_57235A - 5710674] = 0;
                                    *(_DWORD *)(v8 + 68)                           = v33;
                                }
                                std::__1::basic_string<char, std::__1::char_traits<char>, std::__1::allocator<char>>::assign(v33, &v62);
                            }
                        }
                    }
                }
                v34 = v25 | 0x1000;
                v35 = v58;
                v36 = v57;
                if (*(_DWORD *)(v57 + v58 - 60) != 1)
                    v34 = v25;
                v37                  = *(_DWORD *)(v8 + 8);
                *(_DWORD *)(v8 + 64) = v34;
                *(_DWORD *)(v8 + 8)  = v37 | 0x440;
                v38                  = *(char **)(v8 + 44);
                if (v38 == (char *)google::protobuf::internal::empty_string_)
                {
                    v38                                            = (char *)operator new(0xCu);
                    *(_DWORD *)&v38[(_DWORD)&loc_572354 - 5710672] = 0;
                    *(_DWORD *)v38                                 = 0;
                    *(_DWORD *)&v38[(_DWORD)&loc_57235A - 5710674] = 0;
                    *(_DWORD *)(v8 + 44)                           = v38;
                    v35                                            = v58;
                    v36                                            = v57;
                }
                v39 = v36 + v35 - 1236;
                v40 = v36;
                v41 = v35;
                std::__1::basic_string<char, std::__1::char_traits<char>, std::__1::allocator<char>>::assign(v38, v39);
                v42 = *(_DWORD *)(v40 + v41 - 92);
                if (v42)
                {
                    v43 = v8;
                    v53 = v8;
                    v44 = *(_DWORD *)(v40 + v41 - 108);
                    *(_DWORD *)((char *)&loc_57235A + v53 - 5710674) |= 0x80u;
                    v45 = *(char **)((char *)&loc_57238A + v43 - 5710674);
                    if (v45 == (char *)google::protobuf::internal::empty_string_)
                    {
                        v46                                            = v42;
                        v45                                            = (char *)operator new(0xCu);
                        v42                                            = v46;
                        *(_DWORD *)&v45[(_DWORD)&loc_572354 - 5710672] = 0;
                        *(_DWORD *)v45                                 = 0;
                        *(_DWORD *)&v45[(_DWORD)&loc_57235A - 5710674] = 0;
                        *(_DWORD *)(v53 + 56)                          = v45;
                    }
                    std::__1::basic_string<char, std::__1::char_traits<char>, std::__1::allocator<char>>::assign(v45, v44, v42);
                    v8 = v53;
                }
                v47 = *(_DWORD *)(v40 + v41 - 8);
                if (v47 || *(_DWORD *)(v40 + v41 - 4))
                {
                    v48 = *(_DWORD *)(v8 + 8);
                    *(_DWORD *)(v8 + 8) |= 0x20000u;
                    *(_DWORD *)(v8 + 92)  = v47;
                    *(_DWORD *)(v8 + 104) = *(_DWORD *)(v40 + v41 - 4);
                    *(_DWORD *)(v8 + 88)  = *(_DWORD *)(v40 + v41 - 12);
                    *(_DWORD *)(v8 + 8)   = v48 | 0x78000;
                    v49                   = *(char **)(v8 + 84);
                    if (v49 == (char *)google::protobuf::internal::empty_string_)
                    {
                        v49                                            = (char *)operator new(0xCu);
                        *(_DWORD *)&v49[(_DWORD)&loc_572354 - 5710672] = 0;
                        *(_DWORD *)v49                                 = 0;
                        *(_DWORD *)&v49[(_DWORD)&loc_57235A - 5710674] = 0;
                        *(_DWORD *)(v8 + 84)                           = v49;
                    }
                    std::__1::basic_string<char, std::__1::char_traits<char>, std::__1::allocator<char>>::assign(v49, v40 + v41);
                    v50 = *(_DWORD *)(v40 + v41 + 20);
                    v51 = *(_DWORD *)(v40 + v41 + 24);
                    *(_DWORD *)(v8 + 8) |= 0x80000u;
                    *(_DWORD *)(v8 + 100) = v51;
                    *(_DWORD *)(v8 + 96)  = v50;
                }
                v5 = v54 + 1;
                v1 = this;
                if (v54 + 1 >= *(_DWORD *)(this + 6436))
                    break;
                v58 += 1328;
                v3 = v61;
            }
        }
        CCMInterface::BSendMsgToCM(v1 + 36, &v60);
        v2 = &__stack_chk_guard;
        CProtoBufMsg<CMsgClientGamesPlayed>::~CProtoBufMsg(&v60);
    }
    return *(_DWORD *)v2;
}