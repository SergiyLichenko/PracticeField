/* Problem Statement 
http://www.geeksforgeeks.org/searching-for-patterns-set-3-rabin-karp-algorithm/ */

using System;


namespace GitHub
{
    class Program
    {
        static void Main()
        {
            string text = "abedabcsssf";
            string pattern = "bcs";

            var result = RabinKarp.Compute(text, pattern);
            Console.WriteLine($"Text '{text}' match " +
                              $"pattern '{pattern}'? - {result}");
            Console.ReadLine();
        }
    }

    //Rabin Karp Substring Search Pattern Matching
    public class RabinKarp
    {
        private static int _primeNumber = 101;
        public static bool Compute(string text, string pattern)
        {
            if (text == null || pattern == null)
                throw new ArgumentNullException();
            if (pattern.Length > text.Length)
                return false;
            var patternHash = CreateHash(pattern, 0, pattern.Length);
            var currentHash = CreateHash(text, 0, pattern.Length);

            int startIndex = 0;
            while (startIndex + pattern.Length <= text.Length)
            {
                if (currentHash == patternHash &&
                    text.Substring(startIndex, pattern.Length) == pattern)
                    return true;
                if (startIndex + pattern.Length < text.Length)
                    currentHash = RollingHash(currentHash, text[startIndex], pattern.Length,
                        text[startIndex + pattern.Length]);
                startIndex++;
            }

            return false;
        }

        private static int CreateHash(string str, int startIndex, int length)
        {
            int sum = 0;
            for (int i = startIndex; i < startIndex + length; i++)
                sum += str[i] * (int)Math.Pow(_primeNumber, i);

            return sum;
        }

        private static int RollingHash(int oldHash, char oldValue, int patternLength, char newValue)
        {
            int x = oldHash - oldValue;
            x = x / _primeNumber;
            return x + (int)Math.Pow(_primeNumber, patternLength - 1) * newValue;
        }
    }
}
