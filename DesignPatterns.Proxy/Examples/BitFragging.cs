using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Proxy.Examples
{
    public enum Operations : byte
    {
        [Description("*")]
        Multipilication = 0,
        [Description("/")]
        Division = 1,
        [Description("+")]
        Add = 2,
        [Description("-")]
        Subtractaction = 3,
    }

    // Operations -> name
    public static class OperationsImplementation
    {
        static OperationsImplementation()
        {
            var type = typeof(Operations);
            foreach (Operations op in Enum.GetValues(type))
            {
                MemberInfo[] memInfo = type.GetMember(op.ToString());
                if (memInfo.Length > 0)
                {
                    var attrs = memInfo[0].GetCustomAttributes(
                      typeof(DescriptionAttribute), false);

                    if (attrs.Length > 0)
                    {
                        opNames[op] = ((DescriptionAttribute)attrs[0]).Description[0];
                    }
                }
            }
        }

        private static readonly Dictionary<Operations, char> opNames
          = new Dictionary<Operations, char>();

        // notice the data types!
        private static readonly Dictionary<Operations, Func<double, double, double>> opImpl =
          new Dictionary<Operations, Func<double, double, double>>()
          {
              [Operations.Multipilication] = ((x, y) => x * y),
              [Operations.Division] = ((x, y) => x / y),
              [Operations.Add] = ((x, y) => x + y),
              [Operations.Subtractaction] = ((x, y) => x - y),
          };

        public static double Call(this Operations op, int x, int y) => opImpl[op](x, y);

        public static char Name(this Operations op) => opNames[op];
    }

    public class TwoBitSet
    {
        // 64 bits -> 32 values
        private readonly ulong data;

        public TwoBitSet(ulong data)
        {
            this.data = data;
        }

        public byte this[int index]
        {
            get
            {
                // 00 10 01 01
                int shift = index << 1; // Multiply by 2
                ulong mask = (0b11U << shift); // 00 11 00 00
                return (byte)((data & mask) >> shift); // 00 10 00 00 -> 00 00 00 10
            }
        }
    }

    public class Problem
    {
        // 1 3 5 7
        // Add Mul Add
        private readonly List<int> numbers;
        private readonly List<Operations> operations;

        public Problem(IEnumerable<int> numbers, IEnumerable<Operations> operations)
        {
            this.numbers = new List<int>(numbers);
            this.operations = new List<Operations>(operations);
        }

        public int Eval()
        {
            var opGroups = new[]
            {
                new[] { Operations.Multipilication, Operations.Division},
                new[] { Operations.Add, Operations.Subtractaction}
            };
            startAgain:
            foreach(var group in opGroups)
            {
                for(int i = 0; i < operations.Count; i++)
                {
                    if (group.Contains(operations[i]))
                    {
                        var result = operations[i].Call(numbers[i], numbers[i + 1]);

                        if (result != (int)result) // Fractionval value
                            return int.MinValue;

                        numbers[i] = (int)result;
                        numbers.RemoveAt(i + 1);
                        operations.RemoveAt(i);
                        if (numbers.Count == 1)
                            return numbers[0];

                        goto startAgain;
                    }
                }
            }

            return numbers[0];
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < operations.Count; ++i)
            {
                sb.Append(numbers[i])
                    .Append(operations[i].Name());
            }

            sb.Append(numbers[operations.Count]);
            return sb.ToString();
        }
    }

    public class BitFragging
    {
        public static void Start(string[] args)
        {
            var numbers = new[] { 1, 3, 5, 7 };
            var numberOfOps = numbers.Length - 1;

            for(int result = 0; result <= 10; ++result)
            {
                for(var key = 0UL; key < (1UL << 2 * numberOfOps); ++key)
                {
                    var tbs = new TwoBitSet(key);
                    var ops = Enumerable.Range(0, numberOfOps)
                        .Select(i => tbs[i])
                        .Cast<Operations>().ToArray();

                    var problem = new Problem(numbers, ops);
                    if(problem.Eval() == result)
                    {
                        Console.WriteLine($"{new Problem(numbers, ops)} = {result}");
                        break;
                    }
                }
            }
        }
    }
}
