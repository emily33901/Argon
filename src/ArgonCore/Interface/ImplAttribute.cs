using System;
using System.Collections.Generic;
using System.Text;

namespace ArgonCore.Interface
{
    /// <summary>
    /// Used to signal to <see cref="Loader"/> that this class is used for interface implementations
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class ImplAttribute : Attribute
    {
        /// <summary>
        /// Name that this interface wants to be exported as
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Name of the interface delegates that this class implements
        /// </summary>
        public string Implements { get; set; }

        public bool ServerMapped { get; set; }
    }
}
