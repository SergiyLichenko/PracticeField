/* Problem Statement 
http://www.geeksforgeeks.org/weighted-job-scheduling/ */

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
            var jobs = new int[][]
            {
              new []  {1,2,50},
              new []  {2,100,200},
              new []  {3,5,20},
              new []  {6,19,100},
            };
            var maxProfit = MaxJobProfit(jobs);

            Console.WriteLine($"The max profit is {maxProfit}");
            Console.ReadLine();
        }

        #region Weighted Job Scheduling

        private static int MaxJobProfit(int[][] jobs)
        {
            var data = new int[jobs.Length];
            jobs = jobs.OrderBy(x => x[1]).ToArray();

            for (int i = 0; i < data.Length; i++)
                data[i] = jobs[i][2];

            for (int i = 1; i < jobs.Length; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    //overlap
                    if ((jobs[j][0] < jobs[i][1] && jobs[j][0] > jobs[i][0]) ||
                        (jobs[j][1] < jobs[i][1] && jobs[j][1] > jobs[i][0]))
                    {
                        data[i] = Math.Max(data[i], data[j]);
                    }
                    else
                    {
                        data[i] = Math.Max(data[i], data[j] + jobs[i][2]);
                    }
                }
            }
            return data.Last();
        }

        #endregion
    }

}
