using System;
using System.Collections.Generic;
using System.Text;

using SteamKit2;

using Core;
using Core.Extensions;

namespace Server
{

    /// <summary>
    /// Dispatches packets to the correct handlers
    /// </summary>
    public class PacketHandler : ClientMsgHandler
    {
        private Logger log;

        private Dictionary<EMsg, List<Action<IPacketMsg>>> dispatch_map = new Dictionary<EMsg, List<Action<IPacketMsg>>>();

        public PacketHandler(Client c)
        {
            log = new LoggerUid("PacketHandler", c.Id);
        }

        public override void HandleMsg(IPacketMsg packet)
        {
            log.WriteLine("{0} [{1}] [{2}] -> [{3}]", packet.MsgType, (uint)packet.MsgType, packet.SourceJobID, packet.TargetJobID);

            if (dispatch_map.TryGetValue(packet.MsgType, out var arr))
            {
                foreach (var cb in arr)
                {
                    cb(packet);
                }
            }
        }

        public void Subscribe<T>(EMsg id, Action<ClientMsgProtobuf<T>> func)
        where T : ProtoBuf.IExtensible, new()
        {
            Subscribe(id, new Action<IPacketMsg>(x =>
            {
                var msg = new ClientMsgProtobuf<T>(x);
                func(msg);
            }));
        }

        public void Subscribe(EMsg id, Action<IPacketMsg> cb)
        {
            dispatch_map.FindOrCreate(id).Add(cb);
            if (dispatch_map.TryGetValue(id, out var arr))
            {
                arr.Add(cb);
                return;
            }

            dispatch_map[id] = new List<Action<IPacketMsg>>();
            dispatch_map[id].Add(cb);
        }

        public void Unsubscribe(EMsg id, Action<IPacketMsg> cb)
        {
            if (dispatch_map.TryGetValue(id, out var arr))
            {
                arr.RemoveAll(x => (x == cb));
            }
        }
    }
}
