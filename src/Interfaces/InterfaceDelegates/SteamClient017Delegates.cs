using System;
using System.Runtime.InteropServices;

// Autogenerated @ 23/07/18
namespace InterfaceClient
{
    /// <summary>
    /// Exports the delegates for all interfaces that implement SteamClient017
    /// </summary>
    [ArgonCore.Interface.Delegate(Name = "SteamClient017")]
    class SteamClient017_Delegates
    {
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate int CreateSteamPipe(IntPtr _);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate bool ReleaseSteamPipe(IntPtr _, int pipe);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate int ConnectToGlobalUser(IntPtr _, int pipe);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate int CreateLocalUser(IntPtr _, ref System.Int32 pipe, uint account_type);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate void ReleaseUser(IntPtr _, int user, int pipe);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate IntPtr GetSteamUser(IntPtr _, int user, int pipe, string version);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate IntPtr GetSteamGameServer(IntPtr _, int user, int pipe, string version);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate void SetLocalIPBinding(IntPtr _, uint ip, uint port);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate IntPtr GetSteamFriends(IntPtr _, int user, int pipe, string version);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate IntPtr GetSteamUtils(IntPtr _, int pipe, string version);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate IntPtr GetSteamMatchmaking(IntPtr _, int user, int pipe, string version);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate IntPtr GetSteamMatchmakingServers(IntPtr _, int user, int pipe, string version);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate IntPtr GetSteamGenericInterface(IntPtr _, int user, int pipe, string version);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate IntPtr GetSteamUserStats(IntPtr _, int user, int pipe, string version);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate IntPtr GetSteamGameServerStats(IntPtr _, int user, int pipe, string version);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate IntPtr GetSteamApps(IntPtr _, int user, int pipe, string version);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate IntPtr GetSteamNetworking(IntPtr _, int user, int pipe, string version);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate IntPtr GetSteamRemoteStorage(IntPtr _, int user, int pipe, string version);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate IntPtr GetSteamScreenshots(IntPtr _, int user, int pipe, string version);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate void RunFrame(IntPtr _);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate uint GetIPCCallCount(IntPtr _);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate void SetWarningMessageHook(IntPtr _, IntPtr function);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate bool ShutdownIfAllPipesClosed(IntPtr _);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate IntPtr GetSteamHTTP(IntPtr _, int user, int pipe, string version);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate IntPtr GetSteamUnifiedMessages(IntPtr _, int user, int pipe, string version);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate IntPtr GetSteamController(IntPtr _, int user, int pipe, string version);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate IntPtr GetSteamUGC(IntPtr _, int user, int pipe, string version);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate IntPtr GetSteamAppList(IntPtr _, int user, int pipe, string version);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate IntPtr GetSteamMusic(IntPtr _, int user, int pipe, string version);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate IntPtr GetSteamMusicRemote(IntPtr _, int user, int pipe, string version);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate IntPtr GetSteamHTMLSurface(IntPtr _, int user, int pipe, string version);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate void Set_SteamAPI_CPostAPIResultInProcess(IntPtr _, IntPtr function);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate void Remove_SteamAPI_CPostAPIResultInProcess(IntPtr _, IntPtr function);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate void Set_SteamAPI_CCheckCallbackRegisteredInProcess(IntPtr _, IntPtr function);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate IntPtr GetSteamInventory(IntPtr _, int user, int pipe, string version);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate IntPtr GetSteamVideo(IntPtr _, int user, int pipe, string version);
    }
}
