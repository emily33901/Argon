using System;

using ArgonCore;
using ArgonCore.Interface;

namespace InterfaceInventory
{
    [Impl(Name = "SteamInventory003", Implements = "SteamInventory", ServerMapped = true)]
    public class SteamInventory003 : IBaseInterface
    {
    }
}
