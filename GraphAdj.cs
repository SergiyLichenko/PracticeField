using System.Collections.Generic;
using System.Linq;

namespace GitHub
{
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

        #region Kosaraju's Algorithm - all strongly connected components

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
                allComponents.Add(new HashSet<int>(dfsStack));
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

        #region Johnson's Algorithm - all simple cycles in the graph

        public List<List<int>> GetAllSimpleCycles()
        {
            List<List<int>> result = new List<List<int>>();
            var graph = _adjacentMatrix.ToList();

            for (int i = 1; i < graph.Count; i++)
            {
                var all = FindAllStronglyConnectedComponents(i);//Kosaraju's algorithm to find all strongly connected components

                //select strongly connected component which contains node with least number
                var points = all.FirstOrDefault(x => x.Contains(i) && x.Count > 1);
                if (points == null)
                    continue;

                //create subgraph from current graph, which contains only nodes from strongly connected component
                var subGraph = RestoreGraph(points.ToList(), graph.ToArray());

                var stack = new Stack<int>();
                var blockedSet = new HashSet<int>();
                var blockedMap = new Dictionary<int, HashSet<int>>();

                //find all simple cycles for current node
                List<List<int>> tempResult = new List<List<int>>();
                SimpleCycles(tempResult, subGraph, stack, blockedSet, blockedMap, points.Min(), points.Min(), points.Min());
                result.AddRange(tempResult);

                //remove current node from graph and then repeat again
                graph[i] = null;
                graph = graph.Select(x =>
                {
                    if (x != null && x.Contains(i))
                        x.Remove(i);
                    return x;
                }).ToList();
            }

            return result;
        }

        //Find all simple cycles in current strongly connected component
        private bool SimpleCycles(List<List<int>> result, List<int>[] graph, Stack<int> stack, HashSet<int> blockedSet,
            Dictionary<int, HashSet<int>> blockedMap, int current, int start, int parent)
        {
            stack.Push(current);
            blockedSet.Add(current);

            bool foundCycle = false;

            for (int i = 0; i < graph[current].Count; i++)
            {
                if (graph[current][i] == parent && parent != start)
                    continue;

                if (blockedSet.Contains(graph[current][i]))
                {
                    if (graph[current][i] == start)
                    {
                        result.Add(stack.ToList());
                        result.Last().Reverse();
                        result.Last().Add(graph[current][i]);
                        foundCycle = true;
                    }
                    continue;
                }
                foundCycle = SimpleCycles(result, graph, stack, blockedSet, blockedMap, graph[current][i], start, current) || foundCycle;
            }
            if (!foundCycle)
            {
                foreach (var item in graph[current])
                {
                    if (!blockedMap.ContainsKey(item))
                        blockedMap[item] = new HashSet<int>();
                    blockedMap[item].Add(current);
                }
            }

            var last = stack.Pop();
            if (foundCycle)
            {
                blockedSet.Remove(current);
                if (blockedMap.ContainsKey(last))
                    ClearBlockedMap(blockedMap, last, blockedSet); //Clear blocked nodes if current node is a part of cycle
            }

            return foundCycle;
        }

        //Clear blocked nodes if current node is a part of cycle
        private void ClearBlockedMap(Dictionary<int, HashSet<int>> blockedMap, int last, HashSet<int> blockSet)
        {
            var keys = new List<int>();
            keys.Add(last);
            blockedMap[last].Add(4);
            for (int i = 0; i < keys.Count; i++)
            {
                int currentKey = keys[i];
                if (blockedMap.ContainsKey(currentKey))
                {
                    keys.AddRange(blockedMap[currentKey]);
                    blockedMap.Remove(currentKey);
                    i = -1;
                }
                else
                    i--;
                keys.Remove(currentKey);
                blockSet.Remove(currentKey);
            }
        }

        //Create subgraph from current graph
        private List<int>[] RestoreGraph(List<int> points, List<int>[] graph )
        {
            List<int>[] subGraph = new List<int>[graph.Length];

            for (int i = 0; i < points.Count; i++)
            {
                if (graph[points[i]] != null)
                    subGraph[points[i]] = graph[points[i]].Where(points.Contains).ToList();
            }

            return subGraph.ToArray();
        }

        #endregion
    }
}
