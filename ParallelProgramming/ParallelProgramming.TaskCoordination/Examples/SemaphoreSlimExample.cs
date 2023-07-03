namespace ParallelProgramming.TaskCoordination.Examples.SemaphoreSlimExample;

public class SemaphoreSlimExample
{
    public static void Start(string[] args)
    {
        var semaphore = new SemaphoreSlim(2, 10);
        for (int i = 0; i < 20; i++)
        {
            Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"Entering Task {Task.CurrentId}");
                semaphore.Wait(); // ReleaseCount--
                Console.WriteLine($"Processing Task {Task.CurrentId}");
            });
        }

        while (semaphore.CurrentCount <= 2)
        {
            Console.WriteLine($"Semaphore count: {semaphore.CurrentCount}");
            Thread.Sleep(200);
            semaphore.Release(2); // ReleaseCount += 2
        }
    }
}