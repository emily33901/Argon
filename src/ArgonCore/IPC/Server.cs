using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using NamedPipeWrapper;
using ArgonCore.IPC;

namespace ArgonCore.IPC
{
    using ServerPipe = Server<SerializedFunction, SerializedResult>;

    public class Server
    {
        static private object client_lock = new System.Object();

        static public ServerPipe CurrentPipe { get; set; }

        public static void AllocatePipe()
        {
            CurrentPipe = new ServerPipe("argon_pipe_server");

            CurrentPipe.Start();

            CurrentPipe.ClientConnected += OnClientConnected;
            CurrentPipe.ClientDisconnected += OnClientDisconnected;
            CurrentPipe.ClientMessage += OnClientMessage;
        }

        // TODO: these functions should not block the ipc thread
        private static void OnClientMessage(NamedPipeConnection<SerializedFunction, SerializedResult> connection, SerializedFunction message)
        {
            lock(client_lock)
            {
                Console.WriteLine("Client message...");

                Console.WriteLine("{");
                Console.WriteLine("\tClientId = {0}", connection.Id);
                Console.WriteLine("\tJobId = {0}", message.JobId);
                Console.WriteLine("\tInterfaceId = {0}", message.InterfaceId);
                Console.WriteLine("\tName = \"{0}\"", message.Name);

                if (message.Args == null)
                {
                    Console.WriteLine("\tArgs = {}");
                }
                else
                {
                    Console.WriteLine("\tArgs = {{{0}}}", String.Join(",", message.Args));
                }

                Console.WriteLine("}");

                object result = null;

                try
                {
                    result = ArgonCore.Server.Client.CallSerializedFunction((uint)connection.Id, message.InterfaceId, message.Name, message.Args);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error occured processing job {0}, \"{1}\"", message.JobId, e.Message);
                }

                var result_message = new SerializedResult
                {
                    InterfaceId = message.InterfaceId,
                    JobId = message.JobId,
                    Result = result,
                };

                connection.PushMessage(result_message);
            }
        }

        private static void OnClientDisconnected(NamedPipeConnection<SerializedFunction, SerializedResult> connection)
        {
            Console.WriteLine("Client disconnected [{0}]...", connection.Id);

            // TODO: remove client
        }

        private static void OnClientConnected(NamedPipeConnection<SerializedFunction, SerializedResult> connection)
        {
            lock (client_lock)
            {
                // TODO: create new client
                ArgonCore.Server.Client.CreateNewClient((uint)connection.Id);

                Console.WriteLine("Client connected [{0}]...", connection.Id);
            }

        }
    }
}