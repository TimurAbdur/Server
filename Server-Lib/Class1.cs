using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Server_Lib
{
    public class Server
    {
        FileStream log;
        string dataStart, dataStop;
        int Sum;
        //Количество выбранных благотворителнз фондов
        int K;

        static IPHostEntry ipHost = Dns.GetHostEntry("localhost");
        static IPAddress ipAdd = ipHost.AddressList[0];
        static IPEndPoint ipEndPoint = new IPEndPoint(ipAdd, 8888);
        static Socket socket = new Socket(ipAdd.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        public void Start()
        {
            dataStart = DateTime.Now.ToString("dd.MM.yy hh.mm.ss");
            log = new FileStream($"registration_marathon[{dataStart}].txt", FileMode.Create, FileAccess.Write);
            byte[] data = new byte[1024];
            data = Encoding.UTF8.GetBytes("Сервер запустился: " + dataStart + "\n");
            log.Write(data, 0, data.Length);

            socket.Bind(ipEndPoint);
            Console.WriteLine("Сервер работает");

           
        }

        public void RecieveSend()
        {
            try
            {
                byte[] data = new byte[1024];
                socket.Listen(10);
                Socket socket2 = socket.Accept();
                socket2.Receive(data);
                string recieveStr = Encoding.UTF8.GetString(data);
                Console.WriteLine("Данные от клиента: " + recieveStr);

                string[] obrabotka = recieveStr.Replace("{", "").Replace("}", "").Split(',');
                obrabotka[11] = obrabotka[11][0] + "";
                Sum = 150;
                if (obrabotka[3] == "1") Sum += 100;
                else Sum += 50;

                if (obrabotka[7] == "1") Sum += 300;
                if (obrabotka[8] == "1") Sum += 300;


                if (obrabotka[9] == "1") K++;
                if(obrabotka[10] == "1") K++;
                if (obrabotka[11] == "1") K++;

                
                string surname = obrabotka[0], name = obrabotka[1], lastname = obrabotka[2];
                byte[] dataToLog = new byte[1024];
                dataToLog = Encoding.UTF8.GetBytes($"Полученные данные: {recieveStr}\n");
                log.Write(dataToLog, 0, dataToLog.Length);
                dataToLog = Encoding.UTF8.GetBytes($"Запрос от клиента: {surname} {name} {lastname}\n");
                log.Write(dataToLog, 0, dataToLog.Length);
                if (K == 1)
                {
                    dataToLog = Encoding.UTF8.GetBytes($"Сумма пожертований в одни фонд: {Sum} руб\n");
                }
                if (K == 2)
                {
                    dataToLog = Encoding.UTF8.GetBytes($"Сумма пожертований в два фонд: {Sum} руб\nНа каждый фонд по {Sum / 2} руб");
                }
                if (K == 3)
                {
                    dataToLog = Encoding.UTF8.GetBytes($"Сумма пожертований в три фонд: {Sum} руб\nНа каждый фонд по {Sum / 3} руб");
                }

                log.Write(dataToLog, 0, dataToLog.Length);

                byte[] data2 = new byte[1024];  
                data2 = Encoding.UTF8.GetBytes("Ваш запрос обработан!");
                socket2.Send(data2);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Stop()
        {
            //socket.Shutdown(SocketShutdown.Both);
            socket.Close();

            dataStop = DateTime.Now.ToString("dd.MM.yy hh.mm.ss");
            byte[] data = new byte[1024];
            data = Encoding.UTF8.GetBytes("\nСервер завершил работу: " + dataStop);
            log.Write(data, 0, data.Length);
        }
    }
}
