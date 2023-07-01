using ParallelProgramming.TaskCoordination.Examples.BarrierExample;
using ParallelProgramming.TaskCoordination.Examples.ChildTasks;
using ParallelProgramming.TaskCoordination.Examples.Continuations;

Console.WriteLine($"\n{nameof(Continuations)}\n");
Continuations.Start(args);

Console.WriteLine($"\n{nameof(ChildTasks)}\n");
ChildTasks.Start(args);

Console.WriteLine($"\n{nameof(BarrierExample)}\n");
BarrierExample.Start(args);