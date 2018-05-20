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
        static List<SerializedResult> CurrentResults { get; set; }
        static Dictionary<uint, Semaphore> ResultSemaphores { get; set; }
        static private uint NextJobId { get; set; }

        static public ClientPipe CurrentPipe { get; set; }

        public static void AllocatePipe()
        {
            CurrentResults = new List<SerializedResult>();
            ResultSemaphores = new Dictionary<uint, Semaphore>();

            CurrentPipe = new ClientPipe("argon_pipe_server", ".");

            CurrentPipe.Start();

            CurrentPipe.ServerMessage += OnServerMessage;
        }

        private static void OnServerMessage(NamedPipeConnection<SerializedResult, SerializedFunction> connection, SerializedResult message)
        {
            CurrentResults.Add(message);
            var semaphore = ResultSemaphores[message.JobId];
            semaphore.Release();
        }

        public static void CallSerializedFunction(SerializedFunction f)
        {
            var job = NextJobId;
            NextJobId += 1;
            CurrentPipe.PushMessage(f);

            return;
        }

        public static T CallSerializedFunction<T>(SerializedFunction f)
        {
            var job = NextJobId;
            NextJobId += 1;

            f.JobId = job;

            CurrentPipe.PushMessage(f);

            ResultSemaphores.Add(job, new Semaphore(0, 1));

            return WaitForResultForFunction<T>(f.JobId);
        }

        public static T WaitForResultForFunction<T>(uint jobId)
        {
            var this_semaphore = ResultSemaphores[jobId];
            this_semaphore.WaitOne();
            ResultSemaphores.Remove(jobId);

            var found = CurrentResults.Find(x => x.JobId == jobId);
            CurrentResults.Remove(found);

            var result = (T)found.Result;

            return result;
        }
    }
}
