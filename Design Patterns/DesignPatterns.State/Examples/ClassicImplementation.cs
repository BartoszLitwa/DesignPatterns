namespace DesignPatterns.State.Examples.ClassicImplementation
{
    public class Switch
    {
        public State State = new OffState();

        public void On() { State.On(this); }
        public void Off() { State.Off(this); }
    }

    public abstract class State
    {
        public virtual void On(Switch sw)
        {
            Console.WriteLine("Light is already on.");
        }

        public virtual void Off(Switch sw)
        {
            Console.WriteLine("Light is already off");
        }
    }

    public class OnState : State
    {
        public OnState() => Console.WriteLine("Light turned on.");

        public override void Off(Switch sw)
        {
            Console.WriteLine("Turning light off...");
            sw.State = new OffState();
        }
    }

    public class OffState : State
    {
        public OffState() => Console.WriteLine("Light turned on.");

        public override void On(Switch sw)
        {
            Console.WriteLine("Turning light on...");
            sw.State = new OnState();
        }
    }

    public class ClassicImplementation
    {
        public static void Start(string[] args)
        {
            var sw = new Switch();
            sw.On();
            sw.Off();
            sw.Off();
        }
    }
}
