using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

using SteamKit2;

using Core;

namespace Server
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

            // TODO: none of this works due to a number of differences between the c++ and c# code
            // All callbacks will need to be "manually" translated from c# to c++
            // This could be made better with some custom member per member serialization
            // and deserialization using functions and maps but other than that...

            // var job_result = AsyncCallManager.GetAsyncJob(packet.TargetJobID);

            // if (job_result != null)
            // {
            //     Log.WriteLine("... Was AsyncCall, queuing callback now!");

            //     job_result.finished = true;

            //     var result_buffer = new Core.Util.Buffer();
            //     result_buffer.Write(job_result.job_id);
            //     result_buffer.Write(job_result.callback_id);
            //     result_buffer.Write(packet.GetData().Length);
            //     result_buffer.Write(packet.GetData());

            //     // TODO: Post callback to clients
            //     Server.Client.PendingCallbacks.Enqueue(new Core.IPC.InternalCallbackMsg
            //     {
            //         callback_id = 703,
            //         user_id = -1,
            //         data = result_buffer.GetBuffer(),
            //     });
            // }
        }
    }
}
