using System;
using System.Collections.Generic;
using System.Text;

using Core;
using Core.Interface;
using Core.IPC;
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

        // Lookup for pipeids to their appids
        // This maps the real serverside pipeid to its appid
        public static Dictionary<int, int> PipeAppId { get; set; } = new Dictionary<int, int>();

        // Pipes that are connected to this user
        public HashSet<int> ConnectedPipes { get; private set; }

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
            ConnectedPipes = new HashSet<int>();

            Loader.Load();

            lock (active_client_lock)
            {
                ActiveClients[next_client_id] = this;
                Id = next_client_id;

                next_client_id += 1;

                Log = new LoggerUid("Server.Client", Id);

                InitSteam();
            }
        }

        public void InitSteam()
        {
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

            Log.WriteLine("Initialising modules");

            // Initialise modules (These need to be created now as otherwise we wont be
            // subscribed to callbacks for those classes)

            foreach (var a in Loader.GetInterfaceAssemblies())
            {
                var subclasses = Core.Util.Types.FindSubClassesOfGeneric(typeof(ClientTied<>), a);
                foreach (var c in subclasses)
                {
                    Log.WriteLine("Found class {0}", c.FullName);

                    var find_or_create = c.BaseType.GetMethod("FindOrCreate");

                    find_or_create.Invoke(null, new object[] { Id });
                }
            }

            Log.WriteLine("Modules inited successfully");
        }

        public static int CreateNewClient()
        {
            var c = new Client();
            return c.Id;
        }

        public static int CreateNewClient(int pipe_id)
        {
            var id = CreateNewClient();
            GetClient(id).ConnectPipeToUser(pipe_id);

            return id;
        }

        public void ConnectPipeToUser(int pipe_id)
        {
            ConnectedPipes.Add(pipe_id);
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
            CallbackManager.RunWaitCallbacks(TimeSpan.FromMilliseconds(10));
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

            // TOOD: SteamClientDisconnected callbacks are only sent
            // elsewhere for login failure and not for steam connection failure
            // which we should probably handle here

            Connected = false;

            // Attempt to reconnect straight away
            Connect();
        }

        public void PostCallback(int callback_id, Core.Util.Buffer b)
        {
            // TODO: we probably need a lock here to be threadsafe
            foreach (var p in ConnectedPipes)
            {
                CallbackHandler.PostCallback(p, Id, callback_id, b);
            }
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

            // Handle special server related functions
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
                case "ConnectPipeToUser":
                    {
                        ConnectPipeToUser(pipe_id);
                        return null;
                    }
                default:
                    Log.WriteLine("Method \"{0}\" is not defined for Non-interface client function", name);
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
                            return CreateNewClient(pipe_id);
                        }
                    case "CreateInterface":
                        {
                            return CreateInterfaceNoUser(pipe_id, (string)args[0]).InterfaceId;
                        }
                    case "NextCallback":
                        {
                            return CallbackHandler.GetCallbackForPipe(pipe_id);
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

            if (!GetClient(client_id).ConnectedPipes.Contains(pipe_id))
            {
                ClientLog.WriteLine("Pipe {0} is not connected to Client {1}", pipe_id, client_id);
                return null;
            }

            return GetClient(client_id).CallSerializedFunction(pipe_id, interface_id, name, args);
        }
    }
}
