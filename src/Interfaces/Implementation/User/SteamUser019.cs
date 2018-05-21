using System;

using ArgonCore;
using ArgonCore.Interface;

namespace InterfaceUser
{
    [Impl(Name = "SteamUser019", Implements = "SteamUser", ServerMapped = true)]
    public class SteamUser019 : IBaseInterface
    {
        public IntPtr GetHSteamUser()
        {
            Console.WriteLine("GetHSteamUser");
            return IntPtr.Zero;
        }

        public void TestMeme()
        {
            Console.WriteLine("TestMeme");
        }

        public int TestMeme2(int[] arg)
        {
            Console.WriteLine("TestMeme2");

            var total = 0;
            foreach (var a in arg) total += a;
            return total;
        }
    }
}
