using System;

using System.Threading;

using Server;

namespace ArgonHost
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerPipe.AllocatePipe();

            Console.WriteLine("Server started...");

            var running = true;

            Thread t = new Thread(
                () =>
                {
                    while (running)
                    {
                        Server.Client.RunAllFrames();
                        System.Threading.Thread.Sleep(100);
                    }

                });

            t.Start();

            Console.ReadLine();

            running = false;

            t.Join();

            ServerPipe.CurrentPipe.Stop();

            Console.WriteLine("Done.");
            Console.ReadKey();
        }
    }
}
