using System;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace DesignPatterns.Prototype.Examples.CopyThroughSerialization
{
    [Serializable]
    public class Person
    {
        public Address Address;
        public string[] Names;

        public Person() { }

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

        public override string ToString() => $"{nameof(Names)}: {string.Join(',', Names)}, {nameof(Address)}: {Address}";
    }

    [Serializable]
    public class Address
    {
        public int HouseNumber;
        public string StreetName;

        public Address() { }

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

        public override string ToString() => $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
    }

    public static class ExtensionsMethods
    {
        public static T DeepCopy<T>(this T self) // Has to have [Serializable] on every class
        {
            var stream = new MemoryStream();
            var formatter = new BinaryFormatter();
#pragma warning disable SYSLIB0011 // Type or member is obsolete
            formatter.Serialize(stream, self);
            stream.Seek(0, SeekOrigin.Begin);
            object copy = formatter.Deserialize(stream);
#pragma warning restore SYSLIB0011 // Type or member is obsolete
            stream.Close();
            return (T)copy;
        }

        public static T DeepCopyXml<T>(this T self) // Has to have parameter-less constructors
        {
            using var stream = new MemoryStream();
            var xmlSerializer = new XmlSerializer(typeof(T));
            xmlSerializer.Serialize(stream, self);
            stream.Seek(0, SeekOrigin.Begin);
            return (T)xmlSerializer.Deserialize(stream)!;
        }
    }

    public class CopyThroughSerialization
    {
        public static void Start(string[] args)
        {
            var john = new Person(new[] { "John", "Smith" }, new Address("Road", 123));

            var deepCopy = john.DeepCopy(); // Extensions methods
            deepCopy.Names[0] = "Jane";
            deepCopy.Address.HouseNumber++;

            var deepCopyXml = john.DeepCopyXml(); // Extensions methods
            deepCopyXml.Names[0] = "XML";
            deepCopyXml.Address.HouseNumber--;

            Console.WriteLine(john);
            Console.WriteLine(deepCopy);
            Console.WriteLine(deepCopyXml);
        }
    }
}
