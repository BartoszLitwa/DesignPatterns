using Autofac;
using NUnit.Framework;
using System;
using System.Linq;

namespace DesignPatterns.Singleton.Examples
{
    public interface IDatabase
    {
        int GetPopulation(string name);
    }

    public class SingletonDatabase : IDatabase
    {
        private Dictionary<string, int> _capitals = new();
        private static int _instanceCount = 0;

        public static int Count => _instanceCount;

        private SingletonDatabase()
        {
            Console.WriteLine("Initializing Database");
            _instanceCount++;

            const int batchSize = 2;
            _capitals = File.ReadAllLines(
                    Path.Combine(new FileInfo(typeof(IDatabase).Assembly.Location).DirectoryName!,
                    "capitals.txt"))
                .Select((number, index) => new { number, index })
                .GroupBy(item => item.index / batchSize, item => item.number) // Batch to 2 elements
                .ToDictionary(
                    x => x.ElementAt(0).Trim(),
                    x => int.Parse(x.ElementAt(1).Trim())
                    );
        }

        private static Lazy<SingletonDatabase> _instance // Initialized only once whend first time needed
            = new Lazy<SingletonDatabase>(() => new SingletonDatabase());

        public static SingletonDatabase Instance => _instance.Value;

        public int GetPopulation(string name) => _capitals[name];
    }

    public class OrdinaryDatabase : IDatabase
    {
        private Dictionary<string, int> _capitals = new();

        public OrdinaryDatabase()
        {
            Console.WriteLine("Initializing Database");

            const int batchSize = 2;
            _capitals = File.ReadAllLines(
                    Path.Combine(new FileInfo(typeof(IDatabase).Assembly.Location).DirectoryName!,
                    "capitals.txt"))
                .Select((number, index) => new { number, index })
                .GroupBy(item => item.index / batchSize, item => item.number) // Batch to 2 elements
                .ToDictionary(
                    x => x.ElementAt(0).Trim(),
                    x => int.Parse(x.ElementAt(1).Trim())
                    );
        }

        public int GetPopulation(string name) => _capitals[name];
    }

    public class SingletonRecordFinder
    {
        public int GetTotalPopulation(IEnumerable<string> names)
        {
            int result = names.Aggregate(0, (res, name) => res += SingletonDatabase.Instance.GetPopulation(name));
            return result;
        }
    }

    public class ConfigurableRecordFinder
    {
        private IDatabase _database;

        public ConfigurableRecordFinder(IDatabase database)
        {
            _database = database;
        }

        public int GetTotalPopulation(IEnumerable<string> names)
        {
            int result = names.Aggregate(0, (res, name) => res += _database.GetPopulation(name));
            return result;
        }
    }

    public class DummyDatabase : IDatabase
    {
        public int GetPopulation(string name)
        {
            return new Dictionary<string, int>
            {
                ["alpha"] = 1,
                ["beta"] = 2,
                ["gamma"] = 3
            }[name];
        }
    }

    [TestFixture]
    class SingletonTests
    {
        [Test]
        public void IsSingletonTest()
        {
            var db = SingletonDatabase.Instance;
            var db2 = SingletonDatabase.Instance;
            Assert.That(db, Is.SameAs(db2));
            Assert.That(SingletonDatabase.Count, Is.EqualTo(1));
        }

        [Test]
        public void SingletonTotalPopulationTest()
        {
            var rf = new SingletonRecordFinder();
            var names = new[] { "Seoul", "Mexico City" };
            int tp = rf.GetTotalPopulation(names);
            Assert.That(tp, Is.EqualTo(17_500_000 + 17_400_000));
        }

        [Test]
        public void ConfigurablePopulationTest()
        {
            var rf = new ConfigurableRecordFinder(new DummyDatabase());
            var names = new[] { "alpha", "gamma" };
            int tp = rf.GetTotalPopulation(names);
            Assert.That(tp, Is.EqualTo(1 + 3));
        }

        [Test]
        public void DIPopulationTest()
        {
            var cb = new ContainerBuilder();
            cb.RegisterType<OrdinaryDatabase>()
                .As<IDatabase>()
                .SingleInstance();
            cb.RegisterType<ConfigurableRecordFinder>();

            using var c = cb.Build();
            var rf = c.Resolve<ConfigurableRecordFinder>();

            var names = new[] { "Seoul", "Mexico City" };
            int tp = rf.GetTotalPopulation(names);
            Assert.That(tp, Is.EqualTo(17_500_000 + 17_400_000));
        }
    }

    public class SingletonImplementation
    {
        public static void Starts(string[] args)
        {
            var db = SingletonDatabase.Instance;
            Console.WriteLine(db.GetPopulation("Tokyo"));
        }
    }
}
