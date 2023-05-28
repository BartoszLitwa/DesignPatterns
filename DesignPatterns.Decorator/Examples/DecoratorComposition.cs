using System;
using System.Linq;

namespace DesignPatterns.Decorator.Examples
{
    public interface IShape
    {
        string AsString();
    }

    public class Circle : IShape
    {
        private float _radius;

        public Circle(float radius)
        {
            _radius = radius;
        }

        public float Radius => _radius;

        public string AsString() => $"A circle with a radius {_radius}";

        public void Resize(float factor) => _radius *= factor;
    }

    public class Square : IShape
    {
        private float _side;

        public Square(float side)
        {
            _side = side;
        }
        public float Side => _side;

        public string AsString() => $"A square with a radius {_side}";
    }

    public class ColoredShape : IShape
    {
        private IShape _shape;
        private string _color;

        public ColoredShape(IShape shape, string color)
        {
            _shape = shape;
            _color = color;
        }

        public IShape Shape => _shape;
        public string Color => _color;

        public string AsString() => $"{_shape.AsString()} has the color {_color}";
    }

    public class TransparentShape : IShape
    {
        private IShape _shape;
        private float _transparency;

        public TransparentShape(IShape shape, float transparency)
        {
            _shape = shape;
            _transparency = transparency;
        }

        public string AsString() => $"{_shape.AsString()} has the transparency {_transparency * 100}%";

        public IShape Shape => _shape;
        public float Transparency => _transparency;
    }

    public class DecoratorComposition
    {
        public static void Start(string[] args)
        {
            var square = new Square(12.3f);
            Console.WriteLine(square.AsString());

            var redSquare = new ColoredShape(square, "red");
            Console.WriteLine(redSquare.AsString());

            var transparencySquare = new TransparentShape(redSquare, 0.5f);
            Console.WriteLine(transparencySquare.AsString());
        }
    }
}
