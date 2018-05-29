using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quobject.SocketIoClientDotNet.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace test1
{
    class Program
    {
        static void Main(string[] args)
        {
            var socket = IO.Socket("htt://dev.pingfang.net:5004");
            //socket.Open();


            ConnectionCreate con = new ConnectionCreate();
            con.clientCode = "123123123";
            con.clientName = "lane123";
            JToken jtoken = JToken.FromObject(con);
            socket.On(Socket.EVENT_ERROR, (data) =>
            {


                Console.WriteLine(data);
            });

            socket.On("123", (data) =>
            {
                Console.WriteLine(data);
            });
            socket.Emit("authentication", (data) =>
            {               
                Console.WriteLine(data);
               
            },
                  JsonConvert.SerializeObject(con));

            Thread.Sleep(1000);
            socket.Emit("p2p", JsonConvert.SerializeObject(new P2p("123", "中文")));


            Console.Read();
        }

        /// <summary>
        /// UTF8转换成GB2312
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string utf8_gb2312(string text)
        {
            //声明字符集   
            System.Text.Encoding utf8, gb2312;
            //utf8   
            utf8 = System.Text.Encoding.GetEncoding("utf-8");
            //gb2312   
            gb2312 = System.Text.Encoding.GetEncoding("gb2312");
            byte[] utf;
            utf = utf8.GetBytes(text);
            utf = System.Text.Encoding.Convert(utf8, gb2312, utf);
            //返回转换后的字符   
            return gb2312.GetString(utf);
        }
    }




}
