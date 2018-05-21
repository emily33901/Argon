using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

using NamedPipeWrapper;

using ArgonCore.Client;

namespace InterfaceClient
{
    [ArgonCore.Interface.Impl(Name = "SteamClient017", Implements = "SteamClient", ServerMapped = false)]
    public class SteamClient017 : ArgonCore.Interface.IBaseInterface
    {
        public uint CreateSteamPipe()
        {
            // In our implementation, the pipe handle is essentially irrelevent
            Console.WriteLine("CreateSteamPipe");
            return 1;
        }

        public bool ReleaseSteamPipe(uint pipe)
        {
            Console.WriteLine("ReleaseSteamPipe");
            return true;
        }

        // TODO: Deal with global users
        public uint ConnectToGlobalUser(uint pipe)
        {
            Console.WriteLine("ConnectToGlobalUser");
            return 1;
        }

        public uint CreateLocalUser(ref uint user, uint account_type)
        {
            Console.WriteLine("CreateLocalUser {0} {1}", user, account_type);

            user = Client.CreateNewClient();

            return user;
        }

        public void ReleaseUser(uint user, uint pipe)
        {
            // TODO: remove users
            Console.WriteLine("ReleaseUser");
            return;
        }

        IntPtr CreateInterface(uint user, string version)
        {
            try
            {
                return Client.ActiveClients[user].CreateInterface(version);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in CreateInterface \"{0}\"", e.Message);
                return IntPtr.Zero;
            }
        }

        public IntPtr GetSteamUser(uint user, uint pipe, string version)
        {
            return CreateInterface(user, version);
        }

        public IntPtr GetSteamGameServer(uint user, uint pipe, string version)
        {
            return CreateInterface(user, version);
        }

        public void SetLocalIPBinding(uint ip, uint port)
        {
            Console.WriteLine("SetLocalIPBinding");
            return;
        }

        public IntPtr GetSteamFriends(uint user, uint pipe, string version)
        {
            Console.WriteLine("GetSteamFriends");
            return IntPtr.Zero;
        }

        public IntPtr GetSteamUtils(uint pipe, string version)
        {
            Console.WriteLine("GetSteamUtils");
            return Client.CreateInterfaceNoUser(version);
        }

        public IntPtr GetSteamMatchmaking(uint user, IntPtr pipe, string version)
        {
            Console.WriteLine("GetSteamMatchmaking");
            return CreateInterface(user, version);
        }

        public IntPtr GetSteamMatchmakingServers(uint user, IntPtr pipe, string version)
        {
            Console.WriteLine("GetSteamMatchmakingServers");
            return CreateInterface(user, version);
        }

        public IntPtr GetSteamGenericInterface(uint user, IntPtr pipe, string version)
        {
            Console.WriteLine("GetSteamGenericInterface");
            return CreateInterface(user, version);
        }

        public IntPtr GetSteamUserStats(uint user, IntPtr pipe, string version)
        {
            Console.WriteLine("GetSteamUserStats");
            return CreateInterface(user, version);
        }

        public IntPtr GetSteamGameServerStats(uint user, IntPtr pipe, string version)
        {
            Console.WriteLine("GetSteamGameServerStats");
            return CreateInterface(user, version);
        }

        public IntPtr GetSteamApps(uint user, IntPtr pipe, string version)
        {
            Console.WriteLine("GetSteamApps");
            return CreateInterface(user, version);
        }

        public IntPtr GetSteamNetworking(uint user, IntPtr pipe, string version)
        {
            Console.WriteLine("GetSteamNetworking");
            return CreateInterface(user, version);
        }

        public IntPtr GetSteamRemoteStorage(uint user, IntPtr pipe, string version)
        {
            Console.WriteLine("GetSteamRemoteStorage");
            return CreateInterface(user, version);
        }

        public IntPtr GetSteamScreenshots(uint user, IntPtr pipe, string version)
        {
            Console.WriteLine("GetSteamScreenshots");
            return CreateInterface(user, version);
        }

        public void RunFrame()
        {
            // Pipes automatically run ipc threads so...
            // TODO: is this type of behaviour allowed or do applications EXPECT
            // Callbacks to happen when runframe is called
            Console.WriteLine("RunFrame");
        }

        public uint GetIPCCallCount()
        {
            // TODO: actually keep track of these in the future
            Console.WriteLine("GetIPCCallCount");
            return 1;
        }

        public void SetWarningMessageHook(IntPtr function)
        {
            Console.WriteLine("SetWarningMessageHook");
            return;
        }

        public bool ShutdownIfAllPipesClosed()
        {
            // TODO: what does this actually do?
            Console.WriteLine("ShutdownIfAllPipesClosed");
            return false;
        }

        public IntPtr GetSteamHTTP(uint user, IntPtr pipe, string version)
        {
            Console.WriteLine("GetSteamHTTP");
            return CreateInterface(user, version);
        }

        public IntPtr GetSteamUnifiedMessages(uint user, IntPtr pipe, string version)
        {
            Console.WriteLine("GetSteamUnifiedMessages");
            return CreateInterface(user, version);
        }

        public IntPtr GetSteamController(uint user, IntPtr pipe, string version)
        {
            Console.WriteLine("GetSteamController");
            return CreateInterface(user, version);
        }

        public IntPtr GetSteamUGC(uint user, IntPtr pipe, string version)
        {
            Console.WriteLine("GetSteamUGC");
            return CreateInterface(user, version);
        }

        public IntPtr GetSteamAppList(uint user, IntPtr pipe, string version)
        {
            Console.WriteLine("GetSteamAppList");
            return CreateInterface(user, version);
        }

        public IntPtr GetSteamMusic(uint user, IntPtr pipe, string version)
        {
            Console.WriteLine("GetSteamMusic");
            return CreateInterface(user, version);
        }

        public IntPtr GetSteamMusicRemote(uint user, IntPtr pipe, string version)
        {
            Console.WriteLine("GetSteamMusicRemote");
            return CreateInterface(user, version);
        }

        public IntPtr GetSteamHTMLSurface(uint user, IntPtr pipe, string version)
        {
            Console.WriteLine("GetSteamHTMLSurface");
            return CreateInterface(user, version);
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

        public IntPtr GetSteamInventory(uint user, IntPtr pipe, string version)
        {
            Console.WriteLine("GetSteamInventory");
            return CreateInterface(user, version);
        }

        public IntPtr GetSteamVideo(uint user, IntPtr pipe, string version)
        {
            Console.WriteLine("GetSteamVideo");
            return CreateInterface(user, version);
        }
    }
}
