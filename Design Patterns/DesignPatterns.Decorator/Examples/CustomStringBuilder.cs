﻿using System;
using System.Linq;
using System.Text;

namespace DesignPatterns.Decorator.Examples
{
    public class CodeBuilder
    {
        private StringBuilder builder = new StringBuilder();

        public CodeBuilder Append(bool value)
        {
            builder.Append(value);
            return this;
        }
        public CodeBuilder AppendFormat(IFormatProvider provider, string format, object arg0)
        {
            builder.AppendFormat(provider, format, arg0);
            return this;
        }
        public CodeBuilder AppendJoin(char separator, object[] values)
        {
            builder.AppendJoin(separator, values);
            return this;
        }
        public CodeBuilder AppendLine()
        {
            builder.AppendLine();
            return this;
        }
        public CodeBuilder AppendLine(string? value)
        {
            builder.AppendLine(value);
            return this;
        }
        public CodeBuilder Clear()
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
        public CodeBuilder Insert(int index, bool value)
        {
            builder.Insert(index, value);
            return this;
        }
        public CodeBuilder Remove(int startIndex, int length)
        {
            builder.Remove(startIndex, length);
            return this;
        }
        public CodeBuilder Replace(char oldChar, char newChar)
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

    public class CustomStringBuilder
    {
        public static void Start(string[] args)
        {
            var cb = new CodeBuilder();
            cb.AppendLine("class Foo")
                .AppendLine("{")
                .AppendLine("}");

            Console.WriteLine(cb);
        }
    }
}
