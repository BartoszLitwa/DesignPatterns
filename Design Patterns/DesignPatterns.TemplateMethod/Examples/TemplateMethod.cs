using System.Security;

namespace DesignPatterns.TemplateMethod.Examples.TemplateMethod
{
    public abstract class Game
    {
        public void Run()
        {
            Start();
            while (!HaveWinner)
                TakeTurn();
            Console.WriteLine($"Player {WinningPlayer} wins.");
        }

        protected int currentPlayer;
        protected readonly int numberOfPlayers;

        protected Game(int numberOfPlayer)
        {
            this.numberOfPlayers = numberOfPlayer;
        }

        protected abstract void Start();
        protected abstract void TakeTurn();
        protected abstract bool HaveWinner { get; }
        protected abstract int WinningPlayer { get; }
    }

    public class Chess : Game
    {
        public Chess(int numberOfPlayer) : base(numberOfPlayer)
        {
        }

        protected override int WinningPlayer => currentPlayer;

        protected override void Start() => Console.WriteLine($"Starting a game of chess with {numberOfPlayers} players.");

        protected override void TakeTurn()
        {
            Console.WriteLine($"Turn {turn++} taken by player {currentPlayer}.");
            currentPlayer = (currentPlayer + 1) % numberOfPlayers;
        }

        private int turn = 1;
        private int maxTurns = 10;

        protected override bool HaveWinner => turn == maxTurns;
    }

    public class TemplateMethod
    {
        public static void Start(string[] args)
        {
            var chess = new Chess(2);
            chess.Run();
        }
    }
}
