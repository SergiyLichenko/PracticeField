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
                {1,1,1,1,1},
                {0,0,0,0,0}
            };

            int maxArea = MaxSizeRectanbleOfOnes(matrix);
            Console.WriteLine($"Max area of all 1's " +
                              $"in the matrix is {maxArea}");

            Console.ReadLine();
        }

        #region Max size rectangle of all 1's

        private static int MaxSizeRectanbleOfOnes(int[,] input)
        {
            int[] data = new int[input.GetLength(1)];
            int maxArea = 0;
            for (int i = 0; i < input.GetLength(0); i++)
            {
                for (int j = 0; j < data.Length; j++)
                    data[j] = input[i, j] == 0 ? 0 : data[j] + input[i, j];
                int area = MaxHistogramArea(data);
                maxArea = Math.Max(maxArea, area);
            }
            return maxArea;
        }

        #endregion

        #region Max histogram area

        private static int MaxHistogramArea(int[] heights)
        {
            if (heights.Length == 0)
                return 0;
            var stack = new Stack<int>();
            stack.Push(0);
            int maxArea = stack.Peek();
            for (int i = 1; i <= heights.Length; i++)
            {
                if (i != heights.Length && (stack.Count == 0 || heights[i] >= heights[stack.Peek()]))
                    stack.Push(i);
                else
                {
                    while (true)
                    {
                        int area = 0;

                        if (i != heights.Length && stack.Count != 0 && heights[stack.Peek()] <= heights[i])
                            break;
                        int top = stack.Pop();
                        if (stack.Count == 0)
                            area = heights[top] * i;
                        else
                            area = heights[top] * (i - stack.Peek() - 1);
                        maxArea = maxArea > area ? maxArea : area;

                        if (stack.Count == 0)
                            break;
                    }
                    stack.Push(i);
                }
            }

            return maxArea;
        }

        #endregion
    }

}