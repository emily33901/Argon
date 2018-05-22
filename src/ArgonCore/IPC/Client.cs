using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using NamedPipeWrapper;
using ArgonCore.IPC;

namespace ArgonCore.IPC
{
    using ClientPipe = NamedPipeClient<SerializedResult, SerializedFunction>;

    public class Client
    {
        /// <summary>
        /// Results that are waiting for collection from the pipe
        /// </summary>
        static List<SerializedResult> CurrentResults { get; set; }

        /// <summary>
        /// Semaphores for results that are being waited on
        /// </summary>
        static Dictionary<uint, Semaphore> ResultSemaphores { get; set; }

        /// <summary>
        /// The next job id for this pipe
        /// </summary>
        static private uint NextJobId { get; set; }

        /// <summary>
        /// The current PipeWrapper in use by the client
        /// </summary>
        static public ClientPipe CurrentPipe { get; set; }

        /// <summary>
        /// Allocate the pipe and start the pipe connection with the server
        /// </summary>
        public static void AllocatePipe()
        {
            CurrentResults = new List<SerializedResult>();
            ResultSemaphores = new Dictionary<uint, Semaphore>();

            CurrentPipe = new ClientPipe("argon_pipe_server", ".");

            CurrentPipe.Start();

            CurrentPipe.ServerMessage += OnServerMessage;
        }

        /// <summary>
        /// Called on every message send from the server
        /// </summary>
        /// <param name="connection">Current connection</param>
        /// <param name="message">Message recieved from the server</param>
        private static void OnServerMessage(NamedPipeConnection<SerializedResult, SerializedFunction> connection, SerializedResult message)
        {
            CurrentResults.Add(message);
            var semaphore = ResultSemaphores[message.JobId];
            semaphore.Release();
        }

        /// <summary>
        /// Call a function over ipc using the respective function name and interface id and wait for the result
        /// </summary>
        /// <param name="f">Function and arguments to be called</param>
        public static void CallSerializedFunction(SerializedFunction f)
        {
            var job = NextJobId;
            NextJobId += 1;
            CurrentPipe.PushMessage(f);

            return;
        }

        /// <summary>
        /// Call a function over ipc using the respective function name and interface id and wait for the result
        /// </summary>
        /// <typeparam name="T">Return type of the function</typeparam>
        /// <param name="f"></param>
        /// <returns>Result of calling the function on the server</returns>
        public static T CallSerializedFunction<T>(SerializedFunction f)
        {
            var job = NextJobId;
            NextJobId += 1;

            f.JobId = job;

            CurrentPipe.PushMessage(f);

            ResultSemaphores.Add(job, new Semaphore(0, 1));

            return WaitForResultForFunction<T>(f.JobId);
        }

        /// <summary>
        /// Waits for a job to complete and returns the result
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jobId"></param>
        /// <returns></returns>
        public static T WaitForResultForFunction<T>(uint jobId)
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
