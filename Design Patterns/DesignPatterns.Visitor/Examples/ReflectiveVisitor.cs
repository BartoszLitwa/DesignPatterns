using System.Text;

namespace DesignPatterns.Visitor.Examples.ReflectiveVisitor
{
    using DictType = Dictionary<Type, Action<Expression, StringBuilder>>;

    public abstract class Expression
    {
    }

    public class DoubleExpression : Expression
    {
        public double Value;

        public DoubleExpression(double value)
        {
            Value = value;
        }
    }

    public class AdditionExpression : Expression
    {
        public Expression Left, Right;

        public AdditionExpression(Expression left, Expression right)
        {
            this.Left = left;
            this.Right = right;
        }
    }

    public static class ExpressionPrinter
    {
        private static DictType actions = new()
        {
            [typeof(DoubleExpression)] = (e, sb) =>
            {
                var de = (DoubleExpression)e;
                sb.Append(de.Value);
            },
            [typeof(AdditionExpression)] = (e, sb) =>
            {
                var ae = (AdditionExpression)e;
                sb.Append("(");
                Print(ae.Left, sb);
                sb.Append("+");
                Print(ae.Right, sb);
                sb.Append(")");
            }
        };

        public static void Print(Expression e, StringBuilder sb)
        {
            actions[e.GetType()](e, sb);
        }

        //public static void Print(Expression e, StringBuilder sb)
        //{
        //    if(e is DoubleExpression de)
        //    {
        //        sb.Append(de.Value);
        //    }
        //    else if (e is AdditionExpression ae)
        //    {
        //        sb.Append("(");
        //        Print(ae.Left, sb);
        //        sb.Append("+");
        //        Print(ae.Right, sb);
        //        sb.Append(")");
        //    }
        //}
    }

    public class ReflectiveVisitor
    {
        public static void Start(string[] args)
        {
            var e = new AdditionExpression(
                new DoubleExpression(1),
                new AdditionExpression(
                    new DoubleExpression(2),
                    new DoubleExpression(3)
                ));

            var sb = new StringBuilder();
            ExpressionPrinter.Print(e, sb);
            Console.WriteLine(sb.ToString());
        }
    }
}
