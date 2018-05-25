using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using NamedPipeWrapper;
using ArgonCore.IPC;

namespace ArgonCore.IPC
{
    using InternalClientPipe = NamedPipeClient<SerializedResult, SerializedFunction>;

    public class ClientPipe
    {
        public static List<ClientPipe> ActivePipes { get; set; } = new List<ClientPipe>();

        public InternalClientPipe Pipe { get; private set; }

        /// <summary>
        /// Results that are waiting for collection from the pipe
        /// </summary>
        public List<SerializedResult> CurrentResults { get; private set; }

        /// <summary>
        /// Semaphores for results that are being waited on
        /// </summary>
        public Dictionary<uint, Semaphore> ResultSemaphores { get; private set; }

        /// <summary>
        /// The next job id for this pipe
        /// </summary>
        static private uint NextJobId { get; set; }

        public int Id { get { return ActivePipes.IndexOf(this); } }

        ClientPipe()
        {
            CurrentResults = new List<SerializedResult>();
            ResultSemaphores = new Dictionary<uint, Semaphore>();

            Pipe = new InternalClientPipe("argon_pipe_server", ".");

            Pipe.Start();
            Pipe.ServerMessage += OnServerMessage;

            ActivePipes.Add(this);

            Console.WriteLine("[pipe {0}] Waiting for connection...", Id);
            Pipe.WaitForConnection();
            Console.WriteLine("[pipe {0}] Connected...", Id);
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

            pipe.CurrentResults.Add(message);
            var semaphore = pipe.ResultSemaphores[message.JobId];
            semaphore.Release();
        }

        /// <summary>
        /// Call a function over ipc using the respective function name and interface id and wait for the result
        /// </summary>
        /// <param name="f">Function and arguments to be called</param>
        public void CallSerializedFunction(SerializedFunction f)
        {
            var job = NextJobId;
            NextJobId += 1;

            f.JobId = job;
            f.PipeId = Id;

            Pipe.PushMessage(f);

            return;
        }
        public static void CallSerializedFunction(int id, SerializedFunction f)
        {
            var pipe = ActivePipes[id];
            pipe.CallSerializedFunction(f);
        }

        /// <summary>
        /// Call a function over ipc using the respective function name and interface id and wait for the result
        /// </summary>
        /// <typeparam name="T">Return type of the function</typeparam>
        /// <param name="f"></param>
        /// <returns>Result of calling the function on the server</returns>
        public T CallSerializedFunction<T>(SerializedFunction f)
        {
            var job = NextJobId;
            NextJobId += 1;

            f.JobId = job;
            f.PipeId = Id;

            Pipe.PushMessage(f);

            ResultSemaphores.Add(job, new Semaphore(0, 1));

            return WaitForResultForFunction<T>(f.JobId);
        }
        public static T CallSerializedFunction<T>(int id, SerializedFunction f)
        {
            var pipe = ActivePipes[id];
            return pipe.CallSerializedFunction<T>(f);
        }

        /// <summary>
        /// Waits for a job to complete and returns the result
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jobId"></param>
        /// <returns></returns>
        public T WaitForResultForFunction<T>(uint jobId)
        {
            // Wait for the semaphore and then remove it so gc collects it
            var this_semaphore = ResultSemaphores[jobId];
            this_semaphore.WaitOne();
            ResultSemaphores.Remove(jobId);

            var found = CurrentResults.Find(x => x.JobId == jobId);
            CurrentResults.Remove(found);

            if (found.Result == null) return default(T);

            var result = (T)found.Result;

            return result;
        }
    }
}
