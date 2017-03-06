/* Problem Statement
http://www.geeksforgeeks.org/maximum-profit-by-buying-and-selling-a-share-at-most-k-times/ */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GitHub
{
    class Program
    {
        static void Main()
        {
            int[] costs = new int[] { 12,14,17,10,14,13,12,15 };
            int k = 3;
            var buyDays = new HashSet<int>();
            var sellDays = new HashSet<int>();

            int maxProfit = MaxProfitAndDays(k, costs, buyDays, sellDays);

            Console.WriteLine($"The maximum profit you can get is: {maxProfit}");
            Console.WriteLine("The strategy: \n");
            for (int i = 0; i < costs.Length; i++)
            {
                if (buyDays.Contains(i))
                    Console.WriteLine($"Buy on day {i}");
                if (sellDays.Contains(i))
                    Console.WriteLine($"Sell on day {i}");
            }
            Console.ReadLine();
        }

        #region Max profit with K-transactions

        private static int MaxProfitAndDays(int k, int[] costs, HashSet<int> buyDays, HashSet<int> sellDays)
        {
            var data = new int[k + 1, costs.Length];

            //get max profit
            for (int i = 1; i < data.GetLength(0); i++)
            {
                int maxDif = data[i - 1, 0] - costs[0];
                for (int j = 1; j < data.GetLength(1); j++)
                {
                    data[i, j] = Math.Max(data[i, j - 1], costs[j] + maxDif);
                    maxDif = Math.Max(maxDif, data[i - 1, j] - costs[j]);
                }
            }

            //get sell/buy days
            int prevDay = data.GetLength(1) - 1;
            for (int i = data.GetLength(0) - 1; i >= 0; i--)
            {
                for (int j = prevDay; j >= 0; j--)
                {
                    if (i == 0 && j == 0)
                        break;
                    if (data[i, j] == data[i, j - 1])
                        continue;
                    sellDays.Add(j);

                    for (int n = j - 1; n >= 0; n--)
                    {
                        if (data[i - 1, n] - costs[n] == data[i, j] - costs[j])
                        {
                            buyDays.Add(n);
                            prevDay = n + 1;
                            break;
                        }
                    }
                    break;
                }
            }
            return data[data.GetLength(0) - 1, data.GetLength(1) - 1];
        }

        #endregion
    }

}
