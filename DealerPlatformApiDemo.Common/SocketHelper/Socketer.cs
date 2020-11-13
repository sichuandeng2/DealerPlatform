using System.Net;
using System.Threading;
using System.Net.Sockets;
using System;
using System.IO;
using Newtonsoft.Json;

namespace DealerPlatformApiDemo.Common.SocketHelper
{
    public class Socketer
    {
        private Socket _socket;
        public delegate void ReveiveDataEventHandler(byte headByte,byte[] data,IPAddress iPAddress,int port);
        public event ReveiveDataEventHandler receiveData;
        private bool _startReceive;

        public bool StartReceive{
            get=>_startReceive;
            set{
                  _startReceive = value;
                if(value){
                  ReceiveData();
                }else{
                    Thread.Sleep(30);
                }
            }
        }
        public Socketer()
        {
            _socket = new Socket(AddressFamily.InterNetwork,SocketType.Dgram,ProtocolType.Udp);
        }

        private void ReceiveData()
        {
            //配置监听IP和端口号
            EndPoint ipEndPoint =  new IPEndPoint(IPAddress.Any,6566);
            //绑定Ip和端口号（到这一步位置ipEndPoint这个变量的第一个使命已经完成）
            _socket.Bind(ipEndPoint);
            ThreadPool.QueueUserWorkItem(o=>{
                while(StartReceive){
                    byte[] buffer = new byte[1024*1024];
                    //这里吧ipEndPoint进行废物利用，让他接受发来信息电脑的IP地址
                    int length = _socket.ReceiveFrom(buffer,ref ipEndPoint);
                    if(length>0){
                        byte[] newBuffer = null;
                        using(MemoryStream ms = new MemoryStream()){
                            ms.Write(buffer,1,length);
                            newBuffer = ms.ToArray();
                        }
                        //太监：皇上驾到
                        receiveData(buffer[0],newBuffer,((IPEndPoint)ipEndPoint).Address,((IPEndPoint)ipEndPoint).Port);
                    }
                    Thread.Sleep(1);
                }
            });
        }

        public void SendData(byte[] buffer,string ip,int port){
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ip),port);
            _socket.SendTo(buffer,iPEndPoint);
        }
    }
}