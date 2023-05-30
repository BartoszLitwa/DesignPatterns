using System;
using System.Linq;

namespace DesignPatterns.Decorator.CodingExercise
{
    public class Bird
    {

        public string Fly()
        {
            return (Age < 10) ? "flying" : "too old";
        }

        public int Age { get; set; }
    }

    public class Lizard
    {

        public string Crawl()
        {
            return (Age > 1) ? "crawling" : "too young";
        }

        public int Age { get; set; }
    }

    public class Dragon // no need for interfaces
    {

        private int _age;
        private Bird _bird;
        private Lizard _lizard;

        public Dragon()
        {
            _bird = new Bird();
            _lizard = new Lizard();
        }

        public string Crawl() => _lizard.Crawl();

        public string Fly() => _bird.Fly();

        public int Age
        {
            get
            {
                return _age;
            }
            set
            {
                _age = value;
                _bird.Age = value;
                _lizard.Age = value;
            }
        }
    }
}
