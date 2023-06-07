namespace DesignPatterns.Interpreter.Examples
{
    public interface IElement
    {
        int Value { get; }
    }

    public class Integer : IElement
    {
        private int _value;

        public Integer(int value)
        {
            _value = value;
        }

        public int Value => _value;
    }

    public class BinaryOperation : IElement
    {
        public enum Type
        {
            Addition, Subtraction
        }

        public Type MyType;
        public IElement Left, Right;

        public int Value
        {
            get
            {
                return MyType switch
                {
                    Type.Addition => Left.Value + Right.Value,
                    Type.Subtraction => Left.Value - Right.Value,
                    _ => throw new ArgumentOutOfRangeException(nameof(MyType)),
                };
            }
        }
    }
}
