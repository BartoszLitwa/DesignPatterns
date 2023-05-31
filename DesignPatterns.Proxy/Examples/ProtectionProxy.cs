using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Proxy.Examples.ProtectionProxy
{
    public interface ICar
    {
        void Drive();
    }

    public class Car : ICar
    {
        public void Drive()
        {
            Console.WriteLine("Car is being driven");
        }
    }

    public class Driver
    {
        public int Age { get; set; }
    }

    public class CarProxy : ICar
    {
        private Driver _driver;
        private Car car = new Car();

        public CarProxy(Driver driver)
        {
            _driver = driver;
        }

        public void Drive()
        {
            if (_driver.Age >= 18)
                car.Drive();
            else
                Console.WriteLine("Too young");
        }
    }

    public class ProtectionProxy
    {
        public static void Start(string[] args)
        {
            ICar car = new CarProxy(new Driver { Age = 12});
            car.Drive();
        }
    }
}
