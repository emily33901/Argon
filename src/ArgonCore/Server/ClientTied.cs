using System;
using System.Collections.Generic;
using ArgonCore;

namespace ArgonCore
{
    public class ClientTied<T>
    where T : ClientTied<T>, new()
    {
        private static Dictionary<int, T> Active { get; set; } = new Dictionary<int, T>();
        protected int ClientId { get; private set; }
        protected Server.Client Instance { get { return Server.Client.GetClient(ClientId); } }
        protected static Logger Log { get; set; } = new Logger(typeof(T).FullName);
        public static T FindOrCreate(int id)
        {
            if (Active.TryGetValue(id, out var found))
            {
                return found;
            }

            Log.WriteLine("Creating new instance for id {0}", id);

            Active[id] = new T();

            var ct = Active[id] as ClientTied<T>;
            ct.ClientId = id;

            return Active[id];
        }
    }
}