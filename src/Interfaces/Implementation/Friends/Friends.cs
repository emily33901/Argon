using System;

using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

using Server;
using Core;
using Core.Steam;

using SteamKit2;
using SteamKit2.Internal;

namespace InterfaceFriends
{
    // Original friends group
    class FriendsGroup
    {
        public static Dictionary<int, FriendsGroup> Active { get; set; } = new Dictionary<int, FriendsGroup>();
        public List<SteamID> Members { get; set; } = new List<SteamID>();
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public SteamID ChatRoom { get; set; }
    }

    // WebUI Chat room group
    class ChatRoomGroup
    {

    }

    class ClanGroup
    {
        public static Dictionary<SteamID, ClanGroup> Active { get; set; } = new Dictionary<SteamID, ClanGroup>();

        public SteamID Id { get; set; }
        public string Name { get; set; }

        public EAccountFlags AccountFlags { get; set; }
        public byte[] AvatarHash { get; set; }


        public uint MemberTotal { get; set; }
        public uint MemberOnlineCount { get; set; }
        public uint MemberChattingCount { get; set; }
        public uint MemberInGameCount { get; set; }

        public bool ChatRoomPrivate { get; set; }
        public SteamID ChatRoom { get; set; }

    }

    class ChatMessage
    {
        public string Message { get; set; }
        public SteamID Sender { get; set; }
        public EChatEntryType Type { get; set; }
    }

    class ChatRoom
    {
        public static Dictionary<SteamID, ChatRoom> Active { get; set; } = new Dictionary<SteamID, ChatRoom>();
        public List<ChatMessage> Messages { get; set; } = new List<ChatMessage>();
    }

    class Friends : ClientTied<Friends>
    {
        SteamFriends steam_friends;
        SteamUnifiedMessages unified_messages;

        public override void Init()
        {
            steam_friends = Instance.SteamClient.GetHandler<SteamFriends>();
            unified_messages = Instance.SteamClient.GetHandler<SteamUnifiedMessages>();

            Instance.CallbackManager.Subscribe<SteamFriends.ChatMsgCallback>(cb => OnChatMessage(cb));
            Instance.CallbackManager.Subscribe<SteamFriends.ChatEnterCallback>(cb => OnChatEnter(cb));
            Instance.CallbackManager.Subscribe<SteamFriends.PersonaStateCallback>(cb => OnPersonaState(cb));
            Instance.CallbackManager.Subscribe<SteamFriends.FriendMsgCallback>(cb => OnFriendMessage(cb));
            Instance.CallbackManager.Subscribe<SteamFriends.ClanStateCallback>(cb => OnClanState(cb));

            Instance.CallbackManager.Subscribe<SteamUnifiedMessages.ServiceMethodNotification>(cb => OnServiceMethod(cb));
            Instance.CallbackManager.Subscribe<SteamUnifiedMessages.ServiceMethodResponse>(cb => OnServiceMethodResponse(cb));

            Instance.PacketHandler.Subscribe<CMsgClientFriendsGroupsList>(EMsg.ClientFriendsGroupsList, cb => OnClientFriendsGroupsList(cb));
        }

        public string GetLocalName() => steam_friends.GetPersonaName();

        public int SetLocalName(string name)
        {
            steam_friends.SetPersonaName(name);
            return -1;
        }

        public EPersonaState GetLocalState() => steam_friends.GetPersonaState();
        public void SetLocalState(EPersonaState state)
        {
            Log.WriteLine("SetPersonaState: {0}", state);
            steam_friends.SetPersonaState(state);
        }


        public int GetFriendCount() => steam_friends.GetFriendCount();
        public SteamID GetFriendByIndex(int index) => steam_friends.GetFriendByIndex(index);
        public bool HasFriend(SteamID id)
        {
            var max = steam_friends.GetFriendCount();
            for (int i = 0; i < max; i++)
            {
                if (steam_friends.GetFriendByIndex(i) == id) return true;
            }

            return false;
        }

