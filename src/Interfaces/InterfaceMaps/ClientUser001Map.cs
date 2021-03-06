using System;

// Autogenerated @ 21/08/18
namespace InterfaceUser
{
    /// <summary>
    /// Implements the map for interface CLIENTUSER_INTERFACE_VERSION001
    /// </summary>
    [Core.Interface.Map(Name = "CLIENTUSER_INTERFACE_VERSION001")]
    public class ClientUser001_Map : Core.Interface.IBaseInterfaceMap
    {
        public int GetHSteamUser(IntPtr _)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "GetHSteamUser",
                Args = new object[] {},

            });



            return (int)result.Result;
        }
        public void LogOn(IntPtr _, ulong steamid)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "LogOn",
                Args = new object[] {steamid},

            });



        }
        public void LogOnWithPassword(IntPtr _, string username, string password)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "LogOnWithPassword",
                Args = new object[] {username, password},

            });



        }
        public void LogOnAndCreateNewSteamAccountIfNeeded(IntPtr _)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "LogOnAndCreateNewSteamAccountIfNeeded",
                Args = new object[] {},

            });



        }
        public uint LogOnConnectionless(IntPtr _)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "LogOnConnectionless",
                Args = new object[] {},

            });



            return (uint)result.Result;
        }
        public void LogOff(IntPtr _)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "LogOff",
                Args = new object[] {},

            });



        }
        public bool BLoggedOn(IntPtr _)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "BLoggedOn",
                Args = new object[] {},

            });



            return (bool)result.Result;
        }
        public uint GetLogonState(IntPtr _)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "GetLogonState",
                Args = new object[] {},

            });



            return (uint)result.Result;
        }
        public bool BConnected(IntPtr _)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "BConnected",
                Args = new object[] {},

            });



            return (bool)result.Result;
        }
        public bool BTryingToLogon(IntPtr _)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "BTryingToLogon",
                Args = new object[] {},

            });



            return (bool)result.Result;
        }
        public ulong GetSteamId(IntPtr _)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "GetSteamId",
                Args = new object[] {},

            });



            return (ulong)result.Result;
        }
        public ulong GetConsoleSteamId(IntPtr _)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "GetConsoleSteamId",
                Args = new object[] {},

            });



            return (ulong)result.Result;
        }
        public ulong GetClientInstanceId(IntPtr _)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "GetClientInstanceId",
                Args = new object[] {},

            });



            return (ulong)result.Result;
        }
        public bool IsVACBanned(IntPtr _, uint game)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "IsVACBanned",
                Args = new object[] {game},

            });



            return (bool)result.Result;
        }
        public bool SetEmail(IntPtr _, string new_email)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "SetEmail",
                Args = new object[] {new_email},

            });



            return (bool)result.Result;
        }
        public bool SetConfigString(IntPtr _, uint config_sub_tree, string key, string value)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "SetConfigString",
                Args = new object[] {config_sub_tree, key, value},

            });



            return (bool)result.Result;
        }
        public bool GetConfigString(IntPtr _, uint config_sub_tree, string key, IntPtr value_out, int max_out)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "GetConfigString",
                Args = new object[] {config_sub_tree, key, value_out, max_out},

            });



            return (bool)result.Result;
        }
        public bool SetConfigInt(IntPtr _, uint config_sub_tree, string key, int new_value)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "SetConfigInt",
                Args = new object[] {config_sub_tree, key, new_value},

            });



            return (bool)result.Result;
        }
        public bool GetConfigInt(IntPtr _, uint config_sub_tree, string key, ref int value)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "GetConfigInt",
                Args = new object[] {config_sub_tree, key, value},

            });

            value = (int)result.Args[2];


            return (bool)result.Result;
        }
        public bool DeleteConfigKey(IntPtr _, uint config_sub_tree, string key)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "DeleteConfigKey",
                Args = new object[] {config_sub_tree, key},

            });



            return (bool)result.Result;
        }
        public bool GetConfigStoreKeyName(IntPtr _, uint config_sub_tree, string key, IntPtr value_out, int max_out)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "GetConfigStoreKeyName",
                Args = new object[] {config_sub_tree, key, value_out, max_out},

            });



            return (bool)result.Result;
        }
        public int InitiateGameConnection(IntPtr _, IntPtr blob, uint blob_count, ulong gameserver_id, uint server_ip, short server_port, bool secure)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "InitiateGameConnection",
                Args = new object[] {blob, blob_count, gameserver_id, server_ip, server_port, secure},

            });



            return (int)result.Result;
        }
        public void InitiateGameConnectionOld(IntPtr _)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "InitiateGameConnectionOld",
                Args = new object[] {},

            });



        }
        public void TerminateGameConnection(IntPtr _, uint server_ip, short server_port)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "TerminateGameConnection",
                Args = new object[] {server_ip, server_port},

            });



        }
        public bool TerminateAppMuliStep(IntPtr _, uint a, uint b)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "TerminateAppMuliStep",
                Args = new object[] {a, b},

            });



            return (bool)result.Result;
        }
        public void SetSelfAsPrimaryChatDestination(IntPtr _)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "SetSelfAsPrimaryChatDestination",
                Args = new object[] {},

            });



        }
        public bool IsPrimaryChatDestination(IntPtr _)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "IsPrimaryChatDestination",
                Args = new object[] {},

            });



            return (bool)result.Result;
        }
        public void RequestLegacyCDKey(IntPtr _, uint appid)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "RequestLegacyCDKey",
                Args = new object[] {appid},

            });



        }
        public bool AckGuestPass(IntPtr _, string passcode)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "AckGuestPass",
                Args = new object[] {passcode},

            });



            return (bool)result.Result;
        }
        public bool RedeemGuestPass(IntPtr _, string passcode)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "RedeemGuestPass",
                Args = new object[] {passcode},

            });



            return (bool)result.Result;
        }
        public uint GuestPassToGiveCount(IntPtr _)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "GuestPassToGiveCount",
                Args = new object[] {},

            });



            return (uint)result.Result;
        }
        public uint GetGuestPassToRedeemCount(IntPtr _)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "GetGuestPassToRedeemCount",
                Args = new object[] {},

            });



            return (uint)result.Result;
        }
        public bool GetGuestPassToGiveInfo(IntPtr _, uint nPassIndex, ref uint pgidGuestPassID, ref uint pnPackageID, ref uint pRTime32Created, ref uint pRTime32Expiration, ref uint pRTime32Sent, ref uint pRTime32Redeemed, IntPtr pchRecipientAddress, int cRecipientAddressSize)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "GetGuestPassToGiveInfo",
                Args = new object[] {nPassIndex, pgidGuestPassID, pnPackageID, pRTime32Created, pRTime32Expiration, pRTime32Sent, pRTime32Redeemed, pchRecipientAddress, cRecipientAddressSize},

            });

            pgidGuestPassID = (uint)result.Args[1];
            pnPackageID = (uint)result.Args[2];
            pRTime32Created = (uint)result.Args[3];
            pRTime32Expiration = (uint)result.Args[4];
            pRTime32Sent = (uint)result.Args[5];
            pRTime32Redeemed = (uint)result.Args[6];


            return (bool)result.Result;
        }
        public bool GetGuestPassToGiveOut(IntPtr _, uint nPassIndex)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "GetGuestPassToGiveOut",
                Args = new object[] {nPassIndex},

            });



            return (bool)result.Result;
        }
        public bool GetGuestPassToRedeem(IntPtr _, uint nPassIndex)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "GetGuestPassToRedeem",
                Args = new object[] {nPassIndex},

            });



            return (bool)result.Result;
        }
        public bool GetGuestPassToRedeemInfo(IntPtr _, uint nPassIndex, ref uint pgidGuestPassID, ref uint pnPackageID, ref uint pRTime32Created, ref uint pRTime32Expiration, ref uint pRTime32Sent, ref uint pRTime32Redeemed)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "GetGuestPassToRedeemInfo",
                Args = new object[] {nPassIndex, pgidGuestPassID, pnPackageID, pRTime32Created, pRTime32Expiration, pRTime32Sent, pRTime32Redeemed},

            });

            pgidGuestPassID = (uint)result.Args[1];
            pnPackageID = (uint)result.Args[2];
            pRTime32Created = (uint)result.Args[3];
            pRTime32Expiration = (uint)result.Args[4];
            pRTime32Sent = (uint)result.Args[5];
            pRTime32Redeemed = (uint)result.Args[6];


            return (bool)result.Result;
        }
        public bool GetGuestPassToRedeemSenderName(IntPtr _, uint nPassIndex, IntPtr pchSenderName, int cSenderNameSize)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "GetGuestPassToRedeemSenderName",
                Args = new object[] {nPassIndex, pchSenderName, cSenderNameSize},

            });



            return (bool)result.Result;
        }
        public uint GetNumAppsInGuestPassesToRedeem(IntPtr _)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "GetNumAppsInGuestPassesToRedeem",
                Args = new object[] {},

            });



            return (uint)result.Result;
        }
        public uint GetAppsInGuestPassesToRedeem(IntPtr _, IntPtr app_array_to_fill, int max_fill)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "GetAppsInGuestPassesToRedeem",
                Args = new object[] {app_array_to_fill, max_fill},

            });



            return (uint)result.Result;
        }
        public uint GetCountUserNotifications(IntPtr _)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "GetCountUserNotifications",
                Args = new object[] {},

            });



            return (uint)result.Result;
        }
        public uint GetCountUserNotification(IntPtr _, uint type)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "GetCountUserNotification",
                Args = new object[] {type},

            });



            return (uint)result.Result;
        }
        public void RequestStoreAuthURL(IntPtr _, string a)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "RequestStoreAuthURL",
                Args = new object[] {a},

            });



        }
        public void SetLanguage(IntPtr _, string a)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "SetLanguage",
                Args = new object[] {a},

            });



        }
        public void TrackAppUsageEvent(IntPtr _, ulong game_id, int usage_event, string extra_info)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "TrackAppUsageEvent",
                Args = new object[] {game_id, usage_event, extra_info},

            });



        }
        public uint RaiseConnectionPriority(IntPtr _, uint new_priority)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "RaiseConnectionPriority",
                Args = new object[] {new_priority},

            });



            return (uint)result.Result;
        }
        public void ResetConnectionPriority(IntPtr _)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "ResetConnectionPriority",
                Args = new object[] {},

            });



        }
        public bool BHasCachedCredentials(IntPtr _, string a)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "BHasCachedCredentials",
                Args = new object[] {a},

            });



            return (bool)result.Result;
        }
        public bool SetAccountNameForCachedCredentialLogin(IntPtr _, string name, bool a)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "SetAccountNameForCachedCredentialLogin",
                Args = new object[] {name, a},

            });



            return (bool)result.Result;
        }
        public bool GetCurrentWebAuthToken(IntPtr _, IntPtr out_string, int out_max)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "GetCurrentWebAuthToken",
                Args = new object[] {out_string, out_max},

            });



            return (bool)result.Result;
        }
        public uint RequestWebAuthToken(IntPtr _)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "RequestWebAuthToken",
                Args = new object[] {},

            });



            return (uint)result.Result;
        }
        public void SetLoginInformation(IntPtr _, string username, string password, bool remember_password)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "SetLoginInformation",
                Args = new object[] {username, password, remember_password},

            });



        }
        public void SetTwoFactorCode(IntPtr _, string code)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "SetTwoFactorCode",
                Args = new object[] {code},

            });



        }
        public void ClearLoginInformation(IntPtr _)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "ClearLoginInformation",
                Args = new object[] {},

            });



        }
        public bool GetLanguage(IntPtr _, IntPtr out_string, int out_max)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "GetLanguage",
                Args = new object[] {out_string, out_max},

            });



            return (bool)result.Result;
        }
        public bool BIsCyberCafe(IntPtr _)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "BIsCyberCafe",
                Args = new object[] {},

            });



            return (bool)result.Result;
        }
        public bool BIsAcademicAccount(IntPtr _)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "BIsAcademicAccount",
                Args = new object[] {},

            });



            return (bool)result.Result;
        }
        public bool BIsPortal2EducationAccount(IntPtr _)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "BIsPortal2EducationAccount",
                Args = new object[] {},

            });



            return (bool)result.Result;
        }
        public bool BIsAlienwareDemoAccount(IntPtr _)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "BIsAlienwareDemoAccount",
                Args = new object[] {},

            });



            return (bool)result.Result;
        }
        public void CreateAccount(IntPtr _, string new_username, string new_password, string new_email)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "CreateAccount",
                Args = new object[] {new_username, new_password, new_email},

            });



        }
        public uint ResetPassword(IntPtr _, string account_name, string old_password, string new_password, string code, string answer)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "ResetPassword",
                Args = new object[] {account_name, old_password, new_password, code, answer},

            });



            return (uint)result.Result;
        }
        public void ValidatePasswordResetCodeAndSendSms(IntPtr _, string a, string b)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "ValidatePasswordResetCodeAndSendSms",
                Args = new object[] {a, b},

            });



        }
        public void TrackNatTraversalStat(IntPtr _, IntPtr stat_out)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "TrackNatTraversalStat",
                Args = new object[] {stat_out},

            });



        }
        public void TrackSteamUsageEvent(IntPtr _, uint usage_event, string extra, uint a)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "TrackSteamUsageEvent",
                Args = new object[] {usage_event, extra, a},

            });



        }
        public void TrackSteamGuiUsage(IntPtr _, string a)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "TrackSteamGuiUsage",
                Args = new object[] {a},

            });



        }
        public void SetComputerInUse(IntPtr _)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "SetComputerInUse",
                Args = new object[] {},

            });



        }
    }
}
