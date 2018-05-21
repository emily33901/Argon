using System;

using ArgonCore;
using ArgonCore.Interface;

namespace InterfaceMatchmaking
{
    [Impl(Name = "SteamMatchmaking009", Implements = "SteamMatchmaking", ServerMapped = true)]
    public class SteamMatchmaking009 : IBaseInterface
    {
    }
}
