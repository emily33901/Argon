using System;

using ArgonCore;
using ArgonCore.Interface;
using ArgonCore.Server;

using System.Runtime.InteropServices;

using SteamKit2;

namespace InterfaceUtils
{
    [Impl(Name = "SteamUtils009", Implements = "SteamUtils", ServerMapped = true)]
    public class SteamUtils009 : IBaseInterface
    {
        static Logger Log { get; set; } = new Logger("SteamUtils");

        public uint GetSecondsSinceAppActive()
        {
            return 1;
        }

        public uint GetSecondsSinceComputerActive()
        {
            return 1;
        }

        public uint GetConnectedUniverse()
        {
            return (uint)EUniverse.Public;
        }
        public long GetServerRealTime()
        {
            return ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds();
        }

        public string GetIPCountry()
        {
            return "";
        }

        public bool GetImageSize(int image, ref uint width, ref uint height)
        {
            return false;
        }

        public bool GetImageRGBA(int image, IntPtr dest, int total_dest)
        {
            return false;
        }

        public bool GetCSERIPPort(ref uint ip, ref ushort port)
        {
            return false;
        }

        public byte GetCurrentBatteryPower()
        {
            return 255;
        }

        public uint GetAppId()
        {
            return 0;
        }

        public void SetOverlayNotificationPosition(uint pos)
        {

        }

        public bool IsAPICallCompleted(int handle, ref bool failed)
        {
            failed = false;
            return AsyncCallManager.IsCallFinished(handle);
        }

        public int GetAPICallFailureReason(int handle)
        {
            // TODO: we should probably add something to accurately reflect this...
            return -1;
        }

        public bool GetAPICallResult(int handle, IntPtr callback, int callback_size, int callback_expected, ref bool failed)
        {
            failed = false;

            var result = AsyncCallManager.GetCallResult(handle);

            if (result == null) return false;

            if (callback_size != result.Length)
            {
                Log.WriteLine("callback_size != result.Length for callback {0}", callback_expected);
            }

            Marshal.Copy(result, 0, callback, callback_size);

            return true;
        }

        public void RunFrame()
        {

        }

        public uint GetIPCCallCount()
        {
            return 0;
        }

        public void SetWarningMessageHook(IntPtr function)
        {

        }

        public bool IsOverlayEnabled()
        {
            return true;
        }

        public bool OverlayNeedsPresent()
        {
            return false;
        }

        public int CheckFileSignature(string file_name)
        {
            return 0;
        }

        public bool ShowGamePadTextInput(uint input_mode, uint input_line_mode, IntPtr description, uint max_description, string existing_text)
        {
            return false;
        }

        public uint GetEnteredGamepadTextLength()
        {
            return 0;
        }

        public bool GetEnteredGamepadTextInput(string text, int length)
        {
            return false;
        }

        public string GetSteamUILanguage()
        {
            return "E N G L I S H";
        }

        public bool IsSteamRunningInVR()
        {
            return false;
        }

        public void SetOverlayNotificationInset(int horizontal, int vertical)
        {

        }

        public bool IsSteamInBigPictureMode()
        {
            return false;
        }

        public void StartVRDashboard()
        {

        }

        public bool IsVRHeadsetStreamingEnabled()
        {
            return false;
        }

        public void SetVRHeadsetStreamingEnabled(bool enabled)
        {

        }
    }
}
