using System;
using System.Runtime.InteropServices;

namespace ArgonCore
{
    static class Platform
    {
        public static bool IsWindows()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        }

        public static bool IsLinux()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
        }

        public static bool IsOSX()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
        }

        public static string AssemblyExtension()
        {
            if (IsLinux()) return ".so";
            if (IsWindows()) return ".dll";
            if (IsOSX()) return ".dylib";

            throw new Exception("Unknown platform...");
        }
    }
}