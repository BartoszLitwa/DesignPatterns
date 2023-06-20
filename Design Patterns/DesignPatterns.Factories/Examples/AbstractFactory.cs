using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Factories.Examples
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
        public enum AvailableDrink
        {
            Coffee, Tea
        }

        private Dictionary<AvailableDrink, IHotDrinkFactory> factories = new();

        public HotDrinkMachine()
        {
            foreach(AvailableDrink drink in Enum.GetValues(typeof(AvailableDrink)))
            {
                string typeName = $"DesignPatterns.Factories.Examples.{Enum.GetName(typeof(AvailableDrink), drink)}Factory";
                var factory = (IHotDrinkFactory)Activator.CreateInstance(Type.GetType(typeName)!)!;
                factories.Add(drink, factory);
            }
        }

        public IHotDrink MakeDrink(AvailableDrink drink, int amount) 
            => factories[drink].Prepare(amount);
    }

    public class AbstractFactory
    {
        public static void Start(string[] args)
        {
            var machine = new HotDrinkMachine();
            var drink = machine.MakeDrink(HotDrinkMachine.AvailableDrink.Tea, 100);
            drink.Consume();

            var drink2 = machine.MakeDrink(HotDrinkMachine.AvailableDrink.Coffee, 200);
            drink2.Consume();
        }
    }
}
