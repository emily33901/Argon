using System;
using System.Collections.Generic;
using System.Text;

using ArgonCore.Interface;

namespace ArgonCore.Client
{
    public class Client
    {
        // Used for internal handling of clients
        public static Dictionary<uint, Client> ActiveClients { get; set; } = new Dictionary<uint, Client>();
        public uint next_id = 0;
        public uint Id { get; private set; }

        // Interfaces (or maps) that are allocated on the client
        public List<IBaseInterface> interfaces;

        // NoUser interfaces (or maps) that are allocated on the client
        public static List<IBaseInterface> no_user_interfaces = new List<IBaseInterface>();

        // Logging instance for this client
        public Logger Log { get; private set; }

        public Client()
        {
            interfaces = new List<IBaseInterface>();

            Loader.Load();

            Id = next_id;
            next_id += 1;

            Log = new Logger(Id);

            IPC.Client.AllocatePipe();

            Log.WriteLine("client", "Waiting for pipe...");
            IPC.Client.CurrentPipe.WaitForConnection();

        }

        public static uint CreateNewClient()
        {
            if (ActiveClients == null)
            {
                ActiveClients = new Dictionary<uint, Client>();
            }

            var c = new Client();
            ActiveClients[c.Id] = c;

            return c.Id;
        }

        public object CreateMapContext(string name)
        {
            var instance = Context.CreateInterfaceInstance(Context.FindInterfaceMap(name));

            var interface_id = Server.CreateInterface(name);

            return instance;
        }

        public IntPtr CreateInterface(string name)
        {
            // Always try to make a map
            var (context, iface, is_map) = Interface.Context.CreateInterface(name, true);

            iface.ClientId = (int)Id;
            iface.InterfaceId = -1;

            if (is_map)
            {
                iface.InterfaceId = Server.CreateInterface(name);
            }

            interfaces.Add(iface);

            return context;
        }

        public static IntPtr CreateInterfaceNoUser(string name)
        {
            // In most cases none of these should be mapped (With the exception of ISteamUtils)

            var (context, iface, is_map) = Context.CreateInterface(name);

            iface.ClientId = -1;
            iface.InterfaceId = -1;

            if (is_map)
            {
                iface.InterfaceId = Server.CreateInterface(name);
            }

            no_user_interfaces.Add(iface);

            return context;
        }
    }
}
