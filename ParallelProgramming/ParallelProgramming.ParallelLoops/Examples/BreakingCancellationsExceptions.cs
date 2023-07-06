namespace ParallelProgramming.ParallelLoops.Examples.BreakingCancellationsExceptions;

public class BreakingCancellationsExceptions
{
    public static void Start(string[] args)
    {
        try
        {
            Demo();

        }
        catch (AggregateException ae)
        {
            ae.Handle(e =>
            {
                Console.WriteLine(e.Message);
                return true;
            });
        }
        catch (OperationCanceledException oce)
        {
            Console.WriteLine(oce.Message);
        }

    }

    private static void Demo()
    {
        var cts = new CancellationTokenSource();
        var po = new ParallelOptions()
        {
            CancellationToken = cts.Token
        };
        var result = Parallel.For(0, 20, po, (int i, ParallelLoopState state) =>
        {
            Console.WriteLine($"{i} - [{Task.CurrentId}]");

            if (i == 10)
            {
                // state.Stop();
                // state.Break();
                // throw new Exception();
                
                cts.Cancel();
            }
        });
        
        Console.WriteLine($"\n Was loop completed? {result.IsCompleted}");
        if(result.LowestBreakIteration.HasValue)
            Console.WriteLine($"Lowest break iteration is {result.LowestBreakIteration}");
    }
}