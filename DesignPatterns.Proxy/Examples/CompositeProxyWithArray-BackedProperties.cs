using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Proxy.Examples
{
    public class MasonrySettings
    {
        public bool? All
        {
            get
            {
                if (Pillars == Walls && Walls == Floors)
                    return Pillars;
                return null;
            }
            set
            {
                if (!value.HasValue) return;
                Pillars = value.Value;
                Walls = value.Value;
                Floors = value.Value;
            }
        }

        public bool Pillars, Walls, Floors;
    }

    public class BetterMansorySettings
    {
        private bool[] flags = new bool[3];

        public bool? All
        {
            get
            {
                if (flags.Skip(1).All(f => f == flags[0]))
                    return flags[0];
                return null;
            }
            set
            {
                if(!value.HasValue) return;
                for (int i = 0; i < flags.Length; i++)
                {
                    flags[i] = value.Value;
                }
            }
        }

        public bool Pillars
        {
            set => flags[0] = value;
            get => flags[0];
        }
        public bool Walls
        {
            set => flags[1] = value;
            get => flags[1];
        }
        public bool Floors
        {
            set => flags[2] = value;
            get => flags[2];
        }
    } 

    public class CompositeProxyWithArray_BackedProperties
    {
        public static void Start(string[] args)
        {

        }
    }
}
