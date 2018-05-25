using System;

using System.Threading;

using ArgonCore;
using ArgonCore.Server;

namespace ArgonHost
{
    class Program
    {
        static void Main(string[] args)
        {
            ArgonCore.IPC.Server.AllocatePipe();

            Console.WriteLine("Server started...");

            var running = true;

            Thread t = new Thread(
                () =>
                {
                    while (running)
                    {
                        Client.RunAllFrames();
                    }

                });

            t.Start();

            Console.ReadLine();

            running = false;

            t.Join();

            ArgonCore.IPC.Server.CurrentPipe.Stop();
        }
    }
}
