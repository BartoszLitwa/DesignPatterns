namespace ParallelProgramming.DataSharingAndSynchronization.Examples.CriticalSections
{
    public class BankAccount
    {
        public int Balance { get; private set; }

        private object _padlock = new object();

        public void Deposit(int amount)
        {
            // += operation IS NOT ATOMIC
            // op1: temp <= get_Balance() + amount
            // op2: set_Balance(temp)
            lock (_padlock)
            {
                Balance += amount;
            }
        }

        public void Withdraw(int amount)
        {
            lock (_padlock)
            {
                Balance -= amount;
            }
        }
    }

    public class CriticalSections
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
