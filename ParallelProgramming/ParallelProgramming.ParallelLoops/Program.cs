using ParallelProgramming.ParallelLoops.Examples.BreakingCancellationsExceptions;
using ParallelProgramming.ParallelLoops.Examples.ParallelInvokeForForEach;
using ParallelProgramming.ParallelLoops.Examples.ThreadLocalStorage;

Console.WriteLine($"\n{nameof(ParallelInvokeForForEach)}\n");
ParallelInvokeForForEach.Start(args);

Console.WriteLine($"\n{nameof(BreakingCancellationsExceptions)}\n");
BreakingCancellationsExceptions.Start(args);

Console.WriteLine($"\n{nameof(ThreadLocalStorage)}\n");
ThreadLocalStorage.Start(args);