using System;
using static System.Console;
using System.IO;
using System.Collections.Generic;

namespace Lab_05
{
    class Program
    {
        static void Main(string[] args)
        {
            OutputEncoding = System.Text.Encoding.Unicode;
            int N =0;
            int[] OldList;
            string ListOfNums;
        a:
            WriteLine("Виберіть спосіб введення чисел в масив");
            WriteLine("1) Згенерувати числа\n2) Ввести з клавіатури числа\n3) Заданий контрольний приклад");
            switch (ReadKey().Key)
            {
                case ConsoleKey.D1:
                    Random rnd = new Random();
                    Clear();
                    WriteLine("Заданi N натуральних чисел");
                    Write("N= ");
                    N = Convert.ToInt32(ReadLine());
                    Clear();
                    WriteLine("Заданi " + N + " натуральних чисел");
                    OldList = new int[N];
                    ListOfNums = "";
                    for (int i = 0; i < N; i++)
                    {
                        OldList[i] = rnd.Next(1, 500);
                        Write(OldList[i] + " ");
                    }
                    WriteLine();
                    break;
                case ConsoleKey.D2:
                    Clear();
                    WriteLine("Заданi N натуральних чисел");
                    Write("N= ");
                    N = Convert.ToInt32(ReadLine());
                    Clear();
                    WriteLine("Заданi " + N + " натуральних чисел");
                    OldList = new int[N];
                    ListOfNums = "";
                    for (int i = 0; i < N; i++)
                    {
                        Write(ListOfNums);
                        double NewNum = Convert.ToDouble(ReadLine());
                        if (NewNum > 0 && NewNum % 1 == 0)
                        {
                            ListOfNums += (int)NewNum + " ";
                            CursorTop--;
                            OldList[i] = (int)NewNum;
                        }
                        else
                        {
                            Clear();
                            WriteLine("Заданi " + N + " натуральних чисел");
                            i--;
                        }
                    }
                    WriteLine();
                    break;
                case ConsoleKey.D3:
                    Clear();
                    WriteLine("Заданi 10 натуральних чисел");
                    WriteLine("51 57 98 28 18 71 100 47 5 34");
                    OldList = new int[] { 51, 57, 98, 28, 18, 71, 100, 47, 5, 34 };
                    N = 10;
                    break;
                default:
                    Clear();
                    goto a;
            }
            int[]SortedListOne = Sorted(OldList, N/2, 0);
            int[] SortedListTwo = Sorted(OldList,N-SortedListOne.Length, SortedListOne.Length);
            WriteLine("Відсортований масив: ");
            BackgroundColor = ConsoleColor.Cyan;
            ForegroundColor = ConsoleColor.Black;
            for (int i = 0; i < SortedListOne.Length; i++)
            {
                Write(SortedListOne[i]+" ");
            }
            BackgroundColor = ConsoleColor.Yellow;
            for(int i = SortedListTwo.Length - 1; i >= 0; i--)
            {
                Write(SortedListTwo[i]+" ");
            }
            BackgroundColor= ConsoleColor.Black;
            ForegroundColor= ConsoleColor.White;
            WriteLine();
            WriteLine("Натисність клавішу, щов закінчити роботу");
            ReadKey();
        }
        static int[] Sorted(int[] BaseList, int Length, int Start)
        {
            int[] List = new int[Length]; 
            int MaxVal = int.MinValue;
            for (int i = 0; i < Length; i++)
            {
                List[i] =BaseList[i+Start];
                if (MaxVal < List[i])
                {
                    MaxVal = List[i];
                }
            }
            int[] CountList = new int[MaxVal + 1];
            for (int i = 0; i < Length; i++)
            {
                CountList[List[i]]++;
            }
            for (int i = 1; i < MaxVal + 1; i++)
            {
                CountList[i] += CountList[i - 1];
            }
            int[] SortedList = new int[Length];
            for (int i = Length - 1; i >= 0; i--)
            {
                SortedList[CountList[List[i]] - 1] = List[i];
                CountList[List[i]]--;
            }
            return SortedList;
        }
    } 
}