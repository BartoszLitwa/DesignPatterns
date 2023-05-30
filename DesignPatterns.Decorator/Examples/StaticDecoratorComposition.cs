using System;
using System.Linq;

namespace DesignPatterns.Decorator.Examples.StaticDecoratorComposition
{
    public abstract class Shape
    {
        public abstract string AsString();
    }

    public class Circle : Shape
    {
        private float _radius;

        public Circle() : this(0f) { }

        public Circle(float radius)
        {
            _radius = radius;
        }

        public float Radius1 { 
            get => _radius;
            set => _radius = value;
        }

        public override string AsString() => $"A circle with a radius {_radius}";

        public void Resize(float factor) => _radius *= factor;
    }

    public class Square : Shape
    {
        private float _side;

        public Square() : this(0f) { }

        public Square(float side)
        {
            _side = side;
        }
        public float Side => _side;

        public override string AsString() => $"A square with a radius {_side}";
    }

    public class ColoredShape : Shape
    {
        private Shape _shape;
        private string _color;

        public ColoredShape(Shape shape, string color)
        {
            _shape = shape;
            _color = color;
        }

        public Shape Shape => _shape;
        public string Color => _color;

        public override string AsString() => $"{_shape.AsString()} has the color {_color}";
    }

    public class TransparentShape : Shape
    {
        private Shape _shape;
        private float _transparency;

        public TransparentShape(Shape shape, float transparency)
        {
            _shape = shape;
            _transparency = transparency;
        }

        public override string AsString() => $"{_shape.AsString()} has the transparency {_transparency * 100}%";

        public Shape Shape => _shape;
        public float Transparency => _transparency;
    }

    public class ColoredShape<T> : Shape where T : Shape, new()
    {
        private string _color;
        private T _shape = new();

        public ColoredShape() : this("black") { }

        public ColoredShape(string color)
        {
            _color = color;
        }

        public override string AsString() => $"{_shape.AsString()} has the color {_color}";
    }

    public class TransparentShape<T> : Shape where T : Shape, new()
    {
        private float _transparency;
        private T _shape = new();

        public TransparentShape() : this(0f) { }

        public TransparentShape(float transparency)
        {
            _transparency = transparency;
        }

        public override string AsString() => $"{_shape.AsString()} has the transparency {_transparency * 100}%";
    }

    public class StaticDecoratorComposition
    {
        public static void Start(string[] args)
        {
            var redSquare = new ColoredShape<Square>("Red");
            Console.WriteLine(redSquare.AsString());

            var circle = new TransparentShape<ColoredShape<Circle>>(0.4f);
            Console.WriteLine(circle.AsString());
        }
    }
}
