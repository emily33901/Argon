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

            var huser = steam_user.GetHSteamUser();

            var msg = Client.GetCallback();

            if (msg == null)
            {
                Console.WriteLine("callback is null!");
            }
        }
    }
}
