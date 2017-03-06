using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GitHub
{
    class Program
    {
        static void Main(String[] args)
        {
            string str = "BBABCBCAB";
            string result = LongestPalindromicSubsequence(str);

            Console.WriteLine($"The longest palindromic subsequence " +
                              $"of '{str}' is '{result}' with length {result.Length}");
            Console.ReadLine();
        }

        #region Longest Palindromic Subsequence

        private static string LongestPalindromicSubsequence(string input)
        {
            int[,] data = new int[input.Length + 1, input.Length + 1];

            for (int i = 0; i < input.Length; i++)
                data[i, i] = 1;

            for (int i = 1; i < input.Length; i++)
            {
                for (int j = 0; j < input.Length - i; j++)
                {
                    if (input[j] == input[j + i])
                        data[j, i + j] = data[j + 1, i + j - 1] + 2;
                    else
                        data[j, i + j] = Math.Max(data[j + 1, i + j], data[j, i + j - 1]);
                }
            }

            return GetString(input, data);
        }

        private static string GetString(string input, int[,] data)
        {
            var builder = new StringBuilder();
            int ii = 0;
            int jj = input.Length - 1;
            int currentIndex = 0;
            while (ii != jj)
            {
                if (data[ii, jj - 1] == data[ii, jj])
                {
                    jj--;
                    continue;
                }
                if (data[ii + 1, jj] == data[ii, jj])
                {
                    ii++;
                    continue;
                }
               

                if (data[ii, jj] == data[ii + 1, jj - 1] + 2)
                {
                    builder.Insert(currentIndex, input[ii]);
                    builder.Insert(currentIndex, input[jj]);
                    currentIndex++;
                    ii++;
                    jj--;
                }

            }
            builder.Insert(currentIndex, input[ii]);

            return builder.ToString();
        }
        #endregion
    }

}