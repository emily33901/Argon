using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ArgonCore.Client
{
    public class InternalCallbackMsg
    {
        public uint user_id;
        public uint callback_id;
        public byte[] data;
    }

    /// <summary>
    /// Similar to CallbackMsg_t from steamclient
    /// </summary>
    public struct CallbackMsg
    {
        public uint user_id;
        public uint callback_id;
        public IntPtr data;
        public int data_size;
    }
}
