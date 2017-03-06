using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace GitHub
{
    class Program
    {
        static void Main(String[] args)
        {
            var numbers = new int[] { 9,6,5,1 };
            var target = 11;
            var result = CoinChanging(numbers, target);

            Console.WriteLine($"The minimum number of coins is {result.Count()}, coins:");
            Console.WriteLine(String.Join(",", result));

            Console.ReadLine();
        }

        #region Coin Changing Problem

        private static IEnumerable<int> CoinChanging(int[] coins, int target)
        {
            var data = new int[coins.Length + 1, target + 1];
            for (int i = 0; i < data.GetLength(1); i++)
                data[0, i] = Int32.MaxValue;

            //filling the data
            for (int i = 1; i < data.GetLength(0); i++)
                for (int j = 1; j < data.GetLength(1); j++)
                    data[i, j] = j >= coins[i - 1] ?
                        Math.Min(data[i, j - coins[i - 1]] + 1, data[i - 1, j]) : data[i - 1, j];

            //getting result
            var result = new List<int>();
            int ii = data.GetLength(0) - 1;
            int jj = data.GetLength(1) - 1;

            while (ii != 0 && jj != 0)
            {
                if (data[ii, jj] == data[ii - 1, jj])
                {
                    ii--;
                }
                else
                {
                    result.Add(coins[ii - 1]);
                    jj -= coins[ii - 1];
                    ii--;
                }
            }

            return result;
        }

        #endregion
    }

}