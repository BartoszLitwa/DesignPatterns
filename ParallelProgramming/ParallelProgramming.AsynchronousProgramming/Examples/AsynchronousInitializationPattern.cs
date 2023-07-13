namespace ParallelProgramming.AsynchronousProgramming.Examples.AsynchronousInitializationPattern
{
    public interface IAsyncInit
    {
        Task InitTask { get; }
    }

    public class MyClass : IAsyncInit
    {
        public MyClass()
        {
            InitTask = InitAsync();
        }

        public Task InitTask { get; }

        private async Task InitAsync()
        {
            await Task.Delay(1000);
        }
    }

    public class MyOtherClass : IAsyncInit
    {
        private readonly MyClass myClass;

        public MyOtherClass(MyClass myClass)
        {
            this.myClass = myClass;
            InitTask = InitAsync();
        }

        public Task InitTask { get; }

        private async Task InitAsync()
        {
            if (myClass is IAsyncInit ai)
                await ai.InitTask;

            await Task.Delay(1000);
        }
    }

    public class AsynchronousInitializationPattern
    {
        public static async Task Start(string[] args)
        {
            var myClass = new MyClass();
            var oc = new MyOtherClass(myClass);
            if (oc is IAsyncInit ai)
                await oc.InitTask;
        }
    }
}
