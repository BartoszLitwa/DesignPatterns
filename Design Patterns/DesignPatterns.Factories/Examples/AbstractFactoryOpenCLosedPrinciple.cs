using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Factories.Examples.OCP
{
    public interface IHotDrink
    {
        void Consume();
    }

    internal class Tea : IHotDrink
    {
        public void Consume() => Console.WriteLine("Tea Drink");
    }

    internal class Coffee : IHotDrink
    {
        public void Consume() => Console.WriteLine("Coffee Drink");
    }

    public interface IHotDrinkFactory
    {
        IHotDrink Prepare(int amount);
    }

    internal class TeaFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            Console.WriteLine($"Prepare Tea Drink - {amount}");
            return new Tea();
        }
    }

    internal class CoffeeFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            Console.WriteLine($"Prepare Coffee Drink - {amount}");
            return new Coffee();
        }
    }

    public class HotDrinkMachine
    {
        private List<(string Drink, IHotDrinkFactory Factory)> factories = new();

        public HotDrinkMachine()
        {
            foreach(var type in typeof(HotDrinkMachine).Assembly.GetTypes())
            {
                if(typeof(IHotDrinkFactory).IsAssignableFrom(type) &&
                    !type.IsInterface && !type.IsAbstract)
                {
                    factories.Add((
                        type.Name.Replace("Factory", string.Empty),
                        (IHotDrinkFactory)Activator.CreateInstance(type)!
                        ));
                }
            }
        }

        public IHotDrink MakeDrink()
        {
            Console.WriteLine("Available drnks:");
            factories.Select((tuple, index) => $"{index}: {tuple.Drink}")
                .ToList()
                .ForEach(Console.WriteLine);

            while (true)
            {
                string s;
                if((s = Console.ReadLine()) is not null && int.TryParse(s, out int val)
                    && val >= 0 && val < factories.Count)
                {
                    Console.WriteLine("Specify Amount: ");
                    if ((s = Console.ReadLine()) is not null && int.TryParse(s, out int amount)
                    && amount > 0)
                    {
                        return factories[val].Factory.Prepare(amount);
                    }
                }

                Console.WriteLine("Incorrect Input");
            }
        }

        public IHotDrink MakeDrink(string name, int amount)
        {
            var found = factories.FirstOrDefault(x => x.Drink == name);
            
            return found.Factory.Prepare(amount);
        }
    }

    public class AbstractFactoryOpenCLosedPrinciple
    {
        public static void Start(string[] args)
        {
            var machine = new HotDrinkMachine();
            var drink = machine.MakeDrink("Tea", 500);
            drink.Consume();
        }
    }
}
