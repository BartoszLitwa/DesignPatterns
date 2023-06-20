using System;
using System.Linq;

namespace DesignPatterns.Proxy.CodingExercise
{
    public class Person
    {
        public int Age { get; set; }

        public string Drink() { return "drinking"; }

        public string Drive() { return "driving"; }

        public string DrinkAndDrive() { return "driving while drunk"; }
    }

    public class ResponsiblePerson
    {
        private Person _person;
        public ResponsiblePerson(Person person) { _person = person; }

        public int Age { get { return _person.Age; } set { _person.Age = value; } }

        public string Drink() => Age >= 18 ? _person.Drink() : "too young";

        public string Drive() => Age >= 16 ? _person.Drive() : "too young";

        public string DrinkAndDrive() => "dead";
    }

    public class ProxyCodingExercise
    {
    }
}
