using System.Text;

namespace DesignPatterns.State.CodingExercise
{
    public enum LockStates
    {
        LOCKED, OPEN, ERROR
    }

    public class CombinationLock
    {
        private readonly string _combination;
        public CombinationLock(int[] combination)
        {
            var sb = new StringBuilder();
            foreach (int i in combination)
                sb.Append(i);

            _combination = sb.ToString();
        }

        // you need to be changing this on user input
        public string Status = "LOCKED";
        private LockStates state = LockStates.LOCKED;

        public void EnterDigit(int digit)
        {
            if (Status == "LOCKED")
                Status = "";

            switch(state)
            {
                case LockStates.LOCKED:
                    Status += digit;
                    if(Status == _combination)
                    {
                        Status = "OPEN";
                        state = LockStates.OPEN;
                    } else if(Status.Length >= _combination.Length)
                    {
                        Status = "ERROR";
                        state = LockStates.ERROR;
                    }

                    break;
                case LockStates.OPEN:

                    break;
                case LockStates.ERROR:

                    break;
            }
        }
    }

    public class StateCodingExercise
    {
        public static void Start(string[] args)
        {

        }
    }
}
