namespace ParallelProgramming.AsynchronousProgramming.Examples.AsyncFactoryMethod;

public class Foo
{
    private Foo()
    {
        
    }

    private async Task<Foo> InitAsync()
    {
        await Task.Delay(1000);
        return this;
    }

    public static async Task<Foo> CreateAsync()
    {
        var result = new Foo();
        return await result.InitAsync();
    }
}

public class AsyncFactoryMethod
{
    public static async Task Start(string[] args)
    {
        Foo foo = await Foo.CreateAsync();
    }
}