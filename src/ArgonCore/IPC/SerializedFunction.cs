using System;
using System.Collections.Generic;
using System.Text;

namespace ArgonCore.IPC
{
    /// <summary>
    /// Represents a job sent by the client to be completed on the server
    /// </summary>
    [Serializable]
    public class SerializedJob
    {
        public uint JobId { get; set; }
        public int InterfaceId { get; set; }
    }

    /// <summary>
    /// Represents a function that has been sent by the client to the server
    /// </summary>
    [Serializable]
    public class SerializedFunction : SerializedJob
    {
        public string Name { get; set; }
        public object[] Args { get; set; }
    }

    /// <summary>
    /// Represents a result that is sent from the server to the client
    /// </summary>
    [Serializable]
    public class SerializedResult : SerializedJob
    {
        public object Result { get; set; }
    }
}
