using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubEDA2
{
    public class Edge
    {
        public Node From { get; private set; }
        public Node To { get; private set; }
        public double Cost { get; set; }
        public int Info { get; set; }

        public Edge(Node from, Node to) : this(from, to, 0)
        {
        }

        public Edge(Node from, Node to, int info)
        {
            this.From = from;
            this.To = to;
            this.Info = info;
        }

        public override string ToString()
        {
            return String.Format("{0} =({1:F4})=> {2}", this.From, this.Cost, this.To);
        }
    }
}
