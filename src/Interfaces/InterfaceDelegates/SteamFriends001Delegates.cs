using System;
using System.Runtime.InteropServices;

// Autogenerated @ 21/08/18
namespace InterfaceFriends
{
    /// <summary>
    /// Exports the delegates for all interfaces that implement SteamFriends001
    /// </summary>
    [Core.Interface.Delegate(Name = "SteamFriends001")]
    class SteamFriends001_Delegates
    {
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate string GetPersonaName(IntPtr _);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate uint GetPersonaState(IntPtr _);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate void SetPersonaState(IntPtr _, uint state);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate bool AddFriend(IntPtr _, ulong steam_id);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate bool RemoveFriend(IntPtr _, ulong steam_id);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate bool HasFriend(IntPtr _, ulong steam_id);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate uint GetFriendRelationship(IntPtr _, ulong steam_id);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate uint GetFriendPersonaState(IntPtr _, ulong steam_id);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate bool Deprecated_GetFriendGamePlayed(IntPtr _, ulong steam_id, ref uint app_id, ref uint game_ip, ref uint game_port);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate string GetFriendPersonaName(IntPtr _, ulong steam_id);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate int AddFriendByName(IntPtr _, string name);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate int GetFriendCount(IntPtr _);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate ulong GetFriendByIndex(IntPtr _, int index);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate void SendMsgToFriend(IntPtr _, ulong dest_steam_id, uint msg_type, string message_body);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate void SetFriendRegValue(IntPtr _, ulong steam_id, string key, string value);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate string GetFriendRegValue(IntPtr _, ulong steam_id, string key);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate string GetFriendPersonaNameHistory(IntPtr _, ulong steam_id, int index);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate int GetChatMessage(IntPtr _, ulong steam_id, int msg_index, IntPtr b_pointer, int b_length, ref uint msg_type);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate bool SendMsgToFriend2(IntPtr _, ulong steam_id, uint type, string message, int length);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate int GetChatIDOfChatHistoryStart(IntPtr _, ulong steam_id);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate void SetChatHistoryStart(IntPtr _, ulong steam_id, int index);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate void ClearChatHistory(IntPtr _, ulong steam_id);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate int InviteFriendByEmail(IntPtr _, string email);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate int GetBlockedFriendCount(IntPtr _);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate bool GetFriendGamePlayed(IntPtr _, ulong steam_id, ref ulong game_id, ref uint server_ip, ref short server_port);
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        public delegate bool GetFriendGamePlayed2(IntPtr _, ulong steam_id, ref ulong game_id, ref uint server_ip, ref short server_port);
    }
}
