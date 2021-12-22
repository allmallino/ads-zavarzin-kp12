using System;
using static System.Console;

namespace ЛабаАСД4
{
    class SLList
    {
        public Node tail;
        public Node head;
        public class Node
        {
            public string data;
            public Node next;
            public Node(string data)
            {
                this.data = data;
            }
            public Node(string data, Node next)
            {
                this.data = data;
                this.next = next;
            }
        }
        public SLList(string data)
        {
            head = new Node(data);
            tail = head;
        }
        public void AddFirst(string data) 
        {
            Node current = head;
            Node n = new Node(data);
            if (head == null)
            {
                n.next = head;
                head = n;
                tail = head;
            }
            else
            {
                n.next = head;
                head = n;
            }
        }
        public void AddToPosition(string data, int position) 
        {
            Node current = head;
            int all = 0;
            while (current!=null)
            {
                all++;
                current = current.next;
            }
            if (all > position && position > 0)
            { 
                if(position == 1)
                {
                    AddFirst(data);
                }
                else
                {
                    current = head;
                    for (int i = 1; i < position - 1; i++)
                    {
                        current = current.next;
                    }
                    Node n = new Node(data);
                    n.next = current.next;
                    current.next = n;
                }
            }
            else
            {
                AddFirst(data);
            }
        }
        public void AddLast(string data)
        {
            Node current = tail;
            Node n = new Node(data);
            if (head == null)
            {
                head = n;
                head = tail;
            }
            else
            {
                tail.next = n;
                tail = n;
            }
        }
        public void DeleteFirst() 
        {
            Node current = head;
            if (current != null)
            {
                head = current.next;
                current = null;
            }
            else
            {
                WriteLine("Список вже пустий");
            }
        }
        public void DeleteFromPosition(int position) 
        {
            Node current = head;
            if(current != null)
            {
                for (int i = 1; i < position-1; i++)
                {
                    current = current.next;
                }
                current.next = current.next.next;
            }
            else
            {
                WriteLine("Список вже пустий");
            }
        }
        public void DeleteLast()
        {
            Node current = head;
            if (current != null)
            {
                if(tail == head)
                {
                    head = null;
                }
                else
                {
                    while (current.next != tail)
                    {
                        current = current.next;
                    }
                    tail = current;
                    current.next = null;
                }
            }
            else
            {
                WriteLine("Список вже пустий");
            }
        }
        public void Print() 
        {
            if(head == null)
            {
                WriteLine("Список пустий");
            }
            else
            {
                Node current = head;
                string text = "";
                while (current != null)
                {
                    text = text + " " + current.data;
                    current = current.next;
                }
                WriteLine(text);
            }
            
        }
        public void LBzadanie(int value)
        {
            if (value >= 1 && value <= 50)
            {
                if (head == null)
                {
                    WriteLine("Список порожній");
                }
                else if(head == tail)
                {
                    Node current = head;
                    Node n = new Node(Convert.ToString(value));
                    n.next = head;
                    head = n;
                    tail = n.next;
                }
                else
                {
                    Node current = head;
                    while (current.next != tail)
                    {
                        current = current.next;
                    }
                    Node n = new Node(Convert.ToString(value));
                    n.next = current.next;
                    current.next = n;
                }
                
            }
            else
            {
                WriteLine("Елемент не попадає в проміжок");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            SLList sl = new SLList(Convert.ToString(rnd.Next(1,50)));
            OutputEncoding = System.Text.Encoding.UTF8;
            for(int i =0; i < rnd.Next(3,15); i++)
            {
                sl.AddLast(Convert.ToString(rnd.Next(1, 50)));
            }
            while (true)
            {
                WriteLine("===========================================");
                sl.Print();
                WriteLine("===========================================");
                WriteLine("");
                WriteLine("1)Додати елемент в список по номеру між першим та останнім елементом");
                WriteLine("2)Додати перший елемент в список");
                WriteLine("3)Додати останній елемент в список");
                WriteLine("4)Видалити елемент у списку по номеру між першим та останнім елементом");
                WriteLine("5)Видалити перший елемент у списку");
                WriteLine("6)Видалити останній елемент у списку");
                WriteLine("7)Завдання в лабораторній работі");
                switch (ReadKey().Key)
                {
                    case ConsoleKey.D1:
                        Clear();
                        WriteLine("Введіть номер елемента");
                        Write("Номер: "); int nomer = int.Parse(ReadLine());
                        WriteLine("Введіть елемент");
                        Write("Елемент: "); string val = ReadLine();
                        sl.AddToPosition(val, nomer);
                        break;
                    case ConsoleKey.D2:
                        Clear();
                        WriteLine("Введіть елемент");
                        Write("Елемент: "); val = ReadLine();
                        sl.AddFirst(val);
                        break;
                    case ConsoleKey.D3:
                        Clear();
                        WriteLine("Введіть елемент");
                        Write("Елемент: "); val = ReadLine();
                        sl.AddLast(val);
                        break;
                    case ConsoleKey.D4:
                        Clear();
                        WriteLine("Введіть номер елемента");
                        Write("Номер: "); nomer = int.Parse(ReadLine());
                        sl.DeleteFromPosition(nomer);
                        break;
                    case ConsoleKey.D5:
                        Clear();
                        WriteLine("Перший елемент був видаленим успішно");
                        sl.DeleteFirst();
                        break;
                    case ConsoleKey.D6:
                        Clear();
                        WriteLine("Останній елемент був видаленим успішно");
                        sl.DeleteLast();
                        break;
                    case ConsoleKey.D7:
                        Clear();
                        WriteLine("Завдання:\n Додати новий вузол перед хвостом списку, якщо його значення лежить у діапазоні[1; 50], інакше – не додавати");
                        WriteLine("Введіть елемент");
                        Write("Елемент: "); val = ReadLine();
                        sl.LBzadanie(int.Parse(val));
                        break;
                    default:
                        Clear();
                        break;
                }
                WriteLine("Натисніть на клавішу, щоб повернутися назад");
                ReadKey();
                Clear();
            }
        }
    }
}
