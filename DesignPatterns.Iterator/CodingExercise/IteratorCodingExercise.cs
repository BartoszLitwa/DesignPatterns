using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Iterator.CodingExercise
{
    public class Node<T>
    {
        public T Value;
        public Node<T> Left, Right;
        public Node<T> Parent;

        public Node(T value)
        {
            Value = value;
        }

        public Node(T value, Node<T> left, Node<T> right)
        {
            Value = value;
            Left = left;
            Right = right;

            left.Parent = right.Parent = this;
        }

        public IEnumerable<T> PreOrder
        {
            get
            {
                yield return Value;

                if (Left != null)
                {
                    foreach (var left in Left.PreOrder)
                    {
                        yield return left;
                    }
                }

                if (Right != null)
                {
                    foreach (var right in Right.PreOrder)
                    {
                        yield return right;
                    }
                }
            }
        }
    }

    public class IteratorCodingExercise
    {
        public static void Start(string[] args)
        {

        }
    }
}
