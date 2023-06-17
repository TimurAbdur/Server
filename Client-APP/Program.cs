using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client_LIb;

namespace Client_APP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int marahon = 0, partmarahon = 0;
            int rfid = 1, nagr = 1, bottle = 0, bandana = 0;
            int life = 0, vera = 0, line = 0;
            Console.WriteLine("Добро пожаловать!\nВведите ваше ФИО");
            string surname = Console.ReadLine(), name = Console.ReadLine(), lastname = Console.ReadLine();
            Console.WriteLine("\nВводить строго цифры!");
            Console.WriteLine("\nВыберите марафон в котором хотите учавствовать:\n1) Марафон\n2) Полумарафон");
            int num = int.Parse(Console.ReadLine());
            if (num == 1) {
                marahon = 1; partmarahon = 0;
            }
            else if(num == 2)
            {
                marahon = 0; partmarahon = 1;
            }
            else
            {
                Console.WriteLine("Ошибка! считайте вы выбрали марафон");
                marahon = 1; partmarahon = 0;
            }
            Console.WriteLine("Следующие аксессуары были добавлены вам в корзину:\n1) RFID браслет (50 руб)\n2) нагрудник бегуна (100 руб)");
            Console.WriteLine("Выберите дополнительные аксессуары:\n1) Бутылка воды (300 руб)\n2) Бандана с логотипом (300 руб)\n3) Не нужно");
            int i;
            do
            {
                i = int.Parse(Console.ReadLine());
                if (i == 1) bottle = 1;
                if(i ==2) bandana = 1;
            } while (i < 3 && i > 0);
            
            bool YesOrNo = false;
            do
            {
                Console.WriteLine("Выберите благотворительный фонд. Можно выбрать несколько:\n1) Подари жизнь\n2) Вера\n3) Линия");
                i = int.Parse(Console.ReadLine());
                if (i == 1) life = 1;
                if (i == 2) vera = 1;
                if (i == 3) line = 1;
                Console.WriteLine("Хотите выбрать еще фонд?\n1) Да\n2) Нет");
                i = int.Parse(Console.ReadLine());
                if (i == 1) YesOrNo = true;
                else YesOrNo = false;
            } while (YesOrNo);
            string message = $"{{{surname},{name},{lastname}}},{{{marahon},{partmarahon}}},{{{rfid},{nagr},{bottle},{bandana}}},{{{life},{vera},{line}}}";
            Client client = new Client();
            client.Connect();
            client.SendRecieve(message);
            Console.ReadLine();
            client.Disconnect();
        }
    }
}
