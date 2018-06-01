using System;

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
            f = new Friends();
        }

        public string GetPersonaName()
        {

        }
    }
}
