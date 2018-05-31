using System;
using System.Collections.Generic;
using System.Text;

namespace ArgonCore.Client
{
    class Server
    {
        public static int CreateClient(int pipe_id)
        {
            var f = new IPC.SerializedFunction
            {
                InterfaceId = -1,
                ClientId = -1,
                Name = "CreateClient",
                Args = new object[] { },
            };

            return IPC.ClientPipe.CallSerializedFunction<int>(pipe_id, f);
        }

        /// <summary>
        /// Aks the server to create its instance for this interface
        /// </summary>
        /// <param name="name"></param>
        /// <returns>The interface id created</returns>
        public static int CreateInterfaceNoUser(int pipe_id, string name)
        {
            var f = new IPC.SerializedFunction
            {
                ClientId = -1,
                InterfaceId = -1,
                Name = "CreateInterface",
                Args = new object[] { name },
            };

            return IPC.ClientPipe.CallSerializedFunction<int>(pipe_id, f);
        }

        /// <summary>
        /// Aks the server to create its instance for this interface
        /// </summary>
        /// <param name="name"></param>
        /// <returns>The interface id created</returns>
        public static int CreateInterface(int pipe_id, int client_id, string name)
        {
            var f = new IPC.SerializedFunction
            {
                ClientId = client_id,
                InterfaceId = -1,
                Name = "CreateInterface",
                Args = new object[] { name },
            };

            return IPC.ClientPipe.CallSerializedFunction<int>(pipe_id, f);
        }

        /// <summary>
        /// Get the next callback for this client from the server
        /// </summary>
        /// <returns>The next callback</returns>
        public static InternalCallbackMsg NextCallback(int pipe_id)
        {
            var f = new IPC.SerializedFunction
            {
                ClientId = -1,
                InterfaceId = -1,
                Name = "NextCallback",
                Args = new object[] { },
            };

            return IPC.ClientPipe.CallSerializedFunction<InternalCallbackMsg>(pipe_id, f);
        }

        public static void SetAppId(int pipe_id, int app_id)
        {
            var f = new IPC.SerializedFunction
            {
                ClientId = -1,
                InterfaceId = -1,
                Name = "SetAppId",
                Args = new object[] { app_id },
            };

            IPC.ClientPipe.CallSerializedFunction(pipe_id, f);
        }
    }
}
