using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

using Newtonsoft.Json;
using ArgonCore;

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
            User u = new User();
            u.ConnectClient();

            var new_interface = User.CreateInterface("STEAMCLIENT_INTERFACE_VERSION001");

            Thread t1 = new Thread(
                () =>
                {
                    while (u.Running)
                    {
                        u.RunFrame();
                    }
                });
            t1.Start();

            Thread.Sleep(1000);

            var secrets = JsonConvert.DeserializeObject<Secrets>(File.ReadAllText("..\\..\\secrets.json"));

            u.LogOn(secrets.username, secrets.password);

            Console.WriteLine("Enter your 2fa: ");
            u.SetTwoFactorAndLogOn(Console.ReadLine());

            Console.ReadLine();

            u.DisconnectClient();

            t1.Join();
        }
    }
}
