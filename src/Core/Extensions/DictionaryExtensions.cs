using System;
using System.Collections.Generic;

namespace Core
{
    public static class DictionaryExtensions
    {
        public static Value FindOrCreate<Key, Value>(this IDictionary<Key, Value> dictionary, Key key)
        where Value : new()
        {
            if (dictionary.TryGetValue(key, out var v)) return v;

            v = new Value();
            return (dictionary[key] = v);
        }
    }
}