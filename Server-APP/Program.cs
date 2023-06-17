using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Server_Lib;

namespace Server_APP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();
            server.Start();
            server.RecieveSend();
            Console.WriteLine("Нажмите любую кнопку чтобы завершить...");
            Console.ReadKey();
            server.Stop();
        }
    }
}
