using System;
using System.Collections.Generic;
using System.Text;

namespace ArgonCore.Interface
{
    /// <summary>
    /// Interface that all interface implementations must inherit from
    /// </summary>
    public interface IBaseInterface
    {
        /// <summary>
        /// Set by <see cref="User"/> to allow interfaces to know what user they belong too
        /// </summary>
        int UserId { get; set; }
    }
}
