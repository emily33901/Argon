using System;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using System.Text;

namespace ArgonCore
{
    public class Logger
    {
        const string file = "argon_log.log";

        static List<string> buffered = new List<string>();
        static object file_lock = new object();

        protected string section;

        public Logger(string s)
        {
            section = s;
        }

        public virtual string FormatLogFormat(string format)
        {
            return String.Format("{0}:: {1}", section, format);
        }

        public void WriteLine(string format, params object[] args)
        {
            var new_format = FormatLogFormat(format);
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

    public class LoggerUid : Logger
    {
        int uid = 0;

        public LoggerUid(string section, int id) : base(section)
        {
            uid = id;
        }

        public override string FormatLogFormat(string format)
        {
            return String.Format("{0}[{1}]:: {2}", section, uid, format);
        }
    }
}
