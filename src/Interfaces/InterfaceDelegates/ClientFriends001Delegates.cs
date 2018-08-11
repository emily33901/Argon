using System;
using System.Runtime.InteropServices;

// Autogenerated @ 11/08/18
namespace InterfaceFriends
{
    /// <summary>
    /// Exports the delegates for all interfaces that implement CLIENTFRIENDS_INTERFACE_VERSION001
    /// </summary>
    [ArgonCore.Interface.Delegate(Name = "CLIENTFRIENDS_INTERFACE_VERSION001")]
    class CLIENTFRIENDS_INTERFACE_VERSION001_Delegates
    {
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate string GetPersonaName(IntPtr _);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate void SetPeronaName(IntPtr _, string name);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate int SetPersonaNameEx(IntPtr _, string name, bool send_cb);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate bool IsPersonaNameSet(IntPtr _);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate int GetPersonaState(IntPtr _);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate void SetPersonaState(IntPtr _, SteamKit2.EPersonaState state);
    }
}
