/* Problem Statement
https://leetcode.com/problems/jump-game/?tab=Description */

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
            int[] jumps = { 2,3,1,1,4};

            int minNumber = MinimumNumberOfJumps(jumps);

            if (minNumber == Int32.MaxValue)
                Console.WriteLine("You cannot reach the end");
            else
                Console.WriteLine($"The minimum number of " +
                                  $"jumps to reach the end is {minNumber}");
            Console.ReadLine();
        }

        #region Minimum Number of Jumps

        private static int MinimumNumberOfJumps(int[] input)
        {
            var data = new int[input.Length];
            for (int i = 1; i < data.Length; i++)
                data[i] = Int32.MaxValue;

            for (int i = 1; i < data.Length; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (input[j] >= i - j && data[j] + 1 < data[i])
                        data[i] = data[j] + 1;
                }
            }

            return data.Last();
        }

        #endregion
    }

}
