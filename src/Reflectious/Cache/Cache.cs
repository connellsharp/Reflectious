using System;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace Reflectious
{
    internal class Cache<TKey, TItem>
    {
        private readonly ConcurrentDictionary<TKey, TItem> _dictionary = new ConcurrentDictionary<TKey, TItem>();

        public TItem GetOrAdd(TKey key, Func<TItem> create)
        {
            Debug.Assert(_dictionary.Count < 100, "Shouldn't really be adding this many cache items unless there's a bug.");
                
            return _dictionary.GetOrAdd(key, k => create());
        }
    }
}