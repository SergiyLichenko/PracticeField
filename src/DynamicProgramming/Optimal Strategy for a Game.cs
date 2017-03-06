/* Problem Statement
http://www.geeksforgeeks.org/dynamic-programming-set-31-optimal-strategy-for-a-game/ */

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
            int[] numbers = new int[] { 3, 9, 1, 2 };

            int sum = GetPlayer1MaxValue(numbers);

            Console.WriteLine($"The maximum value first player can get is {sum}");
            Console.ReadLine();
        }

        #region Optimal strategy game pick

        private static int GetPlayer1MaxValue(int[] input)
        {
            var data = new List<int>[input.Length, input.Length];

            for (int i = 0; i < input.Length; i++)
                for (int j = 0; j < input.Length; j++)
                    data[i, j] = new List<int>() { 0, 0 };


            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input.Length - i; j++)
                {
                    int first = input[j];
                    if (j + 1 < data.GetLength(0))
                        first += data[j + 1, j + i][1];
                    int second = input[j + i];
                    if (j + i - 1 >= 0)
                        second += data[j, j + i - 1][1];

                    if (first > second)
                    {
                        data[j, i + j][0] = first;
                        data[j, i + j][1] = data[j + 1, j + i][0];
                    }
                    else
                    {
                        data[j, i + j][0] = second;
                        data[j, i + j][1] = j + i - 1 >= 0 ? data[j, j + i - 1][0] : 0;
                    }
                }
            }

            return data[0, data.GetLength(1) - 1][0];
        }

        #endregion
    }

}
