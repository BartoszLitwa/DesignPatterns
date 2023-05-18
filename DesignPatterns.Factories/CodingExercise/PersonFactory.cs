using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Factories.CodingExercise
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class PersonFactory
    {
        private int _index = 0;

        public Person CreatePerson(string name)
        {
            return new Person { Name = name, Id = _index++ };
        }

        public static void Start(string[] args)
        {
            var pf = new PersonFactory();
            Console.WriteLine(pf.CreatePerson("test 1").Id);
            Console.WriteLine(pf.CreatePerson("test 2").Id);
            Console.WriteLine(pf.CreatePerson("test 3").Id);
        }
    }
}
