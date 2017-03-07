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
            int[,] matrix = new int[,]
            {
              {'X', 'O', 'X', 'X', 'X', 'X'},
              {'X', 'O', 'X', 'X', 'O', 'X'},
              {'X', 'X', 'X', 'O', 'O', 'X'},
              {'X', 'X', 'X', 'X', 'X', 'X'},
              {'X', 'X', 'X', 'O', 'X', 'O'}
            };

            int subSize = MaxSizeWithX(matrix);

            Console.WriteLine($"Max size of submatrix surrounded" +
                              $" by 'X' is {subSize}");
            Console.ReadLine();
        }

        #region Max Subsquare Matrix Surrounded by 'X'

        private static int MaxSizeWithX(int[,] matrix)
        {
            int result = 0;

            var data = new Point[matrix.GetLength(0) + 1, matrix.GetLength(1) + 1];

            for (int i = 1; i < matrix.GetLength(0) + 1; i++)
            {
                for (int j = 1; j < matrix.GetLength(1) + 1; j++)
                {
                    if (matrix[i - 1, j - 1] == 'O')
                        continue;
                    data[i, j] = new Point(data[i - 1, j].X + 1, data[i, j - 1].Y + 1);
                }
            }

            for (int i = data.GetLength(0) - 1; i >= 0; i--)
            {
                for (int j = data.GetLength(1) - 1; j >= 0; j--)
                {
                    int min = Math.Min(data[i, j].X, data[i, j].Y);
                    if (min <= result)
                        continue;

                    while (min > 0)
                    {
                        if (min <= result)
                            break;
                        if (data[i - min + 1, j].Y >= min && data[i, j - min + 1].X >= min)
                            result = min;
                        min--;
                    }
                }
            }

            return result;
        }

        #endregion
    }
}