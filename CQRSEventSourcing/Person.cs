public class Person
{
    public int UniqueId;
    private int age;
    private EventBroker broker;

    public Person(EventBroker broker)
    {
        this.broker = broker;
        this.broker.Commands += BrokerOnCommands;
        this.broker.Queries += BrokerOnQueries;
    }

    private void BrokerOnCommands(object sender, Command command)
    {
        var cac = command as ChangeAgeCommand;
        if(cac is not null && cac.Target == this)
        {
            if(command.Register)
                broker.AllEvents.Add(new AgeChangedEvent(this, age, cac.Age));

            age = cac.Age;
        }
    }

    private void BrokerOnQueries(object sender, Query query)
    {
        var ac = query as AgeQuery;
        if(ac is not null && ac.Target == this)
        {
            ac.Result = age;
        }
    }

    public bool CanVote => age > 18;
}