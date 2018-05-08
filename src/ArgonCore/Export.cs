using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ArgonCore
{
    public class Export
    {
        /// <summary>
        /// Called by the runtime bootstrap in order to get access to argon
        /// </summary>
        /// <returns></returns>
        static IntPtr RecieveArgonCore()
        {
            // Create an argon core interface
            Console.WriteLine("RecieveArgonCore");
            Interface.Loader.Load();
            var (argon_core, _) = Interface.Loader.CreateInterface("ArgonCore001");

            return argon_core;
        }

        static Export inst = new Export();

    }
}
