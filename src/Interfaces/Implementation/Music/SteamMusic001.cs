using System;

using ArgonCore;
using ArgonCore.Interface;

namespace InterfaceMusic
{
    [Impl(Name = "SteamMusic001", Implements = "SteamMusic", ServerMapped = true)]
    public class SteamMusic001 : IBaseInterface
    {
    }
}
