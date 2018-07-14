using System;

using Server;
using ArgonCore.Interface;

namespace InterfaceUser
{
    [Impl(Name = "CLIENTUSER_INTERFACE_VERSION001", Implements = "ClientUser", ServerMapped = true)]
    public class ClientUser001 : IBaseInterface
    {

        // ClientId is assigned after the constructor is called so we need this
        // So that we find the correct client
        public User Instance { get { return User.FindOrCreate(ClientId); } }

        public ClientUser001()
        {
        }

        public int GetHSteamUser()
        {
            Console.WriteLine("> GetHSteamUser CID: {0} HANDLE: {1}", ClientId, Instance.GetHandle());
            return Instance.GetHandle();
        }

        public void LogOn(ulong steamid)
        {
            // There is no real analogue of this directly in steamkit
            // so this will need to be reversed slightly further

            // This probably has something to do with anonymous login...
            // or when there is a loginkey that we can use
            Console.WriteLine("Logon(steamid) is not implemented");
            Instance.LogonInternal();
        }

        public void LogOnWithPassword(string username, string password)
        {
            Instance.LogOnUsernamePassword(username, password);
        }

        public void LogOnAndCreateNewSteamAccountIfNeeded()
        {
            // There is no real analog of this directly in steamkit
            // so this will need to be reverse slightly further

            Console.WriteLine("LogOnAndCreateNewSteamAccountIfNeeded() is not implemented");
        }

        // returns eresult
        public uint LogOnConnectionless()
        {
            return 0;
        }

        public void LogOff()
        {
        }

        public bool BLoggedOn()
        {
            return Instance.GetLogonState() == User.LogonState.LoggedOn;
        }

        public uint GetLogonState()
        {
            return (uint)Instance.GetLogonState();
        }

        public bool BConnected()
        {
            return Instance.Connected();
        }

        public bool BTryingToLogon()
        {
            return Instance.GetLogonState() == User.LogonState.LoggingOn;
        }

        public ulong GetSteamId()
        {
            return Instance.SteamId;
        }

        // This should only be called on consoles...
        // Since we are not targetting that this can just return the normal steamid
        // and print an error
        public ulong GetConsoleSteamId()
        {
            Console.WriteLine("GetConsoleSteamId should never be called!");
            return Instance.SteamId;
        }

        public ulong GetClientInstanceId()
        {
            return Instance.InstanceId;
        }

        public bool IsVACBanned(uint game)
        {
            // Im not sure how this is implemented in the client
            // CClientJobVACBanStatus2::BYieldingRunClientJob calls CUser::AddBannedGames
            // which fills in an array that contains banned games in some form

            // Calling this function with a gameid of 0 returns whether there are any bans

            return false;
        }

        public bool SetEmail(string new_email)
        {
            // This just sets a string in CUser and has no noticable side-effects

            // Returning false is effectively saying "this email is invalid"
            // so we will need to check whether that is valid behaviour
            return false;
        }

        public bool SetConfigString(uint config_sub_tree, string key, string value)
        {
            return false;
        }
        public bool GetConfigString(uint config_sub_tree, string key, IntPtr value_out, int max_out)
        {
            return false;
        }
        public bool SetConfigInt(uint config_sub_tree, string key, int new_value)
        {
            return false;
        }
        public bool GetConfigInt(uint config_sub_tree, string key, ref int value)
        {
            return false;
        }
        public bool DeleteConfigKey(uint config_sub_tree, string key)
        {
            return false;
        }
        public bool GetConfigStoreKeyName(uint config_sub_tree, string key, IntPtr value_out, int max_out)
        {
            return false;
        }

        public int InitiateGameConnection(IntPtr blob, uint blob_count, ulong gameserver_id, uint server_ip, ushort server_port, bool secure)
        {
            // blob will get filled with a game connect token + the app ownership ticket
            // look in reverse folder for more information on what steam does here

            // because this is part of the old user connection handshake mechanism its unlikely that this will
            // be called...

            return 0;
        }

        // This should never be called
        public void InitiateGameConnectionOld()
        {
            Console.WriteLine("InitiateGameConnectionOld should NEVER be called!");
        }

