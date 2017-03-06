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
            string s1 = "GeeksforGeeks";
            string s2 = "GeeksQuiz";

            var substring = LongestCommonSubstring(s1, s2);
            Console.WriteLine($"Longest common substring of '{s1}'" +
                              $" and '{s2}' is\t'{substring}'");
            Console.ReadLine();
        }

        #region Longest Common Substring

        private static string LongestCommonSubstring(string s1, string s2)
        {

            var data = new int[s2.Length + 1, s1.Length + 1];

            for (int i = 1; i < data.GetLength(0); i++)
                for (int j = 1; j < data.GetLength(1); j++)
                    data[i, j] = s1[j - 1] == s2[i - 1] ? data[i - 1, j - 1] + 1 : 0;

            return GetString(data, s2);
        }

        private static string GetString(int[,] data, string s)
        {
            int maxI = 0;
            int maxJ = 0;
            int maxLength = 0;
            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(0); j++)
                {
                    if (data[i, j] > maxLength)
                    {
                        maxLength = data[i, j];
                        maxI = i;
                        maxJ = j;
                    }
                }
            }
            var result = new StringBuilder();
            while (data[maxI, maxJ] != 0)
            {
                result.Append(s[--maxI]);
                maxJ--;
            }
            var res = result.ToString().Reverse();

            return String.Concat(res);
        }

        #endregion
    }

}