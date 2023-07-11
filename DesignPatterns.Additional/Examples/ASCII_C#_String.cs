using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace DesignPatterns.Additional.Examples.ASCII_C__String;

// ascii utf-16
public class str : IEquatable<str>, IEquatable<string>
{
    [NotNull] protected readonly byte[] buffer;

    public str()
    {
        buffer = Array.Empty<byte>();
    }

    public str(string s)
    {
        buffer = Encoding.ASCII.GetBytes(s);
    }

    protected str(byte[] buffer)
    {
        this.buffer = buffer;
    }

    public override string ToString() => Encoding.ASCII.GetString(buffer);

    public static implicit operator str(string s) => new(s);

    public bool Equals(str other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return ((IStructuralEquatable)buffer).Equals(other.buffer, StructuralComparisons.StructuralEqualityComparer);
    }

    public bool Equals(string? other) => other == ToString();

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((str)obj);
    }

    public override int GetHashCode() => ToString().GetHashCode();

    public static str operator +(str first, str second)
    {
        var bytes = new byte[first.buffer.Length + second.buffer.Length];
        first.buffer.CopyTo(bytes, 0);
        second.buffer.CopyTo(bytes, first.buffer.Length);
        return new str(bytes);
    }

    public char this[int index]
    {
        get => (char)buffer[index];
        set => buffer[index] = (byte)value;
    }
}

public class ASCII_C__String
{
    public static void Start(string[] args)
    {
        
    }
}