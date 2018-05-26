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
            Secrets secrets;
            try
            {
                secrets = JsonConvert.DeserializeObject<Secrets>(File.ReadAllText("..\\..\\..\\secrets.json"));
            }
            catch
            {
                secrets = JsonConvert.DeserializeObject<Secrets>(File.ReadAllText("secrets.json"));
            }


            var pipe_id = ArgonCore.IPC.ClientPipe.CreatePipe();
            var id = Client.CreateNewClient(pipe_id);

            var c = Client.GetClient(id);

            // Get clientuser map
            Console.WriteLine("Creating user interface map...");
            dynamic steam_user = c.CreateMapInstance(pipe_id, "CLIENTUSER_INTERFACE_VERSION001");

            var huser = steam_user.GetHSteamUser(IntPtr.Zero);
            Console.WriteLine("huser is {0}", huser);

            // Trigger a logon
            steam_user.LogOnWithPassword(IntPtr.Zero, secrets.username, secrets.password);

            while (!Console.KeyAvailable)
            {
                var msg = Client.GetCallback(pipe_id);
                if (msg == null)
                {
                    Console.WriteLine("callback is null!");
                }
                else
                {
                    Console.WriteLine("Message id: {0}", msg.Value.callback_id);
                }

                Thread.Sleep(1000);
            }
        }
    }
}
