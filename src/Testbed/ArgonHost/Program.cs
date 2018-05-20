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

            Console.ReadLine();

            ArgonCore.IPC.Server.CurrentPipe.Stop();
        }
    }
}
