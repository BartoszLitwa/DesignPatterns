using System.Text;

namespace DesignPatterns.Decorator.Examples
{
    public class MyStringBuilder
    {
        private StringBuilder builder = new StringBuilder();

        public static implicit operator MyStringBuilder(string s)
        {
            var msb = new MyStringBuilder();
            msb.Append(s);
            return msb;
        }

        public static MyStringBuilder operator +(MyStringBuilder msb, string s)
        {
            msb.Append(s);
            return msb;
        }

        public MyStringBuilder Append(bool value)
        {
            builder.Append(value);
            return this;
        }
        public MyStringBuilder Append(string value)
        {
            builder.Append(value);
            return this;
        }
        public MyStringBuilder AppendFormat(IFormatProvider provider, string format, object arg0)
        {
            builder.AppendFormat(provider, format, arg0);
            return this;
        }
        public MyStringBuilder AppendJoin(char separator, object[] values)
        {
            builder.AppendJoin(separator, values);
            return this;
        }
        public MyStringBuilder AppendLine()
        {
            builder.AppendLine();
            return this;
        }
        public MyStringBuilder AppendLine(string? value)
        {
            builder.AppendLine(value);
            return this;
        }
        public MyStringBuilder Clear()
        {
            builder.Clear();
            return this;
        }
        public void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
        {
            builder.CopyTo(sourceIndex, destination, destinationIndex, count);
        }
        public int EnsureCapacity(int capacity)
        {
            return builder.EnsureCapacity(capacity);
        }
        public bool Equals(ReadOnlySpan<char> span)
        {
            return builder.Equals(span);
        }
        public MyStringBuilder Insert(int index, bool value)
        {
            builder.Insert(index, value);
            return this;
        }
        public MyStringBuilder Remove(int startIndex, int length)
        {
            builder.Remove(startIndex, length);
            return this;
        }
        public MyStringBuilder Replace(char oldChar, char newChar)
        {
            builder.Replace(oldChar, newChar);
            return this;
        }
        public string ToString(int startIndex, int length)
        {
            return builder.ToString(startIndex, length);
        }

        public int Capacity
        {
            get => builder.Capacity;

            set => builder.Capacity = value;
        }

        public int Length
        {
            get => builder.Length;

            set => builder.Length = value;
        }

        public int MaxCapacity => builder.MaxCapacity;

        public override string ToString() => builder.ToString();
    }


    public class AdapterDecorator
    {
        public static void Start(string[] args)
        {
            MyStringBuilder s = "Hello ";
            s += "World";
            Console.WriteLine(s);
        }
    }
}
