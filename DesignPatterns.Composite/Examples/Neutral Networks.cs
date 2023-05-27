using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Composite.Examples
{
    public static class NeuronExtensionsMethods
    {
        public static void Connect(this IEnumerable<Neuron> self, IEnumerable<Neuron> other)
        {
            if(ReferenceEquals(self, other)) return;

            foreach(Neuron from in self)
            {
                foreach(Neuron to in other)
                {
                    from.Out.Add(to);
                    to.In.Add(from);
                }
            }
        }
    }

    public class Neuron : IEnumerable<Neuron>
    {
        public float value;
        public List<Neuron> In = new(), Out = new();

        public IEnumerator<Neuron> GetEnumerator()
        {
            yield return this;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class NeuronLayer : Collection<Neuron>
    {

    }

    public class NeuronRing : List<Neuron>
    {

    }

    public class Neutral_Networks
    {
        public static void Start(string[] args)
        {
            var neuron1 = new Neuron();
            var neuron2 = new Neuron();
            neuron1.Connect(neuron2);

            var layer1 = new NeuronLayer();
            var layer2 = new NeuronLayer();

            neuron1.Connect(layer1);
            layer1.Connect(layer2);
        }
    }
}
