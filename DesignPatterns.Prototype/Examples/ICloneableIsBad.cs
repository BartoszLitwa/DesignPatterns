using System;
using System.Linq;
using System.Text.Json;

namespace DesignPatterns.Prototype.Examples
{
    public class Person : ICloneable
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

        public object Clone()
        {
            return new Person(Names, (Address)Address.Clone());
        }

        public override string ToString()
        {
            return $"{nameof(Names)}: {string.Join(',', Names)}, {nameof(Address)}: {Address}";
        }
    }

    public class Address : ICloneable
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

        public object Clone()
        {
            return new Address(StreetName, HouseNumber);
        }

        public override string ToString()
        {
            return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
        }
    }

    public class ICloneableIsBad
    {
        public static void Start(string[] args)
        {
            var john = new Person(new[] { "John", "Smith" }, new Address("Road", 123));

            var jane = john; // Copy reference
            var jan = john.Clone(); // Create a copy
            var copy = new Person(john); // Copy constructor - C++ thing

            Console.WriteLine(john);
        }
    }
}
