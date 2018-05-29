using Quobject.SocketIoClientDotNet.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;


namespace ConnectionTest
{
    class Program
    {

        static void Main(string[] args)
        {
            Uri url = Url.Parse("http://localhost:5004");

            var socket = IO.Socket(url);

            socket.Open();

            
            Console.Read();

        }

        private static void Client_Error(object sender, SocketIOClient.ErrorEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }

}
