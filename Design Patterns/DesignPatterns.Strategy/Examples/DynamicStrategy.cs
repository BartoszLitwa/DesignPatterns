using System.Text;

namespace DesignPatterns.Strategy.Examples.DynamicStrategy
{
    public enum OutputFormat
    {
        Markdown,
        Html
    }

    public interface IListStrategy
    {
        void Start(StringBuilder sb);
        void End(StringBuilder sb);
        void AddListItem(StringBuilder sb, string item);
    }

    public class HtmlListStrategy : IListStrategy
    {
        public void AddListItem(StringBuilder sb, string item) => sb.AppendLine($"  <li>{item}</li>");

        public void End(StringBuilder sb) => sb.AppendLine("</ul>");

        public void Start(StringBuilder sb) => sb.AppendLine("<ul>");
    }

    public class  MarkdownListStrategy : IListStrategy
    {
        public void AddListItem(StringBuilder sb, string item) => sb.AppendLine($" * {item}");

        public void End(StringBuilder sb) { }

        public void Start(StringBuilder sb) { }
    }

    public class TextProcessor
    {
        private StringBuilder sb = new StringBuilder();
        private IListStrategy listStrategy;

        public void SetOutputFormat(OutputFormat format)
        {
            listStrategy = format switch
            {
                OutputFormat.Markdown => new MarkdownListStrategy(),
                OutputFormat.Html => new HtmlListStrategy(),
                _ => throw new ArgumentOutOfRangeException(nameof(format)),
            };
        }

        public void AppendList(IEnumerable<string> items)
        {
            listStrategy.Start(sb);
            foreach(var item in items)
                listStrategy.AddListItem(sb, item);
            listStrategy.End(sb);
        }

        public override string ToString() => sb.ToString();

        public StringBuilder Clear() => sb.Clear();

    }

    public class DynamicStrategy
    {
        public static void Start(string[] args)
        {
            var tp = new TextProcessor();
            tp.SetOutputFormat(OutputFormat.Markdown);
            tp.AppendList(new[] {"foo", "var", "bar"});
            Console.WriteLine(tp);

            tp.Clear();
            tp.SetOutputFormat(OutputFormat.Html);
            tp.AppendList(new[] { "foo", "var", "bar" });
            Console.WriteLine(tp);
        }
    }
}
