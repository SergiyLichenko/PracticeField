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
                {1,2,3},
                {4,8,2},
                {1,5,3}
            };
            var minCost = MinCostPath(matrix);

            Console.WriteLine($"The minimum cost to reach bottom right " +
                              $"from top left is {minCost}");

            Console.ReadLine();
        }

        #region

        private static int MinCostPath(int[,] input)
        {
            var data = new int[input.GetLength(0), input.GetLength(1)];
            data[0, 0] = input[0, 0];
            for (int i = 1; i < input.GetLength(1); i++)
                data[0, i] = data[0, i - 1] + input[0, i];
            for (int i = 1; i < input.GetLength(0); i++)
                data[i, 0] = data[i - 1, 0] + input[i, 0];


            for (int i = 1; i < input.GetLength(0); i++)
                for (int j = 1; j < input.GetLength(1); j++)
                    data[i, j] = Math.Min(Math.Min(data[i - 1, j], data[i, j - 1]), data[i-1,j-1]) + input[i, j];
            return data[data.GetLength(0) - 1, data.GetLength(1) - 1];
        }

        #endregion
    }

}