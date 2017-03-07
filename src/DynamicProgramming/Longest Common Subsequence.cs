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
            string str1 = "12341";
            string str2 = "341213";

            string result = LongestCommonSubsequence(str1, str2);

            Console.WriteLine($"The longest common subsequence between '{str1}'" +
                              $" and '{str2}' is:\n{result}");
            Console.ReadLine();
        }

        #region Longest common subsequence

        private static string LongestCommonSubsequence(string s1, string s2)
        {
            if (s1.Length < s2.Length)
            {
                var temp = s1;
                s1 = s2;
                s2 = temp;
            }
            var data = new int[s1.Length + 1, s2.Length + 1];
            for (int i = 0; i < s1.Length; i++)
            {
                for (int j = 0; j < s2.Length; j++)
                {
                    if (s1[i] == s2[j])
                        data[i + 1, j + 1] = data[i, j] + 1;
                    else
                        data[i + 1, j + 1] = Math.Max(data[i + 1, j], data[i, j + 1]);
                }
            }

            //Get actual string 
            var list = new List<char>();
            int ii = data.GetLength(0) - 1;
            int jj = data.GetLength(1) - 1;

            while (ii != 0 || jj != 0)
            {
                if (jj > 0 && data[ii, jj - 1] == data[ii, jj])
                    jj--;
                else if (ii > 0 && data[ii - 1, jj] == data[ii, jj])
                    ii--;
                else
                {
                    list.Add(s1[ii - 1]);
                    ii--;
                    jj--;
                }
            }
            list.Reverse();
            return String.Join("", list);
        }

        #endregion
    }

}