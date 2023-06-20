using DesignPatterns.Interpreter.Examples;
using System.Text;
using System.Text.RegularExpressions;

namespace DesignPatterns.Interpreter.CodingExercise
{
    public class ExpressionProcessor
    {
        public Dictionary<char, int> Variables = new Dictionary<char, int>();

        public enum NextOp
        {
            Nothing,
            Plus,
            Minus
        }

        public int Calculate(string expression)
        {
            int current = 0;
            var nextOp = NextOp.Nothing;

            var parts = Regex.Split(expression, @"(?<=[+-])");

            foreach (var part in parts)
            {
                var noOp = part.Split(new[] { "+", "-" }, StringSplitOptions.RemoveEmptyEntries);
                var first = noOp[0];
                int value, z;

                if (int.TryParse(first, out z))
                    value = z;
                else if (first.Length == 1 && Variables.ContainsKey(first[0]))
                    value = Variables[first[0]];
                else return 0;

                switch (nextOp)
                {
                    case NextOp.Nothing:
                        current = value;
                        break;
                    case NextOp.Plus:
                        current += value;
                        break;
                    case NextOp.Minus:
                        current -= value;
                        break;
                }

                if (part.EndsWith("+")) nextOp = NextOp.Plus;
                else if (part.EndsWith("-")) nextOp = NextOp.Minus;
            }
            return current;
        }
    }

    public class InterpreterCodingExercise
    {
        public static void Start(string[] args)
        {
            var ep = new ExpressionProcessor();
            ep.Variables = new()
            {
                { 'x', 3}
            };
            Console.WriteLine(ep.Calculate("1+2+3") == 6);
            Console.WriteLine(ep.Calculate("1+2+xy") == 0);
            Console.WriteLine(ep.Calculate("10-2-x") == 5);
        }
    }
}
