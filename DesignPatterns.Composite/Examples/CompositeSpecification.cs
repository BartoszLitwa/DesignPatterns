using System;
using System.Linq;

namespace DesignPatterns.Composite.Examples
{
    public enum Color
    {
        Red, Green, Blue
    }

    public enum Size
    {
        Small, Medium, Large, Huge
    }

    public class Product
    {
        public string Name;
        public Color Color;
        public Size Size;

        public Product(string name, Color color, Size size)
        {
            Name = name;
            Color = color;
            Size = size;
        }
    }

    public class ProductFilter
    {
        public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
        {
            foreach (var p in products)
                if (p.Size == size)
                    yield return p;
        }

        public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
        {
            foreach (var p in products)
                if (p.Color == color)
                    yield return p;
        }
    }

    public abstract class Specification<T>
    {
        public abstract bool IsSatisfied(T t);

        public static Specification<T> operator &(Specification<T> left, Specification<T> right)
        {
            return new AndSpecification<T>(left, right);
        }
    }

    public abstract class CompositeSpecification<T> : Specification<T>
    {
        protected readonly Specification<T>[] items;

        public CompositeSpecification(params Specification<T>[] items)
        {
            this.items = items;
        }
    }

    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, Specification<T> spec);
    }

    public class ColorSpecification : Specification<Product>
    {
        private Color _color;

        public ColorSpecification(Color color)
        {
            _color = color;
        }

        public override bool IsSatisfied(Product t) => t.Color == _color;
    }

    public class SizeSpecification : Specification<Product>
    {
        private Size _size;

        public SizeSpecification(Size size)
        {
            _size = size;
        }

        public override bool IsSatisfied(Product t) => t.Size == _size;
    }

    // Combinator
    public class AndSpecification<T> : CompositeSpecification<T>
    {
        public AndSpecification(params Specification<T>[] items) : base(items) { }

        public override bool IsSatisfied(T t) => items.All(x => x.IsSatisfied(t));
    }

    public class OrSpecification<T> : CompositeSpecification<T>
    {
        public OrSpecification(params Specification<T>[] items) : base(items) { }

        public override bool IsSatisfied(T t) => items.Any(x => x.IsSatisfied(t));
    }

    public class BetterFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, Specification<Product> spec)
        {
            foreach (var t in items)
                if (spec.IsSatisfied(t))
                    yield return t;
        }
    }

    public class CompositeSpecification
    {
        public static void Start(string[] args)
        {
            var apple = new Product("Apple", Color.Green, Size.Small);
            var tree = new Product("Tree", Color.Green, Size.Large);
            var house = new Product("House", Color.Blue, Size.Huge);

            Product[] products = { apple, tree, house };
            var pf = new ProductFilter();
            Console.WriteLine("Green products (old): ");
            foreach (var p in pf.FilterByColor(products, Color.Green))
            {
                Console.WriteLine($" - {p.Name} is green");
            }

            var bf = new BetterFilter();
            Console.WriteLine("Green products (new): ");
            foreach (var p in bf.Filter(products, new ColorSpecification(Color.Green)))
            {
                Console.WriteLine($" - {p.Name} is green");
            }

            Console.WriteLine("Large blue items: ");
            foreach (var p in bf.Filter(products,
                new AndSpecification<Product>(
                    new ColorSpecification(Color.Blue),
                    new SizeSpecification(Size.Large))
                ))
            {
                Console.WriteLine($" - {p.Name} is big and blue");
            }
        }
    }
}
