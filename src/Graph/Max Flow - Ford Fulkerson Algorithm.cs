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
            int n = 6;
           
            var graph = CreateGraph(n);
            QueryMaxFlow(graph, 0, 5);

            Console.ReadLine();
        }

        private static void QueryMaxFlow(GraphAdj<int> graph, int from, int to)
        {
            var maximumFlow = graph.MaximumFlow(from, to);

            Console.WriteLine($"Max flow from {from} " +
                              $"to {to}:  {maximumFlow}");
        }

        private static GraphAdj<int> CreateGraph(int n )
        {
            var graph = new GraphAdj<int>(n, true);

            graph.AddEdge(0, 1, 16); graph.AddEdge(0, 2, 13);
            graph.AddEdge(1, 2, 10); graph.AddEdge(1, 3, 12);
            graph.AddEdge(2, 1, 4); graph.AddEdge(2, 4, 14);
            graph.AddEdge(3, 2, 9); graph.AddEdge(3, 5, 20);
            graph.AddEdge(4, 3, 7); graph.AddEdge(4, 5, 4);

            return graph;
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
                        _adjacentMatrix[i].Add(0);
                    }
                }
            }
        }

        public void AddEdge(int from, int to, int weight)
        {
            _adjacentMatrix[from][to] = weight;
        }


        #region Max Flow - Ford Fulkerson Algorithm

        public int MaximumFlow(int source, int dest)
        {
            var result = 0;
            var graph = _adjacentMatrix;
            var n = _adjacentMatrix.Length;

            while (true)
            {
                var parentMap = new Dictionary<int, int>();
                var queue = new Queue<int>();
                var visited = new HashSet<int>();

                queue.Enqueue(source);
                visited.Add(source);
                while (queue.Count > 0)
                {
                    int currentNode = queue.Dequeue();
                    if (currentNode == dest)
                        break;
                    for (int i = 0; i < n; i++)
                    {
                        if (graph[currentNode][i] == 0 || visited.Contains(i)) continue;

                        queue.Enqueue(i);
                        parentMap[i] = currentNode;
                        visited.Add(i);
                    }
                }
                if (!parentMap.ContainsKey(dest))
                    break;
                var minFlow = MinFlowForPath(parentMap, graph, source, dest);
                result += minFlow;
            }
            return result;
        }

        private int MinFlowForPath(Dictionary<int, int> parentMap, List<int>[] graph, int source, int dest)
        {
            //findin minimum residual capacity
            var stack = new Stack<int>();
            int current = dest;

            while (parentMap.ContainsKey(current))
            {
                stack.Push(current);
                current = parentMap[current];
            }

            stack.Push(source);
            var path = stack.ToList();
            var weights = new List<int>();

            for (int i = 0; i < path.Count - 1; i++)
                weights.Add(graph[path[i]][path[i + 1]]);
            int min = weights.Min();

            //adjust graph
            for (int i = 0; i < path.Count - 1; i++)
            {
                graph[path[i]][path[i + 1]] -= min;
                graph[path[i + 1]][path[i]] += min;
            }


            return min;
        }

        #endregion
    }
}
