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
            int[,] matrix = new int[,]
            {
              {1,1,1},
              {1,1,1},
              {1,1,1}
            };

            int i = matrix.GetLength(0) - 1;
            int j = matrix.GetLength(1) - 1;

            int count = TotalNumberOfWays(matrix, i, j);

            Console.WriteLine($"Total number of ways" +
                              $" to reach\ni = {i}, j = {j} element " +
                              $"from top left is {count}");
            Console.ReadLine();
        }

        #region Total Number of Ways

        private static int TotalNumberOfWays(int[,] input, int i, int j)
        {
            int[] tempMax = new int[input.GetLength(1)];
            tempMax[0] = 1;
            for (int n = 0; n <= i; n++)
            {
                for (int k = 1; k < tempMax.Length; k++)
                {
                    tempMax[k] = tempMax[k - 1] + tempMax[k];
                }
            }
            return tempMax[j];
        }

        #endregion
    }
}