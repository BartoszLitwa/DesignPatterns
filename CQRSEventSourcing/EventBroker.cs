public class EventBroker
{
    // 1. All events that happend.
    public IList<Event> AllEvents = new List<Event>();
    // 2. Commands
    public event EventHandler<Command> Commands;
    // 3. Query
    public event EventHandler<Query> Queries;

    public void Command(Command c)
    {
        Commands?.Invoke(this, c);
    }

    public T Query<T>(Query q)
    {
        Queries?.Invoke(this, q);
        return (T)q.Result;
    }

    public void UndoLast()
    {
        var e = AllEvents.LastOrDefault();
        var ac = e as AgeChangedEvent;
        if(ac is not null)
        {
            Command(new ChangeAgeCommand(ac.Target, ac.OldValue) { Register = false });
            AllEvents.Remove(e);
        }
    }
}