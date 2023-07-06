using System.Collections.Concurrent;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace ParallelProgramming.ParallelLoops.Examples.Partitioning;

public class Partitioning
{
    const int count = 100_000;
    
    [Benchmark]
    public void SquareEachValue()
    {
        var values = Enumerable.Range(0, count);
        var results = new int[count];
        Parallel.ForEach(values, x => { results[x] = (int)Math.Pow(x, 2); });
    }
    
    [Benchmark]
    public void SquareEachValueChunked() // 5 times faster
    {
        var values = Enumerable.Range(0, count);
        var results = new int[count];

        var partitioner = Partitioner.Create(0, count, 10_000);
        Parallel.ForEach(partitioner, range =>
        {
            for (int i = range.Item1; i < range.Item2; i++)
            {
                results[i] = (int)Math.Pow(i, 2);
            }
        });
    }
    
    public static void Start(string[] args)
    {
        var summary = BenchmarkRunner.Run<Partitioning>();
        Console.WriteLine(summary);
    }
}