using System;

using ArgonCore;
using ArgonCore.Interface;

namespace InterfaceGameServer
{
    [Impl(Name = "SteamGameServer012", Implements = "SteamGameServer", ServerMapped = true)]
    public class SteamGameServer012 : IBaseInterface
    {
    }
}
