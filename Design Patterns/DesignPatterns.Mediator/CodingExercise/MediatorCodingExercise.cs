using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Mediator.CodingExercise
{
    public class Participant
    {
        public int Value { get; set; }

        private readonly Mediator _mediator;

        public Participant(Mediator mediator)
        {
            _mediator = mediator;
            _mediator.participants.Add(this);
        }

        public void Say(int n)
        {
            _mediator.Increment(n, this);
        }
    }

    public class Mediator
    {
        public List<Participant> participants = new List<Participant>();

        public Mediator()
        {
            
        }

        public void Increment(int n, Participant say)
        {
            foreach(var p in participants)
                if(!p.Equals(say))
                    p.Value += n;
        }
    }

    public class MediatorCodingExercise
    {
        public static void Start(string[] args)
        {

        }
    }
}
