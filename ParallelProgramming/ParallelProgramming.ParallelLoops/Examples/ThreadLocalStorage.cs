namespace ParallelProgramming.ParallelLoops.Examples.ThreadLocalStorage;

public class ThreadLocalStorage
{
    public static void Start(string[] args)
    {
        InterlockedVersion();

        int sum = 0;

        Parallel.For(1, 1001,
            () => 0, // Thread local storage initialization
            (x, state, tls) =>
            {
                tls += x;
                Console.WriteLine($"Task {Task.CurrentId} has sum {tls}");
                return tls;
            },
            parttalSum => // Sum the calculated partial values
            {
                Console.WriteLine($"Partial value of task {Task.CurrentId} is {parttalSum}");
                Interlocked.Add(ref sum, parttalSum);
            });

        Console.WriteLine($"Sum of 1..1000 = {sum}");
    }

    private static void InterlockedVersion()
    {
        int sum = 0;
        Parallel.For(1, 1001, x => { Interlocked.Add(ref sum, x); });
    }
}