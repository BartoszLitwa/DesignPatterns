using ParallelProgramming.ParallelLINQ.Examples.AsParallelAndParallelQuery;
using ParallelProgramming.ParallelLINQ.Examples.CancellationAndExceptions;
using ParallelProgramming.ParallelLINQ.Examples.CustomAggregation;
using ParallelProgramming.ParallelLINQ.Examples.MergeOptions;

Console.WriteLine($"\n{nameof(AsParallelAndParallelQuery)}\n");
AsParallelAndParallelQuery.Start(args);

Console.WriteLine($"\n{nameof(CancellationAndExceptions)}\n");
CancellationAndExceptions.Start(args);

Console.WriteLine($"\n{nameof(MergeOptions)}\n");
MergeOptions.Start(args);

Console.WriteLine($"\n{nameof(CustomAggregation)}\n");
CustomAggregation.Start(args);