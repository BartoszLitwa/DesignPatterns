namespace DesignPatterns.State.Examples.SwitchExpressions
{
    public enum Chest
    {
        Open, Closed, Locked
    }

    public enum Action
    {
        Open, Close
    }

    public class SwitchExpressions
    {
        static Chest Manipulate(Chest chest, Action action, bool haveKey)
            => (chest, action, haveKey) switch
            {
                (Chest.Locked, Action.Open, true) => Chest.Open,
                (Chest.Closed, Action.Open, _) => Chest.Open,
                (Chest.Open, Action.Close, true) => Chest.Locked,
                (Chest.Open, Action.Close, false) => Chest.Closed,

                _ => chest,
            };

        static Chest ManipulateOld(Chest chest, Action action, bool haveKey)
        {
            switch (chest, action, haveKey)
            {
                case (Chest.Closed, Action.Open, _):
                    return Chest.Open;
                case (Chest.Locked, Action.Open, true):
                    return Chest.Open;
                case (Chest.Open, Action.Close, true):
                    return Chest.Locked;
                case (Chest.Open, Action.Close, false):
                    return Chest.Closed;
                default:
                    Console.WriteLine("Chest unchanged");
                    return chest;
            }
        }

        public static void Start(string[] args)
        {
            var chest = Chest.Locked;
            Console.WriteLine($"Chest is {chest}");

            chest = Manipulate(chest, Action.Open, true);
            Console.WriteLine($"Chest is {chest}");

            chest = Manipulate(chest, Action.Close, false);
            Console.WriteLine($"Chest is {chest}");

            chest = Manipulate(chest, Action.Close, false);
            Console.WriteLine($"Chest is {chest}");
        }
    }
}
