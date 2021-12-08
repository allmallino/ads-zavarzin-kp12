using System;
using static System.Console;

namespace Завдання_1
{
    class Program
    {
        static int[] sort(int[] mass)
        {
            int start = 0, end = mass.Length-1, s;
            Boolean sorted =false;
            while (!sorted)
            {
                sorted = true;
                for (int i = start; i < end; i++)
                    if (mass[i] > mass[i + 1])
                    {
                        s = mass[i + 1];
                        mass[i + 1] = mass[i];
                        mass[i]=s;
                        sorted = false;
                    }
                if (sorted)
                    break;
                end--;
                sorted = true;
                for (int i = end; i > start; i--)
                    if (mass[i-1] > mass[i])
                    {
                        s = mass[i - 1];
                        mass[i - 1] = mass[i];
                        mass[i] = s;
                        sorted = false;
                    }
                start++;
            }
            return mass;
        }
        static  Random rnd = new Random();
        static int random(int[,] mass,int x)
        {
            Boolean b = false;
            int el=0;
            while (!b)
            {
                el = rnd.Next(-99, 100);
                b = true;
                for (int i = 0; i <= x; i++)
                    for (int j = 0; j < mass.GetLength(1); j++)
                        if (mass[i, j] == el)
                            b = false;
            }
            return el;
        }
        static void Main(string[] args)
        {
            OutputEncoding = System.Text.Encoding.UTF8;
            WriteLine("Стврюємо матрицю NxM");
            Write("N= ");int N = int.Parse(ReadLine());
            Write("M= ");int M = int.Parse(ReadLine());
            int k = Math.Min(N, M),h=0;
            int[,] mass = new int[N, M];//Вся матриця
            int[] mass1 = new int[k];//Головна діагональ
            int[] mass2 = new int[k];//Побічна діагональ
            WriteLine("Заповнюємо матрицю.");
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    mass[i,j] = random(mass,i);
                    if (i == j && i != M - 1 - j)
                    {
                        BackgroundColor = ConsoleColor.White;
                        ForegroundColor = ConsoleColor.Black;
                    }
                    else if (i != j && i == M - 1 - j)
                    {
                        BackgroundColor = ConsoleColor.White;
                        ForegroundColor = ConsoleColor.Black;
                    }
                    Write($"{mass[i, j],3}");
                    BackgroundColor = ConsoleColor.Black;
                    ForegroundColor = ConsoleColor.White;
                    Write(" ");
                    }
                WriteLine();
            }
            for(int i =0; i < k; i++)
            {
                if (i != M - 1 - i)
                {
                    mass1[i] = mass[i, i];
                    mass2[i] = mass[i, M - 1 - i];
                }
                else
                {
                    mass1[i] = int.MaxValue;
                    mass2[i] = int.MinValue;
                }
            }
            WriteLine();
            mass1 = sort(mass1);
            mass2 = sort(mass2);
            k--; 
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    if (i == j && i != M - 1 - j)
                    {
                        mass[i, j] = mass1[h];
                        BackgroundColor = ConsoleColor.White;
                        ForegroundColor = ConsoleColor.Black;
                        h++;
                    }
                    else if (i != j && i == M - 1 - j)
                    {
                        mass[i, j] = mass2[k];
                        BackgroundColor = ConsoleColor.White;
                        ForegroundColor = ConsoleColor.Black;
                        k--;
                    }
                    Write($"{mass[i, j],3}");
                    BackgroundColor = ConsoleColor.Black;
                    ForegroundColor = ConsoleColor.White;
                    Write(" ");
                }
                WriteLine();
            }
        }
    }
}
