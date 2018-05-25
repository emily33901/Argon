using System;
using System.IO;
using System.Security.Cryptography;
using ArgonCore;
using ArgonCore.Interface;
using ArgonCore.Server;

using SteamKit2;

namespace InterfaceUser
{
    public class User
    {
        public enum LogonState
        {
            LoggedOff,
            LoggingOn,
            LoggingOff,
            LoggedOn,
        }

        SteamKit2.SteamUser steam_user;

        IBaseInterface Parent { get; set; }
        int ClientId { get { return Parent.ClientId; } }
        Client Instance { get { return Client.GetClient(ClientId); } }

        Logger Log { get { return Instance.Log; } }

        public User(IBaseInterface parent)
        {
            Parent = parent;

            steam_user = Instance.SteamClient.GetHandler<SteamKit2.SteamUser>();

            Instance.CallbackManager.Subscribe<SteamUser.LoggedOnCallback>(cb => OnLoggedOn(cb));
            Instance.CallbackManager.Subscribe<SteamUser.LoggedOffCallback>(cb => OnLoggedOff(cb));
            Instance.CallbackManager.Subscribe<SteamUser.UpdateMachineAuthCallback>(cb => OnMachineAuth(cb));

            // TODO: we need to add a logonkey handler to allow for offline login
        }

        LogonState logon_state;

        public LogonState GetLogonState()
        {
            return logon_state;
        }

        public bool Connected()
        {
            return Instance.Connected;
        }

        public int GetHandle()
        {
            return ClientId + 1;
        }

        public ulong SteamId { get { return steam_user.SteamID.ConvertToUInt64(); } }
        public ulong InstanceId { get { return steam_user.SteamID.AccountInstance; } }

        public enum LogonNeeds
        {
            SteamGuard,
            TwoFactor,
            None,
        }

        LogonNeeds logon_needs;

        string email;
        string username;
        string password;
        string two_factor_code;
        string steam_guard_code;

        void OnMachineAuth(SteamUser.UpdateMachineAuthCallback cb)
        {
            Log.WriteLine("user", "Updating sentryfile...");

            string sentry_name = String.Format("sentry_{0}.bin", username);

            int fileSize;
            byte[] sentryHash;
            using (var fs = File.Open(sentry_name, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                fs.Seek(cb.Offset, SeekOrigin.Begin);
                fs.Write(cb.Data, 0, cb.BytesToWrite);
                fileSize = (int)fs.Length;

                fs.Seek(0, SeekOrigin.Begin);
                using (var sha = SHA1.Create())
                {
                    sentryHash = sha.ComputeHash(fs);
                }
            }

            // inform the steam servers that we're accepting this sentry file
            steam_user.SendMachineAuthResponse(new SteamUser.MachineAuthDetails
            {
                JobID = cb.JobID,

                FileName = cb.FileName,

                BytesWritten = cb.BytesToWrite,
                FileSize = fileSize,
                Offset = cb.Offset,

                Result = EResult.OK,
                LastError = 0,

                OneTimePassword = cb.OneTimePassword,

                SentryFileHash = sentryHash,
            });

            Log.WriteLine("user", "Finished writing SentryFile...");
        }

        void OnAccountLogonDenied(SteamUser.LoggedOnCallback cb)
        {
            logon_state = LogonState.LoggedOff;

            Log.WriteLine("user", "OnAccountLogonDenied: {0} / {1]", cb.Result, cb.ExtendedResult);

            switch (cb.Result)
            {
                case EResult.AccountLoginDeniedNeedTwoFactor:
                    {
                        Log.WriteLine("user", "Needs twofactor code...");
                        logon_needs = LogonNeeds.TwoFactor;
                        return;
                    }
                case EResult.AccountLogonDenied:
                    {
                        Log.WriteLine("user", "Needs steamguard code...");
                        logon_needs = LogonNeeds.SteamGuard;
                        return;
                    }
            }
        }
        void OnAccountLogonSuccess(SteamUser.LoggedOnCallback cb)
        {
            logon_state = LogonState.LoggedOn;
            logon_needs = LogonNeeds.None;

            Log.WriteLine("user", "Logon succeeded!");
        }

        void OnLoggedOn(SteamUser.LoggedOnCallback cb)
        {
            switch (cb.Result)
            {
                case EResult.AccountLogonDenied:
                case EResult.AccountLoginDeniedNeedTwoFactor:
                    {
                        OnAccountLogonDenied(cb);
                        return;
                    }
                case EResult.OK:
                    {
                        OnAccountLogonSuccess(cb);
                        return;
                    }

                default:
                    {
                        Log.WriteLine("user", "Unable to logon to Steam: {0} / {1}", cb.Result, cb.ExtendedResult);
                        return;
                    }
            }
        }

        void OnLoggedOff(SteamUser.LoggedOffCallback cb)
        {
            Log.WriteLine("user", "Logged off...");
        }

        public void LogonInternal()
        {
            if (Connected() == false) Instance.Connect();

            string sentry_name = string.Format("sentry_{0}.bin", username);

            byte[] sentry_hash = null;
            if (File.Exists(sentry_name))
            {
                // if we have a saved sentry file, read and sha-1 hash it
                byte[] sentry_file = File.ReadAllBytes(sentry_name);
                sentry_hash = CryptoHelper.SHAHash(sentry_file);
            }

            steam_user.LogOn(new SteamUser.LogOnDetails
            {
                Username = username,
                Password = password,

                // this value will be null (which is the default) for our first logon attempt
                AuthCode = steam_guard_code,

                // this will also be null on our first logon attempt
                TwoFactorCode = two_factor_code,

                // our subsequent logons use the hash of the sentry file as proof of ownership of the file
                // this will also be null for our first (no authcode) and second (authcode only) logon attempts
                SentryFileHash = sentry_hash,
            });
        }

        public void LogOnUsernamePassword(string new_username, string new_password)
        {
            logon_needs = LogonNeeds.None;
            username = new_username;
            password = new_password;
        }

        public void SetTwoFactor(string code)
        {
            if (logon_needs == LogonNeeds.TwoFactor)
            {
                // Steam authenticator
                two_factor_code = code;
            }
            else if (logon_needs == LogonNeeds.SteamGuard)
            {
                // Steam guard
                steam_guard_code = code;
            }
            else
            {
                Log.WriteLine("user", "SetTwoFactor called but nothing needed!");
            }
        }
    }
}