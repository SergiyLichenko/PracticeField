using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace GitHub
{
    class Program
    {
        public static void Main()
        {
            int[] values = { 60, 100, 120 };
            int[] weights = { 10, 20, 30 };
            int maxValue = 50;

            int max = Knapsack(values, weights, maxValue);

            Console.WriteLine($"The max value you can get is {max}");
            Console.ReadLine();
        }

        #region 0/1 Knapsack problem

        private static int Knapsack(int[] values, int[] weights, int target)
        {
            int[,] data = new int[values.Length + 1, target + 1];

            for (int i = 1; i <= values.Length; i++)
            {
                for (int j = 1; j <= target; j++)
                {
                    if (j < weights[i - 1])
                        data[i, j] = data[i - 1, j];
                    else
                        data[i, j] = Math.Max(values[i - 1] + data[i - 1, j - weights[i - 1]], data[i - 1, j]);
                }
            }

            return data[data.GetLength(0) - 1, data.GetLength(1) - 1];
        }

        #endregion
    }

}