namespace DesignPatterns.Command.CodingExercise
{
    public class Command
    {
        public enum Action
        {
            Deposit,
            Withdraw
        }

        public Action TheAction;
        public int Amount;
        public bool Success;
    }

    public class Account
    {
        public int Balance { get; set; }

        public void Process(Command c)
        {
            switch (c.TheAction)
            {
                case Command.Action.Withdraw:
                    if (c.Amount > Balance)
                    {
                        c.Success = false;
                        return;
                    }

                    Balance -= c.Amount;
                    break;
                case Command.Action.Deposit:
                    Balance += c.Amount;
                    break;
            }
            c.Success = true;
        }
    }

    public class CommandCodingExercise
    {
    }
}
