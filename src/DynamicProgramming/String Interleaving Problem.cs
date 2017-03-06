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
            string str1 = "XXY";
            string str2 = "XXZ";
            string str3 = "XXXXZY";

            var result = IsInterleave(str1, str2, str3);

            Console.WriteLine($"Is '{str3}' interleaved" +
                              $" of '{str1}' and '{str2}'?");
            Console.WriteLine(result);


            Console.ReadLine();
        }

        #region String interleaving problem

        private static bool IsInterleave(string str1, string str2, string str3)
        {
            var data = new bool[str1.Length + 1, str2.Length + 1];

            data[0, 0] = true;
            for (int i = 0; i < str1.Length; i++)
            {
                if (str1[i] == str3[i])
                    data[i + 1, 0] = data[i, 0];
                if (str2[i] == str3[i])
                    data[0, i + 1] = data[0, i];
            }

            for (int i = 1; i < data.GetLength(0); i++)
            {
                for (int j = 1; j < data.GetLength(0); j++)
                {
                    if (str1[i - 1] == str3[i + j - 1])
                        data[i, j] = data[i - 1, j];
                    if (str2[j - 1] == str3[i + j - 1])
                        data[i, j] = data[i, j] || data[i, j - 1];
                }
            }


            return data[data.GetLength(0) - 1, data.GetLength(1) - 1];
        }

        #endregion
    }

}