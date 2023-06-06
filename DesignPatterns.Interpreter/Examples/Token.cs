using System;
using System.Linq;

namespace DesignPatterns.Interpreter.Examples
{
    public class Token
    {
        public enum Type
        {
            Interger, Plus, Minus, LParen, RParen
        }

        public Type MyType;
        public string Text;

        public Token(Type myType, string text)
        {
            MyType = myType;
            Text = text;
        }

        public override string ToString()
        {
            return $"`{Text}`";
        }
    }
}
