using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Factories.Examples
{
    public class Point
    {
        private double x, y;

        private Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public static Point NewCartesianPoint(double x, double y) => new Point(x, y);

        public static Point NewPolarPoint(double rho, double theta)
            => new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));

        public override string ToString() => $"{nameof(x)}: {x}, {nameof(y)}: {y}";
    }

    public class FactoryMethod
    {
        public static void Start(string[] args)
        {
            var point = Point.NewPolarPoint(1.0, Math.PI / 2);
            Console.WriteLine(point);

            var point2 = Point.NewCartesianPoint(1, 6);
            Console.WriteLine(point2);
        }
    }
}
