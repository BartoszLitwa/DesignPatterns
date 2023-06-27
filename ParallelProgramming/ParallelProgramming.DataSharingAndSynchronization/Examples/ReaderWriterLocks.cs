namespace ParallelProgramming.DataSharingAndSynchronization.Examples.ReaderWriterLocks
{
    public class BankAccount
    {
        int balance;
        public int Balance { get => balance; private set => balance = value; }

        public void Deposit(int amount)
        {
            balance += amount;
        }

        public void Withdraw(int amount)
        {
            balance -= amount;
        }

        public void Transfer(BankAccount to, int amount)
        {
            Withdraw(amount);
            to.Deposit(amount);
        }
    }

    public class ReaderWriterLocks
    {
        static ReaderWriterLockSlim padlock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

        static Random random = new Random();

        public static void Start(string[] args)
        {
            int x = 0;
            var tasks = new List<Task>();
            for(int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    //padlock.EnterReadLock();
                    padlock.EnterUpgradeableReadLock();

                    if(i % 2 == 0)
                    {

                        Console.WriteLine($"Entered write lock, x = {x}");
                        padlock.EnterWriteLock();
                        x = 123;
                        padlock.ExitWriteLock();
                    }

                    Console.WriteLine($"Entered read lock, x = {x}");
                    Thread.Sleep(5000);

                    //padlock.ExitReadLock();
                    padlock.ExitUpgradeableReadLock();
                    Console.WriteLine($"Exited read lock, x = {x}");
                }));
            }

            try
            {
                Task.WaitAll(tasks.ToArray());
            } catch (AggregateException ae)
            {
                ae.Handle(e =>
                {
                    Console.WriteLine(e);
                    return true;
                });
            }

            while (true)
            {
                Console.ReadKey();
                padlock.EnterWriteLock();
                Console.WriteLine("Write lock acquired");

                int newValue = random.Next(10);
                x = newValue;
                Console.WriteLine($"Set x = {x}");
                padlock.ExitWriteLock();
                Console.WriteLine("Write lock released");
            }
        }
    }
}
