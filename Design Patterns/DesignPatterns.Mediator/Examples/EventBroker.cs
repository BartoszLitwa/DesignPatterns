using Autofac;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace DesignPatterns.Mediator.Examples
{
    public class Actor
    {
        protected EventBroker broker;

        public Actor(EventBroker broker)
        {
            this.broker = broker;
        }
    }

    public class FootballPlayer : Actor
    {
        public string Name { get; set; }
        public int GoalsScored { get; set; } = 0;
        private IDisposable _scoredSub;
        private IDisposable _sentOffSub;

        public FootballPlayer(EventBroker broker, string name) : base(broker)
        {
            Name = name;

            _scoredSub = broker.OfType<PlayerScoredEvent>()
                .Where(ps => !ps.Name.Equals(Name))
                .Subscribe(pe =>
                {
                    Console.WriteLine($"{Name}: Nicely done, {pe.Name}! It's your {pe.GoalsScored}. goal.");
                });

            _sentOffSub = broker.OfType<PlayerSentOffEvent>()
                .Where(ps => !ps.Name.Equals(Name))
                .Subscribe(pe =>
                {
                    Console.WriteLine($"{Name}: See you ine the lockers, {pe.Name}");
                });
        }

        public void Score()
        {
            GoalsScored++;
            broker.Publish(new PlayerScoredEvent { Name = Name, GoalsScored = GoalsScored });
        }

        public void AssaultReferee()
        {
            broker.Publish(new PlayerSentOffEvent { Name = Name, Reason = "violence" });
            _scoredSub.Dispose();
            _sentOffSub.Dispose();
        }
    }

    public class FootballCoach : Actor
    {
        public FootballCoach(EventBroker broker) : base(broker)
        {
            broker.OfType<PlayerScoredEvent>()
                .Subscribe(pe =>
                {
                    if (pe.GoalsScored < 3)
                        Console.WriteLine($"Coach: Well Done, {pe.Name}!");
                });

            broker.OfType<PlayerSentOffEvent>()
                .Subscribe(pe =>
                {
                    if(pe.Reason.ToLower() == "violence")
                        Console.WriteLine($"Coach: How could you, {pe.Name}?");
                });
        }
    }

    public class PlayerEvent
    {
        public string Name { get; set; }
    }

    public class PlayerScoredEvent : PlayerEvent
    {
        public int GoalsScored { get; set; }
    }

    public class PlayerSentOffEvent : PlayerEvent
    {
        public string Reason { get; set; }
    }

    public class EventBroker : IObservable<PlayerEvent>
    {
        private Subject<PlayerEvent> _subscriptions = new Subject<PlayerEvent>();

        public IDisposable Subscribe(IObserver<PlayerEvent> observer) => _subscriptions.Subscribe(observer);

        public void Publish(PlayerEvent playerEvent) => _subscriptions.OnNext(playerEvent);
    }

    public class EventBrokerExample
    {
        public static void Start(string[] args)
        {
            var cb = new ContainerBuilder();
            cb.RegisterType<EventBroker>().SingleInstance();
            cb.RegisterType<FootballCoach>();
            cb.Register((c, p) => 
                new FootballPlayer(
                    c.Resolve<EventBroker>(),
                    p.Named<string>("name")
                ));

            using var c = cb.Build();
            var coach = c.Resolve<FootballCoach>();
            var p1 = c.Resolve<FootballPlayer>(new NamedParameter("name", "John"));
            var p2 = c.Resolve<FootballPlayer>(new NamedParameter("name", "Chris"));

            p1.Score();
            p1.Score();
            p1.Score(); // Ignored
            p1.AssaultReferee();
            p2.Score();
        }
    }
}
