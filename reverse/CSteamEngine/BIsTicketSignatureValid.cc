int __cdecl CSteamEngine::BIsTicketSignatureValid(CSteamEngine *this, const void *pBlob, unsigned int cbBlob)
{
    int  v3; // eax@1
    int  v4; // eax@1
    char v6; // [sp+10h] [bp-18h]@1

    CAppOwnershipTicket::CAppOwnershipTicket((CAppOwnershipTicket *)&v6, pBlob, cbBlob);

    // Call to CSteamEngine::GetConnectedUniverse(void)
    v3 = (*(int(__cdecl **)(CSteamEngine *))(*(_DWORD *)this + 92))(this);

    // Get the publickey for system from the key manager
    // These are hardcoded into the steamclient binaries
    v4 = CClientKeyMgr::GetPublicKey((int)&g_ClientKeyMgr, v3, "System");

    // Check the ticket against the public key
    return (unsigned __int8)CAppOwnershipTicket::BIsTicketSignatureValid((CAppOwnershipTicket *)&v6, v4);
}