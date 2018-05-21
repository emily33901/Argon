using System;

using ArgonCore;
using ArgonCore.Interface;

namespace InterfaceHTTP
{
    [Impl(Name = "SteamHTTP002", Implements = "SteamHTTP", ServerMapped = true)]
    public class SteamHTTP002 : IBaseInterface
    {
    }
}