        public void TerminateGameConnection(uint server_ip, ushort server_port)
        {

        }

        public bool TerminateAppMuliStep(uint a, uint b)
        {
            return false;
        }

        public void SetSelfAsPrimaryChatDestination()
        {

        }

        public bool IsPrimaryChatDestination()
        {
            return false;
        }

        public void RequestLegacyCDKey(uint appid)
        {

        }

        public bool AckGuestPass(string passcode)
        {
            return false;
        }

        public bool RedeemGuestPass(string passcode)
        {
            return false;
        }

        public uint GuestPassToGiveCount()
        {
            return 0;
        }

        public uint GetGuestPassToRedeemCount()
        {
            return 0;
        }

        public bool GetGuestPassToGiveInfo(uint nPassIndex, ref uint pgidGuestPassID, ref uint pnPackageID, ref uint pRTime32Created, ref uint pRTime32Expiration, ref uint pRTime32Sent, ref uint pRTime32Redeemed, IntPtr pchRecipientAddress, int cRecipientAddressSize)
        {
            return false;
        }
        public bool GetGuestPassToRedeemInfo(uint nPassIndex, ref uint pgidGuestPassID, ref uint pnPackageID, ref uint pRTime32Created, ref uint pRTime32Expiration, ref uint pRTime32Sent, ref uint pRTime32Redeemed)
        {
            return false;
        }
        public bool GetGuestPassToRedeemSenderName(uint nPassIndex, IntPtr pchSenderName, int cSenderNameSize)
        {
            return false;
        }

        public uint GetNumAppsInGuestPassesToRedeem()
        {
            return 0;
        }

        public uint GetAppsInGuestPassesToRedeem(IntPtr app_array_to_fill, int max_fill)
        {
            return 0;
        }

        public uint GetCountUserNotifications()
        {
            return 0;
        }

        public uint GetCountUserNotification(uint type)
        {
            return 0;
        }

        public void RequestStoreAuthURL(string a)
        {
            // TODO: revese this
        }

        public void SetLanguage(string a)
        {
            // TODO: reverse this
        }

        public void TrackAppUsageEvent(ulong game_id, int usage_event, string extra_info)
        {

        }

        public uint RaiseConnectionPriority(uint new_priority)
        {
            return 0;
        }

        public void ResetConnectionPriority()
        {

        }

        public bool BHasCachedCredentials(string a)
        {
            return false;
        }

        public bool SetAccountNameForCachedCredentialLogin(string name, bool a)
        {
            return false;
        }

        public bool GetCurrentWebAuthToken(IntPtr out_string, int out_max)
        {
            return false;
        }
        public uint RequestWebAuthToken()
        {
            // Returns SteamApiCall
            return 0;
        }

        public void SetLoginInformation(string username, string password, bool remember_password)
        {
            if (remember_password)
            {
                Console.WriteLine("SetLoginInformation: remember_password set but we cant!");
            }
            Instance.SetLogonInformation(username, password);
        }

        public void SetTwoFactorCode(string code)
        {
            Instance.SetTwoFactor(code);
        }

        public void ClearLoginInformation()
        {
            Instance.ClearLogonInformation();
        }

        public bool GetLanguage(IntPtr out_string, int out_max)
        {
            return false;
        }

        public bool BIsCyberCafe()
        {
            return false;
        }

        public bool BIsAcademicAccount()
        {
            return false;
        }

        public bool BIsPortal2EducationAccount()
        {
            return false;
        }

        public bool BIsAlienwareDemoAccount()
        {
            return false;
        }

        public void CreateAccount(string new_username, string new_password, string new_email)
        {

        }

        public uint ResetPassword(string account_name, string old_password, string new_password, string code, string answer)
        {
            return 0;
        }

        public void ValidatePasswordResetCodeAndSendSms(string a, string b)
        {

        }

        public void TrackNatTraversalStat(IntPtr stat_out)
        {

        }

        public void TrackSteamUsageEvent(uint usage_event, string extra, uint a)
        {

        }

        public void TrackSteamGuiUsage(string a)
        {

        }

        public void SetComputerInUse()
        {

        }
    }
}