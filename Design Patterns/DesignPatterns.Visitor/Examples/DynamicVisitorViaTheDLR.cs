using System.Text;

namespace DesignPatterns.Visitor.Examples.DynamicVisitorViaTheDLR;

public abstract class Expression
{
}

public class DoubleExpression : Expression
{
    public readonly double Value;

    public DoubleExpression(double value)
    {
        Value = value;
    }
}

public class AdditionExpression : Expression
{
    public readonly Expression Left, Right;

    public AdditionExpression(Expression left, Expression right)
    {
        Left = left;
        Right = right;
    }
}

public class ExpressionPrinter
{
    public void Print(DoubleExpression ae, StringBuilder sb)
    {
        sb.Append(ae.Value);
    }
    
    public void Print(AdditionExpression ae, StringBuilder sb)
    {
        sb.Append('(');
        Print((dynamic)ae.Left, sb);
        sb.Append('+');
        Print((dynamic)ae.Right, sb);
        sb.Append(')');
    }
}

public class DynamicVisitorViaTheDLR
{
    public static void Start(string[] args)
    {
        // 1 + 2
        var expr = new AdditionExpression(
            new DoubleExpression(1),
            new AdditionExpression(
                new DoubleExpression(2),
                new DoubleExpression(3)
            ));

        var ep = new ExpressionPrinter();
        var sb = new StringBuilder();
        ep.Print(expr, sb);
        Console.WriteLine(sb);
    }
}