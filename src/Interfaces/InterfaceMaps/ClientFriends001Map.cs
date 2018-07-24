using System;

// Autogenerated @ 23/07/18
namespace InterfaceFriends
{
    /// <summary>
    /// Implements the map for interface CLIENTFRIENDS_INTERFACE_VERSION001
    /// </summary>
    [ArgonCore.Interface.Map(Name = "CLIENTFRIENDS_INTERFACE_VERSION001")]
    public class ClientFriends001_Map : ArgonCore.Interface.IBaseInterfaceMap
    {
        public string GetPersonaName(IntPtr _)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "GetPersonaName",
               Args = new object[] {},
            };
            return Client.ClientPipe.CallSerializedFunction<string>(PipeId, f);
        }
        public void SetPeronaName(IntPtr _, string name)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "SetPeronaName",
               Args = new object[] {name},
            };
            Client.ClientPipe.CallSerializedFunction(PipeId, f);
        }
        public int SetPersonaNameEx(IntPtr _, string name, bool send_cb)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "SetPersonaNameEx",
               Args = new object[] {name, send_cb},
            };
            return Client.ClientPipe.CallSerializedFunction<int>(PipeId, f);
        }
        public bool IsPersonaNameSet(IntPtr _)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "IsPersonaNameSet",
               Args = new object[] {},
            };
            return Client.ClientPipe.CallSerializedFunction<bool>(PipeId, f);
        }
        public int GetPersonaState(IntPtr _)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "GetPersonaState",
               Args = new object[] {},
            };
            return Client.ClientPipe.CallSerializedFunction<int>(PipeId, f);
        }
        public void SetPersonaState(IntPtr _, SteamKit2.EPersonaState state)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "SetPersonaState",
               Args = new object[] {state},
            };
            Client.ClientPipe.CallSerializedFunction(PipeId, f);
        }
    }
}