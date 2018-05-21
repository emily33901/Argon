using System;

using ArgonCore;
using ArgonCore.Interface;

namespace InterfaceUGC
{
    [Impl(Name = "SteamUGC010", Implements = "SteamUGC", ServerMapped = true)]
    public class SteamUGC010 : IBaseInterface
    {
    }
}
