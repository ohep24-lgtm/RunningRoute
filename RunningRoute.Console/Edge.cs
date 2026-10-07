using System;
using System.Collections.Generic;
using System.Text;

namespace RunningRoute.App
{

  
        public class Edge
        {

            public long TargetNodeId { get; }
            public double Distance { get; }
            public Edge(long targetNodeId, double distance )
            {
                TargetNodeId = targetNodeId ;
                Distance = distance;
            }

        }
    }
