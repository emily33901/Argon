using System;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using System.Text;

namespace ArgonCore
{
    public sealed class Logger
    {
        private int core_id;
        private string file;

        private List<string> buffered = new List<string>();
        private object file_lock = new object();

        public Logger(int id)
        {
            core_id = id;
            file = string.Format("log_user_{0}.log", core_id);
        }

        public void WriteLine(string code_section, string format, params object[] args)
        {
            var new_format = string.Format("[{0}] [{1}] {2}", core_id, code_section, format);
            var formatted = string.Format(new_format, args);
            Console.WriteLine(formatted);

            buffered.Add(formatted);

            var taken = false;
            Monitor.TryEnter(file_lock, ref taken);

            if (taken)
            {
                File.AppendAllLines(file, buffered);
                buffered.Clear();

                Monitor.Exit(file_lock);
            }
        }
    }
}
