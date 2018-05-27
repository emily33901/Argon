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
        public int CreateSteamPipe(IntPtr _)
        {
            Console.WriteLine("CreateSteamPipe");
            return ArgonCore.IPC.ClientPipe.CreatePipe();
        }

        public bool ReleaseSteamPipe(IntPtr _, int pipe)
        {
            Console.WriteLine("WARNING: ReleaseSteamPipe NOT IMPLEMENTED");
            return true;
        }

        // TODO: Deal with global users
        public int ConnectToGlobalUser(IntPtr _, int pipe)
        {
            Console.WriteLine("ConnectToGlobalUser");
            return 0;
        }

        public int CreateLocalUser(IntPtr _, ref int pipe, uint account_type)
        {
            Console.WriteLine("CreateLocalUser {0} {1}", pipe, account_type);

            var user = Client.CreateNewClient(pipe);

            return user;
        }

        public void ReleaseUser(IntPtr _, int user, int pipe)
        {
            // TODO: remove users
            Console.WriteLine("ReleaseUser");
            return;
        }

        IntPtr CreateInterface(int pipe, int user, string version)
        {
            try
            {
                return Client.GetClient(user).CreateInterface(pipe, version);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in CreateInterface \"{0}\"", e.Message);
                return IntPtr.Zero;
            }
        }

        public IntPtr GetSteamUser(IntPtr _, int user, int pipe, string version)
        {
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetSteamGameServer(IntPtr _, int user, int pipe, string version)
        {
            return CreateInterface(pipe, user, version);
        }

        public void SetLocalIPBinding(IntPtr _, uint ip, uint port)
        {
            Console.WriteLine("SetLocalIPBinding");
            return;
        }

        public IntPtr GetSteamFriends(IntPtr _, int user, int pipe, string version)
        {
            Console.WriteLine("GetSteamFriends");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetSteamUtils(IntPtr _, int pipe, string version)
        {
            Console.WriteLine("GetSteamUtils");
            return Client.CreateInterfaceNoUser(pipe, version);
        }

        public IntPtr GetSteamMatchmaking(IntPtr _, int user, int pipe, string version)
        {
            Console.WriteLine("GetSteamMatchmaking");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetSteamMatchmakingServers(IntPtr _, int user, int pipe, string version)
        {
            Console.WriteLine("GetSteamMatchmakingServers");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetSteamGenericInterface(IntPtr _, int user, int pipe, string version)
        {
            Console.WriteLine("GetSteamGenericInterface");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetSteamUserStats(IntPtr _, int user, int pipe, string version)
        {
            Console.WriteLine("GetSteamUserStats");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetSteamGameServerStats(IntPtr _, int user, int pipe, string version)
        {
            Console.WriteLine("GetSteamGameServerStats");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetSteamApps(IntPtr _, int user, int pipe, string version)
        {
            Console.WriteLine("GetSteamApps");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetSteamNetworking(IntPtr _, int user, int pipe, string version)
        {
            Console.WriteLine("GetSteamNetworking");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetSteamRemoteStorage(IntPtr _, int user, int pipe, string version)
        {
            Console.WriteLine("GetSteamRemoteStorage");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetSteamScreenshots(IntPtr _, int user, int pipe, string version)
        {
            Console.WriteLine("GetSteamScreenshots");
            return CreateInterface(pipe, user, version);
        }

        public void RunFrame(IntPtr _)
        {
            // Pipes automatically run ipc threads so...
            // TODO: is this type of behaviour allowable?
            Console.WriteLine("RunFrame");
        }

        public uint GetIPCCallCount(IntPtr _)
        {
            // TODO: actually keep track of these in the future
            Console.WriteLine("GetIPCCallCount");
            return 1;
        }

        public void SetWarningMessageHook(IntPtr _, IntPtr function)
        {
            Console.WriteLine("SetWarningMessageHook");
            return;
        }

        public bool ShutdownIfAllPipesClosed(IntPtr _)
        {
            // TODO: what does this actually do?
            Console.WriteLine("ShutdownIfAllPipesClosed");
            return false;
        }

        public IntPtr GetSteamHTTP(IntPtr _, int user, int pipe, string version)
        {
            Console.WriteLine("GetSteamHTTP");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetSteamUnifiedMessages(IntPtr _, int user, int pipe, string version)
        {
            Console.WriteLine("GetSteamUnifiedMessages");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetSteamController(IntPtr _, int user, int pipe, string version)
        {
            Console.WriteLine("GetSteamController");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetSteamUGC(IntPtr _, int user, int pipe, string version)
        {
            Console.WriteLine("GetSteamUGC");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetSteamAppList(IntPtr _, int user, int pipe, string version)
        {
            Console.WriteLine("GetSteamAppList");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetSteamMusic(IntPtr _, int user, int pipe, string version)
        {
            Console.WriteLine("GetSteamMusic");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetSteamMusicRemote(IntPtr _, int user, int pipe, string version)
        {
            Console.WriteLine("GetSteamMusicRemote");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetSteamHTMLSurface(IntPtr _, int user, int pipe, string version)
        {
            Console.WriteLine("GetSteamHTMLSurface");
            return CreateInterface(pipe, user, version);
        }

        public void Set_SteamAPI_CPostAPIResultInProcess(IntPtr _, IntPtr function)
        {
            Console.WriteLine("Set_SteamAPI_CPostAPIResultInProcess");
        }

        public void Remove_SteamAPI_CPostAPIResultInProcess(IntPtr _, IntPtr function)
        {
            Console.WriteLine("Remove_SteamAPI_CPostAPIResultInProcess");
        }

        public void Set_SteamAPI_CCheckCallbackRegisteredInProcess(IntPtr _, IntPtr function)
        {
            Console.WriteLine("Set_SteamAPI_CCheckCallbackRegisteredInProcess");
        }

        public IntPtr GetSteamInventory(IntPtr _, int user, int pipe, string version)
        {
            Console.WriteLine("GetSteamInventory");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetSteamVideo(IntPtr _, int user, int pipe, string version)
        {
            Console.WriteLine("GetSteamVideo");
            return CreateInterface(pipe, user, version);
        }
    }
}
