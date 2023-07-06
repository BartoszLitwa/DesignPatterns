using ParallelProgramming.ParallelLoops.Examples.BreakingCancellationsExceptions;
using ParallelProgramming.ParallelLoops.Examples.ParallelInvokeForForEach;

Console.WriteLine($"\n{nameof(ParallelInvokeForForEach)}\n");
ParallelInvokeForForEach.Start(args);

Console.WriteLine($"\n{nameof(BreakingCancellationsExceptions)}\n");
BreakingCancellationsExceptions.Start(args);