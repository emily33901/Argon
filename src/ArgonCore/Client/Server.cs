using System;
using System.Collections.Generic;
using System.Text;

namespace ArgonCore.Client
{
    class Server
    {
        /// <summary>
        /// Aks the server to create its instance for this interface
        /// </summary>
        /// <param name="name"></param>
        /// <returns>The interface id created</returns>
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

        /// <summary>
        /// Get the next callback for this client from the server
        /// </summary>
        /// <returns>The next callback</returns>
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
