using System.ComponentModel;

namespace DesignPatterns.Observer.Examples.ObservableCollections
{
    public class Market // Observable
    {
        public BindingList<float> Prices = new();

        public void AddPrice(float price)
        {
            Prices.Add(price);
        }
    }

    public class ObservableCollections // Observer
    {
        public static void Start(string[] args)
        {
            var market = new Market();
            market.Prices.ListChanged += (sender, args) =>
            {
                if(args.ListChangedType == ListChangedType.ItemAdded)
                {
                    float price = ((BindingList<float>)sender!)[args.NewIndex];
                    Console.WriteLine($"We got a price of {price}");
                }
            };
            market.AddPrice(123);
        }
    }
}
