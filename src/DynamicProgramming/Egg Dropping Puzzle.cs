/* Problem Statement
http://www.geeksforgeeks.org/dynamic-programming-set-11-egg-dropping-puzzle/ */

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
            int eggs = 2;
            int floors = 100;

            var result = EggDropCount(floors, eggs);
            Console.WriteLine($"It will take {result} attempts to find " +
                              $"the floor from which the egg will break");
            Console.ReadLine();
        }

        #region Egg dropping problem

        private static int EggDropCount(int floors, int eggs)
        {
            int[,] data = new int[eggs + 1, floors + 1];

            for (int i = 1; i < data.GetLength(1); i++)
                data[1, i] = i;

            for (int i = 2; i < data.GetLength(0); i++)
            {
                for (int j = 1; j < data.GetLength(1); j++)
                {
                    if (j < i)
                    {
                        data[i, j] = data[i - 1, j];
                        continue;
                    }
                    int min = Int32.MaxValue;
                    for (int k = 1; k <= j; k++)
                    {
                        int temp = 1 + Math.Max(data[i - 1, k - 1], data[i, j - k]);
                        min = Math.Min(temp, min);
                    }
                    data[i, j] = min;
                }
            }

            return data[data.GetLength(0) - 1, data.GetLength(1) - 1];
        }

        #endregion
    }

}
