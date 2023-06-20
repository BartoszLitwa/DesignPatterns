using ImpromptuInterface;
using System;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace DesignPatterns.Proxy.Examples.DynamicProxyForLogging
{
    public interface IBankAccount
    {
        void Deposit(int amount);
        bool Withdraw(int amount);
        string ToString();
    }

    public class BankAccount : IBankAccount
    {
        private int balance;
        private int overdraftLimit = -500;

        public void Deposit(int amount)
        {
            balance += amount;
            Console.WriteLine($"Deposited ${amount}, balance is now {balance}");
        }

        public bool Withdraw(int amount)
        {
            if (balance - amount >= overdraftLimit)
            {
                balance -= amount;
                Console.WriteLine($"Withdrew ${amount}, balance is now {balance}");
                return true;
            }
            return false;
        }

        public override string ToString() => $"{nameof(balance)}: {balance}";
    }

    public class Log<T> : DynamicObject
        where T : class, new()
    {
        private readonly T _subject;
        private Dictionary<string, int> _methodCallCOunt = new();

        public Log(T subject)
        {
            _subject = subject;
        }

        public static I As<I>() where I : class
        {
            if (!typeof(I).IsInterface)
                throw new ArgumentException("I must be an interfact type!");

            return new Log<T>(new T()).ActLike<I>();
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            try
            {
                Console.WriteLine($"Invoking {_subject.GetType().Name}.{binder.Name} with args [{string.Join(',', args)}]");

                if (_methodCallCOunt.ContainsKey(binder.Name))
                    _methodCallCOunt[binder.Name]++;
                else
                    _methodCallCOunt.Add(binder.Name, 1);

                result = _subject.GetType().GetMethod(binder.Name)!.Invoke(_subject, args)!;
                return true;
            }
            catch (Exception)
            {
                result = null;
                return false;
            }
        }

        public string Info
        {
            get
            {
                var sb = new StringBuilder();
                foreach (var kv in _methodCallCOunt)
                    sb.AppendLine($"{kv.Key} called {kv.Value} times(s)");
                return sb.ToString();
            }
        }

        public override string ToString() => $"{Info}\n{_subject}";
    }

    public class DynamicProxyForLogging
    {
        public static void Start(string[] args)
        {
            var ba = Log<BankAccount>.As<IBankAccount>();

            ba.Deposit(100);
            ba.Deposit(50);

            ba.Deposit(200);
            ba.Withdraw(100);

            Console.WriteLine(ba);
        }
    }
}
