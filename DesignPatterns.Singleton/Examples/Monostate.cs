using System;
using System.Linq;

namespace DesignPatterns.Singleton.Examples
{
    public class CEO
    {
        private static int _age;
        private static string _name;

        public int Age
        {
            get => _age;
            set => _age = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public override string ToString() => $"{nameof(Name)}: {Name}, {nameof(Age)}: {Age}";
    }

    public class Monostate
    {
        public static void Start(string[] args)
        {
            var ceo = new CEO(); // Refer to the same underlying static properties
            ceo.Name = "Adam Smith";
            ceo.Age = 55;

            var ceo2 = new CEO();
            Console.WriteLine(ceo);
            Console.WriteLine(ceo2);
        }
    }
}
