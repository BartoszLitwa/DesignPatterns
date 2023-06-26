namespace ParallelProgramming.DataSharingAndSynchronization.Examples.InterlockedOperations
{
    public class BankAccount
    {
        int balance;
        public int Balance { get => balance; private set => balance = value; }

        public void Deposit(int amount)
        {
            // += operation IS NOT ATOMIC
            // op1: temp <= get_Balance() + amount
            // op2: set_Balance(temp)

            // Lock Free Programming
            Interlocked.Add(ref balance, amount);

            // 1 These 2 have to be executed first
            // 2
            Thread.MemoryBarrier();
            Interlocked.MemoryBarrier();
            // 3
        }

        public void Withdraw(int amount)
        {
            Interlocked.Add(ref balance, -amount);
        }
    }

    public class InterlockedOperations
    {
        public static void Start(string[] args)
        {
            var ba = new BankAccount();
            
            var cts = new CancellationTokenSource();
            var token = cts.Token;
            var tasks = new List<Task>();
            for(int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for(int j = 0; j < 1000; j++)
                    {
                        ba.Deposit(100);
                    }
                }, token));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        ba.Withdraw(100);
                    }
                }, token));
            }
            Task.WaitAll(tasks.ToArray());

            Console.WriteLine($"Final balance is {ba.Balance}");
        }
    }
}
