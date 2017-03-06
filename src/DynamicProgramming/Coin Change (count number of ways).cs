/* Problem Statement
http://www.geeksforgeeks.org/dynamic-programming-set-7-coin-change/ */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GitHub
{
    class Program
    {
        static void Main(String[] args)
        {
            int[] coins = { 2, 5, 3, 6 };
            int target = 10;
            int count = CoinChangingCount(coins, target);

            Console.WriteLine($"There are total {count} " +
                              $"ways to get the value");
            Console.ReadLine();
        }

        #region Coin Changing Problem (number of ways)  
        private static int CoinChangingCount(int[] coins, int target)
        {
            int[,] data = new int[coins.Length + 1, target + 1];

            for (int i = 0; i < coins.Length; i++)
                data[i, 0] = 1;

            for (int i = 1; i < data.GetLength(0); i++)
            {
                for (int j = 1; j < data.GetLength(1); j++)
                {
                    if (j >= coins[i - 1])
                        data[i, j] = data[i, j - coins[i - 1]] + data[i - 1, j];
                    else
                        data[i, j] = data[i - 1, j];
                }
            }

            return data[data.GetLength(0) - 1, data.GetLength(1) - 1];
        }
        #endregion
    }

}
