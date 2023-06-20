using JetBrains.dotMemoryUnit;
using NUnit.Framework;
using System;
using System.Linq;

namespace DesignPatterns.Flyweight.Examples
{
    public class User
    {
        private string fullName;

        public User(string fullName)
        {
            this.fullName = fullName;
        }
    }

    public class User2
    {
        static List<string> strings = new();
        private int[] names;

        public User2(string fullName)
        {
            names = fullName.Split(' ').Select(getOrAdd).ToArray();

            int getOrAdd(string s)
            {
                int idx = strings.IndexOf(s);
                if (idx != -1)
                    return idx;

                strings.Add(s);
                return strings.Count - 1;
            }
        }

        public string FullName => string.Join(' ', names.Select(idx => strings[idx]));
    }

    public class RepeatingUserNames
    {
        public static void Start(string[] args)
        {

        }
    }

    [TestFixture]
    public class TextFixture
    {
        Random rand;

        [SetUp]
        public void Setup()
        {
            rand = new Random(1234);
        }

        [Test]
        public void TestUser()
        {
            var firstNames = Enumerable.Range(0, 100).Select(_ => RandomString());
            var lastNames = Enumerable.Range(0, 100).Select(_ => RandomString());

            var users = new List<User>();

            foreach (var fName in firstNames)
                foreach (var lName in lastNames)
                    users.Add(new User($"{fName} {lName}"));

            ForceGC();

            dotMemory.Check(memory =>
            {
                Console.WriteLine(memory.SizeInBytes);
            });
        }

        [Test]
        public void TestUser2()
        {
            var firstNames = Enumerable.Range(0, 100).Select(_ => RandomString());
            var lastNames = Enumerable.Range(0, 100).Select(_ => RandomString());

            var users = new List<User2>();

            foreach (var fName in firstNames)
                foreach (var lName in lastNames)
                    users.Add(new User2($"{fName} {lName}"));

            ForceGC();

            dotMemory.Check(memory =>
            {
                Console.WriteLine(memory.SizeInBytes);
            });
        }

        private void ForceGC()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private string RandomString()
        {
            return new string(
                Enumerable.Range(0, 10)
                .Select(i => (char)('a' + rand.Next(26)))
                .ToArray());
        }
    }
}
