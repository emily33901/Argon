using System;

// Autogenerated @ 11/08/18
namespace InterfaceUtils
{
    /// <summary>
    /// Implements the map for interface SteamUtils009
    /// </summary>
    [ArgonCore.Interface.Map(Name = "SteamUtils009")]
    public class SteamUtils009_Map : ArgonCore.Interface.IBaseInterfaceMap
    {
        public int GetSecondsSinceAppActive(IntPtr _)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new ArgonCore.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "GetSecondsSinceAppActive",
                Args = new object[] {},

            });



            return (int)result.Result;
        }
        public int GetSecondsSinceComputerActive(IntPtr _)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new ArgonCore.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "GetSecondsSinceComputerActive",
                Args = new object[] {},

            });



            return (int)result.Result;
        }
        public uint GetConnectedUniverse(IntPtr _)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new ArgonCore.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "GetConnectedUniverse",
                Args = new object[] {},

            });



            return (uint)result.Result;
        }
        public long GetServerRealTime(IntPtr _)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new ArgonCore.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "GetServerRealTime",
                Args = new object[] {},

            });



            return (long)result.Result;
        }
        public string GetIPCountry(IntPtr _)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new ArgonCore.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "GetIPCountry",
                Args = new object[] {},

            });



            return (string)result.Result;
        }
        public bool GetImageSize(IntPtr _, int image, ref uint width, ref uint height)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new ArgonCore.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "GetImageSize",
                Args = new object[] {image, width, height},

            });

            width = (uint)result.Args[1];
            height = (uint)result.Args[2];


            return (bool)result.Result;
        }
        public bool GetImageRGBA(IntPtr _, int image, IntPtr dest, int total_dest)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new ArgonCore.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "GetImageRGBA",
                Args = new object[] {image, dest, total_dest},

            });



            return (bool)result.Result;
        }
        public bool GetCSERIPPort(IntPtr _, ref uint ip, ref short port)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new ArgonCore.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "GetCSERIPPort",
                Args = new object[] {ip, port},

            });

            ip = (uint)result.Args[0];
            port = (short)result.Args[1];


            return (bool)result.Result;
        }
        public System.Byte GetCurrentBatteryPower(IntPtr _)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new ArgonCore.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "GetCurrentBatteryPower",
                Args = new object[] {},

            });



            return (System.Byte)result.Result;
        }
        public int GetAppId(IntPtr _)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new ArgonCore.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "GetAppId",
                Args = new object[] {},

            });



            return (int)result.Result;
        }
        public void SetOverlayNotificationPosition(IntPtr _, uint pos)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new ArgonCore.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "SetOverlayNotificationPosition",
                Args = new object[] {pos},

            });



        }
        public bool IsAPICallCompleted(IntPtr _, int handle, ref bool failed)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new ArgonCore.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "IsAPICallCompleted",
                Args = new object[] {handle, failed},

            });

            failed = (bool)result.Args[1];


            return (bool)result.Result;
        }
        public int GetAPICallFailureReason(IntPtr _, int handle)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new ArgonCore.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "GetAPICallFailureReason",
                Args = new object[] {handle},

            });



            return (int)result.Result;
        }
        public bool GetAPICallResult(IntPtr _, int handle, IntPtr callback, int callback_size, int callback_expected, ref bool failed)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new ArgonCore.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "GetAPICallResult",
                Args = new object[] {handle, callback, callback_size, callback_expected, failed},

            });

            failed = (bool)result.Args[4];


            return (bool)result.Result;
        }
        public void RunFrame(IntPtr _)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new ArgonCore.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "RunFrame",
                Args = new object[] {},

            });



        }
        public uint GetIPCCallCount(IntPtr _)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new ArgonCore.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "GetIPCCallCount",
                Args = new object[] {},

            });



            return (uint)result.Result;
        }
        public void SetWarningMessageHook(IntPtr _, IntPtr function)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new ArgonCore.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "SetWarningMessageHook",
                Args = new object[] {function},

            });



        }
        public bool IsOverlayEnabled(IntPtr _)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new ArgonCore.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "IsOverlayEnabled",
                Args = new object[] {},

            });



            return (bool)result.Result;
        }
        public bool OverlayNeedsPresent(IntPtr _)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new ArgonCore.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "OverlayNeedsPresent",
                Args = new object[] {},

            });



            return (bool)result.Result;
        }
        public int CheckFileSignature(IntPtr _, string file_name)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new ArgonCore.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "CheckFileSignature",
                Args = new object[] {file_name},

            });



            return (int)result.Result;
        }
        public bool ShowGamePadTextInput(IntPtr _, uint input_mode, uint input_line_mode, IntPtr description, uint max_description, string existing_text)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new ArgonCore.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "ShowGamePadTextInput",
                Args = new object[] {input_mode, input_line_mode, description, max_description, existing_text},

            });



            return (bool)result.Result;
        }
        public uint GetEnteredGamepadTextLength(IntPtr _)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new ArgonCore.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "GetEnteredGamepadTextLength",
                Args = new object[] {},

            });



            return (uint)result.Result;
        }
        public bool GetEnteredGamepadTextInput(IntPtr _, string text, int length)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new ArgonCore.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "GetEnteredGamepadTextInput",
                Args = new object[] {text, length},

            });



            return (bool)result.Result;
        }
        public string GetSteamUILanguage(IntPtr _)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new ArgonCore.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "GetSteamUILanguage",
                Args = new object[] {},

            });



            return (string)result.Result;
        }
        public bool IsSteamRunningInVR(IntPtr _)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new ArgonCore.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "IsSteamRunningInVR",
                Args = new object[] {},

            });



            return (bool)result.Result;
        }
        public void SetOverlayNotificationInset(IntPtr _, int horizontal, int vertical)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new ArgonCore.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "SetOverlayNotificationInset",
                Args = new object[] {horizontal, vertical},

            });



        }
        public bool IsSteamInBigPictureMode(IntPtr _)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new ArgonCore.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "IsSteamInBigPictureMode",
                Args = new object[] {},

            });



            return (bool)result.Result;
        }
        public void StartVRDashboard(IntPtr _)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new ArgonCore.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "StartVRDashboard",
                Args = new object[] {},

            });



        }
        public bool IsVRHeadsetStreamingEnabled(IntPtr _)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new ArgonCore.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "IsVRHeadsetStreamingEnabled",
                Args = new object[] {},

            });



            return (bool)result.Result;
        }
        public void SetVRHeadsetStreamingEnabled(IntPtr _, bool enabled)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new ArgonCore.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "SetVRHeadsetStreamingEnabled",
                Args = new object[] {enabled},

            });



        }
    }
}
