using DesignPatterns.Visitor.Examples.ClassicVisitorDoubleDispatch;
using DesignPatterns.Visitor.Examples.IntrusiveVisitor;
using DesignPatterns.Visitor.Examples.ReductionsAndTransforms;
using DesignPatterns.Visitor.Examples.ReflectiveVisitor;

Console.WriteLine($"\n{nameof(IntrusiveVisitor)}\n");
IntrusiveVisitor.Start(args);

Console.WriteLine($"\n{nameof(ReflectiveVisitor)}\n");
ReflectiveVisitor.Start(args);

Console.WriteLine($"\n{nameof(ClassicVisitorDoubleDispatch)}\n");
ClassicVisitorDoubleDispatch.Start(args);

Console.WriteLine($"\n{nameof(ReductionsAndTransforms)}\n");
ReductionsAndTransforms.Start(args);