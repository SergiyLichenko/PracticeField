using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Github
{
    class Program
    {
        public static void Main()
        {
            int nodesCount = 3;

            int count = GetBinaryTreeCount(nodesCount);

            Console.WriteLine($"Count of binary trees" +
                              $" with {nodesCount} nodes is: {count}");
            Console.ReadLine();
        }

        #region Total Number of Possible Binary Trees for a Given N

        private static int GetBinaryTreeCount(int nodesCount)
        {
            int[] mass = new int[nodesCount + 1];
            mass[0] = 1;
            mass[1] = 1;

            for (int i = 2; i < mass.Length; i++)
            {
                int tempSum = 0;
                for (int j = 0; j < i; j++)
                {
                    tempSum += mass[j] * mass[i - j - 1];
                }
                mass[i] = tempSum;
            }

            return mass.Last();
        }

        #endregion
    }
}