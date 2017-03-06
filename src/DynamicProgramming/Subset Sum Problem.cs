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
            var numbers = new int[] { 3,34,4,12,5,2 };
            var target = 9;
            var result = IsSumSubset(numbers, target);

            Console.WriteLine(result);
            Console.ReadLine();
        }

        #region Subset Sum Problem

        private static bool IsSumSubset(int[] numbers, int target)
        {
            var data = new bool[target + 1];
            data[0] = true;

            for (int i = 0; i < numbers.Length; i++)
            {
                for (int j = data.Length - 1; j > 0; j--)
                {
                    data[j] = j >= numbers[i] ? data[j] || data[j - (numbers[i])] : data[j];
                }
            }

            return data.Last();
        }
        #endregion
    }

}