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
            int[] numbers = new int[] { 3, 2, 5, 10, 7 };

            int sum = GetSumNotAdjacent(numbers);

            Console.WriteLine($"The maximum sum you can get is {sum}");
            Console.ReadLine();
        }

        #region Sum not adjacent elements in array

        private static int GetSumNotAdjacent(int[] input)
        {
            int inclusive = 0;
            int exclusive = 0;

            foreach (int number in input)
            {
                int temp = inclusive;
                inclusive = Math.Max(inclusive, exclusive + number);
                exclusive = temp;
            }

            return inclusive;
        }

        #endregion
    }

}