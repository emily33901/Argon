using System;

// Autogenerated @ 23/07/18
namespace InterfaceUser
{
    /// <summary>
    /// Implements the map for interface SteamUser019
    /// </summary>
    [ArgonCore.Interface.Map(Name = "SteamUser019")]
    public class SteamUser019_Map : ArgonCore.Interface.IBaseInterfaceMap
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
            return Client.ClientPipe.CallSerializedFunction<int>(PipeId, f);
        }
        public bool LoggedOn(IntPtr _)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "LoggedOn",
               Args = new object[] {},
            };
            return Client.ClientPipe.CallSerializedFunction<bool>(PipeId, f);
        }
        public ulong GetSteamID(IntPtr _)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "GetSteamID",
               Args = new object[] {},
            };
            return Client.ClientPipe.CallSerializedFunction<ulong>(PipeId, f);
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
            return Client.ClientPipe.CallSerializedFunction<int>(PipeId, f);
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
            Client.ClientPipe.CallSerializedFunction(PipeId, f);
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
            Client.ClientPipe.CallSerializedFunction(PipeId, f);
        }
        public bool GetUserDataFolder(IntPtr _, string buffer, int count)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "GetUserDataFolder",
               Args = new object[] {buffer, count},
            };
            return Client.ClientPipe.CallSerializedFunction<bool>(PipeId, f);
        }
        public void StartVoiceRecording(IntPtr _)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "StartVoiceRecording",
               Args = new object[] {},
            };
            Client.ClientPipe.CallSerializedFunction(PipeId, f);
        }
        public void StopVoiceRecording(IntPtr _)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "StopVoiceRecording",
               Args = new object[] {},
            };
            Client.ClientPipe.CallSerializedFunction(PipeId, f);
        }
        public uint GetAvailableVoice(IntPtr _, System.UInt32[] compressed_data, System.UInt32[] uncompressed, uint desired_sample_rate)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "GetAvailableVoice",
               Args = new object[] {compressed_data, uncompressed, desired_sample_rate},
            };
            return Client.ClientPipe.CallSerializedFunction<uint>(PipeId, f);
        }
        public int GetVoice(IntPtr _, bool want_compressed, IntPtr dest_buffer, uint dest_buffer_size, ref System.UInt32 compressed_bytes_written, bool wants_uncompressed, IntPtr uncompressed_dest, uint uncompressed_buffer_size, ref System.UInt32 bytes_written, uint uncompressed_desired_samplerate)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "GetVoice",
               Args = new object[] {want_compressed, dest_buffer, dest_buffer_size, compressed_bytes_written, wants_uncompressed, uncompressed_dest, uncompressed_buffer_size, bytes_written, uncompressed_desired_samplerate},
            };
            return Client.ClientPipe.CallSerializedFunction<int>(PipeId, f);
        }
        public int DecompressVoice(IntPtr _, IntPtr compressed, uint compressed_size, IntPtr dest_buffer, uint dest_size, ref System.UInt32 bytes_written, uint sample_rate)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "DecompressVoice",
               Args = new object[] {compressed, compressed_size, dest_buffer, dest_size, bytes_written, sample_rate},
            };
            return Client.ClientPipe.CallSerializedFunction<int>(PipeId, f);
        }
        public uint GetOptimalSampleRate(IntPtr _)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "GetOptimalSampleRate",
               Args = new object[] {},
            };
            return Client.ClientPipe.CallSerializedFunction<uint>(PipeId, f);
        }
        public int GetAuthSessionTicket(IntPtr _, IntPtr ticket, uint ticket_size, ref System.Int32 ticket_written)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "GetAuthSessionTicket",
               Args = new object[] {ticket, ticket_size, ticket_written},
            };
            return Client.ClientPipe.CallSerializedFunction<int>(PipeId, f);
        }
        public uint BeginAuthSession(IntPtr _, IntPtr ticket, uint ticket_size, ulong steamid)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "BeginAuthSession",
               Args = new object[] {ticket, ticket_size, steamid},
            };
            return Client.ClientPipe.CallSerializedFunction<uint>(PipeId, f);
        }
        public void EndAuthSession(IntPtr _, ulong steam_id)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "EndAuthSession",
               Args = new object[] {steam_id},
            };
            Client.ClientPipe.CallSerializedFunction(PipeId, f);
        }
        public void CancelAuthTicket(IntPtr _, int ticket_handle)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "CancelAuthTicket",
               Args = new object[] {ticket_handle},
            };
            Client.ClientPipe.CallSerializedFunction(PipeId, f);
        }
        public uint UserHasLicenseForApp(IntPtr _, ulong steamID, uint appID)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "UserHasLicenseForApp",
               Args = new object[] {steamID, appID},
            };
            return Client.ClientPipe.CallSerializedFunction<uint>(PipeId, f);
        }
        public void AdvertiseGame(IntPtr _, ulong game_server_id, uint server_ip, System.UInt16 server_port)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "AdvertiseGame",
               Args = new object[] {game_server_id, server_ip, server_port},
            };
            Client.ClientPipe.CallSerializedFunction(PipeId, f);
        }
        public uint RequestEncryptedAppTicket(IntPtr _, IntPtr data_to_include, uint data_size)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "RequestEncryptedAppTicket",
               Args = new object[] {data_to_include, data_size},
            };
            return Client.ClientPipe.CallSerializedFunction<uint>(PipeId, f);
        }
        public int GetGameBadgeLevel(IntPtr _, int seris, bool foil)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "GetGameBadgeLevel",
               Args = new object[] {seris, foil},
            };
            return Client.ClientPipe.CallSerializedFunction<int>(PipeId, f);
        }
        public int GetSteamLevel(IntPtr _)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "GetSteamLevel",
               Args = new object[] {},
            };
            return Client.ClientPipe.CallSerializedFunction<int>(PipeId, f);
        }
        public uint RequestStoreAuthURL(IntPtr _, string redirect_url)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "RequestStoreAuthURL",
               Args = new object[] {redirect_url},
            };
            return Client.ClientPipe.CallSerializedFunction<uint>(PipeId, f);
        }
        public bool IsPhoneVerified(IntPtr _)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "IsPhoneVerified",
               Args = new object[] {},
            };
            return Client.ClientPipe.CallSerializedFunction<bool>(PipeId, f);
        }
        public bool IsTwoFactorEnabled(IntPtr _)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "IsTwoFactorEnabled",
               Args = new object[] {},
            };
            return Client.ClientPipe.CallSerializedFunction<bool>(PipeId, f);
        }
        public bool IsPhoneIdentifying(IntPtr _)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "IsPhoneIdentifying",
               Args = new object[] {},
            };
            return Client.ClientPipe.CallSerializedFunction<bool>(PipeId, f);
        }
        public bool IsPhoneRequiringVerification(IntPtr _)
        {
            var f = new ArgonCore.IPC.SerializedFunction
            {
               ClientId = ClientId,
               InterfaceId = InterfaceId,
               Name = "IsPhoneRequiringVerification",
               Args = new object[] {},
            };
            return Client.ClientPipe.CallSerializedFunction<bool>(PipeId, f);
        }
    }
}
