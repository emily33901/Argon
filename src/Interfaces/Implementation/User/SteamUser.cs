using System;

using ArgonCore;
using ArgonCore.Interface;
using ArgonCore.Server;

namespace InterfaceUser
{
    public class User
    {
        IBaseInterface Parent { get; set; }

        public uint ClientId { get { return (uint)Parent.ClientId;  } }

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