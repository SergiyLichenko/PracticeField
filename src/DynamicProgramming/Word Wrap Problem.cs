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
            var words = new string[]
            {
                "Geeks", "for", "Geeks",
                "presents", "word", "wrap", "problem"
            };

            int width = 15;

            var lines = TextJustification(words, width);

            Console.WriteLine("Optimized arrangement of words: \n");
            foreach (var line in lines)
                Console.WriteLine(line);
            Console.ReadLine();
        }

        #region Text justification

        private static List<string> TextJustification(string[] words, int width)
        {
            var data = new int[words.Length, words.Length];

            for (int i = 0; i < words.Length; i++)
            {
                for (int j = 0; j < words.Length - i; j++)
                {
                    int cost = 0;
                    var sum = words.Skip(j).Take(i + 1).Select(x => x.Length).Sum() + i;
                    if (i == 0)
                        cost = (int)Math.Pow(width - words[j].Length, 2);
                    else if (sum > width)
                        cost = Int32.MaxValue;
                    else
                        cost = (int)Math.Pow(width - sum, 2);

                    data[j, i + j] = cost;
                }
            }

            var result = GetWords(data, words);
            return result;
        }

        private static List<string> GetWords(int[,] data, string[] words)
        {
            int[] minCost = new int[data.GetLength(1)];
            int[] positions = new int[data.GetLength(1)];

            for (int i = data.GetLength(0) - 1; i >= 0; i--)
            {
                bool shouldSplit = false;
                int min = Int32.MaxValue;
                int position = i + 1;
                for (int j = data.GetLength(1) - 1; j >= i; j--)
                {
                    if (data[i, j] == Int32.MaxValue)
                        shouldSplit = true;
                    if (data[i, j] == Int32.MaxValue && data[i, j - 1] == Int32.MaxValue)
                        continue;

                    int tempCost = 0;
                    if (!shouldSplit)
                        tempCost = data[i, j];
                    else
                        tempCost = data[i, j - 1] + minCost[j];

                    if (tempCost < min)
                    {
                        min = tempCost;
                        position = shouldSplit ? j : j + 1;
                    }
                    if (shouldSplit && j == i + 1) break;
                }
                minCost[i] = min;
                positions[i] = position;
            }

            var result = new List<string>();
            for (int i = 0; i < words.Length; i++)
            {
                var line = String.Join(" ", words.Skip(i).Take(positions[i] - i));
                result.Add(line);
                i += positions[i] - i - 1;
            }

            return result;
        }
        #endregion
    }

}