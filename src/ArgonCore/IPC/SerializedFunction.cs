using System;
using System.Collections.Generic;
using System.Text;

namespace ArgonCore.IPC
{
    [Serializable]
    public class SerializedJob
    {
        public uint JobId { get; set; }
        public int InterfaceId { get; set; }
    }

    [Serializable]
    public class SerializedFunction : SerializedJob
    {
        public string Name { get; set; }
        public object[] Args { get; set; }
    }

    [Serializable]
    public class SerializedResult : SerializedJob
    {
        public object Result { get; set; }
    }
}
