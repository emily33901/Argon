using System;

using SteamKit2;

namespace ArgonCore.Extensions
{
    public static class EResultExtensions
    {
        public static string ExtendedString(this EResult result)
        {
            return String.Format("EResult.{0}", result);
        }
    }
}