        public void AddFriend(SteamID s) => steam_friends.AddFriend(s);
        public void AddFriend(string s) => steam_friends.AddFriend(s);
        public void RemoveFriend(SteamID s) => steam_friends.RemoveFriend(s);
        public EFriendRelationship GetRelationship(SteamID s) => steam_friends.GetFriendRelationship(s);
        public EPersonaState GetFriendState(SteamID s) => steam_friends.GetFriendPersonaState(s);
        public GameID GamePlayed(SteamID s) => steam_friends.GetFriendGamePlayed(s);
        public string GetName(SteamID s) => steam_friends.GetFriendPersonaName(s);

        public void SendMsgToFriend(SteamID target, EChatEntryType type, string message)
        {
            steam_friends.SendChatMessage(target, type, message);
        }

        public string GetFriendNameHistory(SteamID s, int index)
        {
            return "";
        }

        public int GetBlockedFriendCount()
        {
            var total = 0;
            var max = steam_friends.GetFriendCount();
            for (int i = 0; i < max; i++)
            {
                var friend_id = steam_friends.GetFriendByIndex(i);
                if (steam_friends.GetFriendRelationship(friend_id) == EFriendRelationship.Blocked) total += 1;
            }

            return total;
        }

        void OnFriendMessage(SteamFriends.FriendMsgCallback cb)
        {
            Log.WriteLine("Msg from {0} {1} {2} \"{3}\"", cb.Sender, cb.EntryType, (int)cb.EntryType, cb.Message);

            var room_id = cb.Sender;

            var room = ChatRoom.Active.FindOrCreate(room_id);

            room.Messages.Add(new ChatMessage { Message = cb.Message, Sender = cb.Sender, Type = cb.EntryType });

            // TODO: Thread safety
            var message_index = room.Messages.Count - 1;

            var b = new Core.Util.Buffer();
            b.SetAlignment(4);

            b.WriteULong(cb.Sender);
            b.WriteULong(cb.Sender);
            b.Write((byte)cb.EntryType);
            b.Write((byte)(cb.FromLimitedAccount ? 1 : 0));
            // b.Write((byte)0);
            b.Write((uint)message_index);

            Instance.PostCallback(Common.CallbackId(Common.CallbackType.ClientFriends, 5), b);
        }

        void OnChatMessage(SteamFriends.ChatMsgCallback cb)
        {
            Log.WriteLine("OnChatMessage {0}", cb.ChatRoomID);

            var room = ChatRoom.Active.FindOrCreate(cb.ChatRoomID);

            room.Messages.Add(new ChatMessage { Message = cb.Message, Sender = cb.ChatterID, Type = cb.ChatMsgType });
        }

        void OnChatEnter(SteamFriends.ChatEnterCallback cb)
        {
            Log.WriteLine("OnChatEnter {0} {1} {2}", cb.ChatID.Render(), cb.ClanID.Render(), cb.FriendID.Render());
        }

        public ChatMessage GetChatMessage(SteamID s, int id)
        {
            if (ChatRoom.Active.TryGetValue(s, out var r))
            {
                if (id > r.Messages.Count - 1) return null;
                return r.Messages[id];
            }

            return null;
        }

        public int GetClanCount() => steam_friends.GetClanCount();
        public SteamID GetClanByIndex(int index) => steam_friends.GetClanByIndex(index);
        public string GetClanName(SteamID clan) => steam_friends.GetClanName(clan);
        public string GetClanTag(SteamID clan) => GetClanName(clan);

        public void OnPersonaState(SteamFriends.PersonaStateCallback cb)
        {
            if (cb.FriendID.IsClanAccount)
            {
                Log.WriteLine("Found clan {0} <{1}>", cb.FriendID, steam_friends.GetClanName(cb.FriendID));

                var c = ClanGroup.Active.FindOrCreate(cb.FriendID);

                c.Id = cb.FriendID;
                c.AvatarHash = steam_friends.GetClanAvatar(cb.FriendID);
                c.Name = steam_friends.GetClanName(cb.FriendID);
            }

            Core.Util.Buffer b = new Core.Util.Buffer();
            b.SetAlignment(4);

            b.WriteULong(cb.FriendID);
            b.WriteUInt((uint)cb.StatusFlags);

            Instance.PostCallback(Common.CallbackId(Common.CallbackType.SteamFriends, 4), b);
        }

