using System.Numerics;

namespace DesignPatterns.Strategy.CodingExercise.StrategyCodingExercise
{
    public interface IDiscriminantStrategy
    {
        double CalculateDiscriminant(double a, double b, double c);
    }

    public class OrdinaryDiscriminantStrategy : IDiscriminantStrategy
    {
        // todo
        public double CalculateDiscriminant(double a, double b, double c)
        {
            var discriminant = b * b - 4 * a * c;
            return discriminant;
        }
    }

    public class RealDiscriminantStrategy : IDiscriminantStrategy
    {
        // todo (return NaN on negative discriminant!)
        public double CalculateDiscriminant(double a, double b, double c)
        {
            var discriminant = b * b - 4 * a * c;
            return discriminant < 0 ? double.NaN : discriminant;
        }
    }

    public class QuadraticEquationSolver
    {
        private readonly IDiscriminantStrategy strategy;

        public QuadraticEquationSolver(IDiscriminantStrategy strategy)
        {
            this.strategy = strategy;
        }

        public Tuple<Complex, Complex> Solve(double a, double b, double c)
        {
            var disc = new Complex(strategy.CalculateDiscriminant(a, b, c), 0);
            var rootDisc = Complex.Sqrt(disc);
            var plusX = (-b + rootDisc) / (2 * a);
            var minusX = (-b - rootDisc) / (2 * a);
            return new Tuple<Complex, Complex>(minusX, plusX);
        }
    }

    public class StrategyCodingExercise
    {
    }
}
