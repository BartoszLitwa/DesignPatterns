namespace DesignPatterns.TemplateMethod.Examples.FunctionalTemplateMethod
{
    public static class GameTemplate
    {
        public static void Run(
            Action start,
            Action takeTurn,
            Func<bool> haveWinner,
            Func<int> winningPlayer)
        {
            start();
            while (!haveWinner())
                takeTurn();
            Console.WriteLine($"Player {winningPlayer()} wins.");
        }
    }

    public class FunctionalTemplateMethod
    {
        public static void Start(string[] args)
        {
            var numberOfPlayers = 2;
            int currentPlayer = 0;
            int turn = 1, maxTurns = 10;

            void Start() => Console.WriteLine($"Starting a game of chess with {numberOfPlayers} players.");

            bool HaveWinner() => turn == maxTurns;

            void TakeTurn()
            {
                Console.WriteLine($"Turn {turn++} taken by player {currentPlayer}");
                currentPlayer = (currentPlayer + 1) % numberOfPlayers;
            }

            int WinningPlayer() => currentPlayer;

            GameTemplate.Run(Start, TakeTurn, HaveWinner, WinningPlayer);
        }
    }
}
