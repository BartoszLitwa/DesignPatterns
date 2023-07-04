using DesignPatterns.Visitor.CodingExercise;
using DesignPatterns.Visitor.Examples.AcyclicVisitor;
using DesignPatterns.Visitor.Examples.ClassicVisitorDoubleDispatch;
using DesignPatterns.Visitor.Examples.DynamicVisitorViaTheDLR;
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

Console.WriteLine($"\n{nameof(DynamicVisitorViaTheDLR)}\n");
DynamicVisitorViaTheDLR.Start(args);

Console.WriteLine($"\n{nameof(AcyclicVisitor)}\n");
AcyclicVisitor.Start(args);

Console.WriteLine($"\n{nameof(VisitorCodingExercise)}\n");
VisitorCodingExercise.Start(args);