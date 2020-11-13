
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Caching.Memory;

namespace DealerPlatformApiDemo.Common
{
    public  class MemoryHelper
    {
        private static IMemoryCache _memoryCache = null;

        static MemoryHelper()
        {
            if (_memoryCache == null)
            {
                _memoryCache = new MemoryCache(new MemoryCacheOptions());
            }
        }

        public static void SetMemory(string key, object value)
        {
            _memoryCache.Set(key, value);
        }
        public static void SetMemory(string key, object value,TimeSpan ts)
        {
            _memoryCache.Set(key, value,ts);
        }

        public static object GetMemory(string key)
        {
           return _memoryCache.Get(key);
        }
    }
}
