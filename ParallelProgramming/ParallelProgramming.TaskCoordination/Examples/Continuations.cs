namespace ParallelProgramming.TaskCoordination.Examples.Continuations
{
    public class Continuations
    {
        public static void Start(string[] args)
        {
            Example1();

            Example2();

            Example3();
        }

        private static void Example2()
        {
            var task = Task.Factory.StartNew(() => "Task 1");
            var task2 = Task.Factory.StartNew(() => "Task 2");

            var task3 = Task.Factory.ContinueWhenAll(new[] { task, task2 },
                tasks =>
                {
                    Console.WriteLine("Tasks completed: ");
                    foreach (var t in tasks)
                        Console.WriteLine($" - {t.Result}");
                    Console.WriteLine("All tasks done");
                });
        }

        private static void Example3()
        {
            var task = Task.Factory.StartNew(() => "Task 1");
            var task2 = Task.Factory.StartNew(() => "Task 2");

            var task3 = Task.Factory.ContinueWhenAny(new[] { task, task2 },
                task =>
                {
                    Console.WriteLine("Tasks completed: ");
                    Console.WriteLine($" - {task.Result}");
                });
        }

        private static void Example1()
        {
            var task = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Boiling water");
            });

            var task2 = task.ContinueWith(t =>
            {
                Console.WriteLine($"Completed task {t.Id}, pour water into cup.");
            });

            task2.Wait();
        }
    }
}
