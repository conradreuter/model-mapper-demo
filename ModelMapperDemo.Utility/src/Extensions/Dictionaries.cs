using System;
using System.Collections.Generic;

namespace ModelMapperDemo.Utility.Extensions
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Get the value associated with the key from the dictionary, or insert a value.
        /// </summary>
        public static TValue GetOrInsert<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            TKey key,
            Func<TValue> valueCreator)
        {
            if (!dictionary.TryGetValue(key, out var value))
            {
                value = valueCreator();
                dictionary.Add(key, value);
            }
            return value;
        }
    }
}
