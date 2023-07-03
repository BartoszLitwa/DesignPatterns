namespace ParallelProgramming.TaskCoordination.Examples.ManualResetEventSlimAndAutoResetEvent;

public class ManualResetEventSlimAndAutoResetEvent
{
    public static void Start(string[] args)
    {
        ManulResetEventSlimExample();

        AutoResetEventExample();
    }

    private static void ManulResetEventSlimExample()
    {
        Console.WriteLine(nameof(ManulResetEventSlimExample));
        var evt = new ManualResetEventSlim();

        Task.Factory.StartNew(() =>
        {
            Console.WriteLine("Boiling water");
            evt.Set(); // Set the signal
        });

        var makeTea = Task.Factory.StartNew(() =>
        {
            Console.WriteLine("Waiting for water...");
            evt.Wait();
            Console.WriteLine("Here is your tea");
            var ok = evt.Wait(500);
            
            Console.WriteLine(ok ? "Enjoy your tea" : "No tea for you");
        });

        makeTea.Wait();
    }
    
    private static void AutoResetEventExample()
    {
        Console.WriteLine(nameof(AutoResetEventExample));
        var evt = new AutoResetEvent(false);

        Task.Factory.StartNew(() =>
        {
            Console.WriteLine("Boiling water");
            evt.Set(); // true
        });

        var makeTea = Task.Factory.StartNew(() =>
        {
            Console.WriteLine("Waiting for water...");
            evt.WaitOne(); // false -> false
            Console.WriteLine("Here is your tea");
            // We are missing the second evt.Set to successfully unblock it
            var ok = evt.WaitOne(500); // false
            
            Console.WriteLine(ok ? "Enjoy your tea" : "No tea for you");
        });

        makeTea.Wait();
    }
}