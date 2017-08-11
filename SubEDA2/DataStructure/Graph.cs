using System;
using System.Collections.Generic;
using System.Linq;

namespace SubEDA2
{
    public class Graph
    {
        public List<Node> nodes;

        public Graph()
        {
            this.nodes = new List<Node>();
        }

        public Node[] Nodes
        {
            get { return this.nodes.ToArray(); }
        }

        public Node Find(string name)
        {
            return this.nodes.SingleOrDefault(e => e.Name == name);
        }

        public void AddNode(string name)
        {
            AddNode(name, null);
        }

        public void AddNode(string name, object info)
        {
            if (Find(name) != null)
            {
                throw new Exception("Um nó com o mesmo nome já foi adicionado a este grafo.");
            }
            this.nodes.Add(new Node(name, info, 0));
        }

        public void AddNode(string name, object info, double value)
        {
            if (Find(name) != null)
            {
                throw new Exception("Um nó com o mesmo nome já foi adicionado a este grafo.");
            }
            this.nodes.Add(new Node(name, info, 0, value, 1));
        }

        public void RemoveNode(string name)
        {
            Node existingNode = Find(name);
            if (existingNode == null)
            {
                throw new Exception("Não foi possível encontrar o nó a ser removido.");
            }
            this.nodes.Remove(existingNode);
        }

        public void AddEdge(string from, string to, int info)
        {
            Node start = Find(from);
            Node end = Find(to);

            if (start == null)
            {
                throw new Exception("Não foi possível encontrar o nó origem no grafo.");
            }
            if (end == null)
            {
                throw new Exception("Não foi possível encontrar o nó origem no grafo.");
            }
            start.AddEdge(end, info);
        }
        
        public Node[] GetNeighbours(string from)
        {
            Node node = Find(from);

            if (node == null)
            {
                throw new Exception("Não foi possível encontrar o nó origem no grafo.");
            }
            return node.Edges.Select(e => e.To).ToArray();
        }
        
        public bool IsValidPath(ref Node[] nodes, params string[] path)
        {
            return false;
        }

        //public bool Hamiltonian()
        //{
        //    foreach (Node n in this.nodes)
        //    {
        //        bool ret = this.Hamiltonian(n);
        //        if (ret) return true;
        //    }
        //    return false;
        //}
        
        //private bool Hamiltonian(Node n)
        //{
        //    Queue<Node> queue = new Queue<Node>();
        //    Graph arvore = new Graph();
        //    int id = 0;
        //    id++;
        //    arvore.AddNode(id.ToString(), n.Name);
        //    queue.Enqueue(arvore.Find(id.ToString()));
            
        //    while (queue.Count > 0)
        //    {
        //        Node np = queue.Dequeue();
        //        Node currentNode = this.Find(np.Info.ToString());
        //        if (this.nodes.Count == CountNodes(np))
        //            return true;

        //        foreach (Edge edge in currentNode.Edges)
        //        {
        //            if (!ExistNode(np, edge.To.Name))
        //            {
        //                id++;
        //                arvore.AddNode(id.ToString(), edge.To.Name);
        //                Node nf = arvore.Find(id.ToString());
        //                queue.Enqueue(nf);
        //                arvore.AddEdge(nf.Name, np.Name, 1);
        //            }
        //        }
        //    }

        //    return false;
        //}

        //private bool ExistNode(Node np, string p)
        //{
        //    if (np == null) return false;
        //    while (np.Edges.Count > 0)
        //    {
        //        if (np.Info.ToString() == p) return true;
        //        np = np.Edges[0].To;
        //    }
        //    return np.Info.ToString() == p;
        //}

        //private int CountNodes(Node np)
        //{
        //    if (np == null) return 0;
        //    int count = 1;
        //    while (np.Edges.Count > 0)
        //    { count++; np = np.Edges[0].To; }
        //    return count;
        //}
    }
}
