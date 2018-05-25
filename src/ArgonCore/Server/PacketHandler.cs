using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

using SteamKit2;

namespace ArgonCore.Server
{
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

            ArgonCore.Server.Client.PendingCallbacks.Enqueue(new ArgonCore.Client.InternalCallbackMsg
            {
                user_id = client.Id,
                callback_id = (uint)packet.MsgType,
                data = packet.GetData(),
            });
        }
    }
}
