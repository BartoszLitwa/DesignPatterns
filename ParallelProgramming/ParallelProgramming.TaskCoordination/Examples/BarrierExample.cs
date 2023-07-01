namespace ParallelProgramming.TaskCoordination.Examples.BarrierExample
{
    public class BarrierExample
    {
        static Barrier barrier = new Barrier(2, b =>
        {
            Console.WriteLine($"Phase {b.CurrentPhaseNumber} is finished");
        });

        public static void Water()
        {
            Console.WriteLine("Putting the kettle on (takes a bit longer)");
            Thread.Sleep(1000);
            // signal -> wait
            barrier.SignalAndWait(); // 2
            Console.WriteLine("Pouring water into cup."); // 0
            barrier.SignalAndWait(); // 1
            Console.WriteLine("Putting the kettle away");
        }

        public static void Cup()
        {
            Console.WriteLine("Finding the nicest a cup of tea (fast)");
            barrier.SignalAndWait(); // 1
            Console.WriteLine("Adding tea."); // 0
            barrier.SignalAndWait(); // 2
            Console.WriteLine("Adding sugar");
        }

        public static void Start(string[] args)
        {
            var water = Task.Factory.StartNew(Water);
            var cup = Task.Factory.StartNew(Cup);

            var tea = Task.Factory.ContinueWhenAll(new[] { water, cup }, tasks =>
            {
                Console.WriteLine("Enjoy your cup of tea.");
            });
            tea.Wait();
        }
    }
}
