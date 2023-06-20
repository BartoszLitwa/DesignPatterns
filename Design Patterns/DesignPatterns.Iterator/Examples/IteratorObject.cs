namespace DesignPatterns.Iterator.Examples
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
    }

    public class InOrderIterator<T>
    {
        private readonly Node<T> _root;
        private bool _yieldedStart;
        public Node<T> Current;

        public InOrderIterator(Node<T> root) 
        {
            _root = root;
            Current = root;
            while(Current.Left != null)
                Current = Current.Left;
        }

        public bool MoveNext()
        {
            if (!_yieldedStart)
            {
                _yieldedStart = true;
                return true;
            }

            if(Current.Right != null)
            {
                Current = Current.Right;
                while(Current.Left != null)
                    Current = Current.Left;
                return true;
            } else
            {
                var p = Current.Parent;
                while(p != null && Current == p.Right)
                {
                    Current = p;
                    p = p.Parent;
                }
                Current = p;
                return Current != null;
            }
        }

        public void Reset()
        {
            Current = _root;
            _yieldedStart = false;
        }
    }

    public class IteratorObject
    {
        public static void Start(string[] args)
        {
            //  1       in-order: 213
            // / \
            //2   3

            var root = new Node<int>(1, new Node<int>(2), new Node<int>(3));

            var it = new InOrderIterator<int>(root);
            while (it.MoveNext())
            {
                Console.Write(it.Current.Value);
                Console.Write(',');
            }
            Console.WriteLine();
        }
    }
}
