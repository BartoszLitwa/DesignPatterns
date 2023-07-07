namespace ParallelProgramming.ParallelLINQ.Examples.AsParallelAndParallelQuery;

public class AsParallelAndParallelQuery
{
    public static void Start(string[] args)
    {
        const int count = 50;
        var items = Enumerable.Range(1, count).ToArray();
        var results = new int[count];
        
        items.AsParallel().ForAll(x =>
        {
            int newValue = x * x * x;
            Console.Write($"{newValue} ({Task.CurrentId})\t");
            results[x - 1] = newValue;
        });
        Console.WriteLine($"\n");

        foreach (var i in results)
            Console.Write($"{i}\t");
        Console.WriteLine("\n");

        var cubes = items.AsParallel().AsOrdered().Select(x => x * x * x);
        foreach (var i in cubes)
            Console.Write($"{i}\t");
        Console.WriteLine();
    }
}