using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHub
{
    public class Program
    {
        static void Main(string[] args)
        {
            GraphAdj<int> graphAdj = new GraphAdj<int>(10);

            graphAdj.AddEdge(8, 9);
            graphAdj.AddEdge(9, 8);
            graphAdj.AddEdge(1, 8);
            graphAdj.AddEdge(1, 2);
            graphAdj.AddEdge(1, 5);
            graphAdj.AddEdge(2, 9);
            graphAdj.AddEdge(2, 7);
            graphAdj.AddEdge(2, 3);
            graphAdj.AddEdge(3, 2);
            graphAdj.AddEdge(3, 1);
            graphAdj.AddEdge(3, 4);
            graphAdj.AddEdge(3, 6);
            graphAdj.AddEdge(4, 5);
            graphAdj.AddEdge(5, 2);
            graphAdj.AddEdge(6, 4);
            graphAdj.AddEdge(5, 6);

            var simpleCycles = graphAdj.GetAllSimpleCycles();
            foreach (List<int> cycle in simpleCycles)
            {
                string str = String.Join("->", cycle);
                Console.WriteLine(str);
            }
            Console.ReadLine();
        }
    }
}
