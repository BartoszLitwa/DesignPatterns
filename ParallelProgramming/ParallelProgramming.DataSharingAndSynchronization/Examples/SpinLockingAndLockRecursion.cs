namespace ParallelProgramming.DataSharingAndSynchronization.Examples.SpinLockingAndLockRecursion
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
    }

    public class SpinLockingAndLockRecursion
    {
        public static void Start(string[] args)
        {
            var ba = new BankAccount();
            var sl = new SpinLock();
            
            var cts = new CancellationTokenSource();
            var token = cts.Token;
            var tasks = new List<Task>();
            for(int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for(int j = 0; j < 1000; j++)
                    {
                        bool lockTaken = false;
                        try
                        {
                            sl.Enter(ref lockTaken);
                            ba.Deposit(100);
                        }
                        finally
                        {
                            if (lockTaken)
                                sl.Exit();
                        }
                    }
                }, token));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        bool lockTaken = false;
                        try
                        {
                            sl.Enter(ref lockTaken);
                            ba.Withdraw(100);
                        }
                        finally
                        {
                            if (lockTaken)
                                sl.Exit();
                        }
                    }
                }, token));
            }
            Task.WaitAll(tasks.ToArray());

            Console.WriteLine($"Final balance is {ba.Balance}");

            LockRecursion(5);
        }

        static SpinLock sl2 = new SpinLock(true);

        private static void LockRecursion(int x)
        {
            bool lockTaken = false;
            try
            {
                sl2.Enter(ref lockTaken);
            }
            catch (LockRecursionException e)
            {
                Console.WriteLine($"Exception {e}");
            }
            finally
            {
                if (lockTaken)
                {
                    Console.WriteLine($"Took a lock, x = {x}");
                    LockRecursion(x - 1);
                    sl2.Exit();
                } else
                {
                    Console.WriteLine($"Failed to take a lock, x = {x}");
                }
            }
        }
    }
}
