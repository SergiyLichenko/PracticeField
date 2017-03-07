/* Problem Statement
http://www.geeksforgeeks.org/dynamic-programming-set-14-maximum-sum-increasing-subsequence/ */

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
            int[] numbers = new int[] {1,101,2,3,100,4,5 };
            int maxSum = MaxSumIncreasingSubsequence(numbers);

            Console.WriteLine($"The maximum sum " +
                              $"of increasing subsequence is {maxSum}");

            Console.ReadLine();
        }

        #region Max Sum Increasing Subsequence

        private static int MaxSumIncreasingSubsequence(int[] numbers)
        {
            var data = new int[numbers.Length];
            for (int i = 0; i < numbers.Length; i++)
                data[i] = numbers[i];

            for (int i = 1; i < numbers.Length; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (numbers[i] > numbers[j])
                        data[i] = Math.Max(data[i], data[j] + numbers[i]);
                }
            }

            return data.Max();
        }

        #endregion
    }

}
