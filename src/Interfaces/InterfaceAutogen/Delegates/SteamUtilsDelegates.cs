using System;
using System.Runtime.InteropServices;

// Autogenerated @ 24/06/18
namespace InterfaceUtils
{
    /// <summary>
    /// Exports the delegates for all interfaces that implement SteamUtils
    /// </summary>
    [ArgonCore.Interface.Delegate(Name = "SteamUtils")]
    class SteamUtils_Delegates
    {
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate int GetSecondsSinceAppActiveDelegate(IntPtr _);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate int GetSecondsSinceComputerActiveDelegate(IntPtr _);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate uint GetConnectedUniverseDelegate(IntPtr _);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate long GetServerRealTimeDelegate(IntPtr _);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate string GetIPCountryDelegate(IntPtr _);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate bool GetImageSizeDelegate(IntPtr _, int image, ref System.UInt32 width, ref System.UInt32 height);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate bool GetImageRGBADelegate(IntPtr _, int image, IntPtr dest, int total_dest);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate bool GetCSERIPPortDelegate(IntPtr _, ref System.UInt32 ip, ref System.UInt16 port);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate System.Byte GetCurrentBatteryPowerDelegate(IntPtr _);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate int GetAppIdDelegate(IntPtr _);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate void SetOverlayNotificationPositionDelegate(IntPtr _, uint pos);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate bool IsAPICallCompletedDelegate(IntPtr _, int handle, ref System.Boolean failed);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate int GetAPICallFailureReasonDelegate(IntPtr _, int handle);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate bool GetAPICallResultDelegate(IntPtr _, int handle, IntPtr callback, int callback_size, int callback_expected, ref System.Boolean failed);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate void RunFrameDelegate(IntPtr _);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate uint GetIPCCallCountDelegate(IntPtr _);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate void SetWarningMessageHookDelegate(IntPtr _, IntPtr function);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate bool IsOverlayEnabledDelegate(IntPtr _);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate bool OverlayNeedsPresentDelegate(IntPtr _);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate int CheckFileSignatureDelegate(IntPtr _, string file_name);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate bool ShowGamePadTextInputDelegate(IntPtr _, uint input_mode, uint input_line_mode, IntPtr description, uint max_description, string existing_text);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate uint GetEnteredGamepadTextLengthDelegate(IntPtr _);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate bool GetEnteredGamepadTextInputDelegate(IntPtr _, string text, int length);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate string GetSteamUILanguageDelegate(IntPtr _);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate bool IsSteamRunningInVRDelegate(IntPtr _);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate void SetOverlayNotificationInsetDelegate(IntPtr _, int horizontal, int vertical);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate bool IsSteamInBigPictureModeDelegate(IntPtr _);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate void StartVRDashboardDelegate(IntPtr _);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate bool IsVRHeadsetStreamingEnabledDelegate(IntPtr _);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate void SetVRHeadsetStreamingEnabledDelegate(IntPtr _, bool enabled);
    }
}
