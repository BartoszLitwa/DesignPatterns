using ParallelProgramming.ParallelLINQ.Examples.AsParallelAndParallelQuery;
using ParallelProgramming.ParallelLINQ.Examples.CancellationAndExceptions;

Console.WriteLine($"\n{nameof(AsParallelAndParallelQuery)}\n");
AsParallelAndParallelQuery.Start(args);

Console.WriteLine($"\n{nameof(CancellationAndExceptions)}\n");
CancellationAndExceptions.Start(args);