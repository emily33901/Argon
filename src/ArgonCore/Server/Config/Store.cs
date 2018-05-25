using System;
using System.Collections.Generic;
using System.Text;
using System.Dynamic;

using System.Runtime.Serialization;
using System.IO;
using System.Xml.Serialization;

namespace ArgonCore.Server.Config
{
    // Represents a CConfigStore
    // Configstore is usually mirrored to server in some form
    // TODO: this needs to be reversed more
    public class Store
    {
        [Serializable]
        public class Node : DynamicObject
        {
            Dictionary<string, object> store;

            public Node()
            {
                store = new Dictionary<string, object>();
            }

            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                return store.TryGetValue(binder.Name, out result);
            }
        }

        public Node RootNode { get; set; }

        Store()
        {
            RootNode = new Node();
        }

        void WriteToFile(string name)
        {
            var serialiser = new XmlSerializer(typeof(Node));
            using (TextWriter write = new StreamWriter(name))
            {
                serialiser.Serialize(write, RootNode);
            }
        }

    }
}
