using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using NamedPipeWrapper;
using Core.IPC;

using Core;

namespace Client
{
    using InternalClientPipe = NamedPipeClient<SerializedResult, SerializedFunction>;

    public class ClientPipe
    {
        Logger Log { get; set; }

        static uint TotalIpcCallCount { get; set; }

        public static List<ClientPipe> ActivePipes { get; set; } = new List<ClientPipe>();

        /// <summary>
        /// Internal representation of the pipe used by this instance
        /// </summary>
        InternalClientPipe pipe;

        /// <summary>
        /// Results that are waiting for collection from the pipe
        /// </summary>
        List<SerializedResult> current_results;

        /// <summary>
        /// Semaphores for results that are being waited on
        /// </summary>
        Dictionary<uint, Semaphore> result_semaphores;

        /// <summary>
        /// The next job id for this pipe
        /// </summary>
        uint NextJobId { get; set; }

        public int Id { get { return ActivePipes.IndexOf(this); } }

        ClientPipe()
        {
            current_results = new List<SerializedResult>();
            result_semaphores = new Dictionary<uint, Semaphore>();

            pipe = new InternalClientPipe("argon_pipe_server", ".");

            pipe.Start();
            pipe.ServerMessage += OnServerMessage;

            ActivePipes.Add(this);

            Log = new LoggerUid("IPC.ClientPipe", Id);


            Log.WriteLine("Waiting for connection...");
            pipe.WaitForConnection();
            Log.WriteLine("Connected...");
        }

        public static int CreatePipe()
        {
            // In order to ensure that pipe indexes start at 1
            if (ActivePipes.Count == 0) ActivePipes.Add(null);

            var c = new ClientPipe();

            return c.Id;
        }

        /// <summary>
        /// Called on every message send from the server
        /// </summary>
        /// <param name="connection">Current connection</param>
        /// <param name="message">Message recieved from the server</param>
        private static void OnServerMessage(NamedPipeConnection<SerializedResult, SerializedFunction> connection, SerializedResult message)
        {
            var pipe = ActivePipes[message.PipeId];

            pipe.current_results.Add(message);
            var semaphore = pipe.result_semaphores[message.JobId];
            semaphore.Release();
        }

        private void PushMessage(SerializedFunction f)
        {
            TotalIpcCallCount += 1;
            pipe.PushMessage(f);
        }

        public SerializedResult CallSerializedFunction(SerializedFunction f)
        {
            var job = NextJobId;
            NextJobId += 1;

            f.JobId = job;
            f.PipeId = Id;

            PushMessage(f);

            result_semaphores.Add(job, new Semaphore(0, 1));

            return WaitForResultForFunction(f.JobId);
        }

        public static SerializedResult CallSerializedFunction(int id, SerializedFunction f)
        {
            var pipe = ActivePipes[id];
            return pipe.CallSerializedFunction(f);
        }

        public SerializedResult WaitForResultForFunction(uint job_id)
        {
            // Wait for the semaphore and then remove it so gc collects it
            var this_semaphore = result_semaphores[job_id];
            this_semaphore.WaitOne();
            result_semaphores.Remove(job_id);

            var found = current_results.Find(x => x.JobId == job_id);
            current_results.Remove(found);

            return found;
        }

        public static uint GetIpcCallCount()
        {
            var total = TotalIpcCallCount;
            TotalIpcCallCount = 0;
            return total;
        }
    }
}
