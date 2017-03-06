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
            string str = "baaabab";
            string pattern = "*****ba*****ab";

            bool isMatch = WildCardMatching(str, pattern);

            Console.WriteLine($"Does wild card pattern {pattern}" +
                              $" match string {str} ?\n{isMatch}");
            Console.ReadLine();
        }

        #region

        private static bool WildCardMatching(string str, string pattern)
        {
            var data = new bool[str.Length, pattern.Length];
            data[0, 0] = true;
            for (int i = 1; i < data.GetLength(1); i++)
                data[0, i] = pattern[i - 1] == '*' && data[0, i - 1];

            for (int i = 1; i < data.GetLength(0); i++)
            {
                for (int j = 1; j < data.GetLength(1); j++)
                {
                    if (str[i - 1] == pattern[j - 1] || pattern[j - 1] == '?')
                        data[i, j] = true;
                    else if (pattern[j - 1] == '*')
                        data[i, j] = data[i - 1, j] || data[i, j - 1];
                }
            }

            return data[data.GetLength(0) - 1, data.GetLength(1) - 1];
        }

        #endregion

    }

}