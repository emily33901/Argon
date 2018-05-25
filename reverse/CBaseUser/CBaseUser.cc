int __cdecl CBaseUser::CBaseUser(int a1, int a2)
{
    CClientInventory *v3; // edi@1
    CGameStats *      v4; // edi@3
    CBaseUser *       v5; // ST04_4@3
    CBaseUser *       v6; // ST04_4@3

    *(_DWORD *)a1 = off_106F9D8;
    CInternalCallback<CBaseUser, SteamServersConnected_t>::CInternalCallback(
        (CInternalCallbackBase *)(a1 + 4),
        a2,
        a1,
        (int)CBaseUser::OnSteamServersConnected,
        0);
    KeyValuesAD::KeyValuesAD(a1 + 28, "AppEvents");
    *(_DWORD *)(a1 + 32) = a2;
    CCMInterface::CCMInterface(a1 + 36);
    *(_DWORD *)(a1 + 2100) = 0;
    *(_DWORD *)(a1 + 2096) = 0;
    *(_DWORD *)(a1 + 2128) = -1;
    *(_DWORD *)(a1 + 2132) = 0;
    *(_DWORD *)(a1 + 2140) = 0;
    *(_DWORD *)(a1 + 2136) = -1;
    *(_DWORD *)(a1 + 2116) = -1;
    *(_DWORD *)(a1 + 2112) = -1;
    *(_DWORD *)(a1 + 2108) = -1;
    *(_DWORD *)(a1 + 2120) = 1;
    CUtlMemoryBase::CUtlMemoryBase(a1 + 2148, 24, 0, 0);
    *(_DWORD *)(a1 + 2124) = *(_DWORD *)(a1 + 2152);
    CUtlMemoryBase::CUtlMemoryBase(a1 + 2164, 8, 0, 0);
    *(_DWORD *)(a1 + 2180) = 0;
    *(_DWORD *)(a1 + 2184) = *(&dword_E5F668 + 521892) + 8;
    *(_DWORD *)(a1 + 2188) = a1;
    CClientNetworkingAPI::CClientNetworkingAPI((CClientNetworkingAPI *)(a1 + 2192), (CBaseUser *)a1);
    *(_DWORD *)(a1 + 2488) = 0;
    CConfigStore::CConfigStore(a1 + 2492, a1);
    CClientGameCoordinator::CClientGameCoordinator(a1 + 3608, a1);
    CClientHTTP::CClientHTTP((CClientHTTP *)(a1 + 3676), (CBaseUser *)a1);
    CClientUGC::CClientUGC((CClientUGC *)(a1 + 3804), (CBaseUser *)a1);
    *(_DWORD *)(a1 + 4372) = -1;
    *(_DWORD *)(a1 + 4376) = 0;
    *(_DWORD *)(a1 + 4384) = 0;
    *(_DWORD *)(a1 + 4380) = -1;
    *(_DWORD *)(a1 + 4360) = -1;
    *(_DWORD *)(a1 + 4356) = -1;
    *(_DWORD *)(a1 + 4352) = -1;
    *(_DWORD *)(a1 + 4364) = 1;
    CUtlMemoryBase::CUtlMemoryBase(a1 + 4392, 28, 0, 0);
    *(_DWORD *)(a1 + 4368) = *(_DWORD *)(a1 + 4396);
    *(_DWORD *)(a1 + 4408) = a1;
    v3                     = (CClientInventory *)operator new(0x294u);
    CClientInventory::CClientInventory(v3, (CBaseUser *)a1);
    *(_DWORD *)(a1 + 4412) = v3;
    CDepotDownloadMgr::CDepotDownloadMgr((CDepotDownloadMgr *)(a1 + 4416), (CBaseUser *)a1);
    CUtlMemoryBase::CUtlMemoryBase(a1 + 5344, 4, 0, 0);
    *(_DWORD *)(a1 + 5360) = 0;
    CUtlMemoryBase::CUtlMemoryBase(a1 + 5364, 92, 0, 0);
    *(_DWORD *)(a1 + 5380) = 0;
    CUtlMemoryBase::CUtlMemoryBase(a1 + 5384, 4, 0, 0);
    *(_DWORD *)(a1 + 5400) = 0;

    // This represents the vector of game tokens that have been recieved from the server
    CUtlMemoryBase::CUtlMemoryBase(a1 + 5404, 24, 0, 0);
    *(_DWORD *)(a1 + 5420) = 0;
    CUtlMemoryBase::CUtlMemoryBase(a1 + 5436, 4, 0, 0);
    *(_DWORD *)(a1 + 5452) = 0;
    if (!GetCIPThisBox())
    {
        CalcUnIPThisBox();
        SetDefaultPublicPrivateIPThisBox();
    }
    CCMInterface::Init((CCMInterface *)(a1 + 36), (CBaseUser *)a1);
    *(_DWORD *)(a1 + 5424) = 1;
    *(_DWORD *)(a1 + 5432) = 0;
    *(_DWORD *)(a1 + 5456) = 0;
    *(_DWORD *)(a1 + 5460) = 0;
    *(_DWORD *)(a1 + 5428) = 0;
    *(_DWORD *)(a1 + 5464) = 0;
    v4                     = (CGameStats *)operator new(0xA0u);
    CGameStats::CGameStats(v4, (CBaseUser *)a1);
    *(_DWORD *)(a1 + 3672) = v4;
    CBaseUser::UserBaseFolderInstallImage(v5);
    CreateDirRecursive((const char *)&unk_E9B394 + 2087568);
    CBaseUser::UserBaseFolderInstallImage(v6);
    return CreateDirRecursive((const char *)&unk_E9B394 + 2087568);