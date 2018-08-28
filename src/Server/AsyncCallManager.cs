using System;
using System.Collections.Generic;

using System.Runtime.InteropServices;

using SteamKit2;

namespace Server
{
    public class AsyncCallManager
    {
        // TODO: if we keep track of pipe_ids here then we should be able to correctly
        // invalidate jobs that cannot possibly continue

        // TODO: add a PostAsyncJobResult function...

        public class AsyncJobResult
        {
            public int job_id;
            public JobID internal_job_id;
            public int callback_id;
            public bool finished;
            public byte[] result;
        }

        static Dictionary<int, AsyncJobResult> registered_calls = new Dictionary<int, AsyncJobResult>();
        static int last_call_id = 0;
        static object call_id_lock = new object();

        public static int RegisterAsyncJob<T>(AsyncJob<T> job, int callback_id, int user_id)
        where T : CallbackMsg
        {
            lock (call_id_lock)
            {
                last_call_id += 1;
                registered_calls[last_call_id] = new AsyncJobResult()
                {
                    job_id = last_call_id,
                    internal_job_id = job.JobID,
                    finished = false,
                    result = null,
                };
            }

            return last_call_id;
        }

        public static AsyncJobResult GetAsyncJob(JobID id)
        {
            lock (call_id_lock)
            {
                foreach (var v in registered_calls.Values)
                {
                    if (v.internal_job_id == id)
                        return v;
                }
            }

            return null;
        }
        public static bool IsCallFinished(int handle)
        {
            return registered_calls.TryGetValue(handle, out var v) ? v.finished : false;
        }
        public static byte[] GetCallResult(int handle)
        {
            return registered_calls.TryGetValue(handle, out var v) ? v.result : null;
        }
        public static bool IsHandleValid(int handle)
        {
            return registered_calls.TryGetValue(handle, out var _);
        }
    }
}
