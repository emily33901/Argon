using System;
using System.Collections.Generic;
using System.Text;

using Client;
using ArgonCore;
using ArgonCore.Interface;

namespace InterfaceClient
{
    [Impl(Name = "CLIENTENGINE_INTERFACE_VERSION005", ServerMapped = false)]
    class ClientEngine005 : IBaseInterface
    {
        static Logger Log { get; set; } = new Logger("SteamClient.Engine005");

        public int CreateClientPipe(IntPtr _)
        {
            Log.WriteLine("CreateSteamPipe");
            var new_pipe = ClientPipe.CreatePipe();

            Client.Client.TryFindAppId(new_pipe);

            return new_pipe;
        }

        public bool ReleaseClientPipe(IntPtr _, int pipe)
        {
            Log.WriteLine("WARNING: ReleaseClientPipe NOT IMPLEMENTED");
            return true;
        }

        public int CreateGlobalUser(IntPtr _, ref int pipe)
        {
            Log.WriteLine("CreateGlobalUser");
            return 0;
        }

        // TODO: Deal with global users (See ISteamClient017)
        public int ConnectToGlobalUser(IntPtr _, int pipe)
        {
            Log.WriteLine("ConnectToGlobalUser");
            return 0;
        }

        public int CreateLocalUser(IntPtr _, ref int pipe, uint account_type)
        {
            Log.WriteLine("CreateLocalUser {0} {1}", pipe, account_type);

            if (pipe == 0) pipe = ClientPipe.CreatePipe();

            var user = Client.Client.CreateNewClient(pipe);

            return user;
        }

        public void CreatePipeToLocalUser(IntPtr _, int user, ref int pipe)
        {
	    if(pipe == 0) pipe = ClientPipe.CreatePipe();

	    var c = Client.Client.GetClient(user);
	    c.ConnectPipeToClient(pipe);
        }

        public void ReleaseUser(IntPtr _, int user, int pipe)
        {
            // TODO: remove users
            Log.WriteLine("ReleaseUser");
            return;
        }

        public bool IsValidHSteamUserPipe(IntPtr _, int pipe, int user)
        {
            Log.WriteLine("IsValidHSteamUserPipe not implemented...");
            return true;
        }

