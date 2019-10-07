using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace IBAMembersApp.Utilities
{
    public class MemoryCacher
    {
        public object GetValue(string key)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Get(key);
        }

        public bool Add(string key, object value, CacheItemPolicy slidingExp)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Add(key, value, slidingExp);
        }

        public List<string> GetAllKeys()
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Select(r=>r.Key).ToList();
        }

        public void Delete(string key)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            if (memoryCache.Contains(key))
            {
                memoryCache.Remove(key);
            }
        }
    }
}