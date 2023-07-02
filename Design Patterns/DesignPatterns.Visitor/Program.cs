using DesignPatterns.Visitor.Examples.IntrusiveVisitor;
using DesignPatterns.Visitor.Examples.ReflectiveVisitor;

Console.WriteLine($"\n{nameof(IntrusiveVisitor)}\n");
IntrusiveVisitor.Start(args);

Console.WriteLine($"\n{nameof(ReflectiveVisitor)}\n");
ReflectiveVisitor.Start(args);