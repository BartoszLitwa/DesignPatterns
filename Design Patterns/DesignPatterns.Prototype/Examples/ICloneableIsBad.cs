using System;
using System.Linq;
using System.Text.Json;

namespace DesignPatterns.Prototype.Examples
{
    public interface IPrototype<T>
    {
        T DeepCopy();
    }

    public class Person : ICloneable, IPrototype<Person>
    {
        public Address Address;
        public string[] Names;

        // Copy Constructor
        public Person(Person other)
        {
            Names = other.Names;
            Address = new Address(other.Address);
        }

        public Person(string[] names, Address address)
        {
            Names = names;
            Address = address;
        }

        public object Clone() => new Person(Names, (Address)Address.Clone());

        public Person DeepCopy() => new Person(Names, Address.DeepCopy());

        public override string ToString() => $"{nameof(Names)}: {string.Join(',', Names)}, {nameof(Address)}: {Address}";
    }

    public class Address : ICloneable, IPrototype<Address>
    {
        public int HouseNumber;
        public string StreetName;

        // Copy Constructor
        public Address(Address address)
        {
            HouseNumber = address.HouseNumber;
            StreetName = address.StreetName;
        }

        public Address(string streetName, int houseNumber)
        {
            StreetName = streetName;
            HouseNumber = houseNumber;
        }

        public object Clone() => new Address(StreetName, HouseNumber);

        public Address DeepCopy() => new Address(StreetName, HouseNumber);

        public override string ToString() => $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
    }

    public class ICloneableIsBad
    {
        public static void Start(string[] args)
        {
            var john = new Person(new[] { "John", "Smith" }, new Address("Road", 123));

            var jane = john; // Copy reference
            var jan = john.Clone(); // Create a copy
            var copy = new Person(john); // Copy constructor - C++ thing
            var deepCopy = john.DeepCopy(); // IPrototype interface

            Console.WriteLine(john);
        }
    }
}
