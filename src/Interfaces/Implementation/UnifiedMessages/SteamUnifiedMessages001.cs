﻿using System;

using ArgonCore;
using ArgonCore.Interface;

namespace InterfaceUnifiedMessages
{
    [Impl(Name = "SteamUnifiedMessages001", Implements = "SteamUnifiedMessages", ServerMapped = true)]
    public class SteamUnifiedMessages001 : IBaseInterface
    {
    }
}