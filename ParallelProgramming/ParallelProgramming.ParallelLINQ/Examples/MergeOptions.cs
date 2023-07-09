namespace ParallelProgramming.ParallelLINQ.Examples.MergeOptions;

public class MergeOptions
{
    public static void Start(string[] args)
    {
        var numbers = Enumerable.Range(1, 20).ToArray();

        var results = numbers.AsParallel()
            .WithMergeOptions(ParallelMergeOptions.NotBuffered)
            // .WithMergeOptions(ParallelMergeOptions.FullyBuffered)
            .Select(x =>
            {
                var result = Math.Log10(x);
                Console.WriteLine($"Produced {result}");
                return result;
            });

        foreach (var i in results)
        {
            Console.WriteLine($"Consumed {i}");
        }
    }
}