        IntPtr CreateInterface(int pipe, int user, string version)
        {
            Log.WriteLine("CreateInterface {0}", version);
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

        public IntPtr GetClientUser(IntPtr _, int user, int pipe, string version)
        {
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetClientGameServer(IntPtr _, int user, int pipe, string version)
        {
            return CreateInterface(pipe, user, version);
        }

        public void SetLocalIPBinding(IntPtr _, uint ip, uint port)
        {
            Log.WriteLine("SetLocalIPBinding");
            return;
        }

        public string GetUniverseName(IntPtr _, uint universe)
        {
            return Enum.GetName(typeof(SteamKit2.EUniverse), universe);
        }

        public IntPtr GetClientFriends(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetClientFriends");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetClientUtils(IntPtr _, int pipe, string version)
        {
            Log.WriteLine("GetClientUtils");
            return Client.Client.CreateInterfaceNoUser(pipe, version);
        }

        public IntPtr GetClientBilling(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetClientBilling");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetClientMatchmaking(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetClientMatchmaking");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetClientApps(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetClientApps");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetClientMatchmakingServers(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetClientMatchmakingServers");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetClientGameSearch(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetClientGameSearch");
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
            // TODO: actually keep track of these in the future
            Log.WriteLine("GetIPCCallCount");
            return 1;
        }

        public IntPtr GetClientUserStats(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetClientUserStats");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetClientGameServerStats(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetClientGameServerStats");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetClientNetworking(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetClientNetworking");
            return CreateInterface(pipe, user, version);
        }
        public IntPtr GetClientRemoteStorage(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetClientRemoteStorage");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetClientScreenshots(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetClientScreenshots");
            return CreateInterface(pipe, user, version);
        }

        public void SetWarningMessageHook(IntPtr _, IntPtr function)
        {
            Log.WriteLine("SetWarningMessageHook");
            return;
        }

        public IntPtr GetClientGameCoordinator(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetClientGameCoordinator");
            return CreateInterface(pipe, user, version);
        }

        public void SetOverlayNotificationPosition(IntPtr _, uint position)
        {

        }

        public void SetOverlayNotificationInsert(IntPtr _, uint position)
        {

        }

        public bool HookScreenshots(IntPtr _, bool hook)
        {
            return false;
        }

        public bool IsOverlayEnabled(IntPtr _)
        {
            return false;
        }

        public bool GetAPICallResult(IntPtr _, int pipe, uint handle, ref IntPtr callback_buffer, int callback_size, int expected_callback, ref bool failed)
        {
            failed = true;
            return false;
        }

        public IntPtr GetClientProductBuilder(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetClientProductBuilder");
            return CreateInterface(pipe, user, version);
        }
        public IntPtr GetClientDepotBuilder(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetClientDepotBuilder");
            return CreateInterface(pipe, user, version);
        }
        public IntPtr GetClientNetworkDeviceManager(IntPtr _, int pipe, string version)
        {
            Log.WriteLine("GetClientNetworkDeviceManager");
            return Client.Client.CreateInterfaceNoUser(pipe, version);
        }

        public void ConCommandInit(IntPtr _, IntPtr __)
        {
        }

        public IntPtr GetClientAppManager(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetClientAppManager");
            return CreateInterface(pipe, user, version);
        }
        public IntPtr GetClientConfigStore(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetClientConfigStore");
            return CreateInterface(pipe, user, version);
        }

        public bool BOverlayNeedsPresent()
        {
            return false;
        }

        public IntPtr GetClientGameStats(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetClientGameStats");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetClientHTTP(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetClientHTTP");
            return CreateInterface(pipe, user, version);
        }

        public bool BShutdownIfAllPipesClosed()
        {
            return false;
        }

        public IntPtr GetClientAudio(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetClientAudio");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetClientMusic(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetClientMusic");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetClientUnifiedMessages(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetClientUnifiedMessages");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetClientController(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetClientController");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetClientParentalSettings(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetClientParentalSettings");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetClientStreamLauncher(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetClientStreamLauncher");
            return CreateInterface(pipe, user, version);
        }
        public IntPtr GetClientDeviceAuth(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetClientDeviceAuth");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetClientRemoteClientManager(IntPtr _, int pipe, string version)
        {
            Log.WriteLine("GetClientRemoteClientManager");
            return Client.Client.CreateInterfaceNoUser(pipe, version);
        }
        public IntPtr GetClientStreamClient(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetClientStreamClient");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetClientShortcuts(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetClientShortcuts");
            return CreateInterface(pipe, user, version);
        }
        public IntPtr GetClientUGC(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetClientUGC");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetClientInventory(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetClientInventory");
            return CreateInterface(pipe, user, version);
        }
        public IntPtr GetClientVR(IntPtr _, int pipe, string version)
        {
            Log.WriteLine("GetClientVR");
            return Client.Client.CreateInterfaceNoUser(pipe, version);
        }

        public IntPtr GetClientGameNotifications(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetClientGameNotifications");
            return CreateInterface(pipe, user, version);
        }
        public IntPtr GetClientHTMLSurface(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetClientHTMLSurface");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetClientVideo(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetClientVideo");
            return CreateInterface(pipe, user, version);
        }
        public IntPtr GetClientControllerSerialized(IntPtr _, int pipe, string version)
        {
            Log.WriteLine("GetClientControllerSerialized");
            return Client.Client.CreateInterfaceNoUser(pipe, version);
        }

        public IntPtr GetClientAppDisableUpdate(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetClientAppDisableUpdate");
            return CreateInterface(pipe, user, version);
        }

        public int Set_Client_API_CCheckCallbackRegisteredInProcess(IntPtr _, IntPtr callback)
        {
            return 0;
        }

        public IntPtr GetClientBluetoothManager(IntPtr _, int pipe, string version)
        {
            Log.WriteLine("GetClientAppDisableUpdate");
            return Client.Client.CreateInterfaceNoUser(pipe, version);
        }

        public IntPtr GetClientSharedConnection(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetClientSharedConnection");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetClientShader(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetClientShader");
            return CreateInterface(pipe, user, version);
        }

        public IntPtr GetClientNetworkingSocketsSerialized(IntPtr _, int user, int pipe, string version)
        {
            Log.WriteLine("GetClientNetworkingSocketsSerialized");
            return CreateInterface(pipe, user, version);
        }

        // TODO: if we are on unix there are 2 of these...
        public void Destructor_CSteamClient1(IntPtr _)
        {

        }

        public void GetIPCServerMap(IntPtr _)
        {

        }

        public void OnDebugTextArrived(IntPtr _, string text)
        {

        }

        public void OnThreadLocalRegistration(IntPtr _)
        {

        }

        public void OnThreadBuffersOverLimit(IntPtr _)
        {

        }

    }
}
