int __cdecl CUser::InitiateGameConnection(CUser *this, int pBlob, int cbMaxBlob, __int64 gameServerID, __int64 gameId, int unIP, __int16 usPort, char bSecure)
{
    char         v8;     // al@1
    int          v9;     // edi@7
    int          v10;    // eax@7
    int          v11;    // edx@7
    int          v12;    // ST70_4@22
    int          v13;    // edi@22
    unsigned int v14;    // edi@26
    int          v15;    // eax@28
    void *       v16;    // ebx@29
    size_t       v17;    // ecx@30
    int          v18;    // esi@30
    int          v19;    // esi@32
    int          v20;    // eax@32
    char         v21;    // bl@33
    int          v22;    // eax@35
    int          v23;    // ebx@37
    int          v24;    // eax@38
    int          v25;    // esi@39
    CUtlBuffer * v26;    // eax@40
    unsigned int v27;    // esi@40
    int          v28;    // eax@42
    int          result; // eax@48
    bool         v30;    // zf@48
    signed int   v31;    // eax@51
    void *       v32;    // [sp+58h] [bp-70h]@33
    signed int   v33;    // [sp+5Ch] [bp-6Ch]@33
    int          v34;    // [sp+60h] [bp-68h]@26
    char         v35;    // [sp+64h] [bp-64h]@26
    __int16      v36;    // [sp+68h] [bp-60h]@4
    int          v37;    // [sp+6Ch] [bp-5Ch]@23
    int          v38;    // [sp+70h] [bp-58h]@23
    char         v39;    // [sp+88h] [bp-40h]@32
    int          v40;    // [sp+8Ch] [bp-3Ch]@37
    int          v41;    // [sp+9Ch] [bp-2Ch]@32

    v8 = g_bAPIWarningEnabled;
    if (!unIP && g_bAPIWarningEnabled)
    {
        _APIWarning("Invalid game server IP passed to InitiateGameConnection\n");
        v8 = g_bAPIWarningEnabled;
    }
    v36 = usPort;
    if (!usPort && v8)
        _APIWarning("Invalid game server port passed to InitiateGameConnection\n");
    v9  = HIDWORD(gameServerID);
    v10 = (HIDWORD(gameServerID) >> 20) & 0xF;
    v11 = gameServerID;
    if ((unsigned int)(v10 - 1) > 9 || (unsigned int)((HIDWORD(gameServerID) >> 24) - 1) > 3 || v10 == 1 && (!(_DWORD)gameServerID || (HIDWORD(gameServerID) & 0xFFFFFu) > 4))
    {
        goto LABEL_18;
    }
    if ((HIDWORD(gameServerID) & 0xF00000) != 7340032)
    {
        v11 = gameServerID;
        goto LABEL_17;
    }
    v11 = gameServerID;
    if ((_DWORD)gameServerID && !(HIDWORD(gameServerID) & 0xFFFFF))
    {
    LABEL_17:
        if (v11 | HIDWORD(gameServerID) & (unsigned int)&unk_F00000 ^ 0x300000)
            goto LABEL_23;
    }
LABEL_18:
    if (HIDWORD(gameServerID) | v11 ^ 1 && v11 | HIDWORD(gameServerID) ^ 0x1000000 && HIDWORD(gameServerID) | v11 ^ 2 && g_bAPIWarningEnabled)
    {
        v12 = HIDWORD(gameServerID);
        v13 = v11;
        _APIWarning("Invalid game server steamID passed to InitiateGameConnection\n");
        v11 = v13;
        v9  = v12;
    }
LABEL_23:
    v38 = v11;
    v37 = v9;
    if (!(unsigned __int8)CGameID::IsValid((CGameID *)&gameId) && g_bAPIWarningEnabled)
        _APIWarning("Invalid gameID passed to InitiateGameConnection\n");
    v35 = gameId;
    v34 = HIDWORD(gameId);
    v14 = gameId & 0xFFFFFF;
    if (v14 != CSteamEngine::GetAppIDForCurrentPipe((CSteamEngine *)g_pSteamEngine) && g_bAPIWarningEnabled)
    {
        v15 = CSteamEngine::GetAppIDForCurrentPipe((CSteamEngine *)g_pSteamEngine);
        _APIWarning("GameID mismatch. Arg %d Expected %d \n", v14, v15);
    }
    // End input validation checks

    v16 = 0;
    if (cbMaxBlob <= 0)
    {
        v17 = 0;
        v18 = 0;
    }
    else
    {
        v17 = 0;
        v18 = 0;
        if (pBlob)
        {
            // Create a buffer around the blob
            v39 = CUtlBuffer::CUtlBuffer(pBlob, cbMaxBlob);

            // Check whether we have a game connect token (see CBaseUser::CBaseUser or RealSendAuthList)
            if (*(_DWORD *)(this + 5420))
            {
                v19 = *(_DWORD *)(this + 5408);
                v20 = CUtlMemoryBase::NumAllocated((CUtlMemoryBase *)&v39);
                if (v20 - v41 < *(_DWORD *)(v19 + 16) + 4)
                {
                    // There is not enough space left to write the ticket in
                    v32 = 0;
                    v21 = 1;
                    v33 = 0;
                    v18 = 0;
                    goto LABEL_48;
                }

                // Write ticket size
                CUtlBuffer::PutUnsignedInt((CUtlBuffer *)&v39, *(_DWORD *)(v19 + 16));
                v33 = *(_DWORD *)(v19 + 16);
                v23 = v41 + v40;

                // Write ticket data into buffer
                CUtlBuffer::Put((CUtlBuffer *)&v39, *(const void **)(v19 + 4), *(_DWORD *)(v19 + 16));
            }
            else
            {
                // We dont have whatever is supposed to be here
                // So write some placeholder data here

                v22 = CUtlMemoryBase::NumAllocated((CUtlMemoryBase *)&v39);
                if (v22 - v41 < 4)
                {
                    // not enough space to write ticket
                    v32 = 0;
                    v33 = 0;
                    v18 = 0;
                    v21 = 1;
                    goto LABEL_48;
                }

                // Write empty ticket size
                CUtlBuffer::PutUnsignedInt((CUtlBuffer *)&v39, 4u);
                v24 = CUtlMemoryBase::NumAllocated((CUtlMemoryBase *)&v39);
                v32 = 0;
                v33 = 0;
                v18 = 0;
                v21 = 1;
                if (v24 - v41 < 4)
                    goto LABEL_48;
                v33 = 4;
                v25 = v40 + v41;
                CUtlBuffer::PutUnsignedInt((CUtlBuffer *)&v39, 0);
                v23 = v25;
            }

            // Get the appticket for this gameid
            v26 = CUser::LoadAppOwnershipTicketFromDisk(this, v14 /*gameid*/);
            v27 = 0;
            if (v26)
                v27 = *((_DWORD *)v26 + 5); // Get size from the ticket
            v28 = CUtlMemoryBase::NumAllocated((CUtlMemoryBase *)&v39);
            if (v28 - v41 >= (signed int)(v27 + 4))
            {
                // Write size of the ticket
                CUtlBuffer::PutUnsignedInt((CUtlBuffer *)&v39, v27);
                if (v27)
                {
                    // CUser::GetAppOwnershipTicketData
                    (*(void(__cdecl **)(int, unsigned int, int, unsigned int))(*(_DWORD *)this + 428))(
                        this,
                        v14,       // gameid
                        v41 + v40, // Current location in the buffer
                        v27);      // size of blob

                    // Seek buffer to the end of the ticket (although it doesnt matter at this point)
                    CUtlBuffer::SeekPut(&v39, 1, v27);
                }
                v32 = (void *)v23;
                v18 = v41;
                v21 = 0;

            // Cleanup
            LABEL_48:
                CUtlMemoryBase::~CUtlMemoryBase((CUtlMemoryBase *)&v39);
                result = 0;
                v30    = v21 == 0;
                v17    = v33;
                v16    = v32;
                if (!v30)
                    return result;
                goto LABEL_49;
            }
            v32 = (void *)v23;
        }
    }

    // By the end of this section of code
    // The buffer will be as follows
    // 1. unsigned int: GameConnectToken size
    // 2.       void *: GameConnectToken[size]
    // 3. unsigned int: ticket size
    // 4.       void *: Ticket[size]

LABEL_49:

    // Checks if we have the game and then handles gc login + process tracking
    // to perform the shutdown equivilent of these when the game process terminates
    if ((unsigned __int8)CUser::InternalUpdateClientGame(
            (CUser *)this,
            0,
            v38, // gameserverid
            v37, // gameserverid
            v35, // gameid
            v34, // gameid
            0,
            0,
            unIP, // ip
            v36,  // port
            bSecure,
            v16,
            v17,
            *(_DWORD *)(g_pSteamEngine + 232),
            0,
            0,
            0,
            0))
    {
        // This gets the game to send our new updated games list to the server
        CUser::ScheduleSendGameList((CUser *)this);
    }
    v31 = *(_DWORD *)((char *)&loc_58AC97 + this - 5805931);
    if (v31 < 2)
    {
        if (v31 == 1)
            ++*(_DWORD *)(*(_DWORD *)((char *)&loc_58AC8D + this - 5805933) + 20);
    }
    else
    {
        // Shift all of our tokens left by 1 becuase we used one
        // And then decrement the total count
        CUtlMemoryBase::~CUtlMemoryBase(*(CUtlMemoryBase **)((char *)&loc_58AC8D + this - 5805933));
        CUtlVector<CConnectionToken, CUtlMemory<CConnectionToken>>::ShiftElementsLeft(
            (char *)&loc_58AC87 + this - 5805931,
            0,
            1);
        --*(_DWORD *)((char *)&loc_58AC97 + this - 5805931);
    }
    return v18;
}