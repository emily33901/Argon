using System;

using System.Runtime.InteropServices;

using ArgonCore;
using ArgonCore.Interface;

namespace InterfaceFriends
{
    [Impl(Name = "SteamFriends001", Implements = "SteamFriends", ServerMapped = true)]
    public class SteamFriends001 : IBaseInterface
    {
        private Friends f;

        public SteamFriends001()
        {
            f = Friends.FindOrCreate(ClientId);
        }

        public string GetPersonaName()
        {
            return f.GetLocalName();
        }

        public uint GetPersonaState()
        {
            return (uint)f.GetLocalState();
        }

        public void SetPersonaState(uint state)
        {
            f.SetLocalState((SteamKit2.EPersonaState)state);
        }

        public bool AddFriend(ulong steam_id)
        {
            f.AddFriend(new SteamKit2.SteamID(steam_id));

            return true;
        }

        public bool RemoveFriend(ulong steam_id)
        {
            f.RemoveFriend(new SteamKit2.SteamID(steam_id));

            return true;
        }

        public bool HasFriend(ulong steam_id)
        {
            return f.HasFriend(new SteamKit2.SteamID(steam_id));
        }

        public uint GetFriendRelationship(ulong steam_id)
        {
            return (uint)f.GetRelationship(new SteamKit2.SteamID(steam_id));
        }

        public uint GetFriendPersonaState(ulong steam_id)
        {
            return (uint)f.GetFriendState(new SteamKit2.SteamID(steam_id));
        }

        public bool Deprecated_GetFriendGamePlayed(ulong steam_id, ref uint app_id, ref uint game_ip, ref uint game_port)
        {
            Console.WriteLine("Deprecated_GetFriendGamePlayed called!");

            var game_id = f.GamePlayed(new SteamKit2.SteamID(steam_id));
            app_id = game_id.AppID;

            return game_id.AppID != 0;
        }

        public string GetFriendPersonaName(ulong steam_id)
        {
            return f.GetName(new SteamKit2.SteamID(steam_id));
        }

        // TODO: this returns a HSteamCall, but that isnt a thing anymore??
        // For now we just perform the action and then return 0
        public int AddFriendByName(string name)
        {
            f.AddFriend(name);
            return 0;
        }

        public int GetFriendCount()
        {
            return f.GetFriendCount();
        }

        public ulong GetFriendByIndex(int index)
        {
            return f.GetFriendByIndex(index);
        }

        public void SendMsgToFriend(ulong dest_steam_id, uint msg_type, string message_body)
        {
            var steam_id = new SteamKit2.SteamID(dest_steam_id);

            f.SendMsgToFriend(steam_id, (SteamKit2.EChatEntryType)msg_type, message_body);
        }

        public void SetFriendRegValue(ulong steam_id, string key, string value)
        {
            // TODO: hook up to configstore
        }

        public string GetFriendRegValue(ulong steam_id, string key)
        {
            // TODO: hook up to configstore
            return "";
        }

        public string GetFriendPersonaNameHistory(ulong steam_id, int index)
        {
            return "";
        }

        public int GetChatMessage(ulong steam_id, int msg_index, IntPtr msg_out, int max_msg, ref uint msg_type)
        {
            var id = new SteamKit2.SteamID(steam_id);
            var cm = f.GetChatMessage(id, msg_index);

            if (cm != null)
            {
                var total_wrote = Math.Max(max_msg, cm.Message.Length);
                Marshal.Copy(System.Text.Encoding.ASCII.GetBytes(cm.Message), 0, msg_out, max_msg);
                msg_type = (uint)cm.Type;

                return total_wrote;
            }

            return 0;
        }

        public bool SendMsgToFriend(ulong steam_id, uint type, string message, int length)
        {
            f.SendMsgToFriend(new SteamKit2.SteamID(steam_id), (SteamKit2.EChatEntryType)type, message);

            return true;
        }

        public int GetChatIDOfChatHistoryStart(ulong steam_id)
        {
            Console.WriteLine("GetChatIDOfChatHistoryStart should never be called!");
            return 0;
        }

        public void SetChatHistoryStart(ulong steam_id, int index)
        {
            Console.WriteLine("SetChatHistoryStart should never be called!");
        }

        public void ClearChatHistory(ulong steam_id)
        {
        }

        public int InviteFriendByEmail(string email)
        {
            f.AddFriend(email);

            return 0;
        }

        public int GetBlockedFriendCount()
        {
            return f.GetBlockedFriendCount();
        }

        public bool GetFriendGamePlayed(ulong steam_id, ref ulong game_id, ref uint server_ip, ref ushort server_port)
        {
            var id = new SteamKit2.SteamID(steam_id);
            game_id = f.GamePlayed(id);

            return true;
        }
        public bool GetFriendGamePlayed2(ulong steam_id, ref ulong game_id, ref uint server_ip, ref ushort server_port)
        {
            return GetFriendGamePlayed(steam_id, ref game_id, ref server_ip, ref server_port);
        }

    }
}
