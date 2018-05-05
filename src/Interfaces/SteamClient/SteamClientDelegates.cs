namespace InterfaceSteamClient
{
    /// <summary>
    /// Exports the delegates related to the <see cref="SteamClient001"/> interface
    /// </summary>
    [ArgonCore.InterfaceDelegate(Name = "STEAMCLIENT_INTERFACE_VERSION")]
    public class SteamClientDelegates
    {
        public delegate int TestDelegate(string a, int n);
        public delegate string Test2Delegate(string a, string b);
        public delegate int Test3Delegate(string a, int n);
        public delegate string Test4Delegate(string a, string b);
    }
}
