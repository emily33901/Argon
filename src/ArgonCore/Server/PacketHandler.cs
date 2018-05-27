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
        public static Logger Log { get; set; } = new Logger("PacketHandler");
        public Client client;

        public PacketHandler(Client c)
        {
            client = c;
        }

        public override void HandleMsg(IPacketMsg packet)
        {
            Log.WriteLine("{0} [{1}]", packet.MsgType, (uint)packet.MsgType);

            Server.Client.PendingCallbacks.Enqueue(new ArgonCore.Client.InternalCallbackMsg
            {
                user_id = client.Id,
                callback_id = (uint)packet.MsgType,
                data = packet.GetData(),
            });
        }
    }
}
