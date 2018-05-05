using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceSteamClient
{
    [ArgonCore.InterfaceImpl(Implements = "STEAMCLIENT_INTERFACE_VERSION", Name = "STEAMCLIENT_INTERFACE_VERSION002")]
    public class SteamClient002 : ArgonCore.IBaseInterface
    {
        public int UserId { get; set; }

        public int Test3(string a, int n)
        {
            return a.Length + n;
        }

        public string Test4(string a, string b)
        {
            return a + b;
        }
    }
}
