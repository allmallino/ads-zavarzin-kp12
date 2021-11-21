﻿using System;
using static System.Console;

namespace АСД_Лабараторная_2
{
    class Program
    {
        static int[,] Generate(int N)
        {
            Random rnd = new Random();
            int[,] Nums = new int[N, N];
            
            Write("R - Рандомні значення, С - контрольны значення");
            switch (ReadKey().Key)
            {
                case ConsoleKey.C:
                    Clear();
                    int k; 
                    WriteLine("Введіть елемент та нажміть Enter");
                    for(int i=0; i < N; i++)
                    {
                        for(int j=0; j<N; j++)
                        {
                            k = CursorLeft;
                            Nums[i,j] = int.Parse(ReadLine());
                            SetCursorPosition(k, CursorTop-1);
                            Write($"{Nums[i, j],3} ");
                        }
                        SetCursorPosition(0, CursorTop + 1);
                    }
                    break;
                case ConsoleKey.R:
                    Clear();
                    for (int i = 0; i < N; i++)
                    {
                        for (int j = 0; j < N; j++)
                        {
                            Nums[i, j] = rnd.Next(N * N);
                            Write($"{Nums[i, j],3} ");
                        }
                        WriteLine("");
                    }
                    WriteLine("");
                    break;
            }
            return Nums;
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Write("N= "); int N = int.Parse(Console.ReadLine());
            int[,] Nums = Generate(N);
            int max = 0, x = 0, y = 0, n = N, k = 0;
            while (n > 1)
            {
                for (int i = k + 1; i < n+k; i++)
                {
                    Write(Nums[N - (N - n) / 3 - 1, i] + " ");
                    if (Nums[N - (N - n) / 3 - 1, i] > max)
                    {
                        max = Nums[N - (N - n) / 3 - 1, i];
                        x = i;
                        y = N - (N - n) / 3 - 1;
                    }
                }
                n--;
                WriteLine("");
                if (n > 1)
                {
                    for (int i = n+k - 1; i > k; i--)
                    {
                        Write(Nums[i, N - (N - n) / 3 - 1] + " ");
                        if (Nums[i, N - (N - n) / 3 - 1] > max)
                        {
                            max = Nums[i, N - (N - n) / 3 - 1];
                            x = N - (N - n) / 3 - 1;
                            y = i;
                        }
                    }
                    WriteLine("");
                    n--;
                    k += 2;
                    if (n > 1)
                    {
                        for (int i = N-n - k / 2+1; i <N-k / 2; i++)
                        {
                            Write(Nums[i, N+k/2-1-i] + " ");
                            if (Nums[i, N + k / 2 - 1 - i] > max)
                            {
                                max = Nums[i, N + k / 2 - 1 - i];
                                x = N + k / 2 - 1 - i;
                                y = i;
                            }
                        }
                        WriteLine("");
                        n--;
                    }
                }
            }
            WriteLine("");
            WriteLine($"Max = Nums[{y},{x}] = {max}");
            WriteLine("");
            n = N;
            int min = N * N + 1;
            k = 0;
            for(int i = 0; i < N; i++)
            {
                Write(Nums[i, N - i - 1]+" ");
                if(Nums[i, N - i - 1] < min)
                {
                    min = Nums[i, N - i - 1];
                    x = N - i - 1;
                    y = i;
                }
            }
            WriteLine("");
            while (n > 1)
            {
                for(int i = N-k-2; i >= (N-n)/3; i--)
                {
                    Write(Nums[i , (N - n) / 3] + " ");
                    if(Nums[i , (N - n) / 3] < min)
                    {
                        min = Nums[i , (N - n) / 3];
                        x = (N - n) / 3;
                        y = i ;
                    }
                }
                WriteLine("");
                n--;
                k += 2;
                if (n > 1)
                {
                    for(int i = k / 2; i < n + k / 2 - 1; i++)
                    {
                        Write(Nums[(N + 1 - n) / 3, i] + " ");
                        if(Nums[(N + 1 - n) / 3, i] < min)
                        {
                            min = Nums[(N + 1 - n) / 3, i];
                            x = i;
                            y = (N + 1 - n) / 3;
                        }
                    }
                    WriteLine("");
                    n--;
                    if (n > 1)
                    {
                        for(int i = N - n - k + 1; i < N - k; i++)
                        {
                            Write(Nums[i, N - k / 2 - 1 - i] + " ");
                            if(Nums[i, N - k / 2 - 1 - i] < min)
                            {
                                min = Nums[i, N - k / 2 - 1 - i];
                                x = N - k / 2 - 1 - i;
                                y = i;
                            }
                        }
                        WriteLine("");
                        n--;
                    }
                }
            }
            WriteLine("");
            WriteLine($"Min = Nums[{y},{x}] = {min}");
        }
    }
}