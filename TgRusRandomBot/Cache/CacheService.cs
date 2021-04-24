using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgRusRandomBot.Cache
{
    public class CacheService
    {
        private static readonly IMemoryCache Cache = new MemoryCache(new MemoryCacheOptions());

        public static bool TryGetValue<T>(string key, out T value)
        {
            return Cache.TryGetValue(key, out value);
        }

        public static T Set<T>(string key, T value)
        {
            Cache.Remove(key);
            Cache.Set(key, value);
            return value;
        }
    }
}
