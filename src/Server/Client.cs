using System;
using System.Collections.Generic;
using System.Text;

using ArgonCore;
using ArgonCore.Interface;
using ArgonCore.IPC;
using SteamKit2;

namespace Server
{
    /// <summary>
    /// Represents a client on the server
    /// </summary>
    public class Client
    {
        public static object active_client_lock = new object();
        public static int next_client_id = 1;
        static Dictionary<int, Client> ActiveClients { get; set; } = new Dictionary<int, Client>();

        public static Queue<InternalCallbackMsg> PendingCallbacks { get; set; } = new Queue<InternalCallbackMsg>();

        // Lookup for pipeids to their appids
        // This maps the real serverside pipeid to its appid
        public static Dictionary<int, int> PipeAppId { get; set; } = new Dictionary<int, int>();

        // Used for internal handling of clients
        public int Id { get; private set; }

        public List<IBaseInterface> interfaces;

        public static List<IBaseInterface> NoUserInterface { get; set; } = new List<IBaseInterface>();

        // Steamkit helpers for this client
        public SteamClient SteamClient { get; private set; }
        public CallbackManager CallbackManager { get; set; }

        // Whether user is connected / running
        public bool Running { get; private set; }
        public bool Connected { get; private set; }

        // Logging instance for this client
        static Logger ClientLog { get; set; } = new Logger("Server.Client");
        Logger Log { get; set; }

        /// <summary>
        /// Create a new <see cref="Client"/> from the requested instance 
        /// </summary>
        Client()
        {
            interfaces = new List<IBaseInterface>();

            Loader.Load();

            lock (active_client_lock)
            {
                ActiveClients[next_client_id] = this;
                Id = next_client_id;

                next_client_id += 1;

                Log = new LoggerUid("Server.Client", Id);


                // create our steamclient instance
                SteamClient = new SteamClient();

                // create the callback manager which will route callbacks to function calls
                CallbackManager = new CallbackManager(SteamClient);

                // Setup our packet handler for all packets
                SteamClient.AddHandler(new PacketHandler(this));

                // Subscribe to some important callbacks
                CallbackManager.Subscribe<SteamClient.ConnectedCallback>(OnConnect);
                CallbackManager.Subscribe<SteamClient.DisconnectedCallback>(OnDisconnect);

                // Automatically try to connect...
                Connect();
            }
        }

        public static int CreateNewClient()
        {
            var c = new Client();

            return c.Id;
        }

        public void Release()
        {
            Disconnect();

            lock (active_client_lock)
            {
                ActiveClients.Remove(Id);
            }
        }

        public static Client GetClient(int id)
        {
            lock (active_client_lock)
                return ActiveClients[id];
        }

        public IBaseInterface CreateInterface(int pipe_id, string name)
        {
            // Server never needs delegates or contexts becuase it is just managing instances
            // within C#
            var impl = Context.FindImpl(name);
            var instance = (IBaseInterface)Context.CreateInterfaceInstance(impl);
            interfaces.Add(instance);

            instance.ClientId = Id;
            instance.InterfaceId = interfaces.Count - 1;
            instance.PipeId = pipe_id;
            instance.Implementation = impl;

            return instance;
        }

        public static IBaseInterface CreateInterfaceNoUser(int pipe_id, string name)
        {
            var impl = Context.FindImpl(name);
            var instance = (IBaseInterface)Context.CreateInterfaceInstance(impl);
            NoUserInterface.Add(instance);

            instance.PipeId = pipe_id;
            instance.Implementation = impl;

            return instance;
        }

        /// <summary>
        /// Handles connecting the user's client instance to steam.
        /// </summary>
        public void Connect()
        {
            Log.WriteLine("Attempting connection...");
            SteamClient.Connect();
            Running = true;
        }

        /// <summary>
        /// Handles connecting the user's client instance to steam.
        /// </summary>
        public void Disconnect()
        {
            Log.WriteLine("Disconnecting...");
            SteamClient.Disconnect();
            Running = false;
        }

        public void RunFrame()
        {
            // Dont wait indefinitely for new callbacks
            CallbackManager.RunWaitCallbacks(TimeSpan.FromSeconds(1));
        }

        public static void RunAllFrames()
        {
            lock (active_client_lock)
            {
                foreach (var c in ActiveClients)
                {
                    c.Value.RunFrame();
                }
            }
        }

        // Static event handlers
        public void OnConnect(SteamClient.ConnectedCallback cb)
        {
            Log.WriteLine("Connected [token: {0}]", SteamClient.SessionToken);

            Connected = true;
        }

        public void OnDisconnect(SteamClient.DisconnectedCallback cb)
        {
            Log.WriteLine("Disconnected (User Initiated: {0})", cb.UserInitiated);

            // SteamClientDisconnected_t packets are already sent...

            Connected = false;

            // Attempt to reconnect straight away
            Connect();
        }

        object CallSerializedFunction(int pipe_id, int interface_id, string name, object[] args)
        {
            if (interface_id != -1)
            {
                var iface = interfaces[interface_id];
                var mi = iface.Implementation.methods.Find(x => x.Name == name);

                if (mi != null)
                {
                    return mi.Invoke(interfaces[interface_id], args);
                }

                Log.WriteLine("Unable to find method \"{0}\" from interface \"{1}\"", name, iface.Implementation.name);

                return null;
            }

            // TODO: handle special server related functions
            switch (name)
            {
                case "CreateInterface": { return CreateInterface(pipe_id, (string)args[0]).InterfaceId; }
                case "SetAppId":
                    {
                        PipeAppId[pipe_id] = (int)args[0];
                        return null;
                    }
                case "Release":
                    {
                        Release();
                        return null;
                    }
                default:
                    Log.WriteLine("Method \"{0}\" is not defined for Interface -1", name);
                    break;
            }

            return null;
        }

        public static object CallSerializedFunction(int pipe_id, int client_id, int interface_id, string name, object[] args)
        {
            // The pipeid that comes in here is the real unique pipe_id, the message PipeId is the clientside unique pipeid

            if (client_id == -1 && interface_id == -1)
            {
                // TODO: handle special server related functions
                switch (name)
                {
                    case "CreateClient":
                        {
                            return CreateNewClient();
                        }
                    case "CreateInterface":
                        {
                            return CreateInterfaceNoUser(pipe_id, (string)args[0]).InterfaceId;
                        }
                    case "NextCallback":
                        {
                            if (PendingCallbacks.Count > 0)
                            {
                                return PendingCallbacks.Dequeue();
                            }

                            return null;
                        }
                }
                return null;
            }
            else if (client_id == -1 && interface_id != -1)
            {
                var iface = NoUserInterface[interface_id];
                var mi = iface.Implementation.methods.Find(x => x.Name == name);

                if (mi != null)
                {
                    return mi.Invoke(NoUserInterface[interface_id], args);
                }

                ClientLog.WriteLine("Unable to find method \"{0}\" from interface \"{1}\"", name, iface.Implementation.name);

                return null;
            }

            return ActiveClients[client_id].CallSerializedFunction(pipe_id, interface_id, name, args);
        }
    }
}
