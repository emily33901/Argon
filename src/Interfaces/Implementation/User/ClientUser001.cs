using System;

using ArgonCore;
using ArgonCore.Interface;

namespace InterfaceUser
{
    [Impl(Name = "CLIENTUSER_INTERFACE_VERSION001", Implements = "ClientUser", ServerMapped = true)]
    public class ClientUser001 : IBaseInterface
    {
        public User u;

        ClientUser001()
        {
            u = new User(this);
        }

        public int GetHSteamUser()
        {
            return u.GetHandle();
        }

        public void LogOn(ulong steamid)
        {
            // There is no real analogue of this directly in steamkit
            // so this will need to be reversed slightly further

            Console.WriteLine("Logon(steamid) is not implemented");
        }

        public void LogOnWithPassword(string username, string password)
        {
            u.LogOnUsernamePassword(username, password);
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
            return u.GetLogonState() == User.LogonState.LoggedOn;
        }

        public User.LogonState GetLogonState()
        {
            return u.GetLogonState();
        }

        public bool BConnected()
        {
            return u.Connected();
        }

        public bool BTryingToLogon()
        {
            return u.GetLogonState() == User.LogonState.LoggingOn;
        }

        public ulong GetSteamId()
        {
            return u.SteamId;
        }

        // This should only be called on consoles...
        // Since we are not targetting that this can just return the normal steamid
        // and print an error
        public ulong GetConsoleSteamId()
        {
            Console.WriteLine("GetConsoleSteamId should never be called!");
            return u.SteamId;
        }

        public ulong GetClientInstanceId()
        {
            return u.InstanceId;
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
            // blob will get filled with the old appownership ticket + the new one
            // look in reverse folder for more information on what steam does here

            // It is possible that we can also just request a new appticket from the cm for every time this is called.
            // This would allow us to probably bypass the need to check whether the tickets are valid against the various tests
            // becuase we know that the cm will always return a valid key.
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
    }
}