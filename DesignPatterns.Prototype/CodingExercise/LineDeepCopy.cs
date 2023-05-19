using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Prototype.CodingExercise
{
    public interface IDeepCopy<T>
        where T : new()
    {
        T DeepCopy();
    }

    public class Point : IDeepCopy<Point>
    {
        public int X, Y;

        public Point DeepCopy()
        {
            var point = new Point();
            point.X = this.X;
            point.Y = this.Y;
            return point;
        }

        public override string ToString() => $"{nameof(X)}: {X}, {nameof(Y)}: {Y}";
    }

    public class Line : IDeepCopy<Line>
    {
        public Point Start, End;

        public Line DeepCopy()
        {
            var line = new Line();
            line.Start = Start.DeepCopy();
            line.End = End.DeepCopy();
            return line;
        }

        public override string ToString() => $"{nameof(Start)}: {Start}, {nameof(End)}: {End}";
    }

    internal class LineDeepCopy
    {
        public static void Start(string[] args)
        {
            var line = new Line();
            line.Start = new Point();
            line.End = new Point();
            line.Start.X = 0;
            line.Start.Y = 2;
            line.End.X = 4;
            line.End.Y = 8;

            var line2 = line.DeepCopy();
            line.Start.X = 8;
            line.Start.Y = 6;
            line.End.X = 4;
            line.End.Y = 2;

            Console.WriteLine(line);
            Console.WriteLine(line2);
        }
    }
}
