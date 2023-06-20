using System;
using System.Linq;
using System.Text;

namespace DesignPatterns.Singleton
{
    public sealed class BuildingContext : IDisposable
    {
        public int WallHeight;
        private static Stack<BuildingContext> stack = new();

        public static BuildingContext Current => stack.Peek();

        static BuildingContext()
        {
            stack.Push(new BuildingContext(0));
        }

        public BuildingContext(int wallHeight)
        {
            WallHeight = wallHeight;
            stack.Push(this);
        }

        public void Dispose()
        {
            if (stack.Count > 1)
                stack.Pop();
        }
    }

    public class Building
    {
        public List<Wall> Walls = new();

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (Wall wall in Walls)
                sb.AppendLine(wall.ToString());

            return sb.ToString();
        }
    }

    public class Wall
    {
        public Point Start, End;
        public int Height;

        public Wall(Point start, Point end)
        {
            Start = start;
            End = end;
            Height = BuildingContext.Current.WallHeight; // Ambient Context
        }

        public override string ToString() => $"{nameof(Start)}: {Start}, {nameof(End)}: {End}, {nameof(Height)}: {Height}";
    }

    public struct Point
    {
        private int x, y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString() => $"{nameof(x)}: {x}, {nameof(y)}: {y}";
    }

    public class AmbientContext
    {
        public static void Start(string[] args)
        {
            var house = new Building();
            // Ground floor 3000
            using (new BuildingContext(3_000))
            {
                house.Walls.Add(new Wall(new Point(0, 0), new Point(5_000, 0)));
                house.Walls.Add(new Wall(new Point(0, 0), new Point(0, 4_000)));

                // 1st floor 3500
                using (new BuildingContext(3_500))
                {
                    house.Walls.Add(new Wall(new Point(0, 0), new Point(6_000, 0)));
                    house.Walls.Add(new Wall(new Point(0, 0), new Point(0, 4_000)));
                }

                // Ground floor 3000
                house.Walls.Add(new Wall(new Point(5_000, 0), new Point(5_000, 4_000)));
            }

            Console.WriteLine(house.ToString());
        }
    }
}
