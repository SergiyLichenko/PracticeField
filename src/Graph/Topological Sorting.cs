/* Problem Statement
http://www.geeksforgeeks.org/topological-sorting/ */

using System;
using System.Collections.Generic;

namespace GitHub
{
    class Program
    {
        public static void Main()
        {
            int countNodes = 6;
            var graphAdj = CreateGraph(countNodes);

            var topologicallySorted = graphAdj.TopologicalSort();

            Console.WriteLine("Topologically sorted: \n{0}", 
                String.Join("->", topologicallySorted));
            Console.WriteLine();
            Console.ReadLine();
        }

        private static GraphAdj<int> CreateGraph(int n)
        {
            GraphAdj<int> graphAdj = new GraphAdj<int>(n);

            graphAdj.AddEdge(2, 3);
            graphAdj.AddEdge(3, 1);
            graphAdj.AddEdge(4, 0);
            graphAdj.AddEdge(4, 1);
            graphAdj.AddEdge(5, 0);
            graphAdj.AddEdge(5, 2);

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


        #region Topological Sorting
        public IEnumerable<int> TopologicalSort()
        {
            var stack = new Stack<int>();
            var visited = new HashSet<int>();

            for (int i = 0; i < _adjacentMatrix.Length; i++)
            {
                if (visited.Contains(i)) continue;

                TopologicalSortUtil(stack, visited, i);
            }

            return stack;
        }

        public void TopologicalSortUtil(Stack<int> stack, HashSet<int> visited, int current)
        {
            visited.Add(current);
            if (_adjacentMatrix[current] == null)
            {
                stack.Push(current);
                return;
            }

            foreach (var item in _adjacentMatrix[current])
            {
                if (visited.Contains(item)) continue;
                TopologicalSortUtil(stack, visited, item);
            }
            stack.Push(current);
        }
        #endregion
    }
}
