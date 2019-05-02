using System;
using System.Diagnostics;

namespace ShortestPath
{
    class Program
    {
        static void Main(string[] args)
        {
            string result = "";

            //IShortestPath dijkstra = new AStar();
            //Graph graph = CreateGraph();
            //result = dijkstra.Execute(graph, "G", "A");
            //Console.WriteLine($"A*:{result}");

            //dijkstra = new Dijkstra();
            //graph = CreateGraph();
            //result = dijkstra.Execute(graph, "G", "A");
            //Console.WriteLine($"Dijkstra: {result}");

            //------------------------TIMINGS------------------------------------------------

            decimal dijkstraAverage, aStarAverage = 0;
            int maxI = 1000;
            Stopwatch stopWatch = new Stopwatch();

            IShortestPath dijkstra = new Dijkstra();
            for (int i = 0; i < maxI; i++)
            {
                Graph graph = CreateGraph();
                stopWatch.Start();
                result = dijkstra.Execute(graph, "A", "G");
                //Console.WriteLine($"Dijkstra: {result}");
                stopWatch.Stop();

            }
            dijkstraAverage = stopWatch.ElapsedTicks / maxI;
            Console.WriteLine($"Dijkstra average: {dijkstraAverage}");

            stopWatch.Reset();

            //IShortestPath dijkstraWithMinHeap = new DijkstrWithMinHeap();
            //for (int i = 0; i < maxI; i++)
            //{
            //    Graph graph = CreateGraph();
            //    stopWatch.Start();
            //    result = dijkstraWithMinHeap.Execute(graph, "A", "G");
            //    //Console.WriteLine($"Dijkstra with min heap: {result}");
            //    stopWatch.Stop();
            //}
            //Console.WriteLine($"Dijkstra w/ MinHeapr average: {stopWatch.ElapsedTicks / maxI}");

            //stopWatch.Reset();

            IShortestPath AStar = new Dijkstra();
            for (int i = 0; i < maxI; i++)
            {
                Graph graph = CreateGraph();
                stopWatch.Start();
                result = AStar.Execute(graph, "A", "G");
                //Console.WriteLine($"A Star: {result}");
                stopWatch.Stop();
            }
            aStarAverage = stopWatch.ElapsedTicks / maxI;
            Console.WriteLine($"A* average: {aStarAverage}");

            Console.WriteLine($"Percentage difference: {100-(aStarAverage / dijkstraAverage) * 100}");
            Console.ReadKey();
        }

        static Graph CreateGraph()
        {
            Graph graph = new Graph();

            graph.AddVertex("A", 1, 9);
            graph.AddVertex("B", 9, 9);
            graph.AddVertex("C", 1, 1);
            graph.AddVertex("D", 9, 1);
            graph.AddVertex("E", 4, 7);
            graph.AddVertex("F", 3, 6);
            graph.AddVertex("G", 7 ,6);
            graph.AddVertex("H", 3, 4);
            graph.AddVertex("I", 10, 1);//7,4      10, 0 increase for A* star
            graph.AddVertex("J", 6, 3);

            graph.AddEdge("A", "E", 2);
            graph.AddEdge("A", "F", 1);
            graph.AddEdge("A", "C", 4);
            graph.AddEdge("B", "D", 4);
            graph.AddEdge("B", "G", 1);
            graph.AddEdge("C", "A", 4);
            graph.AddEdge("C", "D", 6);
            graph.AddEdge("C", "H", 2);
            graph.AddEdge("D", "C", 6);
            graph.AddEdge("D", "B", 4);
            graph.AddEdge("D", "J", 2);
            graph.AddEdge("D", "I", 1);
            graph.AddEdge("E", "A", 2);
            graph.AddEdge("E", "I", 3);
            graph.AddEdge("F", "A", 1);
            graph.AddEdge("F", "I", 2);
            graph.AddEdge("F", "J", 3);
            graph.AddEdge("G", "B", 1);
            graph.AddEdge("H", "C", 2);
            graph.AddEdge("H", "I", 4);
            graph.AddEdge("I", "E", 3);
            graph.AddEdge("I", "F", 2);
            graph.AddEdge("I", "D", 1);
            graph.AddEdge("I", "H", 4);
            graph.AddEdge("J", "D", 2);
            graph.AddEdge("J", "F", 3);

            return graph;
        }
    }
}
