int __cdecl CAppOwnershipTicket::BIsTicketSignatureValid(CAppOwnershipTicket *this, int publickey)
{
    size_t *         v2; // eax@1
    size_t           v3; // eax@2
    char             v4; // bl@2
    unsigned __int64 v5; // kr00_8@2
    int              v6; // edi@2
    bool             v7; // dl@2
    unsigned int     v9; // [sp+18h] [bp-10h]@0

    v2 = (size_t *)CAppOwnershipTicket::GetAppTicketData(this);
    if (!v2)
        goto LABEL_11;
    v3 = *v2;
    v4 = 0;
    v5 = this->cbBlob - (unsigned __int64)v3;
    v6 = (v5 - 1) >> 32;
    v7 = (unsigned int)(v5 - 1) > 0x3E7F;
    if (v6)
        v7 = v6 != 0;
    if (!v7)
    {
        if (!publickey || (v4 = 1,
                           !(unsigned __int8)CCrypto::RSAVerifySignature(
                               (CCrypto *)this->pBlob,
                               v3,
                               (unsigned __int8 *)(this->pBlob + v3),
                               (const unsigned __int8 *)v5,
                               *(_DWORD *)(publickey + 4),
                               *(const unsigned __int8 **)(publickey + 8),
                               v9)))
        {
        LABEL_11:
            v4 = 0;
        }
    }
    return (unsigned __int8)v4;
}