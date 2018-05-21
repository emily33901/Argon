using System;

using ArgonCore;
using ArgonCore.Interface;

namespace InterfaceMusicRemote
{
    [Impl(Name = "SteamMusicRemote001", Implements = "SteamMusicRemote", ServerMapped = true)]
    public class SteamMusicRemote001 : IBaseInterface
    {
    }
}
