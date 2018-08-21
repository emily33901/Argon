using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

using Core.Interface;

namespace Client
{
    public class Export
    {
        /// <summary>
        /// Called by the runtime bootstrap in order to get access to argon
        /// </summary>
        /// <returns></returns>
        public static IntPtr RecieveCore()
        {
            // Create an argon core interface
            Console.WriteLine("RecieveCore");
            Loader.Load();
            var (argon_core, _, _) = Context.CreateInterface("ArgonClient001");

            return argon_core;
        }
    }
}
