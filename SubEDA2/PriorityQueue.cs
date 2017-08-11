using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SubEDA2
{
    public class PriorityQueue<P, V>
    {
        private SortedDictionary<P, Queue<V>> dictionary = new SortedDictionary<P, Queue<V>>();
        public int count { get; private set; }
        
        public void Insert(P priority, V value)
        {
            ++count;
            Queue<V> q;

            if (!dictionary.TryGetValue(priority, out q))
            {
                q = new Queue<V>();
                dictionary.Add(priority, q);
            }

            q.Enqueue(value);
        }

        public V Remove()
        {
            --count;
            var pair = dictionary.First();
            var v = pair.Value.Dequeue();

            if (pair.Value.Count == 0)
                dictionary.Remove(pair.Key);

            return v;
        }
    }
}
