using System;
using System.Collections.Generic;
using static ShortestPath.Graph;

namespace ShortestPath
{
    class DijkstrWithMinHeap : IShortestPath
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

            MinHeap minHeap = new MinHeap();
            foreach (var v in graph.Vertices)
            {
                minHeap.Push(v);

                if (v.Label.Equals(end))
                    endVertex = v;
            }

            while (minHeap.Count() != 0)
            {
                //use the vertex with least distance
                Vertex vertex = minHeap.removeMinHeap();
               
                //skip is vertex already visited
                if (vertex.IsVisited)
                    continue;

                //if curren vertix distance is 'unlimited', stop.
                if (vertex.Distance == int.MaxValue)
                    break;

                //iterate through every neighboring vertice

                foreach (var edge in vertex.IncidentEdges)
                {
                    var AdjacentVertex = edge.VTwo;

                    //skip if vertex already visited
                    if (!AdjacentVertex.IsVisited)
                    {
                        int weight = edge.Weight;
                        if (vertex.Distance + weight < AdjacentVertex.Distance)
                        {
                            AdjacentVertex.Distance = vertex.Distance + weight;
                            //if distance+weight is lower than the neighboring, use that and set to parent/child dictionary
                            _path[AdjacentVertex] = vertex;
                            minHeap.Push(AdjacentVertex);
                        }
                    }

                }
                vertex.IsVisited = true;
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

        private class MinHeap
        {
            private List<Vertex> Queue { get; set; }

            public MinHeap()
            {
                Queue = new List<Vertex>();
            }
            private int GetLeftChildIndex(int parentIndex)
            {
                return 2 * parentIndex + 1;
            }

            private int GetRightChildIndex(int parentIndex)
            {
                return 2 * parentIndex + 2;
            }

            private int GetParentIndex(int childIndex)
            {
                return (childIndex - 1) / 2;
            }

            private bool HasLeftChild(int index)
            {
                return GetLeftChildIndex(index) < Queue.Count;
            }

            private bool HasRightChild(int index)
            {
                return GetRightChildIndex(index) < Queue.Count;
            }

            private bool HasParent(int index)
            {
                return GetParentIndex(index) >= 0;
            }


            public void Push(Vertex node)
            {
                Queue.Add(node);
                int index = Queue.Count - 1; //last position                                
                while (index > 0 && Queue[index].Distance < Queue[(index - 1) / 2].Distance) //Child searching for its parent.
                {
                    Vertex auxilaryVertex = Queue[index];
                    Queue[index] = Queue[(index - 1) / 2];
                    Queue[(index - 1) / 2] = auxilaryVertex;
                    index = (index - 1) / 2;
                }
            }

            public Vertex removeMinHeap()
            {
                Vertex vertex = Queue[0];
                Queue[0] = Queue[Queue.Count - 1];
                Queue.RemoveAt(Queue.Count - 1);
                int index = 0;
                while (HasLeftChild(index))
                {
                    if (GetLeftChildIndex(index) < Queue.Count && Queue[index].Distance > Queue[GetLeftChildIndex(index)].Distance) //Check if left child > parent
                    {
                        Vertex auxilaryVertex = Queue[index];
                        Queue[index] = Queue[GetLeftChildIndex(index)];
                        Queue[GetLeftChildIndex(index)] = auxilaryVertex;
                    }
                    if (GetRightChildIndex(index) < Queue.Count && Queue[index].Distance > Queue[GetRightChildIndex(index)].Distance) //Check if right child is > parent
                    {
                        Vertex auxilaryVertex = Queue[index];
                        Queue[index] = Queue[GetRightChildIndex(index)];
                        Queue[GetRightChildIndex(index)] = auxilaryVertex;
                    }
                    index++;
                }
                return vertex;
            }

            public int Count()
            {
                return Queue.Count;
            }
        }
    }
}
