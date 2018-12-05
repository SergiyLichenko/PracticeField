using System;
using System.Linq;


namespace GitHub
{
    class Program
    {
        static void Main()
        {
            string text = "xabcabzabc";
            string pattern = "abc";

            var result = ZAlgorithm.Compute(text, pattern);
            Console.WriteLine($"Text '{text}' match " +
                              $"pattern '{pattern}'? - {result}");
            Console.ReadLine();
        }
    }

    //Z Algorithm Substring Search Pattern Matching
    public class ZAlgorithm
    {
        private const char UniqueChar = '#';

        public static bool Compute(string text, string pattern)
        {
            if (text == null || pattern == null)
                throw new ArgumentNullException();
            if (pattern.Length > text.Length)
                return false;

            var input = pattern + UniqueChar + text;
            var zArray = GetZArray(input);

            return zArray.Contains(pattern.Length);
        }

        private static int[] GetZArray(string input)
        {
            var zArray = new int[input.Length];

            int offset = 0;
            for (int i = 1; i < input.Length; i++)
            {
                int j = offset;
                while (i+j < input.Length && input[j] == input[i + j])
                    j++;
                zArray[i] = j;

                int right = i + j;

                bool isOverflow = false;
                for (int k = 1; k < j; k++)
                {
                    if (zArray[k] + i + 1 >= right)
                    {
                        isOverflow = true;
                        offset = right - i - 1;
                        break;
                    }
                    zArray[i + 1] = zArray[k];
                    i++;
                }

                if (!isOverflow)
                    offset = 0;
            }

            return zArray;
        }
    }
}
