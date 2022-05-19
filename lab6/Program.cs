using System;
using static System.Console;

namespace Lab06
{
    internal class Program
    {
        struct queue
        {
            private string[] lineral;
            public queue(int length)
            {
                lineral = new string[length];
            }
            public void dequeue()
            {
                for (int i = 0; i < lineral.Length - 1; i++)
                {
                    if (lineral[i] == null)
                    {
                        return;
                    }
                    lineral[i] = lineral[i + 1];
                }
                lineral[lineral.Length - 1] = null;
                return;
            }
            public bool isEmpty()
            {
                for (int i = 0; i < lineral.Length; i++)
                {
                    if (lineral[i] != null)
                    {
                        return false;
                    }
                }
                return true;
            }
            public string head() => lineral[0];
            public void enqueue(string element)
            {
                if (lineral[lineral.Length - 1] != null)
                {
                    for (int i = 0; i < lineral.Length; i++)
                    {
                        Clear();
                        WriteLine("Черга переповнена, видаляємо послідовно елементи\n");
                        Show();
                        Write("Натисніть клавішу, щоб продовжити...");
                        ReadKey();
                        dequeue();
                    }
                }
                else
                {
                    for (int i = 0; i < lineral.Length; i++)
                    {
                        if (lineral[i] == null)
                        {
                            lineral[i] = element;
                            break;
                        }
                    }
                }
                
            }
            public void Show()
            {
                for (int i = lineral.Length - 1; i >= 0; i--)
                {
                    WriteLine($"|{lineral[i],7}|");
                }
            }
        }
        static bool isTrue(string element)
        {
            bool result = true;
            if (element.Length == 3)
            {
                result &= (int)element[0] >= 65 && (int)element[0] <= 90;
                result &= (int)element[1] >= 48 && (int)element[1] <= 57;
                result &= (int)element[2] >= 65 && (int)element[2] <= 90;
            }
            else if (element.Length == 7)
            {
                for (int i = 0; i < 7; i++)
                {
                    if (i % 3 == 0)
                        result &= (int)element[i] >= 65 && (int)element[i] <= 90;
                    else
                        result &= (int)element[i] >= 48 && (int)element[i] <= 57;
                }
            }
            else
                result = false;
            return result;
        }
        static void Main(string[] args)
        {
            OutputEncoding = System.Text.Encoding.UTF8;
            queue linQueue =new queue(6);
            string element, X;
        a:  WriteLine("Добрий день. Визначте який спосіб вам потрібен:");
            WriteLine("1)Контрольний приклад;");
            WriteLine("2)Ввести з клавіатури;");
            switch (ReadKey().Key)
            {
                case ConsoleKey.D1:
                    Clear();
                    linQueue.enqueue("E39S24R");
                    Write("Маємо чергу, що має розмір 6.\n");
                    linQueue.Show();
                    WriteLine("Зараз в черзі 1 елемент. Введемо F79H23S, щоб додати елемент до черги");
                    Write("Натисніть клавішу, щоб піти далі...");
                    ReadKey();

                    Clear();
                    linQueue.enqueue("F79H23S");
                    Write("Тепер наша черга має 2 елемента\n");
                    linQueue.Show();
                    WriteLine("Введемо C43X61W, щоб додати ще один елемент до черги");
                    Write("Натисніть клавішу, щоб піти далі...");
                    ReadKey();

                    Clear();
                    linQueue.enqueue("C43X61W");
                    Write("Тепер наша черга має 3 елемента\n");
                    linQueue.Show();
                    WriteLine("Видалимо перший елемент, для цього пишемо K4G");
                    Write("Натисніть клавішу, щоб піти далі...");
                    ReadKey();

                    Clear();
                    linQueue.dequeue();
                    Write("Тепер наша черга має 2 елемента\n");
                    linQueue.Show();
                    WriteLine("Видалимо знову перший елемент, для цього пишемо F9G");
                    Write("Натисніть клавішу, щоб піти далі...");
                    ReadKey();

                    Clear();
                    linQueue.dequeue();
                    Write("Тепер наша черга має 1 елемент\n");
                    linQueue.Show();
                    WriteLine("Видалимо останній елемент, для цього пишемо C2R");
                    Write("Натисніть клавішу, щоб піти далі...");
                    ReadKey();

                    Clear();
                    linQueue.dequeue();
                    Write("Тепер наша черга порожня\n");
                    linQueue.Show();
                    Write("Натисніть клавішу, щоб піти далі...");
                    ReadKey();
                    break;
                case ConsoleKey.D2:
                b:  Clear();
                    Write("Визначте розмір черги:\nLength = ");
                    try
                    {
                        linQueue = new queue(Convert.ToInt32(ReadLine()));
                    } catch { goto b; }
                c:  Clear();
                    Write("Додайте перший елемент типу XOOXOOX(де X-літера,O-цифра):\nelement = ");
                    X = ""+ReadLine();
                    if (X.Length!=7 || !isTrue(X))
                        goto c;
                    linQueue.enqueue(X);
                    break;
                default:
                    Clear();
                    goto a;
            }
            while (!linQueue.isEmpty())
            {
                Clear();
                WriteLine("Введіть XOOXOOX(де X-літера, O-цифра), щоб додати елемент у чергу;\n" +
                    "Або введіть XOX(де X-літера, O-цифра), щоб видалити голову якщо літери і цифри співпадуть\n");
                linQueue.Show();
                element = "" + ReadLine();
                if (element.Length == 7) 
                { 
                    if (isTrue(element))
                        linQueue.enqueue(element);
                }else if(isTrue(element))
                    linQueue.dequeue();
            }
            Clear();
            Write("Кінець програми. Натисніть клавшу, щоб вийти....");
            ReadKey();
        }
    }
}