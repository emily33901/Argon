using System;

// Autogenerated @ 26/05/18
namespace InterfaceUser
{
    /// <summary>
    /// Implements the map for interface CLIENTUSER_INTERFACE_VERSION001
    /// </summary>
    [ArgonCore.Interface.Map(Name = "CLIENTUSER_INTERFACE_VERSION001", Implements = "ClientUser")]
    public class ClientUser001_Map : ArgonCore.Interface.IBaseInterfaceMap
    {
        public int GetHSteamUser(IntPtr _)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "GetHSteamUser",
               Args = new object[] {},
            };
            return ArgonCore.IPC.ClientPipe.CallSerializedFunction<int>(PipeId, f);
        }
        public void LogOn(IntPtr _, ulong steamid)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "LogOn",
               Args = new object[] {steamid},
            };
            ArgonCore.IPC.ClientPipe.CallSerializedFunction(PipeId, f);
        }
        public void LogOnWithPassword(IntPtr _, string username, string password)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "LogOnWithPassword",
               Args = new object[] {username, password},
            };
            ArgonCore.IPC.ClientPipe.CallSerializedFunction(PipeId, f);
        }
        public void LogOnAndCreateNewSteamAccountIfNeeded(IntPtr _)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "LogOnAndCreateNewSteamAccountIfNeeded",
               Args = new object[] {},
            };
            ArgonCore.IPC.ClientPipe.CallSerializedFunction(PipeId, f);
        }
        public uint LogOnConnectionless(IntPtr _)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "LogOnConnectionless",
               Args = new object[] {},
            };
            return ArgonCore.IPC.ClientPipe.CallSerializedFunction<uint>(PipeId, f);
        }
        public void LogOff(IntPtr _)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "LogOff",
               Args = new object[] {},
            };
            ArgonCore.IPC.ClientPipe.CallSerializedFunction(PipeId, f);
        }
        public bool BLoggedOn(IntPtr _)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "BLoggedOn",
               Args = new object[] {},
            };
            return ArgonCore.IPC.ClientPipe.CallSerializedFunction<bool>(PipeId, f);
        }
        public uint GetLogonState(IntPtr _)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "GetLogonState",
               Args = new object[] {},
            };
            return ArgonCore.IPC.ClientPipe.CallSerializedFunction<uint>(PipeId, f);
        }
        public bool BConnected(IntPtr _)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "BConnected",
               Args = new object[] {},
            };
            return ArgonCore.IPC.ClientPipe.CallSerializedFunction<bool>(PipeId, f);
        }
        public bool BTryingToLogon(IntPtr _)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "BTryingToLogon",
               Args = new object[] {},
            };
            return ArgonCore.IPC.ClientPipe.CallSerializedFunction<bool>(PipeId, f);
        }
        public ulong GetSteamId(IntPtr _)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "GetSteamId",
               Args = new object[] {},
            };
            return ArgonCore.IPC.ClientPipe.CallSerializedFunction<ulong>(PipeId, f);
        }
        public ulong GetConsoleSteamId(IntPtr _)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "GetConsoleSteamId",
               Args = new object[] {},
            };
            return ArgonCore.IPC.ClientPipe.CallSerializedFunction<ulong>(PipeId, f);
        }
        public ulong GetClientInstanceId(IntPtr _)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "GetClientInstanceId",
               Args = new object[] {},
            };
            return ArgonCore.IPC.ClientPipe.CallSerializedFunction<ulong>(PipeId, f);
        }
        public bool IsVACBanned(IntPtr _, uint game)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "IsVACBanned",
               Args = new object[] {game},
            };
            return ArgonCore.IPC.ClientPipe.CallSerializedFunction<bool>(PipeId, f);
        }
        public bool SetEmail(IntPtr _, string new_email)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "SetEmail",
               Args = new object[] {new_email},
            };
            return ArgonCore.IPC.ClientPipe.CallSerializedFunction<bool>(PipeId, f);
        }
        public bool SetConfigString(IntPtr _, uint config_sub_tree, string key, string value)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "SetConfigString",
               Args = new object[] {config_sub_tree, key, value},
            };
            return ArgonCore.IPC.ClientPipe.CallSerializedFunction<bool>(PipeId, f);
        }
        public bool GetConfigString(IntPtr _, uint config_sub_tree, string key, IntPtr value_out, int max_out)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "GetConfigString",
               Args = new object[] {config_sub_tree, key, value_out, max_out},
            };
            return ArgonCore.IPC.ClientPipe.CallSerializedFunction<bool>(PipeId, f);
        }
        public bool SetConfigInt(IntPtr _, uint config_sub_tree, string key, int new_value)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "SetConfigInt",
               Args = new object[] {config_sub_tree, key, new_value},
            };
            return ArgonCore.IPC.ClientPipe.CallSerializedFunction<bool>(PipeId, f);
        }
        public bool GetConfigInt(IntPtr _, uint config_sub_tree, string key, ref System.Int32 value)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "GetConfigInt",
               Args = new object[] {config_sub_tree, key, value},
            };
            return ArgonCore.IPC.ClientPipe.CallSerializedFunction<bool>(PipeId, f);
        }
        public bool DeleteConfigKey(IntPtr _, uint config_sub_tree, string key)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "DeleteConfigKey",
               Args = new object[] {config_sub_tree, key},
            };
            return ArgonCore.IPC.ClientPipe.CallSerializedFunction<bool>(PipeId, f);
        }
        public bool GetConfigStoreKeyName(IntPtr _, uint config_sub_tree, string key, IntPtr value_out, int max_out)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "GetConfigStoreKeyName",
               Args = new object[] {config_sub_tree, key, value_out, max_out},
            };
            return ArgonCore.IPC.ClientPipe.CallSerializedFunction<bool>(PipeId, f);
        }
        public int InitiateGameConnection(IntPtr _, IntPtr blob, uint blob_count, ulong gameserver_id, uint server_ip, System.UInt16 server_port, bool secure)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "InitiateGameConnection",
               Args = new object[] {blob, blob_count, gameserver_id, server_ip, server_port, secure},
            };
            return ArgonCore.IPC.ClientPipe.CallSerializedFunction<int>(PipeId, f);
        }
        public void InitiateGameConnectionOld(IntPtr _)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "InitiateGameConnectionOld",
               Args = new object[] {},
            };
            ArgonCore.IPC.ClientPipe.CallSerializedFunction(PipeId, f);
        }
        public void TerminateGameConnection(IntPtr _, uint server_ip, System.UInt16 server_port)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "TerminateGameConnection",
               Args = new object[] {server_ip, server_port},
            };
            ArgonCore.IPC.ClientPipe.CallSerializedFunction(PipeId, f);
        }
        public bool TerminateAppMuliStep(IntPtr _, uint a, uint b)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "TerminateAppMuliStep",
               Args = new object[] {a, b},
            };
            return ArgonCore.IPC.ClientPipe.CallSerializedFunction<bool>(PipeId, f);
        }
        public void SetSelfAsPrimaryChatDestination(IntPtr _)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "SetSelfAsPrimaryChatDestination",
               Args = new object[] {},
            };
            ArgonCore.IPC.ClientPipe.CallSerializedFunction(PipeId, f);
        }
        public bool IsPrimaryChatDestination(IntPtr _)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "IsPrimaryChatDestination",
               Args = new object[] {},
            };
            return ArgonCore.IPC.ClientPipe.CallSerializedFunction<bool>(PipeId, f);
        }
        public void RequestLegacyCDKey(IntPtr _, uint appid)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "RequestLegacyCDKey",
               Args = new object[] {appid},
            };
            ArgonCore.IPC.ClientPipe.CallSerializedFunction(PipeId, f);
        }
        public bool AckGuestPass(IntPtr _, string passcode)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "AckGuestPass",
               Args = new object[] {passcode},
            };
            return ArgonCore.IPC.ClientPipe.CallSerializedFunction<bool>(PipeId, f);
        }
        public bool RedeemGuestPass(IntPtr _, string passcode)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "RedeemGuestPass",
               Args = new object[] {passcode},
            };
            return ArgonCore.IPC.ClientPipe.CallSerializedFunction<bool>(PipeId, f);
        }
        public uint GuestPassToGiveCount(IntPtr _)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "GuestPassToGiveCount",
               Args = new object[] {},
            };
            return ArgonCore.IPC.ClientPipe.CallSerializedFunction<uint>(PipeId, f);
        }
        public uint GetGuestPassToRedeemCount(IntPtr _)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "GetGuestPassToRedeemCount",
               Args = new object[] {},
            };
            return ArgonCore.IPC.ClientPipe.CallSerializedFunction<uint>(PipeId, f);
        }
        public bool GetGuestPassToGiveInfo(IntPtr _, uint nPassIndex, ref System.UInt32 pgidGuestPassID, ref System.UInt32 pnPackageID, ref System.UInt32 pRTime32Created, ref System.UInt32 pRTime32Expiration, ref System.UInt32 pRTime32Sent, ref System.UInt32 pRTime32Redeemed, IntPtr pchRecipientAddress, int cRecipientAddressSize)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "GetGuestPassToGiveInfo",
               Args = new object[] {nPassIndex, pgidGuestPassID, pnPackageID, pRTime32Created, pRTime32Expiration, pRTime32Sent, pRTime32Redeemed, pchRecipientAddress, cRecipientAddressSize},
            };
            return ArgonCore.IPC.ClientPipe.CallSerializedFunction<bool>(PipeId, f);
        }
        public bool GetGuestPassToRedeemInfo(IntPtr _, uint nPassIndex, ref System.UInt32 pgidGuestPassID, ref System.UInt32 pnPackageID, ref System.UInt32 pRTime32Created, ref System.UInt32 pRTime32Expiration, ref System.UInt32 pRTime32Sent, ref System.UInt32 pRTime32Redeemed)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "GetGuestPassToRedeemInfo",
               Args = new object[] {nPassIndex, pgidGuestPassID, pnPackageID, pRTime32Created, pRTime32Expiration, pRTime32Sent, pRTime32Redeemed},
            };
            return ArgonCore.IPC.ClientPipe.CallSerializedFunction<bool>(PipeId, f);
        }
        public bool GetGuestPassToRedeemSenderName(IntPtr _, uint nPassIndex, IntPtr pchSenderName, int cSenderNameSize)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "GetGuestPassToRedeemSenderName",
               Args = new object[] {nPassIndex, pchSenderName, cSenderNameSize},
            };
            return ArgonCore.IPC.ClientPipe.CallSerializedFunction<bool>(PipeId, f);
        }
        public uint GetNumAppsInGuestPassesToRedeem(IntPtr _)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "GetNumAppsInGuestPassesToRedeem",
               Args = new object[] {},
            };
            return ArgonCore.IPC.ClientPipe.CallSerializedFunction<uint>(PipeId, f);
        }
        public uint GetAppsInGuestPassesToRedeem(IntPtr _, IntPtr app_array_to_fill, int max_fill)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "GetAppsInGuestPassesToRedeem",
               Args = new object[] {app_array_to_fill, max_fill},
            };
            return ArgonCore.IPC.ClientPipe.CallSerializedFunction<uint>(PipeId, f);
        }
        public uint GetCountUserNotifications(IntPtr _)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "GetCountUserNotifications",
               Args = new object[] {},
            };
            return ArgonCore.IPC.ClientPipe.CallSerializedFunction<uint>(PipeId, f);
        }
        public uint GetCountUserNotification(IntPtr _, uint type)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "GetCountUserNotification",
               Args = new object[] {type},
            };
            return ArgonCore.IPC.ClientPipe.CallSerializedFunction<uint>(PipeId, f);
        }
        public void RequestStoreAuthURL(IntPtr _, string a)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "RequestStoreAuthURL",
               Args = new object[] {a},
            };
            ArgonCore.IPC.ClientPipe.CallSerializedFunction(PipeId, f);
        }
        public void SetLanguage(IntPtr _, string a)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "SetLanguage",
               Args = new object[] {a},
            };
            ArgonCore.IPC.ClientPipe.CallSerializedFunction(PipeId, f);
        }
        public void TrackAppUsageEvent(IntPtr _, ulong game_id, int usage_event, string extra_info)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "TrackAppUsageEvent",
               Args = new object[] {game_id, usage_event, extra_info},
            };
            ArgonCore.IPC.ClientPipe.CallSerializedFunction(PipeId, f);
        }
        public uint RaiseConnectionPriority(IntPtr _, uint new_priority)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "RaiseConnectionPriority",
               Args = new object[] {new_priority},
            };
            return ArgonCore.IPC.ClientPipe.CallSerializedFunction<uint>(PipeId, f);
        }
        public void ResetConnectionPriority(IntPtr _)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "ResetConnectionPriority",
               Args = new object[] {},
            };
            ArgonCore.IPC.ClientPipe.CallSerializedFunction(PipeId, f);
        }
        public bool BHasCachedCredentials(IntPtr _, string a)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "BHasCachedCredentials",
               Args = new object[] {a},
            };
            return ArgonCore.IPC.ClientPipe.CallSerializedFunction<bool>(PipeId, f);
        }
        public bool SetAccountNameForCachedCredentialLogin(IntPtr _, string name, bool a)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "SetAccountNameForCachedCredentialLogin",
               Args = new object[] {name, a},
            };
            return ArgonCore.IPC.ClientPipe.CallSerializedFunction<bool>(PipeId, f);
        }
        public bool GetCurrentWebAuthToken(IntPtr _, IntPtr out_string, int out_max)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "GetCurrentWebAuthToken",
               Args = new object[] {out_string, out_max},
            };
            return ArgonCore.IPC.ClientPipe.CallSerializedFunction<bool>(PipeId, f);
        }
        public uint RequestWebAuthToken(IntPtr _)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "RequestWebAuthToken",
               Args = new object[] {},
            };
            return ArgonCore.IPC.ClientPipe.CallSerializedFunction<uint>(PipeId, f);
        }
        public void SetLoginInformation(IntPtr _, string username, string password, bool remember_password)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "SetLoginInformation",
               Args = new object[] {username, password, remember_password},
            };
            ArgonCore.IPC.ClientPipe.CallSerializedFunction(PipeId, f);
        }
        public void SetTwoFactorCode(IntPtr _, string code)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "SetTwoFactorCode",
               Args = new object[] {code},
            };
            ArgonCore.IPC.ClientPipe.CallSerializedFunction(PipeId, f);
        }
        public void ClearLoginInformation(IntPtr _)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "ClearLoginInformation",
               Args = new object[] {},
            };
            ArgonCore.IPC.ClientPipe.CallSerializedFunction(PipeId, f);
        }
        public bool GetLanguage(IntPtr _, IntPtr out_string, int out_max)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "GetLanguage",
               Args = new object[] {out_string, out_max},
            };
            return ArgonCore.IPC.ClientPipe.CallSerializedFunction<bool>(PipeId, f);
        }
        public bool BIsCyberCafe(IntPtr _)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "BIsCyberCafe",
               Args = new object[] {},
            };
            return ArgonCore.IPC.ClientPipe.CallSerializedFunction<bool>(PipeId, f);
        }
        public bool BIsAcademicAccount(IntPtr _)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "BIsAcademicAccount",
               Args = new object[] {},
            };
            return ArgonCore.IPC.ClientPipe.CallSerializedFunction<bool>(PipeId, f);
        }
        public bool BIsPortal2EducationAccount(IntPtr _)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "BIsPortal2EducationAccount",
               Args = new object[] {},
            };
            return ArgonCore.IPC.ClientPipe.CallSerializedFunction<bool>(PipeId, f);
        }
        public bool BIsAlienwareDemoAccount(IntPtr _)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "BIsAlienwareDemoAccount",
               Args = new object[] {},
            };
            return ArgonCore.IPC.ClientPipe.CallSerializedFunction<bool>(PipeId, f);
        }
        public void CreateAccount(IntPtr _, string new_username, string new_password, string new_email)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "CreateAccount",
               Args = new object[] {new_username, new_password, new_email},
            };
            ArgonCore.IPC.ClientPipe.CallSerializedFunction(PipeId, f);
        }
        public uint ResetPassword(IntPtr _, string account_name, string old_password, string new_password, string code, string answer)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "ResetPassword",
               Args = new object[] {account_name, old_password, new_password, code, answer},
            };
            return ArgonCore.IPC.ClientPipe.CallSerializedFunction<uint>(PipeId, f);
        }
    }
}
