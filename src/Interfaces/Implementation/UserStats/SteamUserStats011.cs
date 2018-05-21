using System;

using ArgonCore;
using ArgonCore.Interface;

namespace InterfaceUserStats
{
    [Impl(Name = "SteamUserStats011", Implements = "SteamUserStats", ServerMapped = true)]
    public class SteamUserStats011 : IBaseInterface
    {
    }
}