        public void OnClanState(SteamFriends.ClanStateCallback cb)
        {
            foreach (var e in cb.Events)
            {
                Core.Util.Buffer b = new Core.Util.Buffer();
                b.SetAlignment(4);

                b.WriteULong(e.ID);
                b.WriteULong(cb.ClanID);
                // TODO: pad to 256 characters
                b.WriteString(e.Headline);
                b.WriteUInt(Platform.ToUnixTime(e.EventTime));
                b.WriteULong(e.GameID);

                Instance.PostCallback(Common.CallbackId(Common.CallbackType.ClientFriends, 2), b);
            }

            // TODO: do something with announcements

            var clan = ClanGroup.Active.FindOrCreate(cb.ClanID);

            var name_changed = clan.Name != cb.ClanName;
            var avatar_changed = clan.AvatarHash != cb.AvatarHash;
            var acc_info_changed = clan.AccountFlags != cb.AccountFlags;

            // Might be completely unnecessary
            if (name_changed || avatar_changed || acc_info_changed)
            {
                Core.Util.Buffer b = new Core.Util.Buffer();
                b.SetAlignment(4);

                b.WriteULong(cb.ClanID);
                b.WriteBool(name_changed);
                b.WriteBool(avatar_changed);
                b.WriteBool(acc_info_changed);
                Instance.PostCallback(Common.CallbackId(Common.CallbackType.ClientFriends, 19), b);
            }

            // Set all the new info that we have
            clan.Name = cb.ClanName;
            clan.AccountFlags = cb.AccountFlags;
            clan.AvatarHash = cb.AvatarHash;

            clan.ChatRoomPrivate = cb.ChatRoomPrivate;

            clan.MemberChattingCount = cb.MemberChattingCount;
            clan.MemberInGameCount = cb.MemberInGameCount;
            clan.MemberOnlineCount = cb.MemberOnlineCount;
            clan.MemberTotal = cb.MemberTotalCount;

            Log.WriteLine("OnClanState for {0} <{1}>", clan.Id, clan.Name);
        }

        public void OnServiceMethod(SteamUnifiedMessages.ServiceMethodNotification cb)
        {
            Log.WriteLine("OnServiceMethod for method '{0}'", cb.MethodName);

            if (cb.ServiceName == "ChatRoomClient")
            {
                switch (cb.RpcName)
                {
                    default:
                        Log.WriteLine("Unknown method `{0}` for service `{1}`", cb.RpcName, "ChatRoomClient");
                        break;
                }
            }

        }

        public void OnServiceMethodResponse(SteamUnifiedMessages.ServiceMethodResponse cb)
        {
            Log.WriteLine("OnServiceMethodResponse for method '{0}'", cb.MethodName);
        }

        public void OnClientFriendsGroupsList(ClientMsgProtobuf<CMsgClientFriendsGroupsList> cb)
        {
            Log.WriteLine("OnClientFriendsGroupList incremental: {0} removal: {1}", cb.Body.bincremental, cb.Body.bremoval);

            foreach (var g in cb.Body.friendGroups)
            {
                var group = FriendsGroup.Active.FindOrCreate(g.nGroupID);
                group.GroupId = g.nGroupID;
                group.GroupName = g.strGroupName;
            }

            foreach (var g in cb.Body.memberships)
            {
                var group = FriendsGroup.Active.FindOrCreate(g.nGroupID);
                group.ChatRoom = g.ulSteamID;
            }

            Log.WriteLine("There are now {0} friends groups", FriendsGroup.Active.Count());
        }

        // TODO: cache these
        public void GetFriendsGroupCount()
        {
        }
    }
}
