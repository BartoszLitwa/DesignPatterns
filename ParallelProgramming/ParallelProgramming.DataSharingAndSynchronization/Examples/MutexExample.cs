namespace ParallelProgramming.DataSharingAndSynchronization.Examples.MutexExample
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

    public class MutexExample
    {
        public static void Start(string[] args)
        {
            var ba = new BankAccount();
            var ba2 = new BankAccount();

            Mutex mutex = new Mutex();
            Mutex mutex2 = new Mutex();

            var tasks = new List<Task>();
            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        bool haveLock = mutex.WaitOne();
                        try
                        {
                            ba.Deposit(1);
                        }
                        finally
                        {
                            if(haveLock)
                                mutex.ReleaseMutex();
                        }
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        bool haveLock = mutex2.WaitOne();
                        try
                        {
                            ba2.Deposit(1);
                        }
                        finally
                        {
                            if (haveLock)
                                mutex2.ReleaseMutex();
                        }
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        bool haveLock = Mutex.WaitAll(new[] { mutex, mutex2 });
                        try
                        {
                            ba.Transfer(ba2, 1);
                        }
                        finally
                        {
                            if (haveLock)
                            {
                                mutex.ReleaseMutex();
                                mutex2.ReleaseMutex();
                            }
                        }
                    }
                }));
            }
            Task.WaitAll(tasks.ToArray());

            Console.WriteLine($"Final balance is {ba.Balance}");
            Console.WriteLine($"Final balance2 is {ba2.Balance}");
        }
    }
}
