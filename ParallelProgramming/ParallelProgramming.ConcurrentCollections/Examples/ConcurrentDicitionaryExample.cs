using System.Collections.Concurrent;

namespace ParallelProgramming.ConcurrentCollections.Examples.ConcurrentDicitionaryExample
{
    public class ConcurrentDicitionaryExample
    {
        private static ConcurrentDictionary<string, string> capitals = new();

        public static void AddParis()
        {
            var success = capitals.TryAdd("France", "Paris");
            string who = Task.CurrentId.HasValue ? ("Task" + Task.CurrentId) : "Main thread";
            Console.WriteLine($"{who} {(success ? "added" : "did not add")} the element");
        }

        public static void Start(string[] args)
        {
            Task.Factory.StartNew(AddParis).Wait();
            AddParis();

            capitals["Poland"] = "Cracow";

            capitals.AddOrUpdate("Poland", "Warsaw", (key, oldValue) => oldValue + " -> Warsaw");
            Console.WriteLine($"Capital of Poland is: {capitals["Poland"]}");

            capitals["Sweden"] = "Uppsala";
            var capOfSweden = capitals.GetOrAdd("Sweden", "Stockholm");
            Console.WriteLine($"Capital of Sweden is: {capitals["Sweden"]}");

            const string toRemove = "Poland";
            var firstRemove = true;
            while (firstRemove)
            {
                var didRemove = capitals.TryRemove(toRemove, out var removed);
                if (didRemove)
                {
                    Console.WriteLine($"We just removed {removed}");
                }
                else
                {
                    Console.WriteLine($"Failed to remove the capital of {toRemove}");
                    firstRemove = false;
                }
            }

            var count = capitals.Count; // Expensive for concurrent collections
            foreach(var kv in capitals)
            {
                Console.WriteLine($"- {kv.Value} is the capital of {kv.Key}");
            }
        }
    }
}
