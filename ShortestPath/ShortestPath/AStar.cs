using System;
using System.Collections.Generic;
using static ShortestPath.Graph;

namespace ShortestPath
{
    class AStar : IShortestPath
    {
        private Dictionary<Vertex, Vertex> _path = new Dictionary<Vertex, Vertex>();

        public string Execute(Graph graph, string start, string end)
        {
            Vertex startVertex = null;
            Vertex endVertex = null;

            //Find and set the start vertex to 0.
            if (graph.Vertices[0].Label != start)
            {
                startVertex = graph.Vertices.Find(p => p.Label == start);
            }
            else
                startVertex = graph.Vertices[0];
            startVertex.Distance = 0;

            //repeat for every vertice in the 'unvisited' list
            while (graph.Vertices.Count != 0)
            {
                //use the vertex with least distance
                Vertex vertex = graph.Vertices[0];
                foreach (var v in graph.Vertices)
                {
                    if (v.Distance < vertex.Distance)
                        vertex = v;

                    //if end vertex, store it for later use
                    if (v.Label.Equals(end))
                    {
                        endVertex = v;
                    }
                }

                //remove it from the 'unvisited' list
                graph.Vertices.Remove(vertex);
                
                //if curren vertix distance is 'unlimited', stop.
                if (vertex.Distance == int.MaxValue)
                    break;

                //skip is vertex already visited
                if (vertex.IsVisited)
                    continue;
                
                //iterate through every neighboring vertice
                foreach (var edge in vertex.IncidentEdges)
                {
                    var AdjacentVertex = edge.VTwo;

                    //skip if vertex already visited
                    if (!AdjacentVertex.IsVisited)
                    {
                        //distance from the stat to the node
                        var gN= vertex.Distance + edge.Weight;

                        //Euclidean Distance from the node to the goal
                        var hN = Heuristic(AdjacentVertex, endVertex);// Math.Sqrt(((AdjacentVertex.Location.X - endVertex.Location.X) * (AdjacentVertex.Location.X- endVertex.Location.X) + (AdjacentVertex.Location.Y - endVertex.Location.Y) * (AdjacentVertex.Location.Y - endVertex.Location.Y)));
                        if (gN + hN < AdjacentVertex.Distance)
                        {
                            AdjacentVertex.Distance = gN + (int)hN;
                            //if distance+weight is lower than the neighboring, use that and set to parent/child dictionary
                            _path[AdjacentVertex] = vertex;
                        }
                    }

                }

                vertex.IsVisited = true;

                if (endVertex == vertex)
                    break;
            }

            //Finally, print the path into a string, repeat looking in the path dictionary
            //from the end vertex, setting it to the parent and then becoming the end
            //and print the parent until the parent becomes the start vertex again.
            string Path = endVertex.Label;
            while (true)
            {
                Vertex ParentVertex = _path[endVertex];
                Path = Path + " " + ParentVertex.Label;
                if (ParentVertex.Equals(startVertex))
                    break;
                endVertex = ParentVertex;
            }

            Path = Reverse(Path);
            Path = Path.Replace(" ", "->");
            return Path;
        }

        private string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        private double Heuristic(Vertex v, Vertex goal)
        {
            var dx = Math.Abs(v.Location.X - goal.Location.X);
            var dy = Math.Abs(v.Location.Y - goal.Location.Y);
            return  Math.Sqrt(dx * dx + dy * dy);
        }
    }
}
