using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace InterfaceSteamClient
{
    [ArgonCore.InterfaceImpl(Name = "SteamClient017", Implements = "SteamClient")]
    public class SteamClient017 : ArgonCore.IBaseInterface
    {
        public int UserId { get; set; }

        public IntPtr CreateSteamPipe()
        {
            Console.WriteLine("CreateSteamPipe");
            return IntPtr.Zero;
        }

        public bool ReleaseSteamPipe(IntPtr pipe)
        {
            Console.WriteLine("ReleaseSteamPipe");
            return false;
        }

        public IntPtr ConnectToGlobalUser(IntPtr pipe)
        {
            Console.WriteLine("ConnectToGlobalUser");
            return IntPtr.Zero;
        }

        public IntPtr CreateLocalUser(ref IntPtr user, uint account_type)
        {
            Console.WriteLine("CreateLocalUser {0} {1}", user, account_type);
            return IntPtr.Zero;
        }

        public void ReleaseUser(IntPtr user, IntPtr pipe)
        {
            Console.WriteLine("ReleaseUser");
            return;
        }

        public IntPtr GetSteamUser(IntPtr user, IntPtr pipe, string version)
        {
            Console.WriteLine("GetSteamUser");
            return IntPtr.Zero;
        }

        public IntPtr GetSteamGameServer(IntPtr user, IntPtr pipe, string version)
        {
            Console.WriteLine("GetSteamGameServer");
            return IntPtr.Zero;
        }

        public void SetLocalIPBinding(uint ip, uint port)
        {
            Console.WriteLine("SetLocalIPBinding");
            return;
        }

        public IntPtr GetSteamFriends(IntPtr user, IntPtr pipe, string version)
        {
            Console.WriteLine("GetSteamFriends");
            return IntPtr.Zero;
        }

        public IntPtr GetSteamUtils(IntPtr pipe, string version)
        {
            Console.WriteLine("GetSteamUtils");
            return IntPtr.Zero;
        }

        public IntPtr GetSteamMatchmaking(IntPtr user, IntPtr pipe, string version)
        {
            Console.WriteLine("GetSteamMatchmaking");
            return IntPtr.Zero;
        }

        public IntPtr GetSteamMatchmakingServers(IntPtr user, IntPtr pipe, string version)
        {
            Console.WriteLine("GetSteamMatchmakingServers");
            return IntPtr.Zero;
        }

        public  IntPtr GetSteamGenericInterface(IntPtr user, IntPtr pipe, string version)
        {
            Console.WriteLine("GetSteamGenericInterface");
            return IntPtr.Zero;
        }

        public IntPtr GetSteamUserStats(IntPtr user, IntPtr pipe, string version)
        {
            Console.WriteLine("GetSteamUserStats");
            return IntPtr.Zero;
        }

        public IntPtr GetSteamGameServerStats(IntPtr user, IntPtr pipe, string version)
        {
            Console.WriteLine("GetSteamGameServerStats");
            return IntPtr.Zero;
        }

        public IntPtr GetSteamApps(IntPtr user, IntPtr pipe, string version)
        {
            Console.WriteLine("GetSteamApps");
            return IntPtr.Zero;
        }

        public IntPtr GetSteamNetworking(IntPtr user, IntPtr pipe, string version)
        {
            Console.WriteLine("GetSteamNetworking");
            return IntPtr.Zero;
        }

        public IntPtr GetSteamRemoteStorage(IntPtr user, IntPtr pipe, string version)
        {
            Console.WriteLine("GetSteamRemoteStorage");
            return IntPtr.Zero;
        }

        public IntPtr GetSteamScreenshots(IntPtr user, IntPtr pipe, string version)
        {
            Console.WriteLine("GetSteamScreenshots");
            return IntPtr.Zero;
        }

        public void RunFrame()
        {
            Console.WriteLine("RunFrame");
        }

        public uint GetIPCCallCount()
        {
            Console.WriteLine("GetIPCCallCount");
            return 0;
        }

        public void SetWarningMessageHook(IntPtr function)
        {
            Console.WriteLine("SetWarningMessageHook");
            return;
        }

        public bool ShutdownIfAllPipesClosed()
        {
            Console.WriteLine("ShutdownIfAllPipesClosed");
            return false;
        }

        public IntPtr GetSteamHTTP(IntPtr user, IntPtr pipe, string version)
        {
            Console.WriteLine("GetSteamHTTP");
            return IntPtr.Zero;
        }

        public IntPtr GetSteamUnifiedMessages(IntPtr user, IntPtr pipe, string version)
        {
            Console.WriteLine("GetSteamUnifiedMessages");
            return IntPtr.Zero;
        }

        public IntPtr GetSteamController(IntPtr user, IntPtr pipe, string version)
        {
            Console.WriteLine("GetSteamController");
            return IntPtr.Zero;
        }

        public IntPtr GetSteamUGC(IntPtr user, IntPtr pipe, string version)
        {
            Console.WriteLine("GetSteamUGC");
            return IntPtr.Zero;
        }

        public IntPtr GetSteamAppList(IntPtr user, IntPtr pipe, string version)
        {
            Console.WriteLine("GetSteamAppList");
            return IntPtr.Zero;
        }

        public IntPtr GetSteamMusic(IntPtr user, IntPtr pipe, string version)
        {
            Console.WriteLine("GetSteamMusic");
            return IntPtr.Zero;
        }

        public IntPtr GetSteamMusicRemote(IntPtr user, IntPtr pipe, string version)
        {
            Console.WriteLine("GetSteamMusicRemote");
            return IntPtr.Zero;
        }

        public IntPtr GetSteamHTMLSurface(IntPtr user, IntPtr pipe, string version)
        {
            Console.WriteLine("GetSteamHTMLSurface");
            return IntPtr.Zero;
        }

        public void Set_SteamAPI_CPostAPIResultInProcess(IntPtr function)
        {
            Console.WriteLine("Set_SteamAPI_CPostAPIResultInProcess");

        }

        public void Remove_SteamAPI_CPostAPIResultInProcess(IntPtr function)
        {
            Console.WriteLine("Remove_SteamAPI_CPostAPIResultInProcess");

        }

        public void Set_SteamAPI_CCheckCallbackRegisteredInProcess(IntPtr function)
        {
            Console.WriteLine("Set_SteamAPI_CCheckCallbackRegisteredInProcess");

        }

        public IntPtr GetSteamInventory(IntPtr user, IntPtr pipe, string version)
        {
            Console.WriteLine("GetSteamInventory");
            return IntPtr.Zero;
        }

        public IntPtr GetSteamVideo(IntPtr user, IntPtr pipe, string version)
        {
            Console.WriteLine("GetSteamVideo");
            return IntPtr.Zero;
        }
    }
}
