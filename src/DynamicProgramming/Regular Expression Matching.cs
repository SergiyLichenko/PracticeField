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
            string str = "aab";
            string pattern = "c*a*b";

            bool isMatch = IsMatch(str, pattern);

            Console.WriteLine($"Does string '{str}' match pattern '{pattern}'?\n{isMatch}");
            Console.ReadLine();
        }

        #region Regular expression matching

        private static bool IsMatch(string str, string pattern)
        {
            var matrix = new bool[str.Length + 1, pattern.Length + 1];

            //Initializing for empty str
            matrix[0, 0] = true;
            for (int i = 0; i < pattern.Length; i++)
            {
                if (pattern[i] == '*')
                {
                    if (i > 0 && matrix[0, i - 1])
                        matrix[0, i + 1] = true;
                    if (pattern[i - 1] == '.')
                    {
                        matrix[0, i + 1] = matrix[0, i - 1];
                    }
                }
            }

            //check pattern matching for each substring of str and pattern
            for (int i = 0; i < str.Length; i++)
            {
                for (int j = 0; j < pattern.Length; j++)
                {
                    if (str[i] == pattern[j] || pattern[j] == '.')
                    {
                        matrix[i + 1, j + 1] = matrix[i, j];
                        continue;
                    }
                    if (pattern[j] == '*')
                    {
                        if (matrix[i + 1, j - 1])
                        {
                            matrix[i + 1, j + 1] = true;
                            continue;
                        }
                        if (pattern[j - 1] == '.' || str[i] == pattern[j - 1])
                        {
                            matrix[i + 1, j + 1] = matrix[i, j + 1];
                        }
                    }
                }
            }

            return matrix[matrix.GetLength(0) - 1, matrix.GetLength(1) - 1];
        }

        #endregion
    }

}