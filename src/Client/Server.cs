using System;
using System.Collections.Generic;
using System.Text;

using Core.IPC;

namespace Client
{
    class Server
    {
        public static int CreateClient(int pipe_id)
        {
            var f = new SerializedFunction
            {
                ClientId = -1,
                InterfaceId = -1,
                Name = "CreateClient",
                Args = new object[] { },
            };

            return (int)ClientPipe.CallSerializedFunction(pipe_id, f).Result;
        }

        public static void ReleaseClient(int pipe_id, int client_id)
        {
            var f = new SerializedFunction
            {
                ClientId = client_id,
                InterfaceId = -1,
                Name = "Release",
                Args = new object[] { }
            };

            ClientPipe.CallSerializedFunction(pipe_id, f);
        }

	public static void ConnectPipeToClient(int pipe_id, int client_id)
	{
	    ClientPipe.CallSerializedFunction(pipe_id, new SerializedFunction
		    {
			ClientId = client_id,
			InterfaceId = -1,
			Name = "ConnectPipeToClient",
			Args = new object[] { },
		    });
	}

        /// <summary>
        /// Aks the server to create its instance for this interface
        /// </summary>
        /// <param name="name"></param>
        /// <returns>The interface id created</returns>
        public static int CreateInterfaceNoUser(int pipe_id, string name)
        {
            var f = new SerializedFunction
            {
                ClientId = -1,
                InterfaceId = -1,
                Name = "CreateInterface",
                Args = new object[] { name },
            };

            return (int)ClientPipe.CallSerializedFunction(pipe_id, f).Result;
        }

        /// <summary>
        /// Aks the server to create its instance for this interface
        /// </summary>
        /// <param name="name"></param>
        /// <returns>The interface id created</returns>
        public static int CreateInterface(int pipe_id, int client_id, string name)
        {
            var f = new SerializedFunction
            {
                ClientId = client_id,
                InterfaceId = -1,
                Name = "CreateInterface",
                Args = new object[] { name },
            };

            return (int)ClientPipe.CallSerializedFunction(pipe_id, f).Result;
        }

        /// <summary>
        /// Get the next callback for this client from the server
        /// </summary>
        /// <returns>The next callback</returns>
        public static InternalCallbackMsg NextCallback(int pipe_id)
        {
            var f = new SerializedFunction
            {
                ClientId = -1,
                InterfaceId = -1,
                Name = "NextCallback",
                Args = new object[] { },
            };

            return (InternalCallbackMsg)ClientPipe.CallSerializedFunction(pipe_id, f).Result;
        }

        public static void SetAppId(int pipe_id, int app_id)
        {
            var f = new SerializedFunction
            {
                ClientId = -1,
                InterfaceId = -1,
                Name = "SetAppId",
                Args = new object[] { app_id },
            };

            ClientPipe.CallSerializedFunction(pipe_id, f);
        }
    }
}
