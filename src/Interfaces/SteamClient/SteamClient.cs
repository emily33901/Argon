using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceSteamClient
{
    [ArgonCore.InterfaceImpl(Implements = "STEAMCLIENT_INTERFACE_VERSION", Name = "STEAMCLIENT_INTERFACE_VERSION001")]
    public class SteamClient : ArgonCore.IBaseInterface
    {
        public int UserId { get; set; }

        public int Test(string a, int n)
        {
            return a.Length + n;
        }
    }
}
