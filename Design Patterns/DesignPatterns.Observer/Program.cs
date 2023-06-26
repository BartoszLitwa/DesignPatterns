using DesignPatterns.Observer.Examples;
using DesignPatterns.Observer.Examples.BidirectionalObserver;
using DesignPatterns.Observer.Examples.ObservableCollections;
using DesignPatterns.Observer.Examples.ObserverViaSpecialInterfaces;
using DesignPatterns.Observer.Examples.ObserverViaTheEventKeyword;
using DesignPatterns.Observer.Examples.PropertyDependencies;

Console.WriteLine($"{nameof(ObserverViaTheEventKeyword)}\n");
ObserverViaTheEventKeyword.Start(args);

Console.WriteLine($"{nameof(WeakEventPattern)}\n");
WeakEventPattern.Start(args);

Console.WriteLine($"{nameof(ObserverViaSpecialInterfaces)}\n");
ObserverViaSpecialInterfaces.Start(args);

Console.WriteLine($"{nameof(ObservableCollections)}\n");
ObservableCollections.Start(args);

Console.WriteLine($"{nameof(BidirectionalObserver)}\n");
BidirectionalObserver.Start(args);

Console.WriteLine($"{nameof(PropertyDependencies)}\n");
PropertyDependencies.Start(args);