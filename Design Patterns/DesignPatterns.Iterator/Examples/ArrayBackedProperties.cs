using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Iterator.Examples.ArrayBackedProperties
{
    public class Creature : IEnumerable<int>
    {
        private int[] _stats = new int[3];

        private const int _strength = 0;
        public int Strength { get => _stats[_strength]; set => _stats[_strength] = value; }

        private const int _agility = 1;
        public int Agility { get => _stats[_agility]; set => _stats[_agility] = value; }

        private const int _intelligence = 2;
        public int Intelligence { get => _stats[_intelligence]; set => _stats[_intelligence] = value; }

        public double AvgStat => _stats.Average();
        public double MinStat => _stats.Min();
        public double MaxStat => _stats.Max();

        public IEnumerator<int> GetEnumerator() => _stats.AsEnumerable().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int this[int index]
        {
            get => _stats[index];
            set => _stats[index] = value;
        }
    }

    public class ArrayBackedProperties
    {
        public static void Start(string[] args)
        {

        }
    }
}
