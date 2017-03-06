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
            string str = "ababbbabbababa";
            int count = PalindromePartition(str);

            Console.WriteLine($"It will take {count} splits" +
                              $" to make each partition to be palindrome");

            Console.ReadLine();
        }

        #region Palindrome Partition

        private static int PalindromePartition(string str)
        {
            var data = new int[str.Length, str.Length];

            for (int len = 0; len < str.Length; len++)
            {
                for (int i = 0; i < str.Length - len; i++)
                {
                    var substring = str.Substring(i, len + 1);
                    if (IsPalindrome(substring))
                        continue;
                    data[i, i + len] = Int32.MaxValue;
                    for (int j = i; j < i + len; j++)
                        data[i, i + len] = Math.Min(data[i, i + len],
                                1 + data[i, j] + data[j + 1, i + len]);
                }
            }

            return data[0, data.GetLength(1) - 1];
        }

        private static bool IsPalindrome(string input)
        {
            for (int i = 0; i < input.Length / 2; i++)
                if (input[i] != input[input.Length - 1 - i])
                    return false;
            return true;
        }

        #endregion
    }

}