using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using DealerPlatformApiDemo.Core;
using StackExchange.Redis;

namespace DealerPlatformApiDemo.Common.RedisHelper {
    public static partial class RedisHelper {
        public static void SetHashMemory (string key, Dictionary<string, string> values) {
            var hashEntry = new List<HashEntry> ();
            foreach (var value in values) {
                hashEntry.Add (new HashEntry (value.Key, value.Value));
            }
            db.HashSet (key, hashEntry.ToArray ());
        }
        public static void SetHashMemory (string key, params HashEntry[] entries) {
            db.HashSet (key, entries);
        }

        public static void SetHashMemory<T> (string key, T entity) where T : BaseEntity {
            List<HashEntry> hashEntries = new List<HashEntry> ();
            Type type = typeof (T);
            PropertyInfo[] props = type.GetProperties ();
            foreach (var prop in props) {
                string name = prop.Name;
                object value = prop.GetValue (entity);
                if (value.GetType ().Name == "Boolean") value = (bool) value?1 : 0;
                hashEntries.Add (new HashEntry (name, value?.ToString ()));
            }
            db.HashSet (key, hashEntries.ToArray ());
        }

        public static void SetHashMemory<T> (string key, IEnumerable<T> entities) where T : BaseEntity {
            foreach (var entity in entities) {
                SetHashMemory (key + ":" + entity.Id, entity);
            }
        }
        /// <summary>
        /// 根据Key或者该Key下所有的键值对(不推荐，除非是取单独的一个对象)
        /// </summary>
        /// <param name="key"></param>
        // public static void GetHashMemory (string key) {
        //     var res = db.HashGetAll (key);
        //     /*
        //      ** 结果是：
        //      ** [1] {Id:1}
        //      ** [2] {UserName:Ace}
        //      ** 这个结果显然不是我们想要的
        //      */
        //     var list = res.ToList ();
        // }

        // public static List<T> GetHashMemory<T> (string key)where T:BaseEntity{

        // }
        public static List<T> GetHashMemory<T> (string key) where T : BaseEntity, new () {
            var keyList = GetKeys (key);
            List<T> list = new List<T> ();
            foreach (var keyItem in keyList) {
                var res = db.HashGetAll (keyItem);
                // Type type = typeof(T);
                // var t = Activator.CreateInstance(type);
                T t = new T ();
                var props = t.GetType ().GetProperties ();
                foreach (var item in res) {
                    foreach (var prop in props) {
                        if (prop.Name == item.Name) {
                            prop.SetValue (t, Convert.ChangeType (item.Value, prop.PropertyType));
                            break;
                        }
                    }
                }
                 list.Add (t);
            }
           return list;
        }
        public static bool HashDelete(string redisKey,string hashField){
            return db.HashDelete(redisKey,hashField);
        }
    }
}