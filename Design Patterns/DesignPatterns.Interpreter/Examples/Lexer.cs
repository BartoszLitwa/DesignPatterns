using System.Text;

namespace DesignPatterns.Interpreter.Examples
{
    public class Lexer
    {
        public static List<Token> Lex(string input)
        {
            var result = new List<Token>();
            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsWhiteSpace(input[i]))
                    continue;

                result.Add(input[i] switch
                {
                    '+' => new Token(Token.Type.Plus, "+"),
                    '-' => new Token(Token.Type.Minus, "-"),
                    '(' => new Token(Token.Type.LParen, "("),
                    ')' => new Token(Token.Type.RParen, ")"),
                    _ => Digit(input, ref i)
                });
            }
            return result;
        }

        private static Token Digit(string input, ref int index)
        {
            var sb = new StringBuilder();
            for (; index < input.Length; index++)
            {
                if (char.IsDigit(input[index]))
                    sb.Append(input[index]);
                else
                {
                    index--;
                    return new Token(Token.Type.Interger, sb.ToString());
                }
            }
            return null;
        }
    }
}
