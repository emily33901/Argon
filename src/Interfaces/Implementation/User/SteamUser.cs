using System;

using ArgonCore;
using ArgonCore.Interface;
using ArgonCore.Server;

namespace InterfaceUser
{
    public class User
    {
        IBaseInterface Parent { get; set; }

        public int ClientId { get { return Parent.ClientId; } }

        public User(IBaseInterface parent)
        {
            Parent = parent;
        }

        public bool LoggedOn()
        {
            return Client.GetClient(ClientId).Connected;
        }
    }
}