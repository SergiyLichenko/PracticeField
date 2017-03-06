/* Problem Statement
http://www.geeksforgeeks.org/dynamic-programming-set-13-cutting-a-rod/ */

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
            //length->profit
            var rods = new int[][]
            {
                new[] {1,1},
                new[] {2,5},
                new[] {3,8},
                new[] {4,9},
                new[] {5,10},
                new[] {6,17},
                new[] {7,17},
                new[] {8,20},
            };
            var totalLength = 8;
            int result = 0;

            var chunks = CuttingRodProfit(rods, totalLength, ref result);

            Console.WriteLine($"The maximum profit you can get is {result}");
            Console.WriteLine("Here are possible solutions:");
            foreach (var chunk in chunks)
                Console.WriteLine(String.Join(" ", chunk));

            Console.ReadLine();
        }


        #region Cutting rod to maximize profit

        private static List<List<int>> CuttingRodProfit(int[][] input, int totalLength, ref int result)
        {
            var data = new int[input.Length + 1, totalLength + 1];

            for (int i = 1; i < data.GetLength(0); i++)
            {
                for (int j = 1; j < data.GetLength(1); j++)
                {
                    if (j < input[i - 1][0])
                        data[i, j] = data[i - 1, j];
                    else
                    {
                        var current = input[i - 1];
                        data[i, j] = +Math.Max(data[i - 1, j], current[1] + data[i, j - current[0]]);
                    }
                }
            }
            result = data[data.GetLength(0) - 1, data.GetLength(1) - 1];
            var res = new List<List<int>>();
            var currentList = new List<int>();

            //recursively get all possible solutions from data matrix
            GetAllRods(res, data, input,
                data.GetLength(0) - 1, data.GetLength(1) - 1, currentList);

            return res;
        }

        private static void GetAllRods(List<List<int>> res, int[,] data, int[][] input,
            int i, int j, List<int> currentList)
        {
            if (i == 0 && j == 0)
            {
                res.Add(currentList);
                return;
            }
            var currentRod = input[i - 1];

            if (currentRod[0] <= j && data[i, j] == data[i, j - currentRod[0]] + currentRod[1])
            {
                currentList.Add(currentRod[0]);
                GetAllRods(res, data, input, i, j - currentRod[0], new List<int>(currentList));
                currentList.Remove(currentRod[0]);
            }
            if (data[i, j] == data[i - 1, j])
                GetAllRods(res, data, input, i - 1, j, new List<int>(currentList));
        }

        #endregion
    }

}
