using ParallelProgramming.AsynchronousProgramming.Examples.AsyncFactoryMethod;
using ParallelProgramming.AsynchronousProgramming.Examples.AsynchronousInitializationPattern;
using ParallelProgramming.AsynchronousProgramming.Examples.AsynchronousLazyInitialization;
using ParallelProgramming.AsynchronousProgramming.Examples.BeyondTheElvisOperator;
using ParallelProgramming.AsynchronousProgramming.Examples.TaskExamples;

Console.WriteLine($"\n{nameof(TaskExamples)}\n");
TaskExamples.Start(args);

Console.WriteLine($"\n{nameof(AsyncFactoryMethod)}\n");
await AsyncFactoryMethod.Start(args);


Console.WriteLine($"\n{nameof(AsyncFactoryMethod)}\n");
await AsynchronousInitializationPattern.Start(args);

Console.WriteLine($"\n{nameof(AsynchronousLazyInitialization)}\n");
await AsynchronousLazyInitialization.Start(args);

Console.WriteLine($"\n{nameof(BeyondTheElvisOperator)}\n");
BeyondTheElvisOperator.Start(args);