using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelProgramming.ConcurrentCollections.Examples
{
    public class ConcurrentQueueExample
    {
        public static void Start(string[] args)
        {
            var q = new ConcurrentQueue<int>();
            q.Enqueue(1);
            q.Enqueue(2);

            int result;
            if(q.TryDequeue(out result))
            {
                Console.WriteLine($"Removed element {result}");
            }

            if(q.TryPeek(out result))
            {
                Console.WriteLine($"Front element is {result}");
            }
        }
    }
}
