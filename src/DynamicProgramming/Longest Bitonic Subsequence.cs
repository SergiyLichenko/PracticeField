/* Problem Statement
http://www.geeksforgeeks.org/dynamic-programming-set-15-longest-bitonic-subsequence/ */

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
            int[] numbers = new int[] { 2, -1, 4, 3, 5, -1, 3, 2 };

            int length = LongestBitonicSubsequence(numbers);

            Console.WriteLine($"Longest bitonic subsequence is {length}");
            Console.ReadLine();
        }

        #region Longest Bitonic Subsequence

        private static int LongestBitonicSubsequence(int[] input)
        {
            //Longest increasing subsequence from left to right
            var leftToRight = new int[input.Length];

            //Longest increasing subsequence from right to left
            var rightToLeft = new int[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                int tempCount = 1;
                int tempCount2 = 1;
                for (int j = 0; j < i; j++)
                {
                    if (input[j] < input[i] && tempCount <= leftToRight[j])
                        tempCount = leftToRight[j] + 1;

                    if (input[input.Length - 1 - j] < input[input.Length - 1 - i] &&
                        tempCount <= rightToLeft[input.Length - 1 - j])
                        tempCount2 = rightToLeft[input.Length - 1 - j] + 1;
                }
                leftToRight[i] = tempCount;
                rightToLeft[rightToLeft.Length - 1 - i] = tempCount2;
            }
            int max = 0;
            for (int i = 0; i < input.Length; i++)
                max = max > leftToRight[i] + rightToLeft[i] - 1 ?
                    max : leftToRight[i] + rightToLeft[i] - 1;

            return max;
        }

        #endregion
    }
}
