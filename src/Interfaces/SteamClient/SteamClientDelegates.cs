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
            [MarshalAs(UnmanagedType.LPStr)] string a,
            int n);
    }
}
