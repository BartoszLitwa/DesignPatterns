using System;
using System.Collections;
using System.Linq;

namespace DesignPatterns.Composite.CodingExercise
{
    public interface IValueContainer : IEnumerable<int>
    {
    }

    public class SingleValue : IValueContainer
    {
        public int Value;

        public IEnumerator<int> GetEnumerator()
        {
            yield return Value;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class ManyValues : List<int>, IValueContainer
    {
    }

    public static class ExtensionMethods
    {
        public static int Sum(this IEnumerable<IValueContainer> containers)
        {
            int result = 0;
            foreach (var c in containers)
                foreach (var i in c)
                    result += i;
            return result;
        }
    }
}
