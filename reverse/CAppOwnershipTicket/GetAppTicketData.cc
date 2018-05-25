int __cdecl CAppOwnershipTicket::GetAppTicketData(CAppOwnershipTicket *this)
{
    int          v1;     // ecx@1
    unsigned int v2;     // edx@2
    int          result; // eax@6
    char         v4;     // [sp+18h] [bp-210h]@9
    char         v5;     // [sp+118h] [bp-110h]@9
    int          v6;     // [sp+218h] [bp-10h]@1

    v6 = __stack_chk_guard;
    v1 = this->pBlob;
    if (this->pBlob)
    {
        v2 = this->cbBlob;
        if (v2 >= 8 && *(_DWORD *)v1 <= v2 && *(_DWORD *)v1 >= 0x28u)
        {
            if (*(_DWORD *)(v1 + 4) >= 2u)
            {
                result = 0;
                if (v2 > 0x27)
                    result = this->pBlob;
                return result;
            }
            CDbgFmtMsg::CDbgFmtMsg((CDbgFmtMsg *)&v4, "Unknown Steam3 App Ownership Ticket Version: %d", *(_DWORD *)(v1 + 4));
            CDbgFmtMsg::CDbgFmtMsg((CDbgFmtMsg *)&v5, "Assertion Failed: %s", &v4);
            AssertMsgImplementation(
                &v5,
                0,
                "/Users/buildbot/buildslave/steam_rel_client_osx/build/src/common/appownershipticket.cpp",
                179,
                0);
        }
    }
    result = __stack_chk_guard;
    if (__stack_chk_guard == v6)
        result = 0;
    return result;
}