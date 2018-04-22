using System;
using System.Collections.Generic;
using System.Text;

using SteamKit2;

namespace steamclient_common
{
    /// <summary>
    /// Dispatches packets to the correct handlers
    /// </summary>
    public class PacketHandler : ClientMsgHandler
    {
        public User user;

        public PacketHandler(User u)
        {
            user = u;
        }

        public override void HandleMsg(IPacketMsg packet)
        {
            user.Log.WriteLine("packet", "{0} [{1}]", packet.MsgType, (uint)packet.MsgType);
        }
    }
}
