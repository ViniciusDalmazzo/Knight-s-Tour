using System;
using System.Collections.Generic;

namespace SubEDA2
{
    public class Node
    {
        public string Name { get; set; }
        public object Info { get; set; }
        public List<Edge> Edges { get; private set; }
        public int Nivel { get; set; }
        public double Value { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool Visited {get;set;}
        public int Path { get; set; }
        public Node Parent { get; set; }

        public Node()
        {
            this.Edges = new List<Edge>();
        }

        public Node(string name)
        {
            this.Name = name;
        }

        public Node(string name, object info, int nivel) : this()
        {
            this.Name = name;
            this.Info = info;
            this.Nivel = nivel;
        }

        public Node(string name, object info, int nivel, double value, int path) : this()
        {
            this.Name = name;
            this.Info = info;
            this.Nivel = nivel;
            this.Value = value;
            this.Path = path;
        }

        public void AddEdge(Node to)
        {
            AddEdge(to, 0);
        }

        public void AddEdge(Node to, int info)
        {
            this.Edges.Add(new Edge(this, to, info));
        }

        public override string ToString()
        {
            if (this.Info != null)
            {
                return String.Format("{0}({1})", this.Name, this.Info);
            }
            return this.Name.ToString();
        }

    }
}
