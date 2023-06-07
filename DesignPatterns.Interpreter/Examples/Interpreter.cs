using System;
using System.Linq;
using System.Text;

namespace DesignPatterns.Interpreter.Examples
{

    public class Interpreter
    {
        public static void Start(string[] args)
        {
            // ( 13 + 4 ) - ( 12 + 1 )
            string input = "(13 + 4)-(12 + 1)";
            var tokens = Lexer.Lex(input);
            Console.WriteLine(string.Join("\t", tokens));

            var parsed = Parser.Parse(tokens);
            Console.WriteLine($"{input} = {parsed.Value}");
        }
    }
}
