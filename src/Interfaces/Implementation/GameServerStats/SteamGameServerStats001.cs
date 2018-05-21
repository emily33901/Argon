using System;

using ArgonCore;
using ArgonCore.Interface;

namespace InterfaceGameServerStats
{
    [Impl(Name = "SteamGameServerStats001", Implements = "SteamGameServerStats", ServerMapped = true)]
    public class SteamGameServerStats001 : IBaseInterface
    {
    }
}
