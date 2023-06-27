using Autofac;
using System.Reflection;

namespace DesignPatterns.Observer.ExamplesDeclarativeEventSubscriptionsWithInterfaces
{
    public interface IEvent
    {

    }

    public interface ISend<TEvent> where TEvent : IEvent
    {
        event EventHandler<TEvent> Sender;
    }

    public interface IHandle<TEvent> where TEvent : IEvent
    {
        void Handle(object sender, TEvent args);
    }

    public class  ButtonPressedEvent : IEvent
    {
        public int NumberOfClicks;
    }

    public class Button : ISend<ButtonPressedEvent>
    {
        public event EventHandler<ButtonPressedEvent> Sender;

        public void Fire(int clicks)
        {
            Sender?.Invoke(this, new ButtonPressedEvent
            {
                NumberOfClicks = clicks
            });
        }
    }

    public class Logging : IHandle<ButtonPressedEvent>
    {
        public void Handle(object sender, ButtonPressedEvent args)
        {
            Console.WriteLine($"Button clicked {args.NumberOfClicks} times");
        }
    }

    public class DeclarativeEventSubscriptionsWithInterfaces
    {
        public static void Start(string[] args)
        {
            var cb = new ContainerBuilder();
            var ass = Assembly.GetExecutingAssembly();

            cb.RegisterAssemblyTypes(ass)
                .AsClosedTypesOf(typeof(ISend<>))
                .SingleInstance();

            cb.RegisterAssemblyTypes(ass)
                .Where(t => t.GetInterfaces()
                    .Any(i =>
                        i.IsGenericType &&
                        i.GetGenericTypeDefinition() == typeof(IHandle<>)
                    ))
                .OnActivated(act =>
                {
                    // IHandle<Type>
                    var instanceType = act.Instance.GetType();
                    var interfaces = instanceType.GetInterfaces();
                    foreach (var i in interfaces)
                    {
                        if (i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IHandle<>))
                        {
                            // IHandle<Type>
                            var arg0 = i.GetGenericArguments()[0];
                            // ISend<Type> construct
                            var senderType = typeof(ISend<>).MakeGenericType(arg0);
                            // Every single ISend<Type> in container
                            // IEnumerable<IType> -> every instance of IType
                            var allSenderTypes = typeof(IEnumerable<>)
                                .MakeGenericType(senderType);
                            // IENumerable<ISend<Type>>
                            var allServices = act.Context.Resolve(allSenderTypes);
                            foreach (var service in (IEnumerable<object>)allServices)
                            {
                                var eventInfo = service.GetType().GetEvent("Sender");
                                var handleMethod = instanceType.GetMethod("Handle");
                                var handler = Delegate.CreateDelegate(
                                    eventInfo.EventHandlerType, null, handleMethod);

                                eventInfo.AddEventHandler(service, handler);
                            }
                        }
                    }
                })
                .SingleInstance()
                .AsSelf();

            using var container = cb.Build();
            var button = container.Resolve<Button>();
            var logging = container.Resolve<Logging>();

            button.Fire(1);
            button.Fire(2);
        }
    }
}
