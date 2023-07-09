namespace ParallelProgramming.ParallelLINQ.Examples.CustomAggregation;

public class CustomAggregation
{
    public static void Start(string[] args)
    {
        // var sum = Enumerable.Range(1, 1000).Sum();

        // var sum = Enumerable.Range(1, 1000)
        //     .Aggregate(0, (i, accumulator) => i + accumulator);

        var sum = ParallelEnumerable.Range(1, 1000)
            .Aggregate(
                0,
                (partialSum, i) => partialSum += i, // update Accumulator
                (total, subTotal) => total += subTotal, // combine Accumulator
                i => i // result
            );

        Console.WriteLine($"Sum = {sum}");
    }
}