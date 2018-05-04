using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ArgonCore;
using DLLExport;

namespace DllExportTest
{
    public static class Main
    {
        [DllExport(CallingConvention = CallingConvention.StdCall, ExportName = "CreateInterface")]
        public static IntPtr CreateInterface(IntPtr name)
        {
            Console.WriteLine("CreateInterface called! name is {0}", name.ToString());

            var string_name = Marshal.PtrToStringAnsi(name);

            if (string_name == null)
            {
                Console.WriteLine("string_name == null !");
                return IntPtr.Zero;
            }

            return User.CreateInterfaceNoUser(string_name);
        }
    }
}
