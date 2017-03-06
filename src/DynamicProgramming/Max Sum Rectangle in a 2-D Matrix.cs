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
            int[][] matrix =
            {
                new[] {1, 2, -1, -4, -20},
                new[] {-8, -3, 4, 2, 1},
                new[] {3, 8, 10, 1, 3},
                new[] {-4, -1, 1, 7, -6}
            };
            int left = 0, top = 0, right = 0, bottom = 0;
            var maxSum = MaxSumSubMatrix(matrix, ref left, ref top, ref right, ref bottom);

            Console.WriteLine($"The maximum sum of submatrix is {maxSum}");
            Console.WriteLine("Here is submatrix: ");

            for (int i = top; i <= bottom; i++)
            {
                for (int j = left; j <= right; j++)
                    Console.Write(matrix[i][j] + "\t");
                Console.WriteLine();
            }
            Console.ReadLine();
        }

        #region Maximum sum 2-D submatrix

        private static int MaxSumSubMatrix(int[][] matrix, ref int left, ref int top, ref int right, ref int bottom)
        {
            int maxSubMatrix = 0;
            for (int i = 0; i < matrix[0].Length; i++)
            {
                var temp = new int[matrix.Length];
                for (int j = i; j < matrix[0].Length; j++)
                {
                    int begin = 0;
                    int end = 0;
                    var currentColumn = matrix.Select(x => x[j]).ToArray();

                    for (int k = 0; k < temp.Length; k++)
                        temp[k] += currentColumn[k];
                    //use Kadane's algorithm to find max subarray in a given array
                    var maxSubArray = GetMaximumSumSubArray(temp, ref begin, ref end);
                    if (maxSubArray > maxSubMatrix)
                    {
                        left = i; right = j; top = begin; bottom = end;
                        maxSubMatrix = maxSubArray;
                    }
                }
            }
            return maxSubMatrix;
        }

        #endregion

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
                    begin = i + 1;
                    tempSum = 0;
                }

            }

            return totalSum;
        }

        #endregion
    }

}