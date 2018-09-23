
namespace Core.Steam
{
    public class Common
    {
        public enum CallbackType : int
        {
            SteamUser = 100,
            SteamGameServer = 200,
            SteamFriends = 300,
            SteamBilling = 400,
            SteamMatchmaking = 500,
            SteamContentServer = 600,
            SteamUtils = 700,
            ClientFriends = 800,
            ClientUser = 900,
            SteamApps = 1000,
            SteamUserStats = 1100,
            SteamNetworking = 1200,
            ClientRemoteStorage = 1300,
            SteamUserItems = 1400,
            SteamGameServerItems = 1500,
            ClientUtils = 1600,
            SteamGameCoordinator = 1700,
            SteamGameServerStats = 1800,
            Steam2Async = 1900,
            SteamGameStats = 2000,
            ClientHTTP = 2100,
            ClientScreenshots = 2200,
            SteamScreenshots = 2300,
            ClientAudio = 2400,
            SteamUnifiedMessages = 2500,
            ClientUnifiedMessages = 2600,
            ClientController = 2700,
            SteamController = 2800,
            ClientParentalSettings = 2900,
            ClientDeviceAuth = 3000,
            ClientNetworkDeviceManager = 3100,
            ClientMusic = 3200,
            ClientRemoteClientManager = 3300,
            ClientUGC = 3400,
            SteamStreamClient = 3500,
            ClientProductBuilder = 3600,
            ClientShortcuts = 3700,
            ClientRemoteControlManager = 3800,
            SteamAppList = 3900,
            SteamMusic = 4000,
            SteamMusicRemote = 4100,
            ClientVR = 4200,
            ClientReserved = 4300,
            SteamReserved = 4400,
            SteamHTMLSurface = 4500,
            ClientVideo = 4600,
            ClientInventory = 4700,
        };

        public static int CallbackId(CallbackType t, int o)
        {
            return (int)t + o;
        }
    }
}
