using System.Xml.Linq;
using RunningRoute.Core;    
namespace RunningRoute.Runner
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // test code need to be removed as just copied directly from claude
            var a = new Node(1, 51.5074, -0.1278); // London
            var b = new Node(2, 51.4545, -2.5879); // Bristol
            var c = new Node(3, 51.4816, -3.1791); // Cardiff

            var graph = new Graph();
            graph.AddNode(a);
            graph.AddNode(b);
            graph.AddNode(c);

            // A -> B, B -> C, A -> C (direct, made expensive)
            graph.AddEdge(a.NodeId, new Edge(b.NodeId, 100));
            graph.AddEdge(b.NodeId, new Edge(c.NodeId, 100));
            graph.AddEdge(a.NodeId, new Edge(c.NodeId, 500));

            var path = AStar.Find(graph, a.NodeId, c.NodeId);

            Console.WriteLine($"Path count: {path.Count}");
            foreach (var n in path)
                Console.WriteLine(n);
        }
    }
}
