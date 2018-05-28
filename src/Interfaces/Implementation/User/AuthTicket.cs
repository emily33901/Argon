using System;


namespace InterfaceUser
{
    public class AuthTicket
    {
        public uint handle;
        public byte[] ticket;
        public bool cancelled = false;
    }
}