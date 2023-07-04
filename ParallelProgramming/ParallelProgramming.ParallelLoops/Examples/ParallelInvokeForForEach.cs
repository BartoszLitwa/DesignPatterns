namespace ParallelProgramming.ParallelLoops.Examples.ParallelInvokeForForEach;

public class ParallelInvokeForForEach
{
    public static IEnumerable<int> CustomRange(int start, int end, int step)
    {
        for (int i = start; i < end; i += step)
        {
            yield return i;
        }
    }
    
    public static void Start(string[] args)
    {
        Invoke();
        For();
        ForEach();

        var po = new ParallelOptions();
        Parallel.ForEach(CustomRange(1, 20, 3), po, Console.WriteLine);
    }

    private static void ForEach()
    {
        string[] words = { "oh", "what", "a", "day" };
        Parallel.ForEach(words, word => { Console.WriteLine($"{word} has length {word.Length} - {Task.CurrentId}"); });
    }

    private static void For()
    {
        Parallel.For(1, 11, i => { Console.WriteLine($"{i * i}\t"); });
    }

    private static void Invoke()
    {
        var a = new Action(() => Console.WriteLine($"First {Task.CurrentId}"));
        var b = new Action(() => Console.WriteLine($"Second {Task.CurrentId}"));
        var c = new Action(() => Console.WriteLine($"Third {Task.CurrentId}"));
        Parallel.Invoke(a, b, c);
    }
}