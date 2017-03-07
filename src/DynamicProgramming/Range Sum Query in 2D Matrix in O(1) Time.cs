/* Problem Statement
https://leetcode.com/problems/range-sum-query-2d-immutable/?tab=Description */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GitHub
{
    class Program
    {
        public static void Main()
        {
            int[,] matrix = new int[,]
            {
              {3, 0, 1, 4, 2 },
              {5, 6, 3, 2, 1 },
              {1, 2, 0, 1, 5 },
              {4, 1, 0, 1, 7 },
              {1, 0, 3, 0, 5 }
            };

            int[,] sumMatrix = GetSumMatrix(matrix);

            int sum1 = QuerySum(sumMatrix, 2, 1, 4, 3);

            Console.WriteLine($"Sum of submatrix: {sum1}");
            Console.ReadLine();
        }       

        #region Range Sum Query in 2D Matrix in O(1) Time
            
        private static int[,] GetSumMatrix(int[,] input)
        {
            int[,] sumMatrix = new int[input.GetLength(0) + 1, input.GetLength(1) + 1];

            for (int i = 1; i < sumMatrix.GetLength(0); i++)
            {
                for (int j = 1; j < sumMatrix.GetLength(1); j++)
                {
                    sumMatrix[i, j] = sumMatrix[i - 1, j] + sumMatrix[i, j - 1] + input[i - 1, j - 1] -
                                      sumMatrix[i - 1, j - 1];
                }
            }

            return sumMatrix;
        }

        private static int QuerySum(int[,] sumMatrix, int row1, int col1, int row2, int col2)
            => sumMatrix[row2 + 1, col2 + 1] - sumMatrix[row2 + 1, col1] -
                    sumMatrix[row1, col2 + 1] + sumMatrix[row1, col1];
        
        #endregion
    }
}
