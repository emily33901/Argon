using System;
using System.Collections.Generic;
using System.Text;

using ArgonCore.Interface;

namespace ArgonCore.Client
{
    public class Client
    {
        // Used for internal handling of clients
        public uint Id { get; private set; }

        // Interfaces (or maps) that are allocated on the client
        public List<IBaseInterface> interfaces;

        // Logging instance for this client
        public Logger Log { get; private set; }

        public Client()
        {
            Loader.Load();

            Id = 0;

            Log = new Logger(Id);

            IPC.Client.AllocatePipe();

            Log.WriteLine("client", "Waiting for pipe...");
            IPC.Client.CurrentPipe.WaitForConnection();
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

            if (is_map)
            {
                var interface_id = Server.CreateInterface(name);
            }
            else
            {
                iface.InterfaceId = -1;
            }

            interfaces.Add(iface);

            return context;
        }

        public static IntPtr CreateInterfaceNoUser(string name)
        {
            // In theory none of these interfaces should ever be mapped
            var (context, iface, _) = Context.CreateInterface(name);

            iface.ClientId = -1;
            iface.InterfaceId = -1;

            return context;
        }
    }
}
