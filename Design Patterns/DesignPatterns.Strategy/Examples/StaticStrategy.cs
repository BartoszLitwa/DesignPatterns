using System.Text;

namespace DesignPatterns.Strategy.Examples.StaticStrategy
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

    public class MarkdownListStrategy : IListStrategy
    {
        public void AddListItem(StringBuilder sb, string item) => sb.AppendLine($" * {item}");

        public void End(StringBuilder sb) { }

        public void Start(StringBuilder sb) { }
    }

    public class TextProcessor<LS>
        where LS : IListStrategy, new()
    {
        private StringBuilder sb = new StringBuilder();
        private IListStrategy listStrategy = new LS();

        public void AppendList(IEnumerable<string> items)
        {
            listStrategy.Start(sb);
            foreach (var item in items)
                listStrategy.AddListItem(sb, item);
            listStrategy.End(sb);
        }

        public override string ToString() => sb.ToString();

        public StringBuilder Clear() => sb.Clear();

    }

    public class StaticStrategy
    {
        public static void Start(string[] args)
        {
            var tp = new TextProcessor<MarkdownListStrategy>();
            tp.AppendList(new[] { "foo", "var", "bar" });
            Console.WriteLine(tp);

            var tp2 = new TextProcessor<HtmlListStrategy>();
            tp2.AppendList(new[] { "foo", "var", "bar" });
            Console.WriteLine(tp2);
        }
    }
}
