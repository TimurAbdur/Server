using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client_LIb
{
    public class Client
    {
        static IPHostEntry ipHost = Dns.GetHostEntry("localhost");
        static IPAddress ipAdd = ipHost.AddressList[0];
        static IPEndPoint ipEndPoint = new IPEndPoint(ipAdd, 8888);
        static Socket socket = new Socket(ipAdd.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        public void Connect()
        {
            socket.Connect(ipEndPoint);
        }

        public void SendRecieve(string message)
        {

            byte[] data = new byte[1024];
            data = Encoding.UTF8.GetBytes(message);
            socket.Send(data);
            Console.WriteLine("Данные отправлены!");
            socket.Receive(data);
            string recieveStr = Encoding.UTF8.GetString(data);
            Console.WriteLine(recieveStr);
        }

        public void Disconnect()
        {
            socket.Disconnect(false);
        }
    }
}
