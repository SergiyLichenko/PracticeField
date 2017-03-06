using System;
using System.Linq;

namespace GitHub
{
    public class BinaryTreeNode<T>
    {
        public T Data { get;  set; }
        public BinaryTreeNode<T> Left { get; set; }
        public BinaryTreeNode<T> Right { get; set; }

        public BinaryTreeNode(T data)
        {
            Data = data;
        }
        public BinaryTreeNode()
        {
            
        }
    }

    class Program
    {
        static void Main(String[] args)
        {
            var nodes = new int[] { 10, 12, 20 };
            var values = new int[] { 34, 8, 50 };
            int weight = 0;
            var result = OptimalBinarySearchTree(nodes, values, ref weight);

            Console.WriteLine($"The weight of optimal binary search tree is {weight}");

            Console.ReadLine();
        }

        #region Optimal Binary Search Tree

        private static BinaryTreeNode<int> OptimalBinarySearchTree(int[] nodes, int[] values, ref int weight)
        {
            var data = new int[nodes.Length, nodes.Length];
            var parents = new int[nodes.Length, nodes.Length];

            //create data using bottom off dynamic programming
            for (int l = 0; l < nodes.Length; l++)
            {
                for (int i = 0; i < nodes.Length - l; i++)
                {
                    int value = 0;
                    for (int j = i; j <= i + l; j++)
                        value += values[j];

                    int min = Int32.MaxValue;
                    int parent = 0;
                    for (int j = i; j <= i + l; j++)
                    {
                        int temp = 0;
                        if (j - 1 >= 0)
                            temp += data[i, j - 1];
                        if (j + 1 < data.GetLength(0))
                            temp += data[j + 1, i + l];
                        if (temp < min)
                        {
                            min = temp;
                            parent = j;
                        }
                    }
                    data[i, i + l] = min == Int32.MaxValue ? value : min + value;
                    parents[i, i + l] = parent;
                }
            }
            weight = data[0, data.GetLength(1) - 1];
            var indeces = Enumerable.Range(0, nodes.Length).ToArray();
            var root = new BinaryTreeNode<int>();

            //create binary search tree data structure
            CreateTree(parents, nodes, values, indeces, root);

            return root;
        }

        private static void CreateTree(
            int[,] parents, int[] nodes, int[] values, int[] indeces, BinaryTreeNode<int> root)
        {
            int indexParent = 0;
            if (indeces.Length > 1)
                indexParent = parents[indeces.Min(), indeces.Max()];
            root.Data = nodes[indexParent];

            if (indexParent > 0)
            {
                root.Left = new BinaryTreeNode<int>();
                CreateTree(parents, nodes.Take(indexParent).ToArray(), values.Take(indexParent).ToArray(),
                    indeces.Take(indexParent).ToArray(), root.Left);
            }
            if (indexParent < nodes.Length - 1)
            {
                root.Right = new BinaryTreeNode<int>();
                CreateTree(parents, nodes.Skip(indexParent + 1).ToArray(), values.Skip(indexParent + 1).ToArray(),
                    indeces.Skip(indexParent + 1).ToArray(), root.Right);
            }

        }

        #endregion
    }

}