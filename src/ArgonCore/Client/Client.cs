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
        public static Dictionary<int, Client> ActiveClients { get; set; } = new Dictionary<int, Client>();

        public int Id { get; private set; }

        // Interfaces (or maps) that are allocated on the client
        public List<IBaseInterface> interfaces;

        // NoUser interfaces (or maps) that are allocated on the client
        public static List<IBaseInterface> NoUserInterfaces { get; set; } = new List<IBaseInterface>();

        // Logging instance for this client
        public Logger Log { get; private set; }

        // The pipeid that this user is backed by
        private int PipeId { get; set; }

        Client()
        {
            interfaces = new List<IBaseInterface>();

            Loader.Load();
        }

        /// <summary>
        /// Create a new client instance
        /// </summary>
        /// <returns>The id of this new client</returns>
        public static int CreateNewClient(int pipe_id)
        {
            var c = new Client();

            c.Id = Server.CreateClient(pipe_id);
            c.Log = new Logger(c.Id);

            ActiveClients[c.Id + 1] = c;

            return c.Id + 1;
        }

        /// <summary>
        /// Create a new instance (without creating a delelgate map) of an interface
        /// </summary>
        /// <param name="name"></param>
        /// <returns>New instance created</returns>
        public object CreateMapInstance(int pipe_id, string name)
        {
            var instance = (IBaseInterfaceMap)Context.CreateInterfaceInstance(Context.FindInterfaceMap(name));

            var interface_id = Server.CreateInterface(pipe_id, Id, name);

            instance.InterfaceId = interface_id;
            instance.ClientId = Id;
            instance.PipeId = pipe_id;

            return instance;
        }

        /// <summary>
        /// Create a new interface instance and context
        /// </summary>
        /// <param name="name"></param>
        /// <returns>New context created</returns>
        public IntPtr CreateInterface(int pipe_id, string name)
        {
            // Always try to make a map
            var (context, iface, is_map) = Interface.Context.CreateInterface(name, true);

            if (context == IntPtr.Zero) return IntPtr.Zero;

            iface.ClientId = Id;
            iface.InterfaceId = -1;

            if (is_map)
            {
                iface.InterfaceId = Server.CreateInterface(pipe_id, Id, name);

                var map = (IBaseInterfaceMap)iface;

                map.PipeId = pipe_id;
            }

            interfaces.Add(iface);

            return context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns>New context created</returns>
        public static IntPtr CreateInterfaceNoUser(int pipe_id, string name)
        {
            // In most cases none of these should be mapped (With the exception of ISteamUtils)
            var (context, iface, is_map) = Context.CreateInterface(name, true);

            if (context == IntPtr.Zero) return IntPtr.Zero;

            iface.ClientId = -1;
            iface.InterfaceId = -1;

            if (is_map)
            {
                iface.InterfaceId = Server.CreateInterfaceNoUser(pipe_id, name);

                var map = (IBaseInterfaceMap)iface;
                map.PipeId = pipe_id;
            }

            NoUserInterfaces.Add(iface);

            return context;
        }

        public static IntPtr CreateInterfaceNoUserNoPipe(string name)
        {
            // None of these can possibly be maps!
            var (context, iface, _) = Context.CreateInterface(name);

            if (context == IntPtr.Zero) return IntPtr.Zero;

            iface.ClientId = -1;
            iface.InterfaceId = -1;

            NoUserInterfaces.Add(iface);

            return context;
        }

        private static Dictionary<int, IntPtr> CallbackAllocHandles { get; set; } = new Dictionary<int, IntPtr>();

        public static CallbackMsg? GetCallback(int pipe_id)
        {
            // Ask the server for the callback
            var c = Server.NextCallback(pipe_id);

            // No new callback from server
            if (c == default(InternalCallbackMsg))
            {
                return null;
            }

            IntPtr current_value;
            bool found = CallbackAllocHandles.TryGetValue(pipe_id, out current_value);

            if (found && current_value != IntPtr.Zero)
            {
                // This mimics the behaviour of the steam functions that do this
                Console.WriteLine("Attempt to alloc new callback before old one has been freed");

                // Free it for them...
                FreeCallback(pipe_id);
            }

            // Allocate space for the data portion of the msg
            CallbackAllocHandles[pipe_id] = Marshal.AllocHGlobal(c.data.Length);
            Marshal.Copy(c.data, 0, CallbackAllocHandles[pipe_id], c.data.Length);

            return new CallbackMsg
            {
                // Make sure to convert from the server userid to the client userid
                user_id = c.user_id + 1,
                callback_id = c.callback_id,
                data = CallbackAllocHandles[pipe_id],
                data_size = c.data.Length,
            };
        }

        public static void FreeCallback(int pipe_id)
        {
            IntPtr current_value;
            bool found = CallbackAllocHandles.TryGetValue(pipe_id, out current_value);

            if (found)
            {
                Marshal.FreeHGlobal(current_value);
                current_value = IntPtr.Zero;
            }
        }
    }
}
