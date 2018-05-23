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
        public IntPtr CreateInterface(IntPtr _, string name)
        {
            Console.WriteLine("ArgonCore001.CreateInterface({0})", name);
            return Client.CreateInterfaceNoUser(name);
        }

        /// <summary>
        /// Linked to <see cref="Client.GetCallback"/>
        /// </summary>
        /// <param name="pipe"></param>
        /// <param name="c"></param>
        /// <returns>Whether there is a new callback</returns>
        public bool GetCallback(IntPtr _, uint pipe, ref CallbackMsg c)
        {
            var new_callback = Client.GetCallback();

            if (new_callback == null) return false;

            c = (CallbackMsg)new_callback;

            return true;
        }

        /// <summary>
        /// Frees the last callback to this pipe
        /// </summary>
        /// <param name="c"></param>
        public void FreeLastCallback(IntPtr _, ref CallbackMsg c)
        {
            Client.FreeCallback();
        }
    }
}
