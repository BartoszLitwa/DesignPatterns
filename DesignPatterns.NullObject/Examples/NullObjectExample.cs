using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NullObject.Examples.NullObjectExample
{
    public interface ILog
    {
        void Info(string msg);
        void Warn(string msg);
    }

    public class ConsoleLog : ILog
    {
        public void Info(string msg) => Console.WriteLine(msg);

        public void Warn(string msg) => Console.WriteLine($"Warning: {msg}");
    }

    public class BankAccount
    {
        private ILog log;
        private int balance;

        public BankAccount(ILog log) => this.log = log;

        public void Deposit(int amount)
        {
            balance += amount;
            log.Info($"Deposited: {amount}, balance: {balance}");
        }
    }

    public class NullLog : ILog
    {
        public void Info(string msg) { }

        public void Warn(string msg) { }
    }

    public class NullObjectExample
    {
        public static void Start(string[] args)
        {
            var cb = new ContainerBuilder();
            cb.RegisterType<BankAccount>();
            cb.RegisterType<NullLog>().As<ILog>();

            using var c = cb.Build();
            var ba = c.Resolve<BankAccount>();
            ba.Deposit(100);
        }
    }
}
