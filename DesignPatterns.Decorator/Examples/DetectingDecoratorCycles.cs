using System;
using System.Linq;
using System.Text;

namespace DesignPatterns.Decorator.Examples.DetectingDecoratorCycles
{
    public abstract class Shape
    {
        public abstract string AsString();
    }

    public class Circle : Shape
    {
        private float _radius;

        public Circle(float radius)
        {
            _radius = radius;
        }

        public float Radius => _radius;

        public override string AsString() => $"A circle with a radius {_radius}";

        public void Resize(float factor) => _radius *= factor;
    }

    // 2 classes
    // Foo, Foo<T> : Foo

    public abstract class ShapeDecorator : Shape // Non-Generic
    {
        protected internal readonly List<Type> types = new();
        protected internal Shape shape;

        public ShapeDecorator(Shape shape)
        {
            this.shape = shape;
            if (shape is ShapeDecorator sd) // Recursive
                types.AddRange(sd.types);
        }
    }

    public abstract class ShapeDecorator<TSelf, TCyclePolicy> : ShapeDecorator // Generic
        where TCyclePolicy : ShapeDecoratorCyclePolicy, new()
    {
        protected readonly TCyclePolicy policy = new();

        protected ShapeDecorator(Shape shape) : base(shape)
        {
            if (policy.TypeAdditionalAllowed(typeof(TSelf), types))
                types.Add(typeof(TSelf));
        }
    }

    public class ShapeDecoratorWithPolicy<TSelf> : ShapeDecorator<TSelf, ThrowOnCyclePolicy>
    {
        public ShapeDecoratorWithPolicy(Shape shape) : base(shape)
        {
        }

        public override string AsString() => string.Empty;
    }

    public class Square : Shape
    {
        private float _side;

        public Square(float side)
        {
            _side = side;
        }
        public float Side => _side;

        public override string AsString() => $"A square with a radius {_side}";
    }

    public class ColoredShape :
        ShapeDecorator<ColoredShape, AbsorbCyclePolicy>
    //ShapeDecorator<ColoredShape, ThrowOnCyclePolicy>
    //ShapeDecorator<ColoredShape, CyclesAllowedPolicy>
    //ShapeDecoratorWithPolicy<ColoredShape>
    {
        private Shape _shape;
        private string _color;

        public ColoredShape(Shape shape, string color) : base(shape)
        {
            _shape = shape;
            _color = color;
        }

        public Shape Shape => _shape;
        public string Color => _color;

        public override string AsString()
        {
            var sb = new StringBuilder(_shape.AsString());
            // Types initialized
            // types[0] -> current
            // types[1..] -> what it wraps
            if (policy.ApplicationAllowed(typeof(ColoredShape), types.Skip(1).ToList()))
                sb.Append($" has the color {_color}");

            return sb.ToString();
        }
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

    public abstract class ShapeDecoratorCyclePolicy
    {
        /// <param name="type">Type we are currently trying to apply</param>
        /// <param name="allTypes">All types that are already applied</param>
        public abstract bool TypeAdditionalAllowed(Type type, IList<Type> allTypes);

        /// <param name="type">Type we are currently trying to apply</param>
        /// <param name="allTypes">All types that are already applied</param>
        public abstract bool ApplicationAllowed(Type type, IList<Type> allTypes);
    }

    #region Policies

    public class ThrowOnCyclePolicy : ShapeDecoratorCyclePolicy
    {
        private bool handler(Type type, IList<Type> allTypes)
        {
            if (allTypes.Contains(type))
                throw new InvalidOperationException(
                    $"Cycle detected! Type is already a {type.FullName}");
            return true;
        }

        public override bool ApplicationAllowed(Type type, IList<Type> allTypes) => handler(type, allTypes);

        public override bool TypeAdditionalAllowed(Type type, IList<Type> allTypes) => handler(type, allTypes);
    }

    public class CyclesAllowedPolicy : ShapeDecoratorCyclePolicy
    {
        private bool handler(Type type, IList<Type> allTypes) => true;

        public override bool ApplicationAllowed(Type type, IList<Type> allTypes) => handler(type, allTypes);

        public override bool TypeAdditionalAllowed(Type type, IList<Type> allTypes) => handler(type, allTypes);
    }

    public class AbsorbCyclePolicy : ShapeDecoratorCyclePolicy
    {
        public override bool ApplicationAllowed(Type type, IList<Type> allTypes) => !allTypes.Contains(type);
        public override bool TypeAdditionalAllowed(Type type, IList<Type> allTypes) => true;
    }

    #endregion

    public class DetectingDecoratorCycles
    {
        public static void Start(string[] args)
        {
            var circle = new Circle(2);
            var colored1 = new ColoredShape(circle, "red");
            var colored2 = new ColoredShape(colored1, "blue");
            Console.WriteLine(colored2.AsString());
        }
    }
}
