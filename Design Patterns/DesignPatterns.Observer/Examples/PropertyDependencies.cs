using System.ComponentModel;
using System.Linq.Expressions;

namespace DesignPatterns.Observer.Examples.PropertyDependencies
{
    public class PropertyNotificationSupport : INotifyPropertyChanged
    {
        private readonly Dictionary<string, HashSet<string>> affectedBy = new();

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            foreach(var affected in affectedBy.Keys)
            {
                if (affectedBy[affected].Contains(propertyName))
                {
                    OnPropertyChanged(affected);
                }
            }
        }

        protected Func<T> property<T>(string name, Expression<Func<T>> expr)
        {
            Console.WriteLine($"Creating computed proerty for expression {expr}");

            var visitor = new MemberAccessVisitor(GetType());
            visitor.Visit(expr);

            if (visitor.PropertyNames.Any())
            {
                if(!affectedBy.ContainsKey(name))
                    affectedBy.Add(name, new HashSet<string>());

                foreach(var propName in visitor.PropertyNames)
                {
                    if (propName != name)
                        affectedBy[name].Add(propName);
                }
            }

            return expr.Compile();
        }

        private class MemberAccessVisitor : ExpressionVisitor
        {
            private readonly Type declaringType;
            public readonly IList<string> PropertyNames = new List<string>();

            public MemberAccessVisitor(Type declaringType)
            {
                this.declaringType = declaringType;
            }

            public override Expression Visit(Expression expr)
            {
                if (expr != null && expr.NodeType == ExpressionType.MemberAccess)
                {
                    var memberExpr = (MemberExpression)expr;
                    if (memberExpr.Member.DeclaringType == declaringType)
                    {
                        PropertyNames.Add(memberExpr.Member.Name);
                    }
                }

                return base.Visit(expr);
            }
        }
    }

    public class Person : PropertyNotificationSupport
    {
        private int _age;
        public int Age
        {
            get => _age;
            set
            {

                if (_age == value)
                    return;
                _age = value;
                OnPropertyChanged(nameof(Age));
            }
        }

        public Person()
        {
            // Build a graph behind the scenes
            canVote = property(nameof(CanVote), () => Age >= 16 && Citizen);
        }

        private readonly Func<bool> canVote;
        public bool CanVote => canVote();

        private bool citizen;
        public bool Citizen
        {
            get => citizen;
            set
            {
                if (citizen == value)
                {
                    return;
                }

                citizen = value;
                OnPropertyChanged(nameof(Citizen));
            }
        }
    }

    public class PropertyDependencies
    {
        public static void Start(string[] args)
        {
            var p = new Person();
            p.PropertyChanged += (sender, eventArgs) =>
            {
                Console.WriteLine($"{eventArgs.PropertyName} changed");
            };
            p.Age = 15;
            p.Citizen = true;
        }
    }
}
