using System;


namespace GitHub
{
    class Program
    {
        static void Main()
        {
            string text = "abcabbabcababb";
            string pattern = "babcab";

            var result = KMP.Compute(text, pattern);
            Console.WriteLine($"Text '{text}' match " +
                              $"pattern '{pattern}'? - {result}");
            Console.ReadLine();
        }
    }

    //KMP (Knuth-Morris-Prath) Substring Search
    public class KMP
    {
        public static bool Compute(string text, string pattern)
        {
            if (text == null || pattern == null)
                throw new ArgumentNullException();
            if (pattern.Length > text.Length)
                return false;

            var prefixSuffixArray = GetPrefixSuffixArray(pattern);
            var result = CheckText(text, pattern, prefixSuffixArray);

            return result;
        }

        private static int[] GetPrefixSuffixArray(string input)
        {
            int[] result = new int[input.Length];

            int j = 0;
            for (int i = 1; i < input.Length; i++)
            {
                if (input[i] == input[j])
                {
                    j++;
                    result[i] = j;
                }
                else if (j != 0)
                {
                    j = result[j - 1];
                    i--;
                }
            }

            return result;
        }

        private static bool CheckText(string text, string pattern, int[] prefixSuffixArray)
        {
            int j = 0;

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == pattern[j])
                    j++;
                else if (j != 0)
                {
                    j = prefixSuffixArray[j - 1];
                    i--;
                }

                if (j == pattern.Length)
                    return true;
            }

            return false;
        }
    }
}
