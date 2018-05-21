using System;

using ArgonCore;
using ArgonCore.Interface;

namespace InterfaceMatchmakingServers
{
    [Impl(Name = "SteamMatchmakingServers002", Implements = "SteamMatchmakingServers", ServerMapped = true)]
    public class SteamMatchmakingServers002 : IBaseInterface
    {
    }
}
