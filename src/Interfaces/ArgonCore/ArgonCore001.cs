using System;
using ArgonCore;
using ArgonCore.Interface;

namespace InterfaceArgonCore
{
    /// <summary>
    /// Exports Argon functions to unmanaged code
    /// </summary>
    [Impl(Name = "ArgonCore001", Implements = "ArgonCore", ServerMapped = false)]
    public class ArgonCore001 : IBaseInterface
    {
        /// <summary>
        /// Linked to <see cref="User.CreateInterfaceNoUser(string)"/>
        /// </summary>
        /// <param name="name"></param>
        public IntPtr CreateInterface(string name)
        {
            return User.CreateInterfaceNoUser(name);
        }
    }
}
