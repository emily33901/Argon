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

            Console.WriteLine("handle is {0}", id);

            var c = Client.GetClient(id);

            // Get clientuser map
            Console.WriteLine("Creating user interface map...");
            dynamic steam_user = c.CreateMapInstance(pipe_id, "CLIENTUSER_INTERFACE_VERSION001");

            Console.WriteLine("steam_user.ClientId = {0}", steam_user.ClientId);

            var huser = steam_user.GetHSteamUser(IntPtr.Zero);
            Console.WriteLine("huser is {0}", huser);

            // Trigger a logon
            steam_user.LogOnWithPassword(IntPtr.Zero, secrets.username, secrets.password);

            Console.Write("Enter your 2fa: ");
            steam_user.SetTwoFactorCode(IntPtr.Zero, Console.ReadLine());
            steam_user.LogOn(IntPtr.Zero, 0);

            while (!Console.KeyAvailable)
            {
                CallbackMsg? callback;

                do
                {
                    callback = Client.GetCallback(pipe_id);
                    if (callback == null)
                    {
                        //Console.WriteLine("callback is null!");
                    }
                    else
                    {
                        Console.WriteLine("Message id: {0}", callback.Value.callback_id);

                        Client.FreeCallback(pipe_id);
                    }
                } while (callback != null);


                Thread.Sleep(100);
            }
        }
    }
}
