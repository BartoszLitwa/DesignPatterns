using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Builder.Examples.FunctionalBuilder
{
    public class Person
    {
        public string Name, Position;

        public override string ToString() => $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
    }

    public abstract class FunctionalBuilder<TSubject, TSelf>
        where TSelf : FunctionalBuilder<TSubject, TSelf>
        where TSubject : new()
    {
        private readonly List<Func<TSubject, TSubject>> actions = new();

        public TSelf Do(Action<TSubject> action) => AddAction(action);

        public TSubject Build() => actions.Aggregate(new TSubject(), (person, func) => func(person));

        private TSelf AddAction(Action<TSubject> action)
        {
            actions.Add(p =>
            {
                action(p); // Perform an action
                return p;
            });

            return (TSelf)this;
        }
    }

    public sealed class PersonBuilder : FunctionalBuilder<Person, PersonBuilder>
    {
        public PersonBuilder Called(string name) => Do(p => p.Name = name);
    }

    //public sealed class PersonBuilder
    //{
    //    private readonly List<Func<Person, Person>> actions = new();

    //    public PersonBuilder Called(string name) => Do(p => p.Name = name);

    //    public PersonBuilder Do(Action<Person> action) => AddAction(action);

    //    public Person Build() => actions.Aggregate(new Person(), (person, func) => func(person));

    //    private PersonBuilder AddAction(Action<Person> action)
    //    {
    //        actions.Add(p =>
    //        {
    //            action(p); // Perform an action
    //            return p;
    //        }); 

    //        return this;
    //    }
    //}

    public static class PersonBuilderExtensions
    {
        public static PersonBuilder WorksAs(this PersonBuilder builder, string position)
            => builder.Do(p => p.Position = position);
    }

    public class FunctionalBuilder
    {
        public static void Start(string[] args)
        {
            var person = new PersonBuilder()
                .Called("Bartosz")
                .WorksAs("Worker")
                .Build();

            Console.WriteLine(person);
        }
    }
}
