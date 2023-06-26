namespace ParallelProgramming.DataSharingAndSynchronization.Examples
{
    public class SharedMutex
    {
        public static void Start(string[] args)
        {
            const string appName = "MyApp";
            Mutex mutex;

            try
            {
                mutex = Mutex.OpenExisting(appName);
                Console.WriteLine($"Sorry, {appName} is already running");
            }
            catch (WaitHandleCannotBeOpenedException)
            {
                Console.WriteLine("We can run the program just fine");
                mutex = new Mutex(false, appName);

                mutex.ReleaseMutex();
            }
        }

    }
}
