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
            var dictionary = new HashSet<string>()
            {
                "i","like","sam",
                "sung","samsung","mobile",
                "ice","cream","icecream",
                "man","go","mango"
            };
            var str = "ilikesamsung";
            var split = WordSplit(dictionary, str);

            if (split.Count == 0)
                Console.WriteLine("No");
            else
            {
                Console.WriteLine("Yes\nPossible split: ");
                Console.WriteLine(String.Join(" ", split));
            }
            Console.ReadLine();
        }

        #region Word Split Problem

        private static List<string> WordSplit(HashSet<string> dictionary, string str)
        {
            var data = new bool[str.Length, str.Length];
            var splitIndeces = new int[str.Length, str.Length];

            for (int i = 0; i < splitIndeces.GetLength(0); i++)
                for (int j = 0; j < splitIndeces.GetLength(1); j++)
                    splitIndeces[i, j] = -1;

            //calculate split indeces
            for (int len = 0; len < str.Length; len++)
            {
                for (int i = 0; i < str.Length - len; i++)
                {
                    var substring = str.Substring(i, len + 1);
                    if (dictionary.Contains(substring))
                        data[i, i + len] = true;
                    else
                    {
                        for (int k = i; k <= i + len; k++)
                        {
                            if (data[i, k] && data[k + 1, i + len])
                            {
                                data[i, i + len] = true;
                                splitIndeces[i, i + len] = k;
                                break;
                            }
                        }
                    }
                }
            }
            //if str cannot be splitted into words
            if (!data[0, data.GetLength(1) - 1])
                return new List<string>();
            return GetSplittedWords(data, splitIndeces, str);

        }

        private static List<string> GetSplittedWords(bool[,] data, int[,] splitIndeces, string str)
        {
            var indeces = new List<int>();
            GetSplitIndeces(splitIndeces, indeces,
                0, data.GetLength(1) - 1);

            var result = new List<string>();
            int lastIndex = 0;
            for (int i = 0; i <= indeces.Count; i++)
            {
                if (i == indeces.Count)
                    result.Add(str.Substring(lastIndex));
                else
                {
                    result.Add(str.Substring(lastIndex, (indeces[i] - lastIndex) + 1));
                    lastIndex = indeces[i] + 1;
                }
            }

            return result;
        }

        private static void GetSplitIndeces(int[,] data, List<int> indeces, int i, int j)
        {
            if (data[i, j] == -1)
                return;
            indeces.Add(data[i, j]);
            GetSplitIndeces(data, indeces, i, data[i, j]);
            GetSplitIndeces(data, indeces, data[i, j] + 1, j);
        }

        #endregion

    }

}