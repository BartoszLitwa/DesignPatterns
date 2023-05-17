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

        private Point(double x, double y) // Should be private
        {
            this.x = x;
            this.y = y;
        }

        public static Point NewCartesianPoint(double x, double y) => new Point(x, y);

        public static Point NewPolarPoint(double rho, double theta)
            => new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));

        public override string ToString() => $"Point - {nameof(x)}: {x}, {nameof(y)}: {y}";
    }

    public class PointF
    {
        private double x, y;

        public PointF(double x, double y) // Has to be public
        {
            this.x = x;
            this.y = y;
        }
        public override string ToString() => $"PointF - {nameof(x)}: {x}, {nameof(y)}: {y}";
    }

    public static class PointFactory
    {
        public static PointF NewCartesianPoint(double x, double y) => new PointF(x, y);

        public static PointF NewPolarPoint(double rho, double theta)
            => new PointF(rho * Math.Cos(theta), rho * Math.Sin(theta));
    }

    public class FactoryMethod
    {
        public static void Start(string[] args)
        {
            var point = Point.NewPolarPoint(1.0, Math.PI / 2);
            Console.WriteLine(point);

            var point2 = Point.NewCartesianPoint(1, 6);
            Console.WriteLine(point2);

            var pointF = PointFactory.NewPolarPoint(1.0, Math.PI / 2);
            Console.WriteLine(pointF);

            var pointF2 = PointFactory.NewCartesianPoint(1, 6);
            Console.WriteLine(pointF2);
        }
    }
}
