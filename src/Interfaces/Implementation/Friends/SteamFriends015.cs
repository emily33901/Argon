using System;

using ArgonCore.Interface;

namespace InterfaceFriends
{
    [Impl(Name = "SteamFriends015", ServerMapped = true)]
    public class SteamFriends015 : IBaseInterface
    {
        private Friends f { get { return Friends.FindOrCreate(ClientId); } }

        public string GetPersonaName()
        {
            return f.GetLocalName();
        }

        public int SetPersonaName(string name)
        {
            return f.SetLocalName(name);
        }

        public uint GetPersonaState()
        {
            return (uint)f.GetLocalState();
        }

        public int GetFriendCount()
        {
            return f.GetFriendCount();
        }

        public ulong GetFriendByIndex(int index)
        {
            return f.GetFriendByIndex(index);
        }
        public uint GetFriendRelationship(ulong steam_id)
        {
            return (uint)f.GetRelationship(new SteamKit2.SteamID(steam_id));
        }

        public uint GetFriendPersonaState(ulong steam_id)
        {
            return (uint)f.GetFriendState(new SteamKit2.SteamID(steam_id));
        }

        public string GetFriendPersonaName(ulong steam_id)
        {
            return f.GetName(new SteamKit2.SteamID(steam_id));
        }

        public bool GetFriendGamePlayed(ulong steam_id, IntPtr friend_game_info_out)
        {
            return false;
        }

        public string GetFriendPersonaNameHistory(ulong steam_id, int index)
        {
            return "";
        }

        public int GetFriendSteamLevel(ulong steam_id)
        {
            return 0;
        }

        public string GetPlayerNickname(ulong steam_id)
        {
            return GetFriendPersonaName(steam_id);
        }

        // TODO: for groups we need to implement the FriendsGroupID_t
        // THESE ARE NOT CLANS!
        public int GetFriendsGroupCount()
        {
            return 0;
        }

        public ushort GetGroupIdByIndex(int index)
        {
            return 0;
        }

        public string GetFriendsGroupName(ushort id)
        {
            return "";
        }

        public int GetFriendsGroupMembersCount(ushort id)
        {
            return 0;
        }

        public void GetFriendsGroupMembersList(ushort id, ref IntPtr steam_id_out, int max_steam_id_out)
        {

        }

        public bool HasFriend(ulong steam_id)
        {
            return f.HasFriend(new SteamKit2.SteamID(steam_id));
        }

        public int GetClanCount()
        {
            return f.GetClanCount();
        }

        public ulong GetClanByIndex(int index)
        {
            return f.GetClanByIndex(index);
        }

        public string GetClanName(ulong steam_id)
        {
            return f.GetClanName(new SteamKit2.SteamID(steam_id));
        }

        public string GetClanTag(ulong steam_id)
        {
            return f.GetClanTag(new SteamKit2.SteamID(steam_id));
        }

        public bool GetClanActivityCounts(ulong steam_id, ref int online, ref int in_game, ref int chatting)
        {
            return false;
        }

        public int DownloadClanActivityCounts(ulong[] clans, int count)
        {
            // async call
            return 0;
        }

        public int GetFriendCountFromSource(ulong source_id)
        {
            return 0;
        }

        public ulong GetFriendFromSourceByIndex(ulong source_id, int index)
        {
            return 0;
        }

        public bool IsUserInSource(ulong steam_id, ulong source_id)
        {
            return false;
        }

        public void SetInGameVoiceSpeaking(ulong steam_id, bool speaking)
        {

        }

        public void ActivateGameOverlay(string dialog)
        {

        }

        public void ActiveGameOverlayToUser(string dialog, ulong steam_id)
        {

        }

        public void ActiveGameOverlayToWebPage(string url)
        {

        }

        public void ActivateGameOverlayToStore(uint app_id, uint flag)
        {

        }

        public void SetPlayedWith(ulong steam_id)
        {

        }

        public void ActivateGameOverlayInviteDialog(ulong steam_id)
        {

        }

