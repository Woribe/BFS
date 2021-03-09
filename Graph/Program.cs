using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    class Program
    {
        static void Main(string[] args)
        {
            //Instance of our graph
            MyGraph<string> myGraph = new MyGraph<string>();

            //Create the graph
            CreateGraph(ref myGraph);

            List<Node<string>> fastestPath = ShortestPath("IDAHO", "FLORIDA", myGraph);
            Console.WriteLine("Final path:");
            foreach (var item in fastestPath)
	        {
                Console.WriteLine(item.Data);
	        }

            Console.ReadKey();
        }

        private static  List<Node<string>> ShortestPath(string from, string to, MyGraph<string> myGraph) {

            Node<string> lookingFrom = myGraph.NodeSet.Find(x => x.Data.Equals(from));
            Node<string> lookingAt = lookingFrom;
            Node<string> lookingFor = myGraph.NodeSet.Find(x => x.Data.Equals(to));

            lookingAt.Visited = true;

            List<Node<string>> path = new List<Node<string>>();
            bool pathFound = false;

            Queue<Node<string>> checkQueue = new Queue<Node<string>>();


            //Løber alle nodes igennem og sætter deres parent til noden de kom fra.
            foreach (var state in myGraph.NodeSet) 
	        {
                foreach (var edge in lookingAt.MyEdges)
	            {                    
                    if(!checkQueue.Contains(edge.To) && !edge.To.Visited ){
                        edge.To.Parent = lookingAt;
                        checkQueue.Enqueue(edge.To);
                    }
	            }
                // Når alle edges på en node er løbet igennem, kigger man på den første i køen.
                if(checkQueue.Count > 0) {
                    lookingAt = checkQueue.Dequeue();
                    lookingAt.Visited = true;
                }
                // Hvis den man kigger på er den man leder efter har man fundet en path.
                if(lookingAt.Equals(lookingFor)) {
                    pathFound = true;
                }
	        }

            //Hvis der er fundet en path, skrives routen ned i en List.
            if(pathFound) {
                lookingAt = lookingFor;
                path.Add(lookingFor);
                while (!lookingAt.Equals(lookingFrom))
	            {
                    path.Add(lookingAt.Parent);
                    lookingAt = lookingAt.Parent;
	            }
                path.Reverse();
            }
            return path;
        }


        private static void CreateGraph(ref MyGraph<string> myGraph)
        {
            //Adds nodes to our graph
            Node<String> n1 = myGraph.AddNode("OREGON");
            Node<String> n2 = myGraph.AddNode("CALIFORNIA");
            Node<String> n3 = myGraph.AddNode("IDAHO");
            Node<String> n4 = myGraph.AddNode("UTAH");
            Node<String> n5 = myGraph.AddNode("NEW MEXICO");
            Node<String> n6 = myGraph.AddNode("KANSAS");
            Node<String> n7 = myGraph.AddNode("SOUTH DAKOTA");
            Node<String> n8 = myGraph.AddNode("NORTH DAKOTA");
            Node<String> n9 = myGraph.AddNode("IOWA");
            Node<String> n10 = myGraph.AddNode("TENNESSEE");
            Node<String> n11 = myGraph.AddNode("NEW YORK");
            Node<String> n12 = myGraph.AddNode("FLORIDA");
            Node<String> n13 = myGraph.AddNode("TEXAS");

            //Creates edges between the graphs nodes
            myGraph.AddEdge("OREGON", "CALIFORNIA");
            myGraph.AddEdge("CALIFORNIA", "UTAH");
            myGraph.AddEdge("UTAH", "IDAHO");
            myGraph.AddEdge("UTAH", "NEW MEXICO");
            myGraph.AddEdge("NEW MEXICO", "KANSAS");
            myGraph.AddEdge("NEW MEXICO", "TEXAS");
            myGraph.AddEdge("TEXAS", "TENNESSEE");
            myGraph.AddEdge("TEXAS", "FLORIDA");
            myGraph.AddEdge("TEXAS", "KANSAS");
            myGraph.AddEdge("KANSAS", "SOUTH DAKOTA");
            myGraph.AddEdge("SOUTH DAKOTA", "NORTH DAKOTA");
            myGraph.AddEdge("NORTH DAKOTA", "IOWA");
            myGraph.AddEdge("IOWA", "TENNESSEE");
            myGraph.AddEdge("TENNESSEE", "FLORIDA");
            myGraph.AddEdge("TENNESSEE", "NEW YORK");
        }
    }
}
