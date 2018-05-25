int __cdecl CUser::CUser(int a1, int a2)
{
    int     v3;     // eax@5
    int     result; // eax@6
    int     v5;     // [sp+130h] [bp-78h]@6
    int     v6;     // [sp+134h] [bp-74h]@6
    __int64 v7;     // [sp+138h] [bp-70h]@1
    __int64 v8;     // [sp+140h] [bp-68h]@1
    __int64 v9;     // [sp+148h] [bp-60h]@1
    __int64 v10;    // [sp+150h] [bp-58h]@1
    __int64 v11;    // [sp+158h] [bp-50h]@1
    __int64 v12;    // [sp+160h] [bp-48h]@1
    __int64 v13;    // [sp+168h] [bp-40h]@1
    __int64 v14;    // [sp+170h] [bp-38h]@1
    __int64 v15;    // [sp+178h] [bp-30h]@1
    __int64 v16;    // [sp+180h] [bp-28h]@1
    __int64 v17;    // [sp+188h] [bp-20h]@1
    __int64 v18;    // [sp+190h] [bp-18h]@1

    CBaseUser::CBaseUser(a1, a2);
    *(_DWORD *)a1          = off_10765D8;
    *(_DWORD *)(a1 + 5468) = &off_1076AE8;
    *(_DWORD *)(a1 + 5472) = &off_1076E88;
    *(_DWORD *)(a1 + 5476) = &off_1076F48;
    *(_DWORD *)(a1 + 5480) = &off_1076F54;
    *(_DWORD *)(a1 + 5484) = &off_1076F78;
    v7                     = (unsigned int)CUser::OnSteamServersConnected;
    CInternalCallback<CUser, SteamServersConnected_t>::CInternalCallback(
        (CInternalCallbackBase *)(a1 + 5488),
        a2,
        a1,
        (int)CUser::OnSteamServersConnected,
        0);
    v8 = (unsigned int)CUser::OnSteamServersDisconnected;
    CInternalCallback<CUser, SteamServersDisconnected_t>::CInternalCallback(
        (CInternalCallbackBase *)(a1 + 5512),
        a2,
        a1,
        (int)CUser::OnSteamServersDisconnected,
        0);
    v9 = (unsigned int)CUser::OnAppInfoUpdateComplete;
    CInternalCallback<CUser, AppInfoUpdateComplete_t>::CInternalCallback(
        (CInternalCallbackBase *)(a1 + 5536),
        a2,
        a1,
        (int)CUser::OnAppInfoUpdateComplete,
        0);
    v10 = (unsigned int)CUser::OnRemoteStorageSyncedServer;
    CInternalCallback<CUser, RemoteStorageAppSyncedServer_t>::CInternalCallback(
        (CInternalCallbackBase *)(a1 + 5560),
        a2,
        a1,
        (int)CUser::OnRemoteStorageSyncedServer,
        0);
    v11 = (unsigned int)CUser::OnRemoteStorageSyncedClient;
    CInternalCallback<CUser, RemoteStorageAppSyncedClient_t>::CInternalCallback(
        (CInternalCallbackBase *)(a1 + 5584),
        a2,
        a1,
        (int)CUser::OnRemoteStorageSyncedClient,
        0);
    v12 = (unsigned int)CUser::OnRemoteStorageConflictResolved;
    CInternalCallback<CUser, RemoteStorageConflictResolution_t>::CInternalCallback(
        (CInternalCallbackBase *)(a1 + 5608),
        a2,
        a1,
        (int)CUser::OnRemoteStorageConflictResolved,
        0);
    v13 = (unsigned int)CUser::OnRemoteStorageStateEvaluated;
    CInternalCallback<CUser, RemoteStorageAppSyncStatusCheck_t>::CInternalCallback(
        (CInternalCallbackBase *)(a1 + 5632),
        a2,
        a1,
        (int)CUser::OnRemoteStorageStateEvaluated,
        0);
    v14 = (unsigned int)CUser::SendGameList;
    CScheduledFunction<CUser>::CScheduledFunction((CBaseScheduledFunction *)(a1 + 5656), a1, (int)CUser::SendGameList, 0);
    CDRM::CDRM(a1 + 5700, a1, a1 + 36);
    CUtlMemoryBase::CUtlMemoryBase(a1 + 5856, 4, 0, 0);
    *(_DWORD *)(a1 + 5872) = 0;
    *(_DWORD *)(a1 + 5876) = 0;
    *(_BYTE *)(a1 + 5885)  = 1;
    *(_DWORD *)(a1 + 5896) = 0;
    CUtlMemoryBase::CUtlMemoryBase(a1 + 5900, 32, 0, 0);
    *(_DWORD *)(a1 + 5916) = 0;
    CUtlMemoryBase::CUtlMemoryBase(a1 + 5944, 20, 0, 0);
    *(_DWORD *)(a1 + 5960) = 0;
    v15                    = (unsigned int)CUser::ScheduledSaveFavorites;
    CScheduledFunction<CUser>::CScheduledFunction(
        (CBaseScheduledFunction *)(a1 + 5984),
        a1,
        (int)CUser::ScheduledSaveFavorites,
        0);
    v16 = (unsigned int)CUser::ScheduledSyncCloud;
    CScheduledFunction<CUser>::CScheduledFunction(
        (CBaseScheduledFunction *)(a1 + 6028),
        a1,
        (int)CUser::ScheduledSyncCloud,
        0);
    CUtlMemoryBase::CUtlMemoryBase(a1 + 6084, 60, 0, 0);
    *(_DWORD *)(a1 + 6100) = 0;
    *(_DWORD *)(a1 + 6124) = -1;
    *(_DWORD *)(a1 + 6128) = 0;
    *(_DWORD *)(a1 + 6136) = 0;
    *(_DWORD *)(a1 + 6132) = -1;
    *(_DWORD *)(a1 + 6112) = -1;
    *(_DWORD *)(a1 + 6108) = -1;
    *(_DWORD *)(a1 + 6104) = -1;
    *(_DWORD *)(a1 + 6116) = 1;
    CUtlMemoryBase::CUtlMemoryBase(a1 + 6144, 40, 0, 0);
    *(_DWORD *)(a1 + 6120) = *(_DWORD *)(a1 + 6148);
    *(_DWORD *)(a1 + 6180) = -1;
    *(_DWORD *)(a1 + 6184) = 0;
    *(_DWORD *)(a1 + 6192) = 0;
    *(_DWORD *)(a1 + 6188) = -1;
    *(_DWORD *)(a1 + 6168) = -1;
    *(_DWORD *)(a1 + 6164) = -1;
    *(_DWORD *)(a1 + 6160) = -1;
    *(_DWORD *)(a1 + 6172) = 1;
    CUtlMemoryBase::CUtlMemoryBase(a1 + 6200, 40, 0, 0);
    *(_DWORD *)(a1 + 6176) = *(_DWORD *)(a1 + 6204);
    CUtlMemoryBase::CUtlMemoryBase(a1 + 6216, 4, 0, 0);
    *(_DWORD *)(a1 + 6232) = 0;
    *(_DWORD *)(a1 + 6256) = -1;
    *(_DWORD *)(a1 + 6260) = 0;
    *(_DWORD *)(a1 + 6268) = 0;
    *(_DWORD *)(a1 + 6264) = -1;
    *(_DWORD *)(a1 + 6244) = -1;
    *(_DWORD *)(a1 + 6240) = -1;
    *(_DWORD *)(a1 + 6236) = -1;
    *(_DWORD *)(a1 + 6248) = 1;
    CUtlMemoryBase::CUtlMemoryBase(a1 + 6276, 24, 0, 0);
    *(_DWORD *)(a1 + 6252) = *(_DWORD *)(a1 + 6280);
    CUtlMemoryBase::CUtlMemoryBase(a1 + 6296, 12, 0, 0);
    *(_DWORD *)(a1 + 6312) = 0;
    CUtlMemoryBase::CUtlMemoryBase(a1 + 6316, 12, 0, 0);
    *(_DWORD *)(a1 + 6332) = 0;
    CUtlHashMap<EUserNotification, int, CDefEquals<EUserNotification>, HashFunctor<EUserNotification>>::CUtlHashMap(a1 + 6336);
    CUtlMemoryBase::CUtlMemoryBase(a1 + 6420, 1328, 0, 0);
    *(_DWORD *)(a1 + 6436) = 0;
    CUtlMemoryBase::CUtlMemoryBase(a1 + 6440, 72, 0, 0);
    *(_DWORD *)(a1 + 6456) = 0;
    CUtlMemoryBase::CUtlMemoryBase(a1 + 6460, 12, 0, 0);
    *(_DWORD *)(a1 + 6476) = 0;
    CUserFriends::CUserFriends(a1 + 6480, a1, a1 + 36);
    CUserStats::CUserStats((CUserStats *)(a1 + 8376), (CBaseUser *)a1, (CCMInterface *)(a1 + 36));
    *(_DWORD *)(a1 + 8908) = -1;
    *(_DWORD *)(a1 + 8912) = 0;
    *(_DWORD *)(a1 + 8920) = 0;
    *(_DWORD *)(a1 + 8916) = -1;
    *(_DWORD *)(a1 + 8896) = -1;
    *(_DWORD *)(a1 + 8892) = -1;
    *(_DWORD *)(a1 + 8888) = -1;
    *(_DWORD *)(a1 + 8900) = 1;
    CUtlMemoryBase::CUtlMemoryBase(a1 + 8928, 24, 0, 0);
    *(_DWORD *)(a1 + 8904) = *(_DWORD *)(a1 + 8932);
    CUserAppInfo::CUserAppInfo(a1 + 8944, a1);
    CSTime::CSTime((CSTime *)(a1 + 9184));
    CProductBuilder::CProductBuilder((CProductBuilder *)(a1 + 9228), (CUser *)a1);
    CDepotBuilder::CDepotBuilder((CDepotBuilder *)(a1 + 9236), (CUser *)a1);
    CUserAppManager::CUserAppManager(a1 + 9244, a1);
    CClientAudio::CClientAudio(a1 + 9576, a1);
    CClientMusic::CClientMusic((CClientMusic *)(a1 + 9708), (CUser *)a1);
    CClientUnifiedMessages::CClientUnifiedMessages(a1 + 9912, a1);
    CClientStreamLauncher::CClientStreamLauncher((CClientStreamLauncher *)(a1 + 9984), (CUser *)a1);
    CClientStreamClient::CClientStreamClient((CClientStreamClient *)(a1 + 9992), (CUser *)a1);
    CClientDeviceAuth::CClientDeviceAuth(a1 + 10084, a1);
    CClientUserGameNotifications::CClientUserGameNotifications(a1 + 10152, a1);
    CClientVideo::CClientVideo((CClientVideo *)(a1 + 10236), (CUser *)a1);
    CClientSharedConnection::CClientSharedConnection(a1 + 10828, a1, a1 + 36);
    CShaderManager::CShaderManager((CShaderManager *)(a1 + 10996), (CUser *)a1);
    CCompatManager::CCompatManager((CCompatManager *)(a1 + 11624), (CUser *)a1);
    *(_DWORD *)(a1 + 12324) = -1;
    *(_DWORD *)(a1 + 12320) = -1;
    *(_DWORD *)(a1 + 12356) = -1;
    *(_DWORD *)(a1 + 12360) = 0;
    *(_DWORD *)(a1 + 12368) = 0;
    *(_DWORD *)(a1 + 12364) = -1;
    *(_DWORD *)(a1 + 12344) = -1;
    *(_DWORD *)(a1 + 12340) = -1;
    *(_DWORD *)(a1 + 12336) = -1;
    *(_DWORD *)(a1 + 12348) = 1;
    *(_DWORD *)(a1 + 12372) = 0;
    CUtlMemoryBase::CUtlMemoryBase(a1 + 12376, 24, 0, 0);
    *(_DWORD *)(a1 + 12352) = *(_DWORD *)(a1 + 12380);
    CUserRemoteStorage::CUserRemoteStorage((CUserRemoteStorage *)(a1 + 12392), (CUser *)a1);
    CUserScreenshots::CUserScreenshots((CUserScreenshots *)(a1 + 13636), (CUser *)a1);
    CUserShortcuts::CUserShortcuts((CUserShortcuts *)(a1 + 13964), (CUser *)a1);
    CUserWorkshopItems::CUserWorkshopItems((CUserWorkshopItems *)(a1 + 14236), (CUser *)a1);
    CUtlMemoryBase::CUtlMemoryBase(a1 + 14380, 16, 0, 0);
    *(_DWORD *)(a1 + 14396) = 0;
    CUtlHashMap<unsigned int, AppMinutesPlayed_t, CDefEquals<unsigned int>, HashFunctor<unsigned int>>::CUtlHashMap(a1 + 14400);
    *(_BYTE *)(a1 + 14481)  = 0;
    *(_DWORD *)(a1 + 16560) = -1;
    *(_DWORD *)(a1 + 16564) = 0;
    *(_DWORD *)(a1 + 16572) = 0;
    *(_DWORD *)(a1 + 16568) = -1;
    *(_DWORD *)(a1 + 16548) = -1;
    *(_DWORD *)(a1 + 16544) = -1;
    *(_DWORD *)(a1 + 16540) = -1;
    *(_DWORD *)(a1 + 16552) = 1;
    CUtlMemoryBase::CUtlMemoryBase(a1 + 16580, 28, 0, 0);
    *(_DWORD *)(a1 + 16556) = *(_DWORD *)(a1 + 16584);
    *(_BYTE *)(a1 + 16596)  = 0;
    *(_DWORD *)(a1 + 16604) = 0;
    *(_DWORD *)(a1 + 16600) = 0;
    *(_DWORD *)(a1 + 16612) = 0;
    *(_DWORD *)(a1 + 16608) = 0;
    *(_DWORD *)(a1 + 16620) = 0;
    *(_DWORD *)(a1 + 16616) = 0;
    *(_DWORD *)(a1 + 16644) = -1;
    *(_DWORD *)(a1 + 16648) = 0;
    *(_DWORD *)(a1 + 16656) = 0;
    *(_DWORD *)(a1 + 16652) = -1;
    *(_DWORD *)(a1 + 16632) = -1;
    *(_DWORD *)(a1 + 16628) = -1;
    *(_DWORD *)(a1 + 16624) = -1;
    *(_DWORD *)(a1 + 16636) = 1;
    CUtlMemoryBase::CUtlMemoryBase(a1 + 16664, 68, 0, 0);
    *(_DWORD *)(a1 + 16640) = *(_DWORD *)(a1 + 16668);
    *(_DWORD *)(a1 + 16700) = -1;
    *(_DWORD *)(a1 + 16704) = 0;
    *(_DWORD *)(a1 + 16712) = 0;
    *(_DWORD *)(a1 + 16708) = -1;
    *(_DWORD *)(a1 + 16688) = -1;
    *(_DWORD *)(a1 + 16684) = -1;
    *(_DWORD *)(a1 + 16680) = -1;
    *(_DWORD *)(a1 + 16692) = 1;
    CUtlMemoryBase::CUtlMemoryBase(a1 + 16720, 28, 0, 0);
    *(_DWORD *)(a1 + 16696) = *(_DWORD *)(a1 + 16724);
    CLock::CLock((CLock *)(a1 + 16744));
    *(_DWORD *)(a1 + 16780) = 0;
    CParentalSettingsManager::CParentalSettingsManager(a1 + 16784, 0);
    v17 = (unsigned int)CUser::ScheduledGetSignedParentalSettings;
    CScheduledFunction<CUser>::CScheduledFunction(
        (CBaseScheduledFunction *)(a1 + 17020),
        a1,
        (int)CUser::ScheduledGetSignedParentalSettings,
        0);
    *(_DWORD *)(a1 + 17064) = 1000;
    *(_DWORD *)(a1 + 17068) = 0;
    v18                     = (unsigned int)CUser::ScheduledGetOfflineLogonTicket;
    CScheduledFunction<CUser>::CScheduledFunction(
        (CBaseScheduledFunction *)(a1 + 17072),
        a1,
        (int)CUser::ScheduledGetOfflineLogonTicket,
        0);
    *(_DWORD *)(a1 + 17116) = 1000;
    CUtlBuffer::CUtlBuffer(a1 + 17120, 0, 0, 0);
    CSiteLicenseClient::CSiteLicenseClient((CSiteLicenseClient *)(a1 + 17192), (CUser *)a1);
    *(_DWORD *)(a1 + 17180) = 0;
    *(_DWORD *)(a1 + 15508) = 0;
    *(_DWORD *)(a1 + 16536) = 0;
    *(_DWORD *)(a1 + 5881)  = 0;
    if (*(_DWORD *)(a1 + 12372))
        AssertMsgImplementation(
            "Assertion Failed: 0",
            0,
            "/Users/buildbot/buildslave/steam_rel_client_osx/build/src/public/tier1/utlrbtree.h",
            1755,
            0);
    else
        *(_DWORD *)(a1 + 12372) = CDefOps<unsigned int>::LessFunc;
    if (*(char **)(a1 + 5896) != "english")
    {
        (*(void(__cdecl **)(_DWORD *, _DWORD, _DWORD))(*g_pMemAllocSteam + 16))(
            g_pMemAllocSteam,
            *(_DWORD *)(a1 + 5896),
            0);
        v3 = (*(int(__cdecl **)(_DWORD *, signed int, const char *, signed int, _DWORD, _DWORD))(*g_pMemAllocSteam + 24))(
            g_pMemAllocSteam,
            8,
            "/Users/buildbot/buildslave/steam_rel_client_osx/build/src/public/tier1/utlstring.h",
            379,
            0,
            0);
        *(_DWORD *)(a1 + 5896) = v3;
        *(_DWORD *)(v3 + 4)    = &loc_687369;
        *(_DWORD *)v3          = 1818717797;
    }
    *(_BYTE *)(a1 + 5880)   = 0;
    *(_DWORD *)(a1 + 5888)  = 0;
    *(_DWORD *)(a1 + 5892)  = 0;
    *(_DWORD *)(a1 + 5936)  = 0;
    *(_DWORD *)(a1 + 6412)  = 0;
    *(_DWORD *)(a1 + 6416)  = 0;
    *(_DWORD *)(a1 + 2104)  = 0;
    *(_BYTE *)(a1 + 5980)   = 0;
    *(_DWORD *)(a1 + 5976)  = 0;
    *(_BYTE *)(a1 + 14480)  = 0;
    *(_DWORD *)(a1 + 14476) = 0;
    *(_DWORD *)(a1 + 6080)  = 1;
    *(_DWORD *)(a1 + 6072)  = 0;
    *(_DWORD *)(a1 + 6076)  = 0;
    *(_BYTE *)(a1 + 5940)   = 0;
    *(_DWORD *)(a1 + 6292)  = 0;
    *(_DWORD *)(a1 + 5972)  = 0;
    *(_DWORD *)(a1 + 16736) = 0;
    *(_DWORD *)(a1 + 16740) = 0;
    *(_BYTE *)(a1 + 16768)  = 0;
    *(_DWORD *)(a1 + 16772) = 0;
    *(_DWORD *)(a1 + 16776) = 0;
    *(_BYTE *)(a1 + 5970)   = 0;
    *(_WORD *)(a1 + 5968)   = 0;
    *(_DWORD *)(a1 + 5964)  = 0;
    *(_DWORD *)(a1 + 9180)  = -1;
    *(_DWORD *)(a1 + 9176)  = -1;
    *(_DWORD *)(a1 + 9192)  = 0;
    CSTime::SetFromServerTime((CSTime *)(a1 + 9184), -60000000LL);
    v6                      = ((*(int(__cdecl **)(int))(*(_DWORD *)g_pSteamEngine + 92))(g_pSteamEngine) << 24) | 0x100001;
    v5                      = 0;
    result                  = CCMInterface::SetSteamID((CCMInterface *)(a1 + 36), (CSteamID *)&v5);
    *(_DWORD *)(a1 + 5932)  = 0;
    *(_DWORD *)(a1 + 17168) = 0;
    *(_DWORD *)(a1 + 17172) = 0;
    *(_DWORD *)(a1 + 17176) = 0;
    *(_DWORD *)(a1 + 17012) = 0;
    *(_DWORD *)(a1 + 17016) = 0;
    *(_DWORD *)(a1 + 5920)  = 0;
    *(_DWORD *)(a1 + 5928)  = -1;
    *(_DWORD *)(a1 + 5924)  = -1;
    *(_DWORD *)(a1 + 17188) = 0;
    *(_DWORD *)(a1 + 17184) = 0;
    return result;
}