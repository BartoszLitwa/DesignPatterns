using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.SOLID.Principles
{
    public class LiskovSubstitution
    {
        public class Rectangle
        {
            public virtual int Width { get; set; }
            public virtual int Height { get; set; }

            public Rectangle()
            {

            }

            public Rectangle(int width, int height)
            {
                Width = width;
                Height = height;
            }


            public override string ToString()
            {
                return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
            }
        }

        public class Square : Rectangle
        {
            public override int Height { set => base.Height = base.Width = value; }
            public override int Width { set => base.Height = base.Width = value; }
        }

        public static int Area(Rectangle rc) => rc.Width * rc.Height;
        
        public static void Start(string[] args)
        {
            var rc = new Rectangle(2, 3);
            Console.WriteLine($"{rc} has area {Area(rc)}");

            Rectangle sq = new Square();
            sq.Width = 4;
            Console.WriteLine($"{sq} has area {Area(sq)}");
        }
    }
}
