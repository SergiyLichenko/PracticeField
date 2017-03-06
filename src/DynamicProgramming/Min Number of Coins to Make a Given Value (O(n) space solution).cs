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
            int[] coins = { 2, 5, 3, 6 };
            int total = 10;

            var result = CoinChangingProblem(coins, total);

            Console.WriteLine($"It will take {result.Count()} " +
                              $"coins to form {total}");
            Console.WriteLine($"Coins: {string.Join(" ", result)}");
            Console.ReadLine();
        }

        #region Coin Changing Problem

        private static List<int> CoinChangingProblem(int[] coins, int total)
        {
            int[] numbers = new int[total + 1];
            int[] indeces = new int[total + 1];

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = Int32.MaxValue - 1;
                indeces[i] = -1;
            }
            numbers[0] = 0;

            //fill data
            for (int i = 0; i < coins.Length; i++)
            {
                for (int j = 0; j < numbers.Length; j++)
                {
                    if (coins[i] <= j && numbers[j] > 1 + numbers[j - coins[i]])
                    {
                        numbers[j] = 1 + numbers[j - coins[i]];
                        indeces[j] = i;
                    }
                }

            }

            //get coins
            var result = new List<int>();
            int ii = numbers.Length - 1;
            while (ii != 0)
            {
                var coin = coins[indeces[ii]];
                result.Add(coin);
                ii -= coin;
            }
            return result;
        }
        #endregion
    }

}