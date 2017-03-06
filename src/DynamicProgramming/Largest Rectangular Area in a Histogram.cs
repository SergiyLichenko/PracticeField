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
            var histogram = new int[] {6, 2, 5, 4, 5, 1, 6};

            int maxArea = MaxHistogramArea(histogram);
            Console.WriteLine($"Max area of histogram is {maxArea}");

            Console.ReadLine();
        }

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