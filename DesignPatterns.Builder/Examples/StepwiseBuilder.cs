using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DesignPatterns.Builder.Examples
{
    public enum CarType
    {
        Sedan, Crossover
    }

    public class Car
    {
        public CarType CarType;
        public int WheelSize;

        public override string ToString() => $"{nameof(CarType)}: {CarType}, {nameof(WheelSize)}: {WheelSize}";
    }

    public interface ISpecifyCarType
    {
        ISpecifyWheelSize OfType(CarType type);
    }

    public interface ISpecifyWheelSize
    {
        IBuildCar WithWheels(int wheelSize);
    }

    public interface IBuildCar
    {
        public Car Build();
    }

    public class CarBuilder
    {
        private class Impl :
            ISpecifyCarType,
            ISpecifyWheelSize,
            IBuildCar
        {
            private Car car = new();

            Car IBuildCar.Build()
            {
                return car;
            }

            ISpecifyWheelSize ISpecifyCarType.OfType(CarType type)
            {
                car.CarType = type;
                return this;
            }

            IBuildCar ISpecifyWheelSize.WithWheels(int wheelSize)
            {
                switch(car.CarType)
                {
                    case CarType.Crossover when wheelSize < 17 || wheelSize > 20:
                    case CarType.Sedan when wheelSize < 15 || wheelSize > 17:
                        throw new ArgumentException($"Wrong size of wheel for {car.CarType}");
                }

                car.WheelSize = wheelSize;
                return this;
            }
        }

        public static ISpecifyCarType Create()
        {
            return new Impl();
        }
    }

    public class StepwiseBuilder
    {
        public static void Start(string[] args)
        {
            var car = CarBuilder.Create()
                .OfType(CarType.Crossover)
                .WithWheels(18)
                .Build();

            Console.WriteLine(car);
        }
    }
}
