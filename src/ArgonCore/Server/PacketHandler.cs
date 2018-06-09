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
            Log.WriteLine("{0} [{1}] [{2}] -> [{3}]", packet.MsgType, (uint)packet.MsgType, packet.SourceJobID, packet.TargetJobID);

            Server.Client.PendingCallbacks.Enqueue(new ArgonCore.Client.InternalCallbackMsg
            {
                user_id = client.Id,
                callback_id = (uint)packet.MsgType,
                data = packet.GetData(),
            });

            var job_result = AsyncCallManager.GetAsyncJob(packet.TargetJobID);

            if (job_result != null)
            {
                Log.WriteLine("... Was AsyncCall, queuing callback now!");

                job_result.finished = true;

                var result_buffer = new ArgonCore.Util.Buffer();
                result_buffer.Write(job_result.job_id);
                result_buffer.Write(job_result.callback_id);
                result_buffer.Write(packet.GetData().Length);
                result_buffer.Write(packet.GetData());

                // TODO: Post callback to clients
                Server.Client.PendingCallbacks.Enqueue(new ArgonCore.Client.InternalCallbackMsg
                {
                    callback_id = 703,
                    user_id = -1,
                    data = result_buffer.GetBuffer(),
                });
            }
        }
    }
}
