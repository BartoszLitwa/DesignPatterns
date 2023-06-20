using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;

namespace DesignPatterns.Adapter.Examples
{
    public class Point
    {
        public int X, Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Equals(Point other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return X.Equals(other.X) && Y.Equals(other.Y);
        }

        public override bool Equals(object obj)
        {
            if (obj is Point point)
            {
                return Equals(point);
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 47;
                hashCode = (hashCode * 53) ^ X.GetHashCode();
                hashCode = (hashCode * 53) ^ Y.GetHashCode();
                return hashCode;
            }
        }
    }

    public class Line
    {
        public Point Start, End;

        public Line(Point start, Point end)
        {
            Start = start;
            End = end;
        }

        public bool Equals(Line other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Equals(Start, other.Start) && Equals(End, other.End);
        }

        public override bool Equals(object obj)
        {
            if (obj is Line line)
            {
                return Equals(line);
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 47;
                if (Start != null)
                {
                    hashCode = (hashCode * 53) ^ EqualityComparer<Point>.Default.GetHashCode(Start);
                }

                if (End != null)
                {
                    hashCode = (hashCode * 53) ^ EqualityComparer<Point>.Default.GetHashCode(End);
                }

                return hashCode;
            }
        }
    }

    public class VectorObject : Collection<Line>
    {

    }

    public class VectorRectangle : VectorObject
    {
        public VectorRectangle(int x, int y, int width, int height)
        {
            Add(new Line(new Point(x, y), new Point(x + width, y)));
            Add(new Line(new Point(x + width, y), new Point(x + width, y + height)));
            Add(new Line(new Point(x, y), new Point(x, y + height)));
            Add(new Line(new Point(x, y + height), new Point(x + width, y + height)));
        }
    }

    public class LineToPointAdapter : IEnumerable<Point>
    {
        private static int _count = 0;
        private static Dictionary<int, List<Point>> cache = new();

        public LineToPointAdapter(Line line)
        {
            var hash = line.GetHashCode();
            if (cache.ContainsKey(hash))
                return;

            Console.WriteLine($"{++_count}: Generating points for line" +
                $" [{line.Start.X}, {line.Start.Y}]-[{line.End.X}, {line.End.Y}]");

            int left = Math.Min(line.Start.X, line.End.X);
            int right = Math.Max(line.Start.X, line.End.X);
            int top = Math.Min(line.Start.Y, line.End.Y);
            int bottom = Math.Max(line.Start.Y, line.End.Y);
            int dx = right - left;
            int dy = line.End.Y - line.Start.Y;

            var points = new List<Point>();
            if (dx == 0)
            {
                for (int y = top; y <= bottom; ++y)
                {
                    points.Add(new Point(left, y));
                }
            }
            else if (dy == 0)
            {
                for (int x = left; x <= right; ++x)
                {
                    points.Add(new Point(x, top));
                }
            }

            cache.Add(hash, points);
        }

        public IEnumerator<Point> GetEnumerator() => cache.Values.SelectMany(x => x).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class VectorRasterDemo
    {
        private static readonly List<VectorObject> vectorObjects
            = new List<VectorObject>
            {
                new VectorRectangle(1,1, 10, 10),
                new VectorRectangle(3,3, 6, 6),
            };

        public static void DrawPoint(Point p)
        {
            Console.Write('.');
        }

        public static void Start(string[] args)
        {
            Draw();
            Draw();
        }

        private static void Draw()
        {
            foreach (var vo in vectorObjects)
            {
                foreach (var line in vo)
                {
                    var adapter = new LineToPointAdapter(line);
                    foreach (var point in adapter)
                        DrawPoint(point);
                }
            }
        }
    }
}
