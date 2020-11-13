using System;
namespace DealerPlatformApiDemo.Common.RedisHelper
{
    public static partial class RedisHelper
    {
        public static void SetStringMemory(string key,string value,TimeSpan ts){
            db.StringSet(key,value,ts);
        }
        public static void AppendStringMemory(string key,string value){
            db.StringAppend(key,value);
        }
        public static string GetStringMemory(string key){
           return db.StringGet(key);
        }
    }
}