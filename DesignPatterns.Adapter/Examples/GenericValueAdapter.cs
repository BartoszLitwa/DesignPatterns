using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Adapter.Examples
{
    public interface IInteger
    {
        int Value { get; }
    }

    public static class Dimensions
    {
        public class Two : IInteger
        {
            public int Value => 2;
        }

        public class Three : IInteger
        {
            public int Value => 3;
        }
    }

    public class Vector<TSelf, Type, Dimension>
        where TSelf : Vector<TSelf, Type, Dimension>, new()
        where Dimension : IInteger, new()
    {
        protected Type[] Data;

        public Vector()
        {
            Data = new Type[new Dimension().Value];
        }

        public Vector(params Type[] values)
        {
            var requiredSize = new Dimension().Value;
            Data = new Type[requiredSize];

            var providedSize = values.Length;
            for(int i = 0; i < Math.Min(requiredSize, providedSize); ++i)
            {
                Data[i] = values[i];
            }
        }

        public static TSelf Create(params Type[] values)
        {
            var result = new TSelf();
            var requiredSize = new Dimension().Value;
            result.Data = new Type[requiredSize];

            var providedSize = values.Length;
            for (int i = 0; i < Math.Min(requiredSize, providedSize); ++i)
            {
                result.Data[i] = values[i];
            }

            return result;
        }

        public Type this[int index]
        {
            get => Data[index];
            set => Data[index] = value;
        }

        public Type X
        {
            get => Data[0];
            set => Data[0] = value;
        }
    }

    // In-Between Class
    public class VectorOfInt<TSelf, D> : Vector<TSelf, int, D>
        where TSelf : Vector<TSelf, int, D>, new()
        where D : IInteger, new()
    {
        public VectorOfInt() { }

        public VectorOfInt(params int[] values) : base(values) { }

        public static VectorOfInt<TSelf, D> operator+(VectorOfInt<TSelf, D> left, VectorOfInt<TSelf, D> right)
        {
            var result = new VectorOfInt<TSelf, D>();
            var size = new D().Value;
            for(int i = 0; i < size; ++i)
            {
                result[i] = left[i] + right[i];
            }

            return result;
        }
    }

    // In-Between Class
    public class VectorOfFloat<TSelf, D> : Vector<TSelf, float, D>
        where TSelf : Vector<TSelf, float, D>, new()
        where D : IInteger, new()
    {
        public VectorOfFloat() { }

        public VectorOfFloat(params float[] values) : base(values) { }

        public static VectorOfFloat<TSelf, D> operator +(VectorOfFloat<TSelf, D> left, VectorOfFloat<TSelf, D> right)
        {
            var result = new VectorOfFloat<TSelf, D>();
            var size = new D().Value;
            for (int i = 0; i < size; ++i)
            {
                result[i] = left[i] + right[i];
            }

            return result;
        }
    }

    public class Vector2i : VectorOfInt<Vector2i, Dimensions.Two>
    {
        public Vector2i() { }

        public Vector2i(params int[] values) : base(values) { }
    }

    public class Vector3f : VectorOfFloat<Vector3f, Dimensions.Three>
    {
        public override string ToString() => string.Join(',', Data);
    }

    public class GenericValueAdapter
    {
        public static void Start(string[] args)
        {
            var v = new Vector2i(1, 2);
            v[0] = 0;

            var vv = new Vector2i(3, 2);

            var result = v + vv;

            Vector3f v3 = Vector3f.Create(3.5f, 2f, 3f);
        }
    }
}
