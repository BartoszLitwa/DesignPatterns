using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.SOLID.Principles
{
    public class DependencyInversion
    {
        public enum Relationship
        {
            Parent, Child, Sinling
        }

        public class Person
        {
            public string Name;
        }

        public interface IRelationshipBrowser
        {
            IEnumerable<Person> FindAllChildrenOf(Person p);
        }

        // Low-level
        public class Relationships : IRelationshipBrowser
        {
            private List<(Person, Relationship, Person)> _relations = new();

            public void AddParentChild(Person parent, Person child)
            {
                _relations.Add((parent, Relationship.Parent, child));
                _relations.Add((child, Relationship.Child, parent));
            }

            public IEnumerable<Person> FindAllChildrenOf(Person p)
            {
                foreach (var r in _relations.Where(x =>
                    x.Item1.Name == "John" && x.Item2 == Relationship.Parent))
                {
                    yield return r.Item3;
                }
            }
            
            // public List<(Person, Relationship, Person)> Relations => _relations;
        }

        public class Research
        {
            //public Research(Relationships relationships)
            //{
            //    var relations = relationships.Relations;
            //    foreach (var r in relations.Where(x => 
            //        x.Item1.Name == "John" && x.Item2 == Relationship.Parent))
            //    {
            //        Console.WriteLine($"John has a child called {r.Item3.Name}");
            //    }
            //}

            public Research(IRelationshipBrowser browser, Person p)
            {
                foreach(var c in browser.FindAllChildrenOf(p))
                    Console.WriteLine($"John has a child called {c.Name}");

            }
        }
        
        public static void Start(string[] args)
        {
            var parent = new Person { Name = "John" };
            var child1 = new Person { Name = "Chris" };
            var child2 = new Person { Name = "Mary" };

            var relationships = new Relationships();
            relationships.AddParentChild(parent, child1);
            relationships.AddParentChild(parent, child2);

            _ = new Research(relationships, parent);
        }
    }
}
