using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatterns.ChainOfResponsibility.CodingExercise
{
    public abstract class Creature
    {
        public abstract int BaseAttack { get; }
        public abstract int BaseDefense { get; }

        public virtual int Attack { get; }
        public virtual int Defense { get; }

        protected Game game;

        protected Creature(Game game)
        {
            this.game = game;
        }

    }

    public class Goblin : Creature
    {
        public override int BaseAttack => 1;
        public override int BaseDefense => 1;

        public Goblin(Game game) : base(game) { }

        public override int Attack
        {
            get => BaseAttack + game.Creatures.OfType<GoblinKing>().Count();
        }

        public override int Defense
        {
            get => BaseDefense + game.Creatures.OfType<Goblin>().Count() - 1;
        }
    }

    public class GoblinKing : Goblin
    {
        public override int BaseAttack => 3;
        public override int BaseDefense => 3;

        public GoblinKing(Game game) : base(game) { }
    }

    public class Game
    {
        public IList<Creature> Creatures = new List<Creature>();
    }

    public class ChainOfResponsibilityCodingExercise
    {
        public static void Start(string[] args)
        {
            var game = new Game();
            var goblin = new Goblin(game);
            game.Creatures.Add(goblin);
            Console.WriteLine(goblin.Attack == 1);
            Console.WriteLine(goblin.Defense == 1);
        }
    }
}
