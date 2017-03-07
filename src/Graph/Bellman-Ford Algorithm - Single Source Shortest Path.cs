/* Problem Statement 
http://www.geeksforgeeks.org/dynamic-programming-set-23-bellman-ford-algorithm/ */

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
            var graph = new GraphAdj<int>(5, true);

            graph.AddEdge(0, 1, 6);
            graph.AddEdge(0, 3, 7);
            graph.AddEdge(1, 3, 8);
            graph.AddEdge(1, 4, -4);
            graph.AddEdge(1, 2, 5);
            graph.AddEdge(2, 1, -2);
            graph.AddEdge(3, 2, -3);
            graph.AddEdge(3, 4, 9);
            graph.AddEdge(4, 2, 7);
            graph.AddEdge(4, 0, 2);

            int from = 0;
            int to = 4;
            int distance = 0;
            string path = String.Empty;

            bool containsNegativeCycle = graph.GetShortestPaths(from, to, ref distance, ref path);

            Console.WriteLine("Shortest path from {0} to {1} is" +
                              " {2} with total cost {3}", from, to, path, distance);
            Console.WriteLine("Graph contains negative " +
                              "cycle? - " + containsNegativeCycle);
            Console.ReadLine();
        }
    }

    public class GraphAdj<T>
    {
        private List<int>[] _adjacentMatrix;
        public GraphAdj(int size, bool withWeights = false)
        {
            _adjacentMatrix = new List<int>[size];
            if (withWeights)
            {
                for (int i = 0; i < size; i++)
                {
                    _adjacentMatrix[i] = new List<int>(size);
                    for (int j = 0; j < size; j++)
                    {
                        _adjacentMatrix[i].Add(Int32.MaxValue);
                    }
                }
            }
        }

        public void AddEdge(int from, int to, int weight)
        {
            _adjacentMatrix[from][to] = weight;
        }


        #region Bellman-Ford Algorithm - Single Source Shortest Sath

        public bool GetShortestPaths(int from, int to, ref int distance, ref string path)
        {
            var allEdges = new Dictionary<int[], int>();
            var distanceMap = new Dictionary<int, int>();
            var parentMap = new Dictionary<int, int?>();

            //getting all edges from graph, initializing distance and parent maps
            for (int i = 0; i < _adjacentMatrix.Length; i++)
            {
                distanceMap.Add(i, Int32.MaxValue);
                parentMap.Add(i, null);
                for (int j = 0; j < _adjacentMatrix[i].Count; j++)
                {
                    if (_adjacentMatrix[i][j] != 0)
                    {
                        allEdges.Add(new int[] { i, j }, _adjacentMatrix[i][j]);
                    }
                }
            }
            distanceMap[from] = 0;

            bool graphContainsNegativeCycle = false;

            //Bellman-Ford 
            for (int i = 0; i < _adjacentMatrix.Length; i++)
            {
                for (int j = 0; j < allEdges.Count; j++)
                {
                    var currentKey = allEdges.Keys.ElementAt(j);
                    if (allEdges[currentKey] == Int32.MaxValue ||
                        distanceMap[currentKey[0]] == Int32.MaxValue)
                        continue;

                    if (distanceMap[currentKey[1]] >
                        distanceMap[currentKey[0]] + allEdges[currentKey])
                    {
                        if (i == _adjacentMatrix.Length - 1)
                        {
                            graphContainsNegativeCycle = true;
                            break;
                        }
                        distanceMap[currentKey[1]] = distanceMap[currentKey[0]] + allEdges[currentKey];
                        parentMap[currentKey[1]] = currentKey[0];
                    }
                }
            }

            if (!graphContainsNegativeCycle)
            {

                distance = distanceMap[to];
                //building shortest path
                var pathNodes = new List<int>();
                var currentNode = parentMap[to];
                while (currentNode.HasValue)
                {
                    pathNodes.Add(currentNode.Value);
                    currentNode = parentMap[currentNode.Value];
                }
                pathNodes.Reverse();
                pathNodes.Add(to);
                path = String.Join("->", pathNodes);
            }
            else
            {
                path = "Does not exist";
                distance = -1;
            }

            return graphContainsNegativeCycle;
        }

        #endregion
    }
}
