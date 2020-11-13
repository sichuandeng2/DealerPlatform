using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
namespace DealerPlatformApiDemo.Common.SocketHelper
{
    public static class ByteConvert
    {
        public static byte[] Serialize(this object obj){
            byte[] buffer;
            using(MemoryStream ms= new MemoryStream()){
                IFormatter formatter = new BinaryFormatter();
                //把obj的传输内容序列化给了内存
                formatter.Serialize(ms,obj);
                buffer = ms.GetBuffer();
            }
            return buffer;
        }

        public static T Deserialize<T>(this byte[] buffer){
            T t;
            using(MemoryStream ms  =new MemoryStream(buffer)){
                IFormatter formatter = new BinaryFormatter();
                t =(T)formatter.Deserialize(ms);
            }
            return t;
        }
    }
}