/* Problem Statement
http://www.geeksforgeeks.org/dynamic-programming-set-21-box-stacking-problem/ */

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
            var boxes = new int[,]
            {
                {4,6,7},
                {1,2,3},
                {4,5,6},
                {10,12,32}
            };
            int maxH = BoxStackingHeight(boxes);

            Console.WriteLine($"Maximum height " +
                              $"you can get is {maxH}");
            Console.ReadLine();
        }

        #region Box Stacking Problem

        private static int BoxStackingHeight(int[,] boxes)
        {
            var rotations = GetBoxRotations(boxes);
            SortRotations(rotations);

            var heights = new int[rotations.GetLength(0)];
            for (int i = 0; i < heights.Length; i++)
                heights[i] = rotations[i, 0];

            for (int i = 1; i < heights.Length; i++)
                for (int j = 0; j < i; j++)
                    if (rotations[i, 1] < rotations[j, 1] &&
                        rotations[i, 2] < rotations[j, 2] &&
                        heights[j] + rotations[i, 0] > heights[i])
                        heights[i] = heights[j] + rotations[i, 0];

            return heights.Last();
        }

        private static void SortRotations(int[,] rotations)
        {
            for (int i = 0; i < rotations.GetLength(0) - 1; i++)
            {
                for (int j = i + 1; j < rotations.GetLength(0); j++)
                {
                    if (rotations[i, 1] * rotations[i, 2] < rotations[j, 1] * rotations[j, 2])
                    {
                        var temp = new int[] { rotations[j, 0], rotations[j, 1], rotations[j, 2] };
                        rotations[j, 0] = rotations[i, 0];
                        rotations[j, 1] = rotations[i, 1];
                        rotations[j, 2] = rotations[i, 2];

                        rotations[i, 0] = temp[0];
                        rotations[i, 1] = temp[1];
                        rotations[i, 2] = temp[2];
                    }
                }
            }
        }

        private static int[,] GetBoxRotations(int[,] boxes)
        {
            var result = new int[boxes.GetLength(0) * 3, boxes.GetLength(1)];
            for (int i = 0; i < boxes.GetLength(0); i++)
            {
                result[3 * i, 0] = boxes[i, 0];
                result[3 * i, 1] = boxes[i, 1];
                result[3 * i, 2] = boxes[i, 2];

                result[3 * i + 1, 0] = boxes[i, 1];
                result[3 * i + 1, 1] = boxes[i, 0];
                result[3 * i + 1, 2] = boxes[i, 2];

                result[3 * i + 2, 0] = boxes[i, 2];
                result[3 * i + 2, 1] = boxes[i, 0];
                result[3 * i + 2, 2] = boxes[i, 1];
            }
            return result;
        }

        #endregion

    }

}
