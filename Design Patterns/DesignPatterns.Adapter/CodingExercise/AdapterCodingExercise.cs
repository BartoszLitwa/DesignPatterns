﻿using System;
using System.Linq;

namespace DesignPatterns.Adapter.CodingExercise
{
    public class Square
    {
        public int Side;
    }

    public interface IRectangle
    {
        int Width { get; }
        int Height { get; }
    }

    public static class ExtensionMethods
    {
        public static int Area(this IRectangle rc)
        {
            return rc.Width * rc.Height;
        }
    }

    public class SquareToRectangleAdapter : IRectangle
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public SquareToRectangleAdapter(Square square)
        {
            this.Width = this.Height = square.Side;
        }
    }
}
