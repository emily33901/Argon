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
            var string_name = Marshal.PtrToStringAnsi(name);
            return User.CreateInterfaceNoUser(string_name);
        }
    }
}
