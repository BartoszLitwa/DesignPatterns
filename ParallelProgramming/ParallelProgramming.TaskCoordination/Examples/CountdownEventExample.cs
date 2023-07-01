namespace ParallelProgramming.TaskCoordination.Examples.CountdownEventExample
{
    public class CountdownEventExample
    {
        private static int taskCount = 5;
        static CountdownEvent cte = new CountdownEvent(taskCount);
        private static Random random = new Random();

        public static void Start(string[] args)
        {
            for(int i = 0; i < taskCount; i++)
            {
                var task = Task.Factory.StartNew(() =>
                {
                    Console.WriteLine($"Entering task {Task.CurrentId}");
                    Thread.Sleep(random.Next(300));
                    cte.Signal();
                    Console.WriteLine($"Exiting task {Task.CurrentId}");
                });
            }

            var finalTask = Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"Waiting for other tasks to complete in {Task.CurrentId}");
                cte.Wait(); // Blocking invokation
                Console.WriteLine("All tasks completed");
            });
            finalTask.Wait();
        }
    }
}
