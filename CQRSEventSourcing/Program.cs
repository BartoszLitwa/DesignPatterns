// CQRS = Command Query Responsibility Segragation
// CQS = Command Query Separation

// Command = do/change
//  Query = query data

var eb = new EventBroker();
var p = new Person(eb);

eb.Command(new ChangeAgeCommand(p, 123));

foreach(var ev in eb.AllEvents)
    Console.WriteLine(ev);

int age;
age = eb.Query<int>(new AgeQuery { Target = p });
Console.WriteLine(age);

eb.UndoLast();

foreach (var ev in eb.AllEvents)
    Console.WriteLine(ev);

age = eb.Query<int>(new AgeQuery { Target = p });
Console.WriteLine(age);

Console.WriteLine();

// Real World implementation
public class PersonStorage
{
    Dictionary<int, Person> people;
}