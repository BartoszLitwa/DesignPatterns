public class Event
{
    // Backtrack
}

public class AgeChangedEvent : Event
{
    public Person Target;
    public int OldValue, NewValue;

    public AgeChangedEvent(Person target, int oldValue, int newValue)
    {
        Target = target;
        OldValue = oldValue;
        NewValue = newValue;
    }

    public override string ToString()
    {
        return $"Changed from {nameof(OldValue)}: {OldValue} to {nameof(NewValue)}: {NewValue}";
    }
}