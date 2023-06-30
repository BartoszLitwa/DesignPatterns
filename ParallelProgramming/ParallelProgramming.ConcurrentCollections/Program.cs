using ParallelProgramming.ConcurrentCollections.Examples;
using ParallelProgramming.ConcurrentCollections.Examples.BLockingCollectionAndProducerConsumerPattern;
using ParallelProgramming.ConcurrentCollections.Examples.ConcurrentDicitionaryExample;

Console.WriteLine($"{nameof(ConcurrentDicitionaryExample)}\n");
ConcurrentDicitionaryExample.Start(args);

Console.WriteLine($"{nameof(ConcurrentQueueExample)}\n");
ConcurrentQueueExample.Start(args);

Console.WriteLine($"{nameof(ConcurrentStackExample)}\n");
ConcurrentStackExample.Start(args);

Console.WriteLine($"{nameof(ConcurrentBagExample)}\n");
ConcurrentBagExample.Start(args);

Console.WriteLine($"{nameof(BLockingCollectionAndProducerConsumerPattern)}\n");
BLockingCollectionAndProducerConsumerPattern.Start(args);