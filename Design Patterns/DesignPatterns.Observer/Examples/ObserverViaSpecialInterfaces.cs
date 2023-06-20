using System.Reactive.Linq;

namespace DesignPatterns.Observer.Examples.ObserverViaSpecialInterfaces
{
    public class Event
    {

    }

    public class FallsIllEvent : Event
    {
        public string Address;
    }

    public class Person : IObservable<Event>
    {
        private readonly HashSet<Subscription> subscriptions = new();

        public IDisposable Subscribe(IObserver<Event> observer)
        {
            var sub = new Subscription(this, observer);
            subscriptions.Add(sub);
            return sub;
        }

        public void FallIll()
        {
            var val = new FallsIllEvent { Address = "123 London Road" };
            foreach(var s in subscriptions)
            {
                s.Observer.OnNext(val);
            }
        }

        private class Subscription : IDisposable
        {
            private readonly Person person;
            public readonly IObserver<Event> Observer;

            public Subscription(Person person, IObserver<Event> observer)
            {
                Observer = observer;
                this.person = person;
            }

            public void Dispose()
            {
                person.subscriptions.Remove(this);
            }
        }
    }

    public class ObserverViaSpecialInterfaces : IObserver<Event>
    {
        public static void Start(string[] args)
        {
            new ObserverViaSpecialInterfaces();
        }

        public ObserverViaSpecialInterfaces()
        {
            var person = new Person();

            person.OfType<FallsIllEvent>()
                .Subscribe(args => Console.WriteLine($"Reactive Extension doctor is required at {args.Address}"));

            IDisposable sub = person.Subscribe(this);
            person.FallIll();
        }

        public void OnCompleted() { }

        public void OnError(Exception error) { }

        public void OnNext(Event value)
        {
            if (value is FallsIllEvent args)
            {
                Console.WriteLine($"A doctor is required at {args.Address}");
            }
        }
    }
}
