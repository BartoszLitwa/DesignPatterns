using System.Text;

namespace DesignPatterns.State.Examples.SwitchBasedStateMachine
{
    public enum State
    {
        Locked, Failed, Unlocked
    }

    public class SwitchBasedStateMachine
    {
        public static void Start(string[] args)
        {
            var code = "1234";
            //var state = State.Locked;
            var state = State.Unlocked;
            var entry = new StringBuilder();

            while (true)
            {
                switch(state)
                {
                    case State.Locked:
                        entry.Append(Console.ReadKey().KeyChar);
                        if(entry.ToString() == code)
                        {
                            state = State.Unlocked;
                            break;
                        }

                        if (!code.StartsWith(entry.ToString()))
                        {
                            //state = State.Failed;
                            goto case State.Failed;
                        }

                        break;
                    case State.Failed:
                        Console.CursorLeft = 0;
                        Console.WriteLine("Failed");
                        entry.Clear();
                        state = State.Locked;
                        break;
                    case State.Unlocked:
                        Console.CursorLeft = 0;
                        Console.WriteLine("Unlocked");
                        return;
                }
            }
        }
    }
}
