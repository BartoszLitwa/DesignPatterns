using System.Collections.Concurrent;

namespace ParallelProgramming.ConcurrentCollections.Examples.BLockingCollectionAndProducerConsumerPattern
{
    public class BLockingCollectionAndProducerConsumerPattern
    {
        static BlockingCollection<int> messages = new(new ConcurrentBag<int>(), 10);

        static CancellationTokenSource cts = new();

        static Random random = new();

        public static void Start(string[] args)
        {
            Task.Factory.StartNew(ProducerAndConsumer, cts.Token);

            Thread.Sleep(5000);
            cts.Cancel();
        }

        private static void ProducerAndConsumer()
        {
            var producer = Task.Factory.StartNew(RunProducer);
            var consumer = Task.Factory.StartNew(RunConsumer);

            try
            {
                Task.WaitAll(new[] { producer, consumer }, cts.Token);
            }
            catch(AggregateException ae)
            {
                ae.Handle(e => true);
            }
        }

        private static void RunProducer()
        {
            while (true)
            {
                cts.Token.ThrowIfCancellationRequested();
                int i = random.Next(100);
                messages.Add(i);
                Console.WriteLine($"+{i}\t");
                Thread.Sleep(random.Next(100));
            }
        }

        private static void RunConsumer()
        {
            foreach(var item in messages.GetConsumingEnumerable())
            {
                cts.Token.ThrowIfCancellationRequested();
                Console.WriteLine($"-{item}\t");
                Thread.Sleep(random.Next(1000));
            }
        }
    }
}
