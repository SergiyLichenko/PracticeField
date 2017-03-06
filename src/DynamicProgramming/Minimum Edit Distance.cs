using System;
using System.Collections.Generic;
using System.Linq;

namespace GitHub
{
    class Program
    {
        static void Main(String[] args)
        {
            var s1 = "sunday";
            var s2 = "saturday";
            var operations = MinimumEditDistance(s1, s2);

            Console.WriteLine($"Minimum edit distance is {operations.Count}\nOperations: ");
            foreach (var item in operations)
            {
                Console.WriteLine(item);
            }

            Console.ReadLine();
        }

        #region Minimum Edit Distance

        private static List<string> MinimumEditDistance(string s1, string s2)
        {
            var operations = new List<string>();

            var data = new int[s2.Length + 1, s1.Length + 1];

            for (int i = 0; i < s2.Length; i++)
                data[i + 1, 0] = i + 1;
            for (int i = 0; i < s1.Length; i++)
                data[0, i + 1] = i + 1;
            //calculate data using dynamic programming
            for (int i = 1; i < data.GetLength(0); i++)
            {
                for (int j = 1; j < data.GetLength(1); j++)
                {
                    if (s1[j - 1] == s2[i - 1])
                        data[i, j] = data[i - 1, j - 1];
                    else
                        data[i, j] = Math.Min(Math.Min(data[i - 1, j], data[i, j - 1]),
                            data[i - 1, j - 1]) + 1;
                }
            }

            //find operations from matrix

            int ii = data.GetLength(0) - 1;
            int jj = data.GetLength(1) - 1;

            while (ii != 0 && jj != 0)
            {
                if (s2[ii - 1] != s1[jj - 1])
                {
                    if (data[ii, jj - 1] + 1 == data[ii, jj])
                    {
                        operations.Add("Remove " + s1[jj - 1]);
                        ii++;
                    }
                    else if (data[ii - 1, jj] + 1 == data[ii, jj])
                    {
                        operations.Add("Add " + s2[ii - 1]);
                        jj++;
                    }
                    else
                        operations.Add(s1[jj - 1] + "->" + s2[ii - 1]);
                }

                ii--;
                jj--;
            }

            return operations;
        }

        #endregion
    }

}