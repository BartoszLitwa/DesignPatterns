using ParallelProgramming.TaskCoordination.Examples.BarrierExample;
using ParallelProgramming.TaskCoordination.Examples.ChildTasks;
using ParallelProgramming.TaskCoordination.Examples.Continuations;

Console.WriteLine($"{nameof(Continuations)}\n");
Continuations.Start(args);

Console.WriteLine($"{nameof(ChildTasks)}\n");
ChildTasks.Start(args);

Console.WriteLine($"{nameof(BarrierExample)}\n");
BarrierExample.Start(args);