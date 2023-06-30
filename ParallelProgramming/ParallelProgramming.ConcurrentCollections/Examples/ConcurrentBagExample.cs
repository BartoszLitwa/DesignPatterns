using System.Collections.Concurrent;

namespace ParallelProgramming.ConcurrentCollections.Examples
{
    public class ConcurrentBagExample
    {
        public static void Start(string[] args)
        {
            // No ordering
            var bag = new ConcurrentBag<int>();
            var tasks = new List<Task>();
            for(int i = 0; i < 10; i++)
            {
                var i1 = i;
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    bag.Add(i1);
                    Console.WriteLine($"{Task.CurrentId} has added {i1}");
                    int reuslt;
                    if(bag.TryPeek(out reuslt))
                    {
                        Console.WriteLine($"{Task.CurrentId} has peeked the value {reuslt}");
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());

            int last;
            if(bag.TryTake(out last))
            {
                Console.WriteLine($"I got last {last}");
            }
        }
    }
}
