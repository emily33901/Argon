using System;
using System.Collections.Generic;
using ArgonCore;

namespace ArgonCore
{
    public class ClientTied<T>
    where T : ClientTied<T>, new()
    {
        // All clienttied instances of this type
        private static Dictionary<int, T> Active { get; set; } = new Dictionary<int, T>();

        // Client id that this is "tied" too
        protected int ClientId { get; private set; }

        // TODO: should both of these be merged? clientid can be got from the client
        // Quick reference to the client via the clientid
        protected Server.Client Instance { get { return Server.Client.GetClient(ClientId); } }

        // Logger for this tied instance
        protected static Logger LogClientTied { get; set; } = new Logger(typeof(T).FullName);
        public Logger Log { get; set; }

        // Init function for the init order fiasco problems
        public virtual void Init() { }

        public static T FindOrCreate(int id)
        {
            if (Active.TryGetValue(id, out var found))
            {
                return found;
            }

            LogClientTied.WriteLine("Creating new instance of {1} for id {0}", id, typeof(T).Name);

            Active[id] = new T
            {
                ClientId = id,
                Log = new LoggerUid(typeof(T).FullName, id),
            };

            Active[id].Init();

            return Active[id];
        }
    }
}