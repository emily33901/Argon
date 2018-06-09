using System;
using System.Collections.Generic;

using System.Runtime.InteropServices;

using SteamKit2;

using ArgonCore.Server;

namespace ArgonCore
{
    public class AsyncCallManager
    {
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
            var call_id = 0;
            lock (call_id_lock)
            {
                call_id += 1;
                registered_calls[call_id] = new AsyncJobResult()
                {
                    job_id = call_id,
                    internal_job_id = job.JobID,
                    finished = false,
                    result = null,
                };
            }

            return call_id;
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
            lock (call_id_lock)
            {
                if (registered_calls.TryGetValue(handle, out var v))
                    return v.finished;

                return false;
            }

        }

        public static byte[] GetCallResult(int handle)
        {
            lock (call_id_lock)
            {
                if (registered_calls.TryGetValue(handle, out var v))
                {
                    if (v.finished) return v.result;
                }

                return null;
            }
        }
    }
}