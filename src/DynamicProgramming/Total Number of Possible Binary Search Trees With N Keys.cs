/* Problem Statement
http://www.geeksforgeeks.org/g-fact-18/ */

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
            int n = 5;
            int count = GetNumberBST(n);

            Console.WriteLine($"It is possible to create {count} binary search trees which has {n} nodes");
            Console.ReadLine();
        }

        #region Count of binary search trees for a given length

        private static int GetNumberBST(int n)
        {
            int[] temp = new int[n + 1];
            temp[0] = 1;
            temp[1] = 1;

            for (int i = 2; i <= n; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    temp[i] += temp[j] * temp[i - j - 1];
                }
            }

            return temp.Last();
        }

        #endregion
    }

}
