using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

using ArgonCore.Interface;

namespace ArgonCore.Client
{
    public class Client
    {
        // Used for internal handling of clients
        public static Dictionary<uint, Client> ActiveClients { get; set; } = new Dictionary<uint, Client>();

        private static uint next_id = 0;
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

        /// <summary>
        /// Create a new client instance
        /// </summary>
        /// <returns>The id of this new client</returns>
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

        /// <summary>
        /// Create a new instance (without creating a delelgat map) of an interface
        /// </summary>
        /// <param name="name"></param>
        /// <returns>New instance created</returns>
        public object CreateMapInstance(string name)
        {
            var instance = Context.CreateInterfaceInstance(Context.FindInterfaceMap(name));

            var interface_id = Server.CreateInterface(name);

            return instance;
        }

        /// <summary>
        /// Create a new interface instance and context
        /// </summary>
        /// <param name="name"></param>
        /// <returns>New context created</returns>
        public IntPtr CreateInterface(string name)
        {
            // Always try to make a map
            var (context, iface, is_map) = Interface.Context.CreateInterface(name, true);

            if (context == IntPtr.Zero) return IntPtr.Zero;

            iface.ClientId = (int)Id;
            iface.InterfaceId = -1;

            if (is_map)
            {
                iface.InterfaceId = Server.CreateInterface(name);
            }

            interfaces.Add(iface);

            return context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns>New context created</returns>
        public static IntPtr CreateInterfaceNoUser(string name)
        {
            // In most cases none of these should be mapped (With the exception of ISteamUtils)

            var (context, iface, is_map) = Context.CreateInterface(name, true);

            if (context == IntPtr.Zero) return IntPtr.Zero;

            iface.ClientId = -1;
            iface.InterfaceId = -1;

            if (is_map)
            {
                iface.InterfaceId = Server.CreateInterface(name);
            }

            no_user_interfaces.Add(iface);

            return context;
        }

        private static IntPtr callback_alloc_handle;

        public static CallbackMsg? GetCallback()
        {
            // Ask the server for the callback
            var c = Server.NextCallback();

            // No new callback from server
            if (c == default(InternalCallbackMsg))
            {
                return null;
            }

            if (callback_alloc_handle != IntPtr.Zero)
            {
                // This mimics the behaviour of the steam functions that do this
                Console.WriteLine("Attempt to alloc new callback before old one has been freed");

                // Free it for them...
                FreeCallback();
            }

            // Allocate space for the data portion of the msg
            callback_alloc_handle = Marshal.AllocHGlobal(c.data.Length);
            Marshal.Copy(c.data, 0, callback_alloc_handle, c.data.Length);

            return new CallbackMsg
            {
                user_id = c.user_id,
                callback_id = c.callback_id,
                data = callback_alloc_handle,
                data_size = c.data.Length,
            };
        }

        public static void FreeCallback()
        {
            Marshal.FreeHGlobal(callback_alloc_handle);
            callback_alloc_handle = IntPtr.Zero;
        }
    }
}
