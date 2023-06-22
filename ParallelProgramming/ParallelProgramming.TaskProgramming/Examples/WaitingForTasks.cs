namespace ParallelProgramming.TaskProgramming.Examples.WaitingForTasks
{
    public class WaitingForTasks
    {
        public static void Start(string[] args)
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;
            var t = new Task(() =>
            {
                Console.WriteLine("I take 5 seconds");
                for (int i = 0; i < 5; i++)
                {
                    token.ThrowIfCancellationRequested();
                    Thread.Sleep(1000);
                }
                Console.WriteLine("Done");
            }, token);
            t.Start();

            var t2 = Task.Factory.StartNew(() => Thread.Sleep(3000), token);

            Thread.Sleep(200);
            //cts.Cancel();

            Task.WaitAll(new[] { t, t2 }, 1000, token); // When cancelled throws an exception
            //Task.WaitAny(t, t2);

            Console.WriteLine($"Task t status is: {t.Status}");
            Console.WriteLine($"Task t2 status is: {t2.Status}");

            Console.WriteLine("WaitingForTasks Done");
        }
    }
}
