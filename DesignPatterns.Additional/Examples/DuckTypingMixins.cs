using System.Collections;

namespace DesignPatterns.Additional.Examples.DuckTypingMixins;

ref struct Foo
{
    public void Dispose()
    {
        Console.WriteLine("Disposing Foo");
    }
}

public interface IMyDisposable<T> : IDisposable
{
    void IDisposable.Dispose()
    {
        Console.WriteLine($"Disposing {typeof(T).Name}");
    }
}

public class MyClass : IMyDisposable<MyClass>
{
    
}


public interface IScalar<T> : IEnumerable<T>
{
    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        yield return (T)this;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class MyScalarClass : IScalar<MyScalarClass>
{
    public override string ToString() => $"{nameof(MyScalarClass)}";
}

public class DuckTypingMixins
{
    public static void Start(string[] args)
    {
        // duck typing
        
        // GetEnumerator() - foreach (IEnumerable<T>)
        // Dispose() - using (IDisposable)
        using var foo = new Foo();

        // mixin
        using var mc = new MyClass();

        var msc = new MyScalarClass();
        foreach (var x in msc)
            Console.WriteLine(x);
    }
}