using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Builder.CodingExercise
{
    public class ObjectToDisplay
    {
        public string Name { get; set; }
        public int Indentation { get; set; }
        public Dictionary<string, string> Fields { get; set; } = new Dictionary<string, string>();
    }

    public abstract class FunctionalBuilder<TSubject, TSelf>
        where TSubject : new ()
        where TSelf : FunctionalBuilder<TSubject, TSelf>
    {
        private readonly List<Func<TSubject, TSubject>> actions = new();

        public TSelf Do(Action<TSubject> action) => AddAction(action);

        public TSubject Build() => actions.Aggregate(new TSubject(), (self, func) => func(self));

        private TSelf AddAction(Action<TSubject> action)
        {
            actions.Add(x =>
            {
                action(x);
                return x;
            });

            return (TSelf)this;
        }
    }

    public class CodeBuilder : FunctionalBuilder<ObjectToDisplay, CodeBuilder>
    {
        public CodeBuilder(string name)
        {
            Do(x => x.Name = name);
            Do(x => x.Indentation = 2);
        }

        public CodeBuilder AddField(string fieldName, string fieldType) => Do(x => x.Fields.Add(fieldName, fieldType));

        public override string ToString()
        {
            var build = this.Build();
            var sb = new StringBuilder();

            sb.Append("public class ").AppendLine(build.Name);
            sb.AppendLine("{");
            foreach (var field in build.Fields)
            {
                var indetation = new string(' ', build.Indentation);
                sb.Append(indetation).Append("public ")
                    .Append(field.Value).Append(' ')
                    .Append(field.Key).AppendLine(";");
            }
            sb.AppendLine("}");

            return sb.ToString();
        }

        public static void Start(string[] args)
        {
            var cb = new CodeBuilder("Person").AddField("Name", "string").AddField("Age", "int");
            Console.WriteLine(cb);
        }
    }
}
