using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Proxy.Examples.ValueProxy
{
    [DebuggerDisplay("{value * 100.0f}%")]
    public struct Percentage
    {
        private readonly float value;

        internal Percentage(float value)
        {
            this.value = value;
        }

        public static float operator *(float f, Percentage p) => f * p.value;
        public static int operator *(int f, Percentage p) => (int)(f * p.value);

        public static Percentage operator +(Percentage a, Percentage b) => new Percentage(a.value + b.value);

        public override string ToString() => $"{value * 100.0f}%";

        public bool Equals(Percentage other) => value.Equals(other.value);
        public override bool Equals(object obj)
        {
            if (obj is Percentage percentage)
            {
                return Equals(percentage);
            }

            return base.Equals(obj);
        }
        public override int GetHashCode() => value.GetHashCode();
    }

    public static class PercentageExtensions
    {
        public static Percentage Percent(this int value) => new Percentage(value / 100.0f);
        public static Percentage Percent(this float value) => new Percentage(value / 100.0f);
    }

    public class ValueProxy
    {
        public static void Start(string[] args)
        {
            Console.WriteLine(10f * 5.Percent());
            Console.WriteLine(2.Percent() + 3.Percent());
        }
    }
}
