using System;

using System.Collections.Generic;

using Server;
using ArgonCore;

using SteamKit2;

namespace InterfaceFriends
{
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

        public override void Init()
        {
            steam_friends = Instance.SteamClient.GetHandler<SteamFriends>();
            Instance.CallbackManager.Subscribe<SteamFriends.ChatMsgCallback>(cb => OnChatMessage(cb));
            Instance.CallbackManager.Subscribe<SteamFriends.PersonaStateCallback>(cb => OnPersonaState(cb));
            Instance.CallbackManager.Subscribe<SteamFriends.FriendMsgCallback>(cb => OnFriendMessage(cb));
        }

        public string GetLocalName() => steam_friends.GetPersonaName();

        public int SetLocalName(string name)
        {
            return AsyncCallManager.RegisterAsyncJob(
                steam_friends.SetPersonaName(name), 347, ClientId);
        }

        public EPersonaState GetLocalState() => steam_friends.GetPersonaState();
        public void SetLocalState(EPersonaState state) => steam_friends.SetPersonaState(state);

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
                if (steam_friends.GetFriendRelationship(friend_id) == EFriendRelationship.Blocked)
                    total += 1;
            }

            return total;
        }

        void OnFriendMessage(SteamFriends.FriendMsgCallback cb)
        {
            Log.WriteLine("Msg from {0} {1} \"{2}\"", cb.Sender, cb.EntryType, cb.Message);

            var room_id = cb.Sender;

            var room = ChatRoom.Active.FindOrCreate(room_id);

            room.Messages.Add(new ChatMessage { Message = cb.Message, Sender = cb.Sender, Type = cb.EntryType });

            ArgonCore.Util.Buffer b = new ArgonCore.Util.Buffer();
            b.Write(cb.Sender);
            b.Write(cb.Sender);
            b.Write(cb.EntryType);
            b.Write(cb.FromLimitedAccount);
            b.Write(room.Messages.Count);

            Instance.PostCallback(805, b);
        }

        void OnChatMessage(SteamFriends.ChatMsgCallback cb)
        {
            Log.WriteLine("Chat message recieved...");

            var room_id = cb.ChatRoomID;

            var room = ChatRoom.Active.FindOrCreate(room_id);

            room.Messages.Add(new ChatMessage { Message = cb.Message, Sender = cb.ChatterID, Type = cb.ChatMsgType });
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
            ArgonCore.Util.Buffer b = new ArgonCore.Util.Buffer();

            b.Write(cb.FriendID);
            b.Write(cb.StatusFlags);

            Instance.PostCallback(304, b);
        }
    }
}