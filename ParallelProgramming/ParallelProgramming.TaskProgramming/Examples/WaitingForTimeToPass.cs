namespace ParallelProgramming.TaskProgramming.Examples.WaitingForTimeToPass
{
    public class WaitingForTimeToPass
    {
        public static void Start(string[] args)
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;
            var t = new Task(() =>
            {
                Console.WriteLine("Press any key to disar; you have 5 seconds.");
                bool cancelled = token.WaitHandle.WaitOne(5000);
                Console.WriteLine(cancelled ? "Disarm" : "Boom");
                //Thread.Sleep(1000);// In the meantime another task could use this resource
                //SpinWait.SpinUntil(1000); // Keeps the resource and avoid context-switching
            }, token);
            t.Start();

            Thread.Sleep(1000);
            cts.Cancel();

            Console.WriteLine("WaitingForTimeToPass Done");
        }
    }
}
