using System;
using System.Linq;

namespace DesignPatterns.Proxy.Examples.PropertyProxy
{
    public class Property<T> where T : new()
    {
        private T _value;

        public T Value
        {
            get => _value;
            set
            {
                if (Equals(this._value, value))
                    return;
                Console.WriteLine($"Assigning new value: {value}");
                this._value = value;
            }
        }

        public Property() : this(Activator.CreateInstance<T>()) { }

        public Property(T value)
        {
            this._value = value;
        }

        public static implicit operator T(Property<T> property) => property.Value; // int n = p_int;

        public static implicit operator Property<T>(T value) => new Property<T>(value); // Property<int> p = 123;

        public bool Equals(Property<T> other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Equals(_value, other._value);
        }

        public override bool Equals(object obj)
        {
            if (obj is Property<T> property)
            {
                return Equals(property);
            }

            return base.Equals(obj);
        }

        public override int GetHashCode() => _value.GetHashCode();
    }

    public class Creature
    {
        private Property<int> _agility = new();

        public int Agility // to use the setter instead of implicit operator
        {
            get => _agility.Value;
            set => _agility.Value = value;
        }
    }

    public class PropertyProxy
    {
        public static void Start(string[] args)
        {
            var c = new Creature();
            c.Agility = 10; 
            c.Agility = 10; 
            c.Agility = 10;
        }
    }
}
