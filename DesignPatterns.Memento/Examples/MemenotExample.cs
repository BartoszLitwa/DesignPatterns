using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Memento.Examples.MemenotExample
{
    public class Memento
    {
        public int Balance { get; }
        public Memento(int balance)
        {
            Balance = balance;
        }
    }

    public class BankAccount
    {
        private int _balance;

        public BankAccount(int balance)
        {
            _balance = balance;
        }

        public Memento Deposit(int amount)
        {
            _balance += amount;
            return new Memento(_balance);
        }

        public void Restore(Memento m)
        {
            _balance = m.Balance;
        }

        public override string ToString() => $"{nameof(_balance)}: {_balance}";
    }

    public class MemenotExample
    {
        public static void Start(string[] args)
        {
            var ba = new BankAccount(100);
            var m1 = ba.Deposit(50); // 150
            var m2 = ba.Deposit(25); // 175
            Console.WriteLine(ba);

            ba.Restore(m1);
            Console.WriteLine(ba);

            ba.Restore(m2);
            Console.WriteLine(ba);
        }
    }
}
