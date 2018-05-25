using System;
using System.Runtime.InteropServices;

// Autogenerated @ 24/05/18
namespace InterfaceClient
{
    /// <summary>
    /// Exports the delegates for all interfaces that implement SteamClient
    /// </summary>
    [ArgonCore.Interface.Delegate(Name = "SteamClient")]
    class SteamClient_Delegates
    {
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate int CreateSteamPipeDelegate(IntPtr _);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate bool ReleaseSteamPipeDelegate(IntPtr _, int pipe);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate int ConnectToGlobalUserDelegate(IntPtr _, int pipe);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate int CreateLocalUserDelegate(IntPtr _, ref System.Int32 pipe, uint account_type);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate void ReleaseUserDelegate(IntPtr _, int user, int pipe);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate IntPtr GetSteamUserDelegate(IntPtr _, int user, int pipe, string version);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate IntPtr GetSteamGameServerDelegate(IntPtr _, int user, int pipe, string version);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate void SetLocalIPBindingDelegate(IntPtr _, uint ip, uint port);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate IntPtr GetSteamFriendsDelegate(IntPtr _, int user, int pipe, string version);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate IntPtr GetSteamUtilsDelegate(IntPtr _, int pipe, string version);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate IntPtr GetSteamMatchmakingDelegate(IntPtr _, int user, int pipe, string version);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate IntPtr GetSteamMatchmakingServersDelegate(IntPtr _, int user, int pipe, string version);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate IntPtr GetSteamGenericInterfaceDelegate(IntPtr _, int user, int pipe, string version);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate IntPtr GetSteamUserStatsDelegate(IntPtr _, int user, int pipe, string version);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate IntPtr GetSteamGameServerStatsDelegate(IntPtr _, int user, int pipe, string version);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate IntPtr GetSteamAppsDelegate(IntPtr _, int user, int pipe, string version);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate IntPtr GetSteamNetworkingDelegate(IntPtr _, int user, int pipe, string version);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate IntPtr GetSteamRemoteStorageDelegate(IntPtr _, int user, int pipe, string version);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate IntPtr GetSteamScreenshotsDelegate(IntPtr _, int user, int pipe, string version);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate void RunFrameDelegate(IntPtr _);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate uint GetIPCCallCountDelegate(IntPtr _);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate void SetWarningMessageHookDelegate(IntPtr _, IntPtr function);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate bool ShutdownIfAllPipesClosedDelegate(IntPtr _);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate IntPtr GetSteamHTTPDelegate(IntPtr _, int user, int pipe, string version);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate IntPtr GetSteamUnifiedMessagesDelegate(IntPtr _, int user, int pipe, string version);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate IntPtr GetSteamControllerDelegate(IntPtr _, int user, int pipe, string version);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate IntPtr GetSteamUGCDelegate(IntPtr _, int user, int pipe, string version);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate IntPtr GetSteamAppListDelegate(IntPtr _, int user, int pipe, string version);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate IntPtr GetSteamMusicDelegate(IntPtr _, int user, int pipe, string version);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate IntPtr GetSteamMusicRemoteDelegate(IntPtr _, int user, int pipe, string version);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate IntPtr GetSteamHTMLSurfaceDelegate(IntPtr _, int user, int pipe, string version);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate void Set_SteamAPI_CPostAPIResultInProcessDelegate(IntPtr _, IntPtr function);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate void Remove_SteamAPI_CPostAPIResultInProcessDelegate(IntPtr _, IntPtr function);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate void Set_SteamAPI_CCheckCallbackRegisteredInProcessDelegate(IntPtr _, IntPtr function);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate IntPtr GetSteamInventoryDelegate(IntPtr _, int user, int pipe, string version);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate IntPtr GetSteamVideoDelegate(IntPtr _, int user, int pipe, string version);
    }
}
