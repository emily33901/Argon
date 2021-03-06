using System;
using System.Runtime.InteropServices;

namespace Core
{
    public static class Platform
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
            return ".dll";
        }

        public static DateTime LoadTime { get; set; } = DateTime.Now;
        public static int MilisecondTime()
        {
            return (DateTime.Now - LoadTime).Milliseconds;
        }

        public static uint ToUnixTime(DateTime t)
        {
            return (uint)(new DateTimeOffset(t)).ToUnixTimeSeconds();
        }
    }
}