        public int GetSmallFriendAvatar(ulong steam_id)
        {
            return 0;
        }
        public int GetMediumFriendAvatar(ulong steam_id)
        {
            return 0;
        }
        public int GetLargeFriendAvatar(ulong steam_id)
        {
            return 0;
        }

        public bool RequestUserInformation(ulong steam_id, bool require_name_only)
        {
            return true;
        }

        public int RequestClanOfficerList(ulong steam_id)
        {
            return 0;
        }

        public ulong GetClanOwner(ulong steam_id)
        {
            return 0;
        }

        public int GetClanOfficerCount(ulong steam_id)
        {
            return 0;
        }

        public ulong GetClanOfficerByIndex(ulong clan, int officer)
        {
            return 0;
        }

        public uint GetUserRestrictions()
        {
            return 0;
        }

        public bool SetRichPresence(string key, string value)
        {
            return false;
        }

        public bool ClearRichPresence()
        {
            return false;
        }

        public string GetFriendRichPresence(ulong steam_id, string key)
        {
            return "";
        }

        public int GetFriendRichPresenceKeyCount(ulong steam_id)
        {
            return 0;
        }

        public string GetFriendRichPresenceKeyByIndex(ulong steam_id, int key)
        {
            return "";
        }

        public void RequestFriendRichPresence(ulong steam_id)
        {

        }

        public bool InviteUserToGame(ulong steam_id, string connect)
        {
            return false;
        }

        public int GetCoplayFriendCount()
        {
            return 0;
        }

        public ulong GetCoplayFriend(int index)
        {
            return 0;
        }

        public int GetFriendCoplayTime(ulong steam_id)
        {
            return 0;
        }

        public uint GetFriendCoplayGame(ulong steam_id)
        {
            return 0;
        }

        public int JoinClanChatRoom(ulong steam_id)
        {
            // async job
            return 0;
        }

        public bool LeaveClanChatRoom(ulong steam_id)
        {
            return false;
        }

        public int GetClanChatMemberCount(ulong steam_id)
        {
            return 0;
        }

        public ulong GetChatMemberByIndex(ulong steam_id, int index)
        {
            return 0;
        }

        public bool SendClanChatMessage(ulong steam_id, string msg)
        {
            return false;
        }

        public int GetClanChatMessage(ulong steam_id, int index, IntPtr text_out, int max_text, uint chat_type, ref ulong chater_id)
        {
            return 0;
        }

        public bool IsClanChatAdmin(ulong chat_id, ulong user_id)
        {
            return false;
        }

        public bool IsClanChatWindowOpenInSteam(ulong steam_id)
        {
            return false;
        }

        public bool OpenClanChatWindowInSteam(ulong steam_id)
        {
            return false;
        }

        public bool CloseClanChatWindowInSteam(ulong steam_id)
        {
            return false;
        }

        public bool SetListenForFriendsMessages(bool intercept)
        {
            return false;
        }

        public bool ReplyToFriendMessage(ulong steam_id, string msg)
        {
            return false;
        }

        public int GetFriendMessage(ulong steam_id, int id, IntPtr data_out, int max_data_out, ref uint chat_type)
        {
            var message = f.GetChatMessage(new SteamKit2.SteamID(steam_id), id);

            ArgonCore.Util.Buffer b = new ArgonCore.Util.Buffer();

            b.Write(message.Message);
            var message_length = message.Message.Length;

            var write_length = message_length < max_data_out ? message_length : max_data_out;

            System.Runtime.InteropServices.Marshal.Copy(b.GetBuffer(), 0, data_out, write_length);

            return write_length;
        }

        public int GetFollowerCount(ulong steam_id)
        {
            // async call
            return 0;
        }

        public int IsFollowing(ulong steam_id)
        {
            // async call
            return 0;
        }

        public int EnumerateFollowingList(uint starting_index)
        {
            // async call
            return 0;
        }

        public bool IsClanPublic(ulong steam_id)
        {
            return false;
        }

        public bool IsClanOfficialGameGroup(ulong steam_id)
        {
            return false;
        }
    }
}