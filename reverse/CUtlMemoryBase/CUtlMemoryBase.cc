int __cdecl CUtlMemoryBase::CUtlMemoryBase(CUtlMemoryBase *this, int element_size, int grow_size, int initial_size)
{
    int result; // eax@5

    this->element_size = element_size;
    this->memory       = 0;
    this->initial_size = initial_size;
    this->grow_size    = grow_size;
    if (!element_size)
        AssertMsgImplementation(
            "Assertion Failed: m_unSizeOfElements > 0",
            0,
            "/Users/buildbot/buildslave/steam_rel_client_osx/build/src/tier1/utlmemory.cpp",
            74,
            0);
    if (grow_size < 0)
        AssertMsgImplementation(
            "Assertion Failed: nGrowSize >= 0",
            0,
            "/Users/buildbot/buildslave/steam_rel_client_osx/build/src/tier1/utlmemory.cpp",
            75,
            0);
    result = this->initial_size;
    if (result)
    {
        result = (*(int(__cdecl **)(_DWORD *, int, const char *, signed int, _DWORD, _DWORD))(*g_pMemAllocSteam + 24))(
            g_pMemAllocSteam,
            this->element_size * result,
            "/Users/buildbot/buildslave/steam_rel_client_osx/build/src/tier1/utlmemory.cpp",
            79,
            0,
            0);
        this->memory = result;
    }
    return result;
}