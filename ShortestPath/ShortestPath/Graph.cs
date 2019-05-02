using System.Collections.Generic;
using System;
using static ShortestPath.Graph.Vertex;

namespace ShortestPath
{
    public class Graph
    {
        public List<Vertex> Vertices { get; private set; }
        public List<UndirectedEdge> Edges { get; private set; }

        public bool AddVertex(string label, int x, int y)
        {
            Vertex vertex = new Vertex(label, new LocationXY(x, y));

            if (Vertices == null)
                Vertices = new List<Vertex>();
            else
            {
                foreach(Vertex v in Vertices)
                {
                    if (v.Equals(vertex))
                        return false;
                }
            }

            Vertices.Add(vertex);
            return true;
        }

        public bool AddEdge(string vOneLabel, string vTwoLabel, int weight)
        {
            Vertex vOne = Vertices.Find(p => p.Label == vOneLabel);
            if (vOne == null)
                throw new ArgumentException("vOneLabel is not a valid vertex label.");

            Vertex vTwo = Vertices.Find(p => p.Label == vTwoLabel);
            if (vTwo == null)
                throw new ArgumentException("vTwoLabel is not a valid vertex label.");
            
            UndirectedEdge undirectedEdge = new UndirectedEdge()
            {
                VOne = vOne,
                VTwo = vTwo,
                Weight = weight
            };

            if (Edges == null)
                Edges = new List<UndirectedEdge>();
            else
            {
                foreach (UndirectedEdge uEdge in Edges)
                {
                    if (uEdge.Equals(undirectedEdge))
                        return false;
                }
            }


            Edges.Add(undirectedEdge);

            vOne.IncidentEdges.Add(undirectedEdge);

            //invert and re add just in case it both ways werent noted in graph creation- it is undirected anyway ;)
            undirectedEdge = new UndirectedEdge()
            {
                VOne = vTwo,
                VTwo = vOne,
                Weight = weight
            };
            vTwo.IncidentEdges.Add(undirectedEdge);
            return true;
        }

        public class UndirectedEdge
        {
            public Vertex VOne { get; set; }
            public Vertex VTwo { get; set; }
            public int Weight { get; set; }

            public bool Equals(UndirectedEdge edge) //"Overwrite" Equals() function
            {
                if (VOne.Equals(edge.VOne) && VTwo.Equals(edge.VTwo))
                {
                    return true;
                }
                return false;
            }
        }

        public class Vertex
        {
            public string Label { get; set; }
            public List<UndirectedEdge> IncidentEdges { get; set; }
            public double Distance { get; set; }
            public bool IsVisited { get; set; }
            public LocationXY Location { get; set; }

            public Vertex(string label, LocationXY location)
            {
                Label = label;
                IncidentEdges = new List<UndirectedEdge>();
                Distance = int.MaxValue;
                IsVisited = false;
                Location = location;
            }

            public bool Equals(Vertex v) //"Overwrite" Equals() function
            {
                if (Label.Equals(v.Label))
                {
                    return true;
                }
                return false;
            }

            public class LocationXY
            {
                public int X { get; }
                public int Y { get; }
                public LocationXY(int x, int y)
                {
                    X = x;
                    Y = y;
                }
            }

        }
    }
}
