using System;

using ArgonCore;
using ArgonCore.Interface;

namespace InterfaceVideo
{
    [Impl(Name = "SteamVideo002", Implements = "SteamVideo", ServerMapped = true)]
    public class SteamVideo002 : IBaseInterface
    {
    }
}
