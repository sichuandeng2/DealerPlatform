using System.Collections.Generic;
using System.Linq;
using StackExchange.Redis;

namespace DealerPlatformApiDemo.Common.RedisHelper {
    public static partial class RedisHelper {
        private static ConnectionMultiplexer redis { get; set; }
        public static IDatabase db { get; set; }
        static RedisHelper () {
            ConfigurationOptions configuration =
                ConfigurationOptions.Parse ("127.0.0.1:6001,password=1qaz2wsx");
            //如果没有这句话，则无法模糊搜索key
            configuration.AllowAdmin = true;
            redis = ConnectionMultiplexer.Connect (configuration);
            db = redis.GetDatabase ();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">testEntityHash:*</param>
        /// <returns></returns>
        public static List<string> GetKeys (string key) {
            List<string> keyList = new List<string> ();
            var eps = redis.GetEndPoints ();
            var ep = eps[0];
            var server = redis.GetServer (ep);
            var keys = server.Keys (0, key);
            keys.ToList ().ForEach (e => {
                keyList.Add (e.ToString ());
            });
            return keyList;
        }
        public static void RemoveKey (string key) {
            var keyList = GetKeys (key);
            foreach (var item in keyList) {
                db.KeyDelete (item);
            }
        }
        public static bool KeyExists (string redisKey) {
            return db.KeyExists (redisKey);
        }
    }
}