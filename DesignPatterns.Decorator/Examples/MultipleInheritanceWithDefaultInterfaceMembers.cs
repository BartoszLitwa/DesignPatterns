using System;
using System.Linq;

namespace DesignPatterns.Decorator.Examples.MultipleInheritanceWithDefaultInterfaceMembers
{
    public interface ICreature
    {
        int Age { get; set; }
    }

    public interface IBird : ICreature
    {
        void Fly()
        {
            if(Age >= 10)
                Console.WriteLine("I am Flying");
        }
    }

    public interface ILizard : ICreature
    {
        void Crawl()
        {
            if (Age < 10)
                Console.WriteLine("I am Crawling");
        }
    }

    public class Organism { }

    // Just to showcase - prevent option to inherit
    public class Dragon : Organism, IBird, ILizard
    {
        public int Age { get; set; }
    }

    public class MultipleInheritanceWithDefaultInterfaceMembers
    {
        public static void Start(string[] args)
        {
            Dragon d = new Dragon { Age = 5 };
            //((ILizard)d).Crawl();
            if (d is IBird bird)
                bird.Fly();
            if(d is ILizard lizard)
                lizard.Crawl();
        }
    }
}
