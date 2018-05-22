using System;
using System.Collections.Generic;
using System.Text;

namespace ArgonCore.Client
{
    class Server
    {
        public static int CreateInterface(string name)
        {
            var f = new IPC.SerializedFunction
            {
                InterfaceId = -1,
                Name = "CreateInterface",
                Args = new object[] { name },
            };

            return IPC.Client.CallSerializedFunction<int>(f);
        }

        public static InternalCallbackMsg NextCallback()
        {
            var f = new IPC.SerializedFunction
            {
                InterfaceId = -1,
                Name = "NextCallback",
                Args = new object[] { },
            };

            return IPC.Client.CallSerializedFunction<InternalCallbackMsg>(f);
        }
    }
}
