﻿using System;
using System.Collections.Generic;
using System.Text;

using ArgonCore.IPC;

namespace ArgonCore.Interface
{
    /// <summary>
    /// Used to signal to <see cref="Loader"/> that this class is used for interface maps
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class MapAttribute : ImplAttribute
    {
    }
}
