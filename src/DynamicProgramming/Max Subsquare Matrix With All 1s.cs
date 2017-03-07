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
            var matrix = new int[,]
            {
                {0,1,1,0,1},
                {1,1,0,1,0},
                {0,1,1,1,0},
                {1,1,1,1,0},
                {0,0,0,0,0}
            };
            int size = MaxSubSquareMatrix(matrix);

            Console.WriteLine($"The maximum subsquare " +
                              $"matrix of all 1's is {size}x{size}");

            Console.ReadLine();
        }

        #region Maximum Sub Square Matrix of all 1's

        private static int MaxSubSquareMatrix(int[,] input)
        {
            var data = new int[input.GetLength(0) + 1, input.GetLength(1) + 1];

            for (int i = 0; i < input.GetLength(0); i++)
            {
                for (int j = 0; j < input.GetLength(1); j++)
                {
                    if (input[i, j] == 0)
                        continue;
                    data[i + 1, j + 1] = Math.Min(Math.Min(data[i, j + 1],
                        data[i + 1, j]), data[i, j]) + 1;
                }
            }
            return data[data.GetLength(0) - 1, data.GetLength(1) - 1];
        }

        #endregion
    }

}