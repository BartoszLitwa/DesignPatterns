using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Builder.Examples
{
    public class Person
    {
        public string Name, Position;

        public class Builder : PersonJobBuilder<Builder>
        {

        }

        public static Builder New => new Builder();

        public override string ToString() => $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
    }

    public abstract class PersonBuilder
    {
        protected Person person = new();

        public Person Build() => person;
    }

    public class PersonInfoBuilder<SELF> : PersonBuilder
        where SELF : PersonInfoBuilder<SELF>
    {
        public SELF Called(string name)
        {
            person.Name = name;
            return (SELF)this;
        }
    }

    public class PersonJobBuilder<SELF> : PersonInfoBuilder<PersonJobBuilder<SELF>>
        where SELF : PersonJobBuilder<SELF>
    {
        public SELF WorksAsA(string position)
        {
            person.Position = position;
            return (SELF)this;
        }
    }

    public class FluentBuilderInheritance
    {
        public static void Start(string[] args)
        {
            var person = Person.New
                .Called("Bartosz") // Returns PersonInfoBuilder<PersonJobBuilder<Person.Builder>>
                .WorksAsA("Worker") // Returns PersonJobBuilder<Person.Builder>
                .Build();
            Console.WriteLine(person);
        }
    }
}
