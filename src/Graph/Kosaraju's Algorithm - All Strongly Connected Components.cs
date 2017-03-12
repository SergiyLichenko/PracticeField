/* Problem Statement
http://www.geeksforgeeks.org/strongly-connected-components/ */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHub
{
    public class Program
    {
        static void Main()
        {
            int n = 5; int start = 0;
            var graphAdj = CreateGraph(n);

            var components = graphAdj.FindAllStronglyConnectedComponents(start);

            Console.WriteLine("Strongly connected components:\n");
            foreach (var item in components)
                Console.WriteLine(String.Join(" ", item));
            Console.ReadLine();
        }

        private static GraphAdj<int> CreateGraph(int n)
        {
            GraphAdj<int> graphAdj = new GraphAdj<int>(n);

            graphAdj.AddEdge(1, 0);
            graphAdj.AddEdge(0, 2);
            graphAdj.AddEdge(0, 3);
            graphAdj.AddEdge(2, 1);
            graphAdj.AddEdge(3, 4);

            return graphAdj;
        }
    }

    public class GraphAdj<T>
    {
        private List<int>[] _adjacentMatrix;
        public GraphAdj(int size)
        {
            _adjacentMatrix = new List<int>[size];
        }

        public void AddEdge(int from, int to)
        {
            if (_adjacentMatrix[from] == null)
                _adjacentMatrix[from] = new List<int>();
            _adjacentMatrix[from].Add(to);
        }

        #region Kosaraju's Algorithm - All Strongly Connected Components

        public IEnumerable<HashSet<int>> FindAllStronglyConnectedComponents(int start, List<int>[] graph = null)
        {
            graph = graph ?? _adjacentMatrix;

            var visited = new HashSet<int>();
            var finishedStack = new Stack<int>();
            var dfsStack = new Stack<int>();

            //first pass
            for (var i = 0; i < graph.Length; i++)
            {
                var currentNode = i == 0 ? start : i;

                if (visited.Contains(currentNode))
                    continue;

                StronglyConnectedComponentsHelper(graph, visited, finishedStack, dfsStack, currentNode);
                finishedStack.Push(currentNode);
            }

            //reverse graph
            var reversed = new List<int>[graph.Length];
            for (var i = 0; i < graph.Length; i++)
                reversed[i] = new List<int>();

            for (int i = 0; i < graph.Length; i++)
            {
                if (graph[i] == null) continue;
                foreach (var item in graph[i])
                    reversed[item].Add(i);
            }

            //result
            var allComponents = new List<HashSet<int>>();

            //second pass
            visited.Clear();
            while (finishedStack.Count > 0)
            {
                dfsStack.Clear();
                var current = finishedStack.Pop();
                if (visited.Contains(current))
                    continue;

                StronglyConnectedComponentsHelper(reversed, visited, finishedStack, dfsStack, current);
                allComponents.Add(new HashSet<int>(dfsStack.Reverse()));
            }

            return allComponents;
        }

        private void StronglyConnectedComponentsHelper(List<int>[] graph, HashSet<int> visited,
            Stack<int> finishedStack, Stack<int> dfsStack, int currentNode)
        {
            visited.Add(currentNode);
            dfsStack.Push(currentNode);

            if (graph[currentNode] == null) return;
            foreach (var item in graph[currentNode])
            {
                if (visited.Contains(item))
                    continue;

                StronglyConnectedComponentsHelper(graph, visited, finishedStack, dfsStack, item);
                finishedStack.Push(item);
            }
        }

        #endregion
    }
}
