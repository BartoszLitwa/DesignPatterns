using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Proxy.Examples
{
    public class Creature
    {
        public byte Age;
        public int X, Y;
    }

    public class Creatures
    {
        private readonly int _size;
        private byte[] age;
        private int[] x, y;

        public Creatures(int size)
        {
            _size = size;
            age = new byte[size];
            x = new int[size];
            y = new int[size];
        }

        public struct CreatureProxy // Placeholder
        {
            readonly Creatures creatures;
            readonly int index;

            public CreatureProxy(Creatures creatures, int index)
            {
                this.creatures = creatures;
                this.index = index;
            }

            public ref byte Age => ref creatures.age[index];
            public ref int X => ref creatures.x[index];
            public ref int Y => ref creatures.y[index];
        }

        public IEnumerator<CreatureProxy> GetEnumerator()
        {
            for (int pos = 0; pos < _size; pos++)
                yield return new CreatureProxy(this, pos);
        }
    }

    public class CompositeProxy_SoA_Aos // Structure of Arrays / Array of Structures
    {
        public static void Start(string[] args)
        {
            var creatures = new Creature[100]; // AoS
                                               // Age X Y _ Age X Y _ Age X Y

            for (int i = 0; i < creatures.Length; i++)
            {
                creatures[i] = new();
                creatures[i].X++;
            }
            // Better for performance
            // CPU doesnt have to jump in memory
            var creatures2 = new Creatures(100); // SoA
            // Age Age Age Age
            // X X X X
            // Y Y Y Y
            foreach (Creatures.CreatureProxy c in creatures2)
            {
                c.X++;
            }
        }
    }
}
