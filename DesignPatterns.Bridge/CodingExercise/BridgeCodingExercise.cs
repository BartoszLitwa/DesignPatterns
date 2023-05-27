using System;
using System.Linq;

namespace DesignPatterns.Bridge.CodingExercise
{
    public interface IRenderer
    {
        string WhatToRenderAs { get; }
    }

    public class VectorRenderer : IRenderer
    {
        public string WhatToRenderAs => "Drawing {0} as lines";
    }

    public class RasterRenderer : IRenderer
    {
        public string WhatToRenderAs => "Drawing {0} as pixels";
    }

    public abstract class Shape
    {
        public string Name { get; set; }
        protected IRenderer renderer { get; private set; }

        public Shape(IRenderer renderer)
        {
            this.renderer = renderer;
        }

        public override string ToString()
        {
            return string.Format(renderer.WhatToRenderAs, Name);
        }
    }

    public class Triangle : Shape
    {
        public Triangle(IRenderer renderer) : base(renderer)
        {
            Name = "Triangle";
        }
    }

    public class Square : Shape
    {
        public Square(IRenderer renderer) : base(renderer)
        {
            Name = "Square";
        }
    }
}
