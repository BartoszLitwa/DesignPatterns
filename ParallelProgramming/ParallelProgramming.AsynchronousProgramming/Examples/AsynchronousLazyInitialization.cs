using Nito.AsyncEx;

namespace ParallelProgramming.AsynchronousProgramming.Examples.AsynchronousLazyInitialization
{
    public class Stuff
    {
        private static int value;

        // Thread pool
        private readonly Lazy<Task<int>> AutoIncValue =
            new Lazy<Task<int>>(async () => {
                await Task.Delay(1000).ConfigureAwait(false);
                return value++;
            });

        // UI
        private readonly Lazy<Task<int>> AutoIncValue2 =
            new Lazy<Task<int>>(() => Task.Run(async () => {
                await Task.Delay(1000);
                return value++;
            }));

        // Nito.AsyncEx
        public AsyncLazy<int> AutoIncValue3 =
            new AsyncLazy<int>(async () => {
                await Task.Delay(1000);
                return value++;
            });

        public async Task UseValue()
        {
            int value = await AutoIncValue.Value;
        }
    }

    public class AsynchronousLazyInitialization
    {
        public static async Task Start(string[] args)
        {

        }
    }
}
