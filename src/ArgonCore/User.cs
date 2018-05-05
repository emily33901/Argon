using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

using SteamKit2;


namespace ArgonCore
{
    /// <summary>
    ///  Implements a backend representation of a user.
    /// </summary>
    public class User
    {
        // Used for nternal handling of users
        private static uint next_user_id = 0;
        public uint Id { get; private set; }

        // Steamkit helpers for this user
        SteamClient client;
        SteamUser user;
        CallbackManager callback_manager;

        // Whether user is connected / running
        public bool Running { get; private set; }
        public bool Connected { get; private set; }

        // Logging instance for this user
        public Logger Log { get; private set; }

        string username;
        string password;

        /// <summary>
        /// Enum showing what part of the login is missing
        /// </summary>
        enum LogonNeeded
        {
            None,
            TwoFactor,
            AuthCode
        }

        LogonNeeded logon_needs;
        string two_factor;
        string auth_code;

        /// <summary>
        /// Create a new <see cref="User"/> from the requested instance 
        /// </summary>
        /// <param name="instance"></param>
        public User()
        {
            InterfaceLoader.Load();

            Id = next_user_id;
            next_user_id += 1;

            Log = new Logger(this);

            {
                // create our steamclient instance
                client = new SteamClient();

                // create the callback manager which will route callbacks to function calls
                callback_manager = new CallbackManager(client);

                // Setup our packet handler for all packets
                client.AddHandler(new PacketHandler(this));

                callback_manager.Subscribe<SteamClient.ConnectedCallback>(OnConnect);
                callback_manager.Subscribe<SteamClient.DisconnectedCallback>(OnDisconnect);
            }

            user = client.GetHandler<SteamUser>();

            callback_manager.Subscribe<SteamUser.LoggedOnCallback>(UserLoggedOn);
            callback_manager.Subscribe<SteamUser.LoggedOffCallback>(UserLoggedOff);

            logon_needs = LogonNeeded.None;
        }

        /// <summary>
        /// Creates an instance of an interface based on <paramref name="name"/> and assigns this users id
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Pointer to the context created</returns>
        public IntPtr CreateInterface(string name)
        {
            var (context, iface) = InterfaceLoader.CreateInterface(name);

            iface.UserId = (int)Id;

            return context;
        }

        /// <summary>
        /// Creates an instance of an interface based on <paramref name="name"/>
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Pointer to the context created</returns>
        public static IntPtr CreateInterfaceNoUser(string name)
        {
            var (context, iface) = InterfaceLoader.CreateInterface(name);

            iface.UserId = -1;

            return context;
        }

        /// <summary>
        /// Handles connecting the users' client instance to steam.
        /// </summary>
        public void ConnectClient()
        {
            Log.WriteLine("client", "Attempting connection...");
            client.Connect();
            Running = true;
        }

        /// <summary>
        /// Handles connecting the users' client instance to steam.
        /// </summary>
        public void DisconnectClient()
        {
            Log.WriteLine("client", "Disconnecting...");
            client.Disconnect();
            Running = false;
        }

        public void RunFrame()
        {
            callback_manager.RunWaitCallbacks(TimeSpan.FromSeconds(1));
        }

        // Static event handlers
        public void OnConnect(SteamClient.ConnectedCallback cb)
        {
            Log.WriteLine("client", "Connected [token: {0}]", client.SessionToken);

            Connected = true;
        }

        public void OnDisconnect(SteamClient.DisconnectedCallback cb)
        {
            Log.WriteLine("client", "Disconnected");

            Connected = false;
        }

        /// <summary>
        /// Performs the actual logon process using the stored <see cref="username"/>, <see cref="password"/>,
        /// along with the <see cref="auth_code"/>, <see cref="two_factor"/> or if found on disk the sentry hash
        /// </summary>
        private void LogOnInternal()
        {
            if (Connected == false) ConnectClient();

            Thread.Sleep(1000);

            string sentry_name = string.Format("sentry_{0}.bin", username);

            byte[] sentry_hash = null;
            if (File.Exists(sentry_name))
            {
                // if we have a saved sentry file, read and sha-1 hash it
                byte[] sentry_file = File.ReadAllBytes("sentry.bin");
                sentry_hash = CryptoHelper.SHAHash(sentry_file);
            }

            user.LogOn(new SteamUser.LogOnDetails
            {
                Username = username,
                Password = password,

                // in this sample, we pass in an additional authcode
                // this value will be null (which is the default) for our first logon attempt
                AuthCode = auth_code,

                // if the account is using 2-factor auth, we'll provide the two factor code instead
                // this will also be null on our first logon attempt
                TwoFactorCode = two_factor,

                // our subsequent logons use the hash of the sentry file as proof of ownership of the file
                // this will also be null for our first (no authcode) and second (authcode only) logon attempts
                SentryFileHash = sentry_hash,
            });
        }

        /// <summary>
        /// Public facing logon function
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public void LogOn(string username, string password)
        {
            this.username = username;
            this.password = password;

            LogOnInternal();
        }

        /// <summary>
        /// Takes a string representing the required twofactor argument and places it in the correct
        /// variable in order to complete logon
        /// </summary>
        /// <param name="twofactor"></param>
        public void SetTwoFactorAndLogOn(string new_two_factor)
        {
            if (logon_needs == LogonNeeded.AuthCode)
            {
                auth_code = new_two_factor;
            }
            else if (logon_needs == LogonNeeded.TwoFactor)
            {
                two_factor = new_two_factor;
            }
            LogOnInternal();
        }

        /// <summary>
        /// Handles the logon event passing those events down the chain if need be
        /// </summary>
        /// <param name="callback"></param>
        public void UserLoggedOn(SteamUser.LoggedOnCallback callback)
        {
            Log.WriteLine(username, "Logged on");

            if (callback.Result == EResult.NoConnection)
            {
                Log.WriteLine(username, "No connection...");
            }
            else if (callback.Result == EResult.InvalidPassword)
            {
                Log.WriteLine(username, "Invalid Password.");
            }

            bool has_steam_guard = callback.Result == EResult.AccountLogonDenied;

            // Use 2fa as this is different to the global concept of "two factor"
            bool has_2fa = callback.Result == EResult.AccountLoginDeniedNeedTwoFactor;

            if (has_steam_guard || has_2fa)
            {
                Log.WriteLine(username, "Account has 2fa");

                logon_needs = LogonNeeded.TwoFactor;

                return;
            }
        }

        public void UserLoggedOff(SteamUser.LoggedOffCallback callback)
        {
            Log.WriteLine("user", "Logged off");
        }
    }
}
