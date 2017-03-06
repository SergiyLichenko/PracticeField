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
            int[] array = { -2, -3, 4, -1, -2, 1, 5, -3 };
            int begin = 0;
            int end = 0;
            var maxSum = GetMaximumSumSubArray(array, ref begin, ref end);
            Console.WriteLine($"The maximum sum of subarray is {maxSum}");
            Console.WriteLine($"Here is the array");
            Console.WriteLine(String.Join(" ", array.Skip(begin+1).Take(end - begin)));
            Console.ReadLine();
        }

        #region Kadane's algorithm, maximum sum subarray

        private static int GetMaximumSumSubArray(int[] input, ref int begin, ref int end)
        {
            int totalSum = 0;
            int tempSum = 0;

            for (int i = 0; i < input.Length; i++)
            {
                tempSum += input[i];
                if (tempSum > totalSum)
                {
                    totalSum = tempSum;
                    end = i;
                }
                if (tempSum < 0)
                {
                    begin = i;
                    tempSum = 0;
                }

            }

            return totalSum;
        }

        #endregion
    }

}