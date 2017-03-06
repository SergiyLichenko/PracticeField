/* Problem Statement
https://leetcode.com/problems/burst-balloons/?tab=Description */

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
            int[] ballons = new int[] { 3, 1, 5, 8 };
            string order = string.Empty;

            var value = BurstBallon(ballons, ref order);

            Console.WriteLine($"Maximum value you can get is {value}, the order is: {order}");
            Console.ReadLine();
        }

        #region Bursting ballon to maximize value

        private static int BurstBallon(int[] ballons, ref string order)
        {
            var tempBallons = new int[ballons.Length + 2];
            for (int i = 0; i < ballons.Length; i++)
                tempBallons[i + 1] = ballons[i];
            tempBallons[0] = 1;
            tempBallons[tempBallons.Length - 1] = 1;

            var data = new List<int>[ballons.Length, ballons.Length];
            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    data[i, j] = new List<int>();
                }
            }

            for (int i = 0; i < ballons.Length; i++)
            {
                for (int j = 0; j < ballons.Length - i; j++)
                {
                    int max = -1;
                    int lastBallon = -1;
                    for (int k = j; k <= i + j; k++)
                    {
                        int left = k == j ? 0 : data[j, k - 1][0];
                        int right = k == j + i ? 0 : data[k + 1, j + i][0];

                        int middle = 0;
                        if (k == j)
                            middle = tempBallons[j] * tempBallons[k + 1] * tempBallons[j + i + 2];
                        else if (k == j + i)
                            middle = tempBallons[j] * tempBallons[k + 1] * tempBallons[j + i + 2];
                        else
                            middle = tempBallons[j] * tempBallons[k + 1] * tempBallons[j + i + 2];

                        int sum = left + right + middle;
                        if (max < sum)
                        {
                            max = sum;
                            lastBallon = k;
                        }
                    }
                    data[j, j + i].Add(max);
                    data[j, j + i].Add(lastBallon);
                }
            }

            GetOrder(data, ballons, ref order);

            return data[0, data.GetLength(1) - 1][0];
        }

        private static void GetOrder(List<int>[,] data, int[] ballons, ref string order)
        {
            int first = 0;
            int last = data.GetLength(1) - 1;

            var stack = new Stack<int>();
            stack.Push(data[first, last][1]);

            while (stack.Count != ballons.Length)
            {
                if (first == stack.Peek())
                    first++;
                if (last == stack.Peek())
                    last--;
                stack.Push(data[first, last][1]);
            }

            var items = stack.Select(x => ballons[x]).ToList();
            order = String.Join("->", items);
        }

        #endregion
    }

}
