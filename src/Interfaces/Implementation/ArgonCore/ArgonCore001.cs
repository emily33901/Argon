using System;
using ArgonCore;
using ArgonCore.Interface;
using ArgonCore.Client;

namespace InterfaceArgonCore
{
    /// <summary>
    /// Exports Argon functions to unmanaged code
    /// </summary>
    [Impl(Name = "ArgonCore001", Implements = "ArgonCore", ServerMapped = false)]
    public class ArgonCore001 : IBaseInterface
    {
        /// <summary>
        /// Linked to <see cref="Client.CreateInterfaceNoUser(string)"/>
        /// </summary>
        /// <param name="name"></param>
        public IntPtr CreateInterface(IntPtr _, int pipe_id, string name)
        {
            Console.WriteLine("ArgonCore001.CreateInterface({0})", name);
            return Client.CreateInterfaceNoUser(pipe_id, name);
        }

        public IntPtr CreateInterfaceNoPipe(IntPtr _, string name)
        {
            Console.WriteLine("ArgonCore001.CreateInterfaceNoPipe({0})", name);
            return Client.CreateInterfaceNoUserNoPipe(name);
        }

        /// <summary>
        /// Linked to <see cref="Client.GetCallback"/>
        /// </summary>
        /// <param name="pipe"></param>
        /// <param name="c"></param>
        /// <returns>Whether there is a new callback</returns>
        public bool GetCallback(IntPtr _, int pipe_id, ref CallbackMsg c)
        {
            Console.WriteLine("ArgonCore001.GetCallback");
            var new_callback = Client.GetCallback(pipe_id);

            if (new_callback == null)
            {
                Console.WriteLine("New callback is null!");
            }
            else
            {
                Console.WriteLine("Callback_id is {0}", new_callback.Value.callback_id);
            }

            if (new_callback == null) return false;

            c = (CallbackMsg)new_callback;

            return true;
        }

        /// <summary>
        /// Frees the last callback to this pipe
        /// </summary>
        /// <param name="c"></param>
        public void FreeLastCallback(IntPtr _, int pipe_id)
        {
            Client.FreeCallback(pipe_id);
        }
    }
}
