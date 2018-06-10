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
        /// <summary>
        /// Lock of for accessing pipe data
        /// </summary>
        static private object client_lock = new System.Object();

        /// <summary>
        /// Current pipe allocated by the server
        /// </summary>
        static public ServerPipe CurrentPipe { get; set; }

        /// <summary>
        /// Create the server pipe and start the connection
        /// </summary>
        public static void AllocatePipe()
        {
            CurrentPipe = new ServerPipe("argon_pipe_server");

            CurrentPipe.Start();

            CurrentPipe.ClientConnected += OnClientConnected;
            CurrentPipe.ClientDisconnected += OnClientDisconnected;
            CurrentPipe.ClientMessage += OnClientMessage;
        }

        // TODO: these functions should not block the ipc thread
        /// <summary>
        /// Handles messages that are sent by clients by dispatching functions and then sending the results back
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="message"></param>
        private static void OnClientMessage(NamedPipeConnection<SerializedFunction, SerializedResult> connection, SerializedFunction message)
        {
            // The pipeId that comes in from the message is the clients unique pipe id,
            // But that is not unique on the server, so we use the connections id.

            lock (client_lock)
            {
                Console.WriteLine("Client message...");

                Console.WriteLine("{");
                Console.WriteLine("\tClientId = {0}", message.ClientId);
                Console.WriteLine("\tJobId = {0}", message.JobId);
                Console.WriteLine("\tInterfaceId = {0}", message.InterfaceId);
                Console.WriteLine("\tPipeId = \"{0}\"", connection.Id);
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
                    result = ArgonCore.Server.Client.CallSerializedFunction(connection.Id, message.ClientId, message.InterfaceId, message.Name, message.Args);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error occured processing job {0}, \"{1}\"", message.JobId, e.Message);
                }

                var result_message = new SerializedResult
                {
                    PipeId = message.PipeId,
                    ClientId = message.ClientId,
                    InterfaceId = message.InterfaceId,
                    JobId = message.JobId,
                    Result = result,
                };

                connection.PushMessage(result_message);
            }
        }

        /// <summary>
        /// Handles client disconnection
        /// </summary>
        /// <param name="connection"></param>
        private static void OnClientDisconnected(NamedPipeConnection<SerializedFunction, SerializedResult> connection)
        {
            Console.WriteLine("Client disconnected [{0}]...", connection.Id);

            // TODO: remove client
        }

        /// <summary>
        /// Handles client connection
        /// </summary>
        /// <param name="connection"></param>
        private static void OnClientConnected(NamedPipeConnection<SerializedFunction, SerializedResult> connection)
        {
            lock (client_lock)
            {
                // TODO: create new client
                Console.WriteLine("Client connected [{0}]...", connection.Id);
            }
        }
    }
}