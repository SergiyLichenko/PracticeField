/* Problem Statement
http://www.geeksforgeeks.org/travelling-salesman-problem-set-1/ */

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
            int n = 10;
            var graph = CreateGraph(n);

            string path = graph.GetTravellingSalesmanPath(1);

            Console.WriteLine($"Travelling salesman path:\n{path}");
            Console.ReadLine();
        }

        private static GraphAdj<int> CreateGraph(int n)
        {
            var graph = new GraphAdj<int>(n, true);

            graph.AddEdge(1, 8, 70); graph.AddEdge(8, 1, 70); graph.AddEdge(1, 9, 30);
            graph.AddEdge(9, 1, 30); graph.AddEdge(1, 10, 10); graph.AddEdge(10, 1, 10);
            graph.AddEdge(1, 2, 50); graph.AddEdge(2, 1, 50); graph.AddEdge(1, 3, 60);
            graph.AddEdge(3, 1, 60); graph.AddEdge(2, 10, 70); graph.AddEdge(10, 2, 70);
            graph.AddEdge(2, 3, 10); graph.AddEdge(3, 2, 10); graph.AddEdge(2, 4, 90);
            graph.AddEdge(4, 2, 90); graph.AddEdge(2, 5, 70); graph.AddEdge(5, 2, 70);
            graph.AddEdge(3, 10, 40); graph.AddEdge(10, 3, 40); graph.AddEdge(3, 5, 70);
            graph.AddEdge(5, 3, 70); graph.AddEdge(3, 4, 20); graph.AddEdge(4, 3, 20);
            graph.AddEdge(4, 5, 20); graph.AddEdge(5, 4, 20); graph.AddEdge(4, 6, 70);
            graph.AddEdge(6, 4, 70); graph.AddEdge(4, 7, 80); graph.AddEdge(7, 4, 80);
            graph.AddEdge(5, 6, 60); graph.AddEdge(6, 5, 60); graph.AddEdge(5, 7, 70);
            graph.AddEdge(7, 5, 70); graph.AddEdge(6, 7, 20); graph.AddEdge(7, 6, 20);
            graph.AddEdge(6, 8, 70); graph.AddEdge(8, 6, 70); graph.AddEdge(7, 8, 40);
            graph.AddEdge(8, 7, 40); graph.AddEdge(7, 9, 50); graph.AddEdge(9, 7, 50);
            graph.AddEdge(8, 9, 30); graph.AddEdge(9, 8, 30); graph.AddEdge(8, 10, 60);
            graph.AddEdge(10, 8, 60); graph.AddEdge(9, 10, 20); graph.AddEdge(10, 9, 20);

            return graph;
        }
    }


    public class GraphAdj<T>
    {
        private List<int>[] _adjacentMatrix;
        public GraphAdj(int size, bool withWeights = false)
        {
            _adjacentMatrix = new List<int>[size + 1];
            if (withWeights)
            {
                for (int i = 1; i < size + 1; i++)
                {
                    _adjacentMatrix[i] = new List<int>(size + 1);
                    for (int j = 0; j < size + 1; j++)
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


        #region Travelling Salesman Problem Held-Karp Algorithm
        public string GetTravellingSalesmanPath(int startVertex)
        {
            var graph = _adjacentMatrix;
            //set max weight for edges which are not presented in the graph
            //edge with weight == 0 is not supported
            foreach (var row in graph)
            {
                if (row == null) continue;
                for (int j = 0; j < row.Count; j++)
                {
                    if (row[j] == 0)
                        row[j] = Int32.MaxValue;
                }
            }

            int n = _adjacentMatrix.Length;

            List<List<int>> subsets = GetAllSubsets(n);
            subsets = subsets.Where(x => !x.Contains(startVertex)).ToList();
            subsets = subsets.OrderBy(x => x.Count).ToList();

            string result = GoTravel(subsets, startVertex, graph);

            return result;
        }

        private string GoTravel(List<List<int>> subsets,
            int startVertex, List<int>[] graph)
        {
            var costs = new Dictionary<TravelSalesmanPath, int>();
            var parents = new Dictionary<TravelSalesmanPath, int>();

            //visit all nodes using BFS and dictionaries above to store
            //intremidiate results
            foreach (List<int> subset in subsets)
            {
                List<int> visiting = null;
                if (subset.Count != graph.Length - 1)
                    visiting =
                       Enumerable.Range(0, graph.Length)
                           .Where(x => x != startVertex && !subset.Contains(x))
                           .ToList();
                else
                    visiting = new List<int>() { startVertex };

                foreach (int currentVisiting in visiting)
                {
                    if (subset.Count == 0)
                    {
                        var path = new TravelSalesmanPath(
                            currentVisiting, new List<int?>());
                        costs[path] = graph[startVertex][currentVisiting];
                        parents[path] = startVertex;
                        continue;
                    }

                    for (int k = 0; k < subset.Count; k++)
                    {
                        if (graph[subset[k]] == null) continue;

                        int cost = graph[subset[k]][currentVisiting];
                        if (cost == Int32.MaxValue)
                            continue;
                        var key = new TravelSalesmanPath(subset[k],
                            new List<int?>(subset.Cast<int?>().
                                Where((x, index) => index != k).ToList()));
                        if (!costs.ContainsKey(key) || costs[key] == Int32.MaxValue)
                            continue;
                        cost += costs[key];
                        if (cost == 270)
                        {
                            var ss = costs.OrderByDescending(x => x.Key.Path.Count).First();
                            ;
                        }
                        var currentKey = new TravelSalesmanPath(currentVisiting,
                            subset.Cast<int?>().ToList());
                        if (costs.ContainsKey(currentKey) && costs[currentKey] < cost)
                            continue;
                        costs[currentKey] = cost;
                        parents[currentKey] = key.To.Value;
                    }
                }
            }

            //make visited shortest path using parents dictionary
            var minCostPath = costs.OrderByDescending(x => x.Key.Path.Count).First();
            //-2 - because -1 for start and -1 for end verteces
            if (minCostPath.Key.Path.Count != graph.Length - 1 - 2)
                return "Does not exists";

            var minPath = new List<int> { startVertex, minCostPath.Key.To.Value };

            var minKey = minCostPath.Key;
            while (parents.ContainsKey(minKey))
            {
                var currentParent = parents[minKey];
                minPath.Add(currentParent);
                minKey = new TravelSalesmanPath(currentParent,
                    minKey.Path.Where(x => x != currentParent).ToList());
            }
            minPath.Reverse();

            return String.Join("->", minPath) + "\nWeight: " + minCostPath.Value;
        }

        private List<List<int>> GetAllSubsets(int n)
        {
            var result = new List<List<int>>();
            var numbers = Enumerable.Range(0, n).ToArray();
            var currentSubset = new List<int>();

            SubsetUtil(result, currentSubset, numbers, 0);

            return result;
        }

        private void SubsetUtil(List<List<int>> result, List<int> currentSubset,
            int[] numbers, int startIndex)
        {
            for (int i = startIndex; i < numbers.Length; i++)
            {
                currentSubset.Add(numbers[i]);
                SubsetUtil(result, currentSubset, numbers, ++startIndex);
                currentSubset.Remove(numbers[i]);
            }

            result.Add(new List<int>(currentSubset));
        }

        #endregion
    }


    public struct TravelSalesmanPath
    {
        public int? To { get; set; }
        public List<int?> Path { get; set; }

        public TravelSalesmanPath(int? to, List<int?> path)
        {
            To = to;
            Path = path;
        }

        public override bool Equals(object obj)
        {
            var equalTo = (TravelSalesmanPath)obj;

            if (equalTo.To != To)
                return false;
            if (equalTo.Path.Count != Path.Count)
                return false;

            return Path.All(item => equalTo.Path.Contains(item));
        }
    }
}
