CUtlBuffer *__cdecl CUser::LoadAppOwnershipTicketFromDisk(int this, unsigned int gameID)
{
    char        v2;                                              // al@1
    CUtlBuffer *v3;                                              // ecx@1
    int         v4;                                              // eax@2
    int(__cdecl * v5)(int, signed int, char *, int *, size_t *); // ecx@4
    int  v6;                                                     // eax@5
    int *v7;                                                     // ebx@5
    int(__cdecl * v8)(int, signed int, char *, int *, size_t *); // esi@6
    CUtlBuffer * v9;                                             // esi@7
    __int64      v10;                                            // rax@10
    int          v11;                                            // edx@10
    CUtlBuffer * v12;                                            // edi@11
    CUtlBuffer * result;                                         // eax@13
    size_t       v14;                                            // [sp+2Ch] [bp-83Ch]@4
    unsigned int v15;                                            // [sp+30h] [bp-838h]@11
    CUtlBuffer * v16;                                            // [sp+34h] [bp-834h]@11
    char         v17;                                            // [sp+38h] [bp-830h]@5
    char         v18;                                            // [sp+39h] [bp-82Fh]@5
    int          v19;                                            // [sp+54h] [bp-814h]@7
    unsigned int gameID;                                         // [sp+58h] [bp-810h]@2
    int          v21;                                            // [sp+858h] [bp-10h]@1

    v21 = __stack_chk_guard;
    v2  = (*(int(__cdecl **)(int, unsigned int))(*(_DWORD *)this + 1084))(this, gameID);
    v3  = 0;
    if (v2)
    {
        // See if the ticket has already been loaded
        v4 = CUtlRBTree<unsigned int, AppOwnershipTicket_t *>::Find(this + 12336,
                                                                    &gameID);
        if (v4 == -1)
        {
            // We couldnt find an appticket that was already loaded so we are going to look for one now
            v14 = 2048;

            v5 = *(int(__cdecl **)(int, signed int, char *, int *, size_t *))(*(_DWORD *)this + 132);

            if (gameID > 9)
            {
                v8 = *(int(__cdecl **)(int, signed int, char *, int *, size_t *))(*(_DWORD *)this + 132);
                v6 = V_snprintf(&v17, 28, "%u", gameID);
                v5 = v8;
                v7 = (int *)&gameID;
            }
            else
            {
                // Convert this into its ascii character
                v17 = gameID + 48;
                v18 = 0;
                v6  = 1;
                v7  = (int *)&gameID;
            }

            v19 = v6;
            v9  = 0;

            // Call to GetConfigBinaryBlob
            if ((unsigned __int8)v5(this,
                                    6,     // key for depots
                                    &v17,  // subkey name (which is gameid)
                                    v7,    // output buffer
                                    &v14)) // output max size
            {
                if ((unsigned __int8)CSteamEngine::BIsTicketSignatureValid((CSteamEngine *)g_pSteamEngine, v7, v14))
                {
                    if ((unsigned __int8)CSteamEngine::BIsTicketForApp((CSteamEngine *)g_pSteamEngine, v7, v14, gameID))
                    {
                        LODWORD(v10) = CSteamEngine::GetTicketSteamID((CSteamEngine *)g_pSteamEngine, v7, v14);
                        HIDWORD(v10) = *(_DWORD *)(this + 173) ^ v11;
                        if (v10 == *(_DWORD *)(this + 169))
                        {
                            // Create a new AppOwnershipTicket
                            v12 = (CUtlBuffer *)operator new(0x30u);
                            CUtlBuffer::CUtlBuffer(v12, 0, 0, 0);
                            CUtlBuffer::EnsureCapacity(v12, v14);
                            CUtlBuffer::Put(v12, v7, v14);
                            v15 = gameID;
                            v16 = v12;
                            v9  = v12;

                            // Add to tree
                            CUtlRBTree<CUtlMap<unsigned int, AppOwnershipTicket_t *, int, bool (*)(unsigned int const &, unsigned int const &)>::Node_t, int, CUtlMap<unsigned int, AppOwnershipTicket_t *, int, bool (*)(unsigned int const &, unsigned int const &)>::CKeyLess, CDefRBTreeBalanceListener<int>>::Insert(
                                this + 12336,
                                &v15,
                                0);
                        }
                    }
                }
            }
            v3 = v9;
        }
        else
        {
            // Load appticket from tree
            v3 = *(CUtlBuffer **)(*(_DWORD *)(this + 12380) + 24 * v4 + 20);
        }
    }

    result = (CUtlBuffer *)__stack_chk_guard;
    if (__stack_chk_guard == v21)
        result = v3;
    return result;
}