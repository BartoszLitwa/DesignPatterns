namespace DesignPatterns.Observer.Examples.ObserverViaTheEventKeyword
{
    public class FallsIllEventArgs
    {
        public string Address;
    }

    public class Person
    {
        public void CatchACold()
        { // Have to check for null - no subscribers = null reference
            FallsIll?.Invoke(this, new FallsIllEventArgs { Address = "123 London Road"});
        }

        public event EventHandler<FallsIllEventArgs> FallsIll;
    }

    public class ObserverViaTheEventKeyword
    {
        public static void Start(string[] args)
        {
            var person = new Person();

            person.FallsIll += CallDoctor;

            person.CatchACold();

            person.FallsIll -= CallDoctor;
        }

        private static void CallDoctor(object? sender, FallsIllEventArgs e)
        {
            Console.WriteLine($"Doctor has been called to {e.Address}");
        }
    }
}
