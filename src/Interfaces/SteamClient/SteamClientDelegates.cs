using System;
using System.Runtime.InteropServices;

namespace InterfaceSteamClient
{
    /// <summary>
    /// Exports the delegates related to the <see cref="SteamClient"/> interface
    /// </summary>
    [ArgonCore.InterfaceDelegate(Name = "STEAMCLIENT_INTERFACE_VERSION")]
    public class SteamClientDelegates
    {
        public delegate int TestDelegate(
            string a,
            int n);
        public delegate string Test2Delegate(
            string a,
            string b);
    }
}
