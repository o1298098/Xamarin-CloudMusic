using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMusic.Actions
{
    public class PriorityQueue<T>
    {
        List<T> queue = new List<T>();
        IComparer<T> comparer;

        public PriorityQueue(IComparer<T> comparer)
        {
            this.comparer = comparer;
        }

        public void Push(T v)
        {
            int i = queue.BinarySearch(v, comparer);
            queue.Insert((i < 0) ? ~i : i, v);
        }

        public T Pop()
        {
            T v = queue[queue.Count - 1];
            queue.RemoveAt(queue.Count - 1);
            return v;
        }
        public List<T> ToArray()
        {
            return queue;
        }
    }
}
