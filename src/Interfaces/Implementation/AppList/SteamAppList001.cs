﻿using System;

using ArgonCore;
using ArgonCore.Interface;

namespace InterfaceAppList
{
    [Impl(Name = "SteamAppList001", Implements = "SteamAppList", ServerMapped = true)]
    public class SteamAppList001 : IBaseInterface
    {
    }
}