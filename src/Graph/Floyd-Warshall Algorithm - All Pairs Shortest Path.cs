/* Problem Statement
http://www.geeksforgeeks.org/dynamic-programming-set-16-floyd-warshall-algorithm/ */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHub
{
    class Program
    {
        public static void Main()
        {
            int countNodes = 4;
            var graphAdj = CreateGraph(countNodes);

            var distance = new List<List<int>>(countNodes);
            var path = new List<List<int>>(countNodes);

            graphAdj.GetAllShortestPaths(distance, path);

            Console.WriteLine($"Distance Matrix");
            PrintMatrix(distance);
            Console.WriteLine();

            Query(distance, path, 1, 3);
            Query(distance, path, 3, 1);
            Query(distance, path, 2, 4);

            Console.ReadLine();
        }

        private static void PrintMatrix(List<List<int>> distance)
        {
            for (int i = 0; i < distance.Count; i++)
            {
                for(int j = 0;j<distance[i].Count;j++)
                    Console.Write(distance[i][j] + "\t");
                Console.WriteLine();
            }
        }

        private static void Query(List<List<int>> distance, List<List<int>> path, int from, int to)
        {
            var res = GetPath(distance, path, from, to);

            Console.WriteLine($"Path between {from} and {to}: \n{res.Path}");
            Console.WriteLine($"Weight: \n{res.Weight}");
            Console.WriteLine();
        }

        private static dynamic GetPath(List<List<int>> distance, List<List<int>> path, int from, int to)
        {
            var nodes = new List<int> { from };

            int currentIndex = to - 1;
            while (currentIndex != from - 1)
            {
                if (currentIndex == Int32.MaxValue)
                    return new { Path = "Does not exist", Weight = Int32.MaxValue };
                nodes.Insert(1, currentIndex + 1);
                currentIndex = path[from - 1][currentIndex];
            }
            return new { Path = String.Join("->", nodes), Weight = distance[from - 1][to - 1] };
        }

        private static GraphAdj<int> CreateGraph(int countNodes)
        {
            GraphAdj<int> graphAdj = new GraphAdj<int>(countNodes);

            graphAdj.AddEdge(1, 2, 100);
            graphAdj.AddEdge(1, 3, -2);
            graphAdj.AddEdge(1, 4, 100);
            graphAdj.AddEdge(2, 1, 4);
            graphAdj.AddEdge(2, 3, 3);
            graphAdj.AddEdge(2, 4, 100);
            graphAdj.AddEdge(4, 2, -1);
            graphAdj.AddEdge(4, 1, 100);
            graphAdj.AddEdge(4, 3, 100);
            graphAdj.AddEdge(3, 1, 100);
            graphAdj.AddEdge(3, 2, 100);
            graphAdj.AddEdge(3, 4, 2);

            return graphAdj;
        }
    }

    public class GraphAdj<T>
    {
        private List<int>[] _adjacentMatrix;
        public GraphAdj(int size)
        {
            _adjacentMatrix = new List<int>[size];
            for (int i = 0; i < size; i++)
            {
                _adjacentMatrix[i] = new List<int>(size);
                for (int j = 0; j < size; j++)
                {
                    _adjacentMatrix[i].Add(Int32.MaxValue);
                }
            }
        }


        public void AddEdge(int from, int to, int weight)
        {
            _adjacentMatrix[from - 1][to - 1] = weight;
        }


        #region Floyd-Warshall All Pairs Shortest Path

        public void GetAllShortestPaths(List<List<int>> distance, List<List<int>> path)
        {
            //construct distance and path matrixes
            for (int i = 0; i < _adjacentMatrix.Length; i++)
            {
                distance.Add(new List<int>(_adjacentMatrix.Length));
                path.Add(new List<int>(_adjacentMatrix.Length));

                for (int j = 0; j < _adjacentMatrix[i].Count; j++)
                {
                    if (i == j)
                        distance[i].Add(0);
                    else
                        distance[i].Add(_adjacentMatrix[i][j]);

                    if (_adjacentMatrix[i][j] == Int32.MaxValue)
                        path[i].Add(Int32.MaxValue);
                    else
                        path[i].Add(i);
                }
            }

            //checking all pairs shortest path
            for (int k = 0; k < _adjacentMatrix.Length; k++)
            {
                for (int i = 0; i < _adjacentMatrix.Length; i++)
                {
                    for (int j = 0; j < _adjacentMatrix.Length; j++)
                    {
                        if (distance[i][k] == Int32.MaxValue || distance[k][j] == Int32.MaxValue)
                            continue;
                        if (distance[i][j] > distance[i][k] + distance[k][j])
                        {
                            distance[i][j] = distance[i][k] + distance[k][j];
                            path[i][j] = path[k][j];
                        }
                    }
                }
            }

        }

        #endregion
    }
}
