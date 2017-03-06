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
            var matrixDimensions = new[] {40, 20, 30, 10, 30};

            string order = String.Empty;
            int operationsCount = MatrixChainMultiplication(matrixDimensions);

            Console.WriteLine($"The minimum amount of operations is {operationsCount}");
            Console.ReadLine();
        }

        #region Matrix chain multiplication

        private static int MatrixChainMultiplication(int[] matrixDimensions)
        {
            var data = new int[matrixDimensions.Length - 1, matrixDimensions.Length - 1];

            for (int length = 2; length < matrixDimensions.Length; length++)
            {
                for (int j = 0; j < matrixDimensions.Length - length; j++)
                {
                    int min = Int32.MaxValue;
                    for (int k = j + 1; k < j + length; k++)
                    {
                        int temp = data[j, k - 1] + data[k, j + length - 1] +
                                   matrixDimensions[j] * matrixDimensions[k] * matrixDimensions[j + length];
                        if (min > temp)
                            min = temp;
                    }
                    if (min != Int32.MaxValue)
                        data[j, length + j - 1] = min;
                }
            }


            return data[0, data.GetLength(1) - 1];
        }

        #endregion
    }

}