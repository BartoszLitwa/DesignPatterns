namespace DesignPatterns.State.Examples.HandmadeStateMachine
{
    public enum State
    {
        OffHook,
        Connecting,
        Connected,
        OnHold
    }

    public enum Trigger
    {
        CallDialed,
        HungUp,
        CallConnected,
        PlacedOnHold,
        TakenOffHold,
        LeftMessage
    }

    public class HandmadeStateMachine
    {
        private static Dictionary<State, List<(Trigger, State)>> rules = new()
        {
            [State.OffHook] = new List<(Trigger, State)>
            {
              (Trigger.CallDialed, State.Connecting)
            },
            [State.Connecting] = new List<(Trigger, State)>
            {
              (Trigger.HungUp, State.OffHook),
              (Trigger.CallConnected, State.Connected)
            },
            [State.Connected] = new List<(Trigger, State)>
            {
              (Trigger.LeftMessage, State.OffHook),
              (Trigger.HungUp, State.OffHook),
              (Trigger.PlacedOnHold, State.OnHold)
            },
            [State.OnHold] = new List<(Trigger, State)>
            {
              (Trigger.TakenOffHold, State.Connected),
              (Trigger.HungUp, State.OffHook)
            }
        };

        public static void Start(string[] args)
        {
            var state = State.OffHook;
            while (true)
            {
                Console.WriteLine($"The phone is currently {state}");
                Console.WriteLine("Select a trigger: ");

                for (int i = 0; i < rules[state].Count; i++)
                {
                    var (trigger, _) = rules[state][i];
                    Console.WriteLine($"{i}. {trigger}");
                }

                //var input = int.Parse(Console.ReadLine());
                var input = 0;

                var (_, s) = rules[state][input];
                state = s;
                break;
            }
        }
    }
}
