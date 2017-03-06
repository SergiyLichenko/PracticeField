/* Problem Statement
http://www.geeksforgeeks.org/dynamic-programming-set-3-longest-increasing-subsequence/ */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace GitHub
{
    class Program
    {
        static void Main(String[] args)
        {
            var numbers = new int[] {10,22,9,33,21,50,41,60,80 };

            var result = LongestIncreasingSubsequence(numbers);

            Console.WriteLine($"The length of longest increasing subsequence is {result}");
            Console.ReadLine();
        }

        #region Longest Increasing Subsequence

        private static int LongestIncreasingSubsequence(int[] numbers)
        {
            var data = new int[numbers.Length];
            for (int i = 0; i < data.Length; i++)
                data[i] = 1;
            for (int i = 1; i < numbers.Length; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (numbers[j] < numbers[i] && data[i]<=data[j])
                        data[i] = data[j] + 1;
                }
            }

            return data.Max();
        }


        #endregion
    }

}
