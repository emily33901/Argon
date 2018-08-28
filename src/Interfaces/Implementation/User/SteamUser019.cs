using System;

using Core;
using Core.Interface;

using System.Runtime.InteropServices;

namespace InterfaceUser
{
    [Impl(Name = "SteamUser019", ServerMapped = true)]
    public class SteamUser019 : IBaseInterface
    {
        public User u;
        public SteamUser019()
        {
            u = User.FindOrCreate(ClientId);
        }

        public int GetHSteamUser()
        {
            u.Log.WriteLine("GetHSteamUser");
            // Make sure to return the handle not the id
            return u.GetHandle();
        }

        public bool LoggedOn()
        {
            u.Log.WriteLine("LoggedOn");
            return u.GetLogonState() == User.LogonState.LoggedOn;
        }

        public ulong GetSteamID()
        {
            u.Log.WriteLine("GetSteamID");
            return u.SteamId;
        }

        public int InitiateGameConnection(IntPtr blob, uint blob_count, ulong gameserver_id, uint server_ip, ushort server_port, bool secure)
        {
            u.Log.WriteLine("InitiateGameConnection");
            return 0;
        }

        public void TerminateGameConnection(uint server_ip, ushort server_port)
        {
            u.Log.WriteLine("TerminateGameConnection");
        }

        public void TrackAppUsageEvent(ulong game_id, int usage_event, string extra_info)
        {
            u.Log.WriteLine("TrackAppUsageEvent");
        }

        public bool GetUserDataFolder(string buffer, int count)
        {
            u.Log.WriteLine("GetUserDataFolder");
            return false;
        }

        public void StartVoiceRecording()
        {
            u.Log.WriteLine("StartVoiceRecording");
        }

        public void StopVoiceRecording()
        {
            u.Log.WriteLine("StopVoiceRecording");

        }

        public uint GetAvailableVoice(uint[] compressed_data, uint[] uncompressed, uint desired_sample_rate)
        {
            u.Log.WriteLine("GetAvailableVoice");
            return 0;
        }

        public int GetVoice(bool want_compressed, IntPtr dest_buffer, uint dest_buffer_size, ref uint compressed_bytes_written,
                            bool wants_uncompressed, IntPtr uncompressed_dest, uint uncompressed_buffer_size,
                            ref uint bytes_written, uint uncompressed_desired_samplerate)
        {
            u.Log.WriteLine("GetVoice");
            return 0;
        }

        public int DecompressVoice(IntPtr compressed, uint compressed_size, IntPtr dest_buffer, uint dest_size, ref uint bytes_written, uint sample_rate)
        {
            u.Log.WriteLine("DecompressVoice");
            return 0;
        }

        public uint GetOptimalSampleRate()
        {
            u.Log.WriteLine("GetOptimalSampleRate");
            return 0;
        }

        public int GetAuthSessionTicket(IntPtr ticket, uint ticket_size, ref int ticket_written)
        {
            u.Log.WriteLine("GetAuthSessionTicket");

            var app_id = u.GetAppIdForPipe(PipeId);

            var new_ticket = u.GetAuthTicket(u.GetAuthSessionTicket(app_id, PipeId));

            if (new_ticket.ticket.Length > ticket_size)
            {
                u.Log.WriteLine("AuthTicket length is bigger than buffer allocated for it!");
                return -1; // Invalid ticket
            }

            // Write the ticket out
            Marshal.Copy(new_ticket.ticket, 0, ticket, new_ticket.ticket.Length);
            ticket_written = new_ticket.ticket.Length;

            return new_ticket.handle;
        }

        public uint BeginAuthSession(IntPtr ticket, uint ticket_size, ulong steamid)
        {
            u.Log.WriteLine("BeginAuthSession");
            return 0;
        }

        public void EndAuthSession(ulong steam_id)
        {
            u.Log.WriteLine("EndAuthSession");
        }

        public void CancelAuthTicket(int ticket_handle)
        {
            u.Log.WriteLine("CancelAuthTicket");

            var ticket = u.GetAuthTicket(ticket_handle);

            if (ticket.cancelled == true)
                u.Log.WriteLine("CancelAuthTicket attempt made on already cancelled ticket??");

            ticket.cancelled = true;
            u.SendClientAuthList();
        }

        public uint UserHasLicenseForApp(ulong steamID, uint appID)
        {
            u.Log.WriteLine("UserHasLicenseForApp");
            return 0;
        }

        bool IsBehindNAT()
        {
            u.Log.WriteLine("IsBehindNAT");
            return false;
        }

        public void AdvertiseGame(ulong game_server_id, uint server_ip, ushort server_port)
        {
            u.Log.WriteLine("AdvertiseGame");
        }

        public uint RequestEncryptedAppTicket(IntPtr data_to_include, uint data_size)
        {
            u.Log.WriteLine("RequestEncryptedAppTicket");
            return 0;
        }

        public int GetGameBadgeLevel(int seris, bool foil)
        {
            u.Log.WriteLine("GetGameBadgeLevel");
            return 0;
        }

        public int GetSteamLevel()
        {
            u.Log.WriteLine("GetSteamLevel");
            return 0;
        }

        public uint RequestStoreAuthURL(string redirect_url)
        {
            u.Log.WriteLine("RequestStoreAuthURL");
            return 0;
        }

        public bool IsPhoneVerified()
        {
            u.Log.WriteLine("IsPhoneVerified");
            return false;
        }

        public bool IsTwoFactorEnabled()
        {
            u.Log.WriteLine("IsTwoFactorEnabled");
            return false;
        }

        public bool IsPhoneIdentifying()
        {
            u.Log.WriteLine("IsPhoneIdentifying");
            return false;
        }

        public bool IsPhoneRequiringVerification()
        {
            u.Log.WriteLine("IsPhoneRequiringVerification");
            return false;
        }
    }
}
