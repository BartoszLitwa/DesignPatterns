﻿using ParallelProgramming.TaskCoordination.Examples.BarrierExample;
using ParallelProgramming.TaskCoordination.Examples.ChildTasks;
using ParallelProgramming.TaskCoordination.Examples.Continuations;
using ParallelProgramming.TaskCoordination.Examples.CountdownEventExample;
using ParallelProgramming.TaskCoordination.Examples.ManualResetEventSlimAndAutoResetEvent;

Console.WriteLine($"\n{nameof(Continuations)}\n");
Continuations.Start(args);

Console.WriteLine($"\n{nameof(ChildTasks)}\n");
ChildTasks.Start(args);

Console.WriteLine($"\n{nameof(BarrierExample)}\n");
BarrierExample.Start(args);

Console.WriteLine($"\n{nameof(CountdownEventExample)}\n");
CountdownEventExample.Start(args);

Console.WriteLine($"\n{nameof(ManualResetEventSlimAndAutoResetEvent)}\n");
ManualResetEventSlimAndAutoResetEvent.Start(args);