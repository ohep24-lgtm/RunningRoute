using System;
using System.Collections.Generic;
using System.Text;

namespace RunningRoute.App
{
    public class Node
    {
        
        public long NodeId { get; }
        public double Lat { get; }
        public double Lon { get; }

       
        public Node(long nodeId, double lat, double lon)
        {
            NodeId = nodeId;
            Lat = lat;
            Lon = lon;
        }

        
        public override string ToString()
        {
            return $"Node {NodeId} ({Lat}, {Lon})";
        }
    }
}
