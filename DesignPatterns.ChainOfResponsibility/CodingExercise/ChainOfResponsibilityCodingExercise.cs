namespace DesignPatterns.ChainOfResponsibility.CodingExercise
{
    public abstract class Creature
    {
        protected Game _game;
        protected int _attack, _defense;

        public int Attack { get; set; }
        public int Defense { get; set; }

    }

    public class Goblin : Creature
    {
        public Goblin(Game game) {

            _game = game;
        }

        protected Goblin(Game game,  int attack = 1, int defense = 1) : this(game)
        {
            _attack = attack;
            _defense = defense;
        }
    }

    public class GoblinKing : Goblin
    {
        public GoblinKing(Game game) : base(game, 3, 3) { }
    }

    public class Game
    {
        public IList<Creature> Creatures;
    }

    public class ChainOfResponsibilityCodingExercise
    {
    }
}
