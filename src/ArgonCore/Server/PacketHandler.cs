using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

using SteamKit2;

namespace ArgonCore.Server
{
    /// <summary>
    /// Similar to CallbackMsg_t from steamclient
    /// </summary>
    [Serializable]
    struct CallbackMsg
    {
        public uint user_id;
        public uint callback_id;
        [MarshalAs(UnmanagedType.LPArray)]
        public byte[] data;
        public int data_size;
    }

    /// <summary>
    /// Dispatches packets to the correct handlers
    /// </summary>
    public class PacketHandler : ClientMsgHandler
    {
        public Client client;

        public PacketHandler(Client c)
        {
            client = c;
        }

        public override void HandleMsg(IPacketMsg packet)
        {
            client.Log.WriteLine("packet", "{0} [{1}]", packet.MsgType, (uint)packet.MsgType);

            CallbackMsg new_message = new CallbackMsg
            {
                user_id = client.Id,
                callback_id = (uint)packet.MsgType,
                data = packet.GetData(),
                data_size = packet.GetData().Length,
            };
        }
    }
}
