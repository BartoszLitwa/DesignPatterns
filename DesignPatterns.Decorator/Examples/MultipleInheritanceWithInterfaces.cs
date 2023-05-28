using System;
using System.Linq;

namespace DesignPatterns.Decorator.Examples
{
    public interface IBird
    {
        int Weight { get; set; }
        void Fly();
    }

    public class Bird : IBird
    {
        public int Weight { get; set; }
        public void Fly() => Console.WriteLine($"Fly {Weight}");
    }

    public interface ILizard
    {
        int Weight { get; set; }

        void Crawl();
    }

    public class Lizard : ILizard
    {
        public int Weight { get; set; }

        public void Crawl() => Console.WriteLine($"Crawl {Weight}");
    }

    public class Dragon : IBird, ILizard
    {
        private Bird _bird = new Bird();
        private Lizard _lizard = new Lizard();
        private int _weight;

        public int Weight
        {
            get => _weight;
            set
            {
                _weight = value;
                _bird.Weight = value;
                _lizard.Weight = value;
            }
        }

        public void Fly() => _bird.Fly();
        public void Crawl() => _lizard.Crawl();
    }

    public class MultipleInheritanceWithInterfaces
    {
        public static void Start(string[] args)
        {
            var d = new Dragon();
            d.Weight = 123;
            d.Fly();
            d.Crawl();
        }
    }
}
