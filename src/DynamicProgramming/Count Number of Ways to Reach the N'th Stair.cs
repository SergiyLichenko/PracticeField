/* Problem Statement
http://www.geeksforgeeks.org/count-ways-reach-nth-stair/ */

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
            int n = 9;
            int count = GetCountStair(n);

            Console.WriteLine($"There is {count} of ways to reach {n}th stair");
            Console.ReadLine();
        }

        #region Number of ways n stair

        private static int GetCountStair(int height)
        {
            int first = 1;
            int second = 1;

            for (int i = 1; i < height; i++)
            {
                second += first;
                first = second - first;
            }
            return second;
        }

        #endregion
    }

}
