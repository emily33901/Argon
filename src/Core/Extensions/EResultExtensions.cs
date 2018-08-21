using System;

using SteamKit2;

namespace Core.Extensions
{
    public static class EResultExtensions
    {
        public static string ExtendedString(this EResult result)
        {
            return String.Format("EResult.{0} [{1}]", result, (uint)result);
        }
    }
}