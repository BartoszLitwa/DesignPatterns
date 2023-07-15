public class Command : EventArgs
{
    public bool Register = true;
}

public class ChangeAgeCommand : Command
{
    public Person Target; // ?
    public int TargetId; // Real-world
    public int Age;

    public ChangeAgeCommand(Person target, int age)
    {
        Target = target;
        Age = age;
    }
}