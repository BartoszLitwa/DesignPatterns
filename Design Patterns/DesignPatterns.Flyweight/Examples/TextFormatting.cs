using System;
using System.Linq;
using System.Text;

namespace DesignPatterns.Flyweight.Examples
{
    public class FormattedText
    {
        private readonly string plainText;
        private bool[] capitalize;

        public FormattedText(string plainText)
        {
            this.plainText = plainText;
            capitalize = new bool[plainText.Length];
        }

        public void Capitalize(int start, int end)
        {
            for (int i = start; i <= end; i++)
                capitalize[i] = true;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < plainText.Length; i++)
            {
                char c = plainText[i];
                sb.Append(capitalize[i] ? char.ToUpper(c) : c);
            }
            return sb.ToString();
        }
    }

    public class BetterFormattedText
    {
        private string plainText;
        private List<TextRange> formatting = new();

        public BetterFormattedText(string plainText)
        {
            this.plainText = plainText;
        }

        public TextRange GetRange(int start, int end)
        {
            var range = new TextRange(start, end);
            formatting.Add(range);
            return range;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < plainText.Length; i++)
            {
                var c = plainText[i];
                for (int f = 0; f < formatting.Count; f++)
                {
                    TextRange? range = formatting[f];
                    sb.Append(range.Capitalize && range.Covers(i) ? char.ToUpper(c) : c);
                }
            }
            return sb.ToString();
        }

        public class TextRange
        {
            public int Start, End;
            public bool Capitalize, Bold, Italic;

            public TextRange(int start, int end)
            {
                Start = start;
                End = end;
            }

            public bool Covers(int pos) => pos >= Start && pos <= End;
        }

    }

    public class TextFormatting
    {
        public static void Start(string[] args)
        {
            var ft = new FormattedText("This is a brave new world");
            ft.Capitalize(10, 15);
            Console.WriteLine(ft);

            var bft = new BetterFormattedText("This is a brave new world");
            bft.GetRange(10, 15).Capitalize = true;
            Console.WriteLine(bft);
        }
    }
}
