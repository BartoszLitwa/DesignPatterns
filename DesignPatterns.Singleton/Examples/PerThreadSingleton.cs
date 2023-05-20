using System;
using System.Linq;

namespace DesignPatterns.Singleton.Examples
{
    public sealed class PerThreadSingleton // Only 1 instance per 1 thread
    {
        private static ThreadLocal<PerThreadSingleton> threadInstance
            => new ThreadLocal<PerThreadSingleton>(() => new PerThreadSingleton());

        public static PerThreadSingleton Instance => threadInstance.Value!;

        public int Id;

        private PerThreadSingleton()
        {
            Id = Thread.CurrentThread.ManagedThreadId;
        }

        public static void Start(string[] args)
        {
            var t1 = Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"Thread 1: " + PerThreadSingleton.Instance.Id);
            });

            var t2 = Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"Thread 2: " + PerThreadSingleton.Instance.Id);
                Console.WriteLine($"Thread 2: " + PerThreadSingleton.Instance.Id);
            });

            Task.WaitAll(t1, t2);
        }
    }
}
