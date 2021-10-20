using System;
using static System.Console;
using static System.Math;

namespace Завдання_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Write("x = "); int x = int.Parse(ReadLine());
            Write("y = "); int y = int.Parse(ReadLine());
            Write("z = "); int z = int.Parse(ReadLine());
            if (Sin(2 * x * y * z) == 0)
            {
                Write("Помилка при знаходженнi а");
                return;
            }
            double a = (x * y * z) / Sin(2 * x * y * z);
            if (Pow(a, 4) + Pow(Log(a + x * y), 1.0 / 3) == 0||a+x*y<=0)
            {
                Write("Помилка при знаходженнi b");
                return;
            }
            double b = 1 / (Pow(a, 4) + Pow(Log(a + x * y), 1.0 / 3));
            WriteLine("a = " + a);
            WriteLine("b = " + b);
            ReadLine();
        }
    }
}
