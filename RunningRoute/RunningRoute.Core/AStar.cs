using System;
using System.Collections.Generic;
using System.Text;

namespace RunningRoute.Core
{
    public static class AStar
    {
   


        public static List<Node> Find(Graph graph, long startId, long goalId)
        {

            // Set gScore of the start node to 0.
            // Initilaze dictionary for g score and
            var gScore = new Dictionary<long, double>
            {
                [startId] = 0.0
            };

            var cameFrom = new Dictionary<long, long>();
            var closed = new HashSet<long>();
            var fringe = new PriorityQueue<long, double>();

            if (!graph.Nodes.ContainsKey(startId) || !graph.Nodes.ContainsKey(goalId))
            {
                return new List<Node>();
            }

            if (startId == goalId)
            {
                return new List<Node> { graph.Nodes[startId] };
            }

            var startNode = graph.Nodes[startId];
            var goalNode = graph.Nodes[goalId];

            // Work out the start node's priority: 0 plus the straight-line distance from start to goal.
            // Put the start node in the open set with that priority.
            fringe.Enqueue(startId, Heuristic(startNode, goalNode));

            // Loop while the open set isn't empty:
            while (fringe.Count > 0)
            {
                // Take out the node with the lowest priority. Call it current.
                var currentId = fringe.Dequeue();

                // If current is already in the closed set, skip it and go back to the top of the loop. (This handles duplicates, which you'll get because the queue can't update priorities.)
                if (closed.Contains(currentId))
                {
                    continue;
                }

                // Add current to the closed set.
                closed.Add(currentId);

                // If current is the goal, stop. You've found the best route, so go to "Rebuild the path" below.
                if (currentId == goalId)
                {
                    break;
                }

                // For each neighbour of current (each edge going out of it):
                if (!graph.Adjacency.TryGetValue(currentId, out var edges))
                {
                    continue;
                }

                foreach (var edge in edges)
                {
                    // If the neighbour is in the closed set, ignore it.
                    if (closed.Contains(edge.TargetNodeId))
                    {
                        continue;
                    }

                    // Work out a candidate distance: gScore of current plus the length of the edge to this neighbour.
                    var candidate = gScore[currentId] + edge.Distance;

                    var existingG = gScore.GetValueOrDefault(
                        edge.TargetNodeId,
                        double.PositiveInfinity);

                    // If the neighbour has no gScore yet, or your candidate is smaller than its current gScore, you've found a better way to reach it:
                    if (candidate < existingG)
                    {
                        // Set the neighbour's gScore to the candidate.
                        gScore[edge.TargetNodeId] = candidate;

                        // Set cameFrom of the neighbour to current.
                        cameFrom[edge.TargetNodeId] = currentId;

                        // Work out its priority: new gScore plus the straight-line distance from the neighbour to the goal.
                        var neighbourNode = graph.Nodes[edge.TargetNodeId];
                        var priority = candidate + Heuristic(neighbourNode, goalNode);

                        // Add the neighbour to the open set with that priority (even if it's already in there).
                        fringe.Enqueue(edge.TargetNodeId, priority);
                    }
                }
            }

            // If the loop ends because the open set is empty and you never reached the goal, there is no route. Report that.
            if (!cameFrom.ContainsKey(goalId))
            {
                return new List<Node>();
            }

            // Rebuild the path
            var path = new List<Node>();
            var cursor = goalId;
            while (cursor != startId)
            {
                path.Add(graph.Nodes[cursor]);
                cursor = cameFrom[cursor];
            }
            path.Add(graph.Nodes[startId]);
            path.Reverse();
            return path;
        }

        private static double Heuristic(Node a, Node b)
        {
            return Geo.Haversine(a.Lat, a.Lon, b.Lat, b.Lon);
        }










    }
}
