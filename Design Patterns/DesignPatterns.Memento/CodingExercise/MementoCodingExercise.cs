using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Memento.CodingExercise
{
    public class Token
    {
        public int Value = 0;

        public Token(int value)
        {
            this.Value = value;
        }
    }

    public class Memento
    {
        public List<Token> Tokens = new List<Token>();
    }

    public class TokenMachine
    {
        public List<Token> Tokens = new List<Token>();

        public Memento AddToken(int value)
        {
            return AddToken(new Token(value));
        }

        public Memento AddToken(Token token)
        {
            Tokens.Add(token);
            var m = new Memento();
            m.Tokens = Copy(Tokens);
            return m;
        }

        public void Revert(Memento m)
        {
            Tokens = Copy(m);
        }

        private List<Token> Copy(List<Token> tokens)
        {
            return tokens.Select(mm => new Token(mm.Value)).ToList();
        }

        private List<Token> Copy(Memento m)
        {
            return Copy(m.Tokens);
        }
    }

    public class MementoCodingExercise
    {
        public static void Start(string[] args)
        {

        }
    }
}
