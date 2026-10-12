using System;
using System.Collections.Generic;
using System.Text;

namespace RunningRoute.Core
{
    public class Graph
    {
        // adjacency: NodeId -> list of outgoing edges
        public Dictionary<long, List<Edge>> Adjacency { get; } = new();
        public Dictionary<long, Node> Nodes { get; } = new();

        public void AddNode(Node node) => Nodes[node.NodeId] = node;

        public void AddEdge(long fromNodeId, Edge edge)
        {
            if (!Adjacency.TryGetValue(fromNodeId, out var list))
            {
                list = new List<Edge>();
                Adjacency[fromNodeId] = list;
            }
            list.Add(edge);
        }
    }
}
