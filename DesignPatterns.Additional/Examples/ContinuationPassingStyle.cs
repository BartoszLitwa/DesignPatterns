using System.Numerics;

namespace DesignPatterns.Additional.Examples.ContinuationPassingStyle
{
    public enum WorkflowResult
    {
        Success, Failure
    }

    public class QuadraticEquationSolver
    {
        // ax^2 + bx + c == 0
        public WorkflowResult Start(double a, double b, double c, out Tuple<Complex, Complex> result)
        {
            var disc = b * b - 4 * a * c;
            if (disc < 0)
            {
                //result = SolveComplex(a, b, disc);
                result = null;
                return WorkflowResult.Failure;
            }
            else
            {
                return SolveSimple(a, b, disc, out result);
            }
        }

        private WorkflowResult SolveComplex(double a, double b, double disc,
            out Tuple<Complex, Complex> result)
        {
            var rootDisc = Complex.Sqrt(new Complex(disc, 0));
            result = Tuple.Create(
                (-b + rootDisc) / (2 * a),
                (-b - rootDisc) / (2 * a)
            );
            return WorkflowResult.Failure;
        }

        private WorkflowResult SolveSimple(double a, double b, double disc,
            out Tuple<Complex, Complex> result)
        {
            var rootDisc = Math.Sqrt(disc);
            result = Tuple.Create(
                new Complex((-b + rootDisc) / (2 * a), 0),
                new Complex((-b - rootDisc) / (2 * a), 0)
            );
            return WorkflowResult.Success;
        }
    }

    public class ContinuationPassingStyle
    {
        public static void Start(string[] args)
        {
            var solver = new QuadraticEquationSolver();
            Tuple<Complex, Complex> solution;
            var flag = solver.Start(1, 10, 16, out solution);
        }
    }
}
