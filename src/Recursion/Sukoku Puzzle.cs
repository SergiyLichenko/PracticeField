/* Problem Statement
http://www.geeksforgeeks.org/backtracking-set-7-suduku/ */

using System;
using System.Collections.Generic;
using System.Linq;

namespace GitHub
{
    class Program
    {
        public static void Main()
        {
            var mass = GetSudokuField();
            Solution solution = new Solution();

            Console.WriteLine("\nSolved sukoku:\n");
            solution.SolveSudoku(mass);

            Console.ReadLine();
        }

        private static char[,] GetSudokuField()
        {
            string[] input =
            {
                "8........",
                "..36.....",
                ".7..9.2..",
                ".5...7...",
                "....457..",
                "...1...3.",
                "..1....68",
                "..85...1.",
                ".9....4.."
            };

            char[,] mass = new char[9, 9];
            Console.WriteLine("Sudoku puzzle by Arto Inkala:\n");
            for (int i = 0; i < input.Length; i++)
            {
                Console.Write("\t");
                for (int j = 0; j < input[i].Length; j++)
                {
                    Console.Write(input[i][j]);
                    mass[i, j] = input[i][j];
                }
                Console.WriteLine();
            }
            return mass;
        }
    }
    public class Solution
    {
        public void SolveSudoku(char[,] board)
        {
            HashSet<int>[,] cells = new HashSet<int>[
              board.GetLength(0), board.GetLength(0)];

            SudokuFunc(board, cells);
        }

        //recursive func
        private bool SudokuFunc(char[,] board, HashSet<int>[,] cells)
        {
            bool hasDot = false;

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(0); j++)
                {
                    if (board[i, j] == '.')
                    {
                        hasDot = true;
                        break;
                    }
                }
                if (hasDot)
                    break;
            }

            if (!hasDot)
            {
                for (int i = 0; i < board.GetLength(0); i++)
                {
                    Console.Write("\t");
                    for (int j = 0; j < board.GetLength(0); j++)
                        Console.Write(board[i, j]);
                    Console.WriteLine();
                }
                return true;
            }
            TraverseBoard(board, cells);

            var sumAvailalbeVars = 0;
            for (int i = 0; i < board.GetLength(0); i++)
                for (int j = 0; j < board.GetLength(0); j++)
                    sumAvailalbeVars += cells[i, j].Count;

            if (hasDot && sumAvailalbeVars == 0)
                return false;

            int[] min = FindMin(cells);

            for (int i = 0; i < cells[min[0], min[1]].Count; i++)
            {

                board[min[0], min[1]] = cells[min[0], min[1]].ElementAt(i).ToString()[0];
                cells[min[0], min[1]].Remove(cells[min[0], min[1]].ElementAt(i));
                if (SudokuFunc(board, cells)) return true;

                board[min[0], min[1]] = '.';
                TraverseBoard(board, cells);
            }
            board[min[0], min[1]] = '.';

            return false;
        }

        //[0] -> i, [1] -> j
        private int[] FindMin(HashSet<int>[,] cells)
        {
            int minI = Int32.MaxValue;
            int minJ = Int32.MaxValue;
            int minCount = Int32.MaxValue;
            for (int i = 0; i < cells.GetLength(0); i++)
            {
                for (int j = 0; j < cells.GetLength(0); j++)
                {
                    if (cells[i, j].Count == 0) continue;
                    if (minCount > cells[i, j].Count)
                    {
                        minCount = cells[i, j].Count;
                        minI = i;
                        minJ = j;
                    }
                }
            }
            return new int[] { minI, minJ };
        }

        private void TraverseBoard(char[,] board, HashSet<int>[,] cells)
        {
            //add 1 -> 9
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(0); j++)
                {
                    cells[i, j] = new HashSet<int>();
                    if (board[i, j] != '.')
                        continue;

                    for (int k = 1; k < 10; k++)
                        cells[i, j].Add(k);
                }
            }

            //board traverse
            for (int i = 0; i < board.GetLength(0); i++)
                for (int j = 0; j < board.GetLength(0); j++)
                    if (board[i, j] == '.')
                        ClearCell(cells, i, j, board);
        }

        private void ClearCell(HashSet<int>[,] cells, int i, int j, char[,] board)
        {
            for (int k = 0; k < board.GetLength(0); k++)
            {
                //horizontal
                if (board[i, k] != '.')
                    cells[i, j].Remove(Convert.ToInt32(board[i, k]) - 48);
                //vertical
                if (board[k, j] != '.')
                    cells[i, j].Remove(Convert.ToInt32(board[k, j]) - 48);
            }

            //square
            for (int k = (i / 3) * 3; k < ((i / 3) + 1) * 3; k++)
                for (int n = (j / 3) * 3; n < ((j / 3) + 1) * 3; n++)
                    if (board[k, n] != '.')
                        cells[i, j].Remove(board[k, n] - 48);
        }
    }
}
