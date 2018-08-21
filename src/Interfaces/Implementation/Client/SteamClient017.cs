using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

using NamedPipeWrapper;

using Client;
using Core;

namespace InterfaceClient
{
    [Core.Interface.Impl(Name = "SteamClient017", ServerMapped = false)]
    public class SteamClient017 : Core.Interface.IBaseInterface
    {
        static Logger Log { get; set; } = new Logger("SteamClient.Client017");

        public int CreateSteamPipe(IntPtr _)
        {
            Log.WriteLine("CreateSteamPipe");
            var new_pipe = ClientPipe.CreatePipe();

            Client.Client.TryFindAppId(new_pipe);

            return new_pipe;
        }

        public bool ReleaseSteamPipe(IntPtr _, int pipe)
        {
            Log.WriteLine("WARNING: ReleaseSteamPipe NOT IMPLEMENTED");
            return true;
        }

        // TODO: Deal with global users
        // In theory as a result of our user model any process can connect
        // to any user that they wish too, they just need to know which one
        // is the "global user". 

        // (This can be implemented as a handle = global user)
        // or we could provide a method by which a user can choose a user to connect too...
        // TODO: Deal with global users
        public int ConnectToGlobalUser(IntPtr _, int pipe)
        {
            Log.WriteLine("ConnectToGlobalUser");
            return 0;
        }

        public int CreateLocalUser(IntPtr _, ref int pipe, uint account_type)
        {
            Log.WriteLine("CreateLocalUser {0} {1}", pipe, account_type);

            if (pipe == 0) pipe = CreateSteamPipe(IntPtr.Zero);

            var user = Client.Client.CreateNewClient(pipe);

            return user;
        }

        public void ReleaseUser(IntPtr _, int user, int pipe)
        {
            if (pipe == 0 || user == 0) return;

            Client.Client.ReleaseClient(user, pipe);

            Log.WriteLine("ReleaseUser");
            return;
        }

        IntPtr CreateInterface(int pipe, int user, string version)
        {
            try
            {
                return Client.Client.GetClient(user).CreateInterface(pipe, version);
            }
            catch (Exception e)
            {
                Log.WriteLine("Exception in CreateInterface \"{0}\"", e.Message);
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
            Log.WriteLine("SetLocalIPBinding");
            return;
        }

        public IntPtr GetSteamFriends(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetSteamFriends");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetSteamUtils(IntPtr _, int pipe, string version)
        {
            Log.WriteLine("GetSteamUtils");
            return Client.Client.CreateInterfaceNoUser(pipe, version);
        }

        public IntPtr GetSteamMatchmaking(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetSteamMatchmaking");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetSteamMatchmakingServers(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetSteamMatchmakingServers");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetSteamGenericInterface(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetSteamGenericInterface");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetSteamUserStats(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetSteamUserStats");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetSteamGameServerStats(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetSteamGameServerStats");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetSteamApps(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetSteamApps");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetSteamNetworking(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetSteamNetworking");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetSteamRemoteStorage(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetSteamRemoteStorage");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetSteamScreenshots(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetSteamScreenshots");
            return CreateInterface(pipe, user, version);
        }

        public void RunFrame(IntPtr _)
        {
            // Pipes automatically run ipc threads so...
            // TODO: is this type of behaviour allowable?
            Log.WriteLine("RunFrame");
        }

        public uint GetIPCCallCount(IntPtr _)
        {
            Log.WriteLine("GetIPCCallCount");
            return Client.ClientPipe.GetIpcCallCount();
        }

        public void SetWarningMessageHook(IntPtr _, IntPtr function)
        {
            Log.WriteLine("SetWarningMessageHook");
            return;
        }

        public bool ShutdownIfAllPipesClosed(IntPtr _)
        {
            // TODO: what does this actually do?
            Log.WriteLine("ShutdownIfAllPipesClosed");
            return false;
        }

        public IntPtr GetSteamHTTP(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetSteamHTTP");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetSteamUnifiedMessages(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetSteamUnifiedMessages");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetSteamController(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetSteamController");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetSteamUGC(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetSteamUGC");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetSteamAppList(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetSteamAppList");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetSteamMusic(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetSteamMusic");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetSteamMusicRemote(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetSteamMusicRemote");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetSteamHTMLSurface(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetSteamHTMLSurface");
            return CreateInterface(pipe, user, version);
        }

        public void Set_SteamAPI_CPostAPIResultInProcess(IntPtr _, IntPtr function)
        {
            Log.WriteLine("Set_SteamAPI_CPostAPIResultInProcess");
        }

        public void Remove_SteamAPI_CPostAPIResultInProcess(IntPtr _, IntPtr function)
        {
            Log.WriteLine("Remove_SteamAPI_CPostAPIResultInProcess");
        }

        public void Set_SteamAPI_CCheckCallbackRegisteredInProcess(IntPtr _, IntPtr function)
        {
            Log.WriteLine("Set_SteamAPI_CCheckCallbackRegisteredInProcess");
        }

        public IntPtr GetSteamInventory(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetSteamInventory");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetSteamVideo(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetSteamVideo");
            return CreateInterface(pipe, user, version);
        }
    }
}
