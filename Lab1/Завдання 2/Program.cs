using System;
using static System.Console;
using static System.Math;

namespace Завдання_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Write("n = "); int n = int.Parse(ReadLine());
            Write("m = "); int m = int.Parse(ReadLine());
            for(int i = n; i<=m; i++)
            {
                int sum = 0;
                for(int k =1; k < i; k++)
                {
                    if (i % k == 0)
                        sum += k;
                }

                if (sum <= m && sum >= n)
                {
                    int sum2 = 0;
                    for (int j = 1; j < sum; j++)
                    {
                        if (sum % j == 0)
                            sum2 += j;
                    }
                    if(sum2==i)
                        WriteLine("{0} i {1}",i,sum);
                }
            }

        }
    }
}
