using System.Text;

namespace DesignPatterns.Visitor.Examples.ClassicVisitorDoubleDispatch
{
    using DictType = Dictionary<Type, Action<Expression, StringBuilder>>;

    public interface IExpressionVisitor
    {
        void Visit(DoubleExpression de);
        void Visit(AdditionExpression ae);
    }

    public abstract class Expression
    {
        public abstract void Accept(IExpressionVisitor visitor);
    }

    public class DoubleExpression : Expression
    {
        public double Value;

        public DoubleExpression(double value)
        {
            Value = value;
        }

        public override void Accept(IExpressionVisitor visitor) =>
            // double dispatch
            visitor.Visit(this);
    }

    public class AdditionExpression : Expression
    {
        public Expression Left, Right;

        public AdditionExpression(Expression left, Expression right)
        {
            this.Left = left;
            this.Right = right;
        }

        public override void Accept(IExpressionVisitor visitor) => visitor.Visit(this);
    }

    public class ExpressionPrinter : IExpressionVisitor
    {
        private StringBuilder sb = new StringBuilder();

        public void Visit(DoubleExpression de)
        {
            sb.Append(de.Value);
        }

        public void Visit(AdditionExpression ae)
        {
            sb.Append("(");
            ae.Left.Accept(this);
            sb.Append("+");
            ae.Right.Accept(this);
            sb.Append(")");
        }

        public override string ToString() => sb.ToString();
    }

    public class ExpressionCalculator : IExpressionVisitor
    {
        public double Result;

        public void Visit(DoubleExpression de)
        {
            Result = de.Value;
        }

        public void Visit(AdditionExpression ae)
        {
            ae.Left.Accept(this);
            var a = Result;
            ae.Right.Accept(this);
            var b = Result;
            Result = a + b;
        }
    }

    public class ClassicVisitorDoubleDispatch
    {
        public static void Start(string[] args)
        {
            var e = new AdditionExpression(
                new DoubleExpression(1),
                new AdditionExpression(
                    new DoubleExpression(2),
                    new DoubleExpression(3)
                ));

            var ep = new ExpressionPrinter();
            ep.Visit(e);
            Console.WriteLine(ep);

            var calc = new ExpressionCalculator();
            calc.Visit(e);
            Console.WriteLine($"{ep} = {calc.Result}");
        }
    }
}
