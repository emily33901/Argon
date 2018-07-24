using System;

using System.Runtime.InteropServices;

using ArgonCore.Interface;

namespace InterfaceFriends
{
    [Impl(Name = "CLIENTFRIENDS_INTERFACE_VERSION001", ServerMapped = true)]
    class ClientFriends001 : IBaseInterface
    {
        private Friends f { get { return Friends.FindOrCreate(ClientId); } }

        public string GetPersonaName() => f.GetLocalName();

        public void SetPeronaName(string name) => f.SetLocalName(name);
        public int SetPersonaNameEx(string name, bool send_cb) => f.SetLocalName(name);

        public bool IsPersonaNameSet() => f.GetLocalName() != null;

        public int GetPersonaState() => (int)f.GetLocalState();
        public void SetPersonaState(SteamKit2.EPersonaState state) => f.SetLocalState(state);
    }
}