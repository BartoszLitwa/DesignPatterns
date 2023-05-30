using System;
using System.Linq;
using System.Text;

namespace DesignPatterns.Flyweight.CodingExercise
{
    public class Sentence
    {
        private string plainText;
        private List<WordToken> tokens;

        public Sentence(string plainText)
        {
            this.plainText = plainText;
            tokens = plainText.Split(' ')
                .Select((token, index) => new WordToken { Index = index })
                .ToList();
        }

        public WordToken this[int index]
        {
            get
            {
                return tokens[index];
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            var split = plainText.Split(' ');
            for (int i = 0; i < split.Length; i++)
            {
                sb.Append(this[i].Capitalize ? split[i].ToUpper() : split[i])
                    .Append(' ');
            }
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        public class WordToken
        {
            public int Index { get; set; }
            public bool Capitalize { get; set; }
        }
    }

    public class FlyweightCodingExercise
    {
        public static void Start(string[] args)
        {
            var sentence = new Sentence("hello world");
            sentence[1].Capitalize = true;
            Console.WriteLine(sentence); // writes "hello WORLD"
        }
    }
}
