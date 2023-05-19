using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Singleton.Examples
{
    public interface IDatabase
    {
        int GetPopulation(string name);
    }

    public class SingletonDatabase : IDatabase
    {
        private Dictionary<string, int> _capitals = new();

        private SingletonDatabase()
        {
            Console.WriteLine("Initializing Database");

            const int batchSize = 2;
            _capitals = File.ReadAllLines("capitals.txt")
                .Select((number, index) => new { number, index })
                .GroupBy(item => item.index / batchSize, item => item.number) // Batch to 2 elements
                .ToDictionary(
                    x => x.ElementAt(0).Trim(),
                    x => int.Parse(x.ElementAt(1).Trim())
                    );
        }

        private static Lazy<SingletonDatabase> _instance // Initialized only once whend first time needed
            => new Lazy<SingletonDatabase>(() => new SingletonDatabase()); 

        public static SingletonDatabase Instance => _instance.Value;

        public int GetPopulation(string name) => _capitals[name];
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
