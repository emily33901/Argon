using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

using Newtonsoft.Json;
using ArgonCore;
using ArgonCore.Client;

namespace testbed
{
    class Program
    {
        struct Secrets
        {
            public string username;
            public string password;
            public string username2;
            public string password2;
        }

        static void Main(string[] args)
        {
            Client c = new Client();

            Console.WriteLine("Creating user interface map...");
            dynamic steam_user = c.CreateMapContext("SteamUser019");

            Console.WriteLine("Calling TestMeme2()");
            var r = steam_user.TestMeme2(new int[] { 10, 20, 30, 40 });
            Console.WriteLine("TestMeme2 returns {0}", r);
        }
    }
}
