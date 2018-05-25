using System;
using System.Collections.Generic;
using System.Text;
using NamedPipeWrapper;

namespace ArgonCore.Interface
{
    /// <summary>
    /// Interface that all interface implementations must inherit from
    /// </summary>
    public class IBaseInterface
    {
        /// <summary>
        /// Set by <see cref="Client"/> to allow interfaces to know what user they belong too
        /// </summary>
        public int ClientId { get; set; }

        public int InterfaceId { get; set; }

        public Plugin.InterfaceImpl Implementation { get; set; }
    }

    public class IBaseInterfaceMap : IBaseInterface
    {
        public int PipeId { get; set; }
    }
}
