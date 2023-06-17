using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Memento.Examples.UndoAndRedo
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
        private List<Memento> changes = new();
        private int current;

        public BankAccount(int balance)
        {
            _balance = balance;
            changes.Add(new Memento(_balance));
        }

        public Memento Deposit(int amount)
        {
            _balance += amount;
            var m = new Memento(_balance);
            changes.Add(m);
            current++;
            return m;
        }

        public Memento Restore(Memento m)
        {
            if(m is not null)
            {
                _balance = m.Balance;
                changes.Add(m);
                return m;
            }
            return null;
        }

        public Memento Undo()
        {
            if(current > 0)
            {
                var m = changes[--current];
                _balance = m.Balance;
                return m;
            }
            return null;
        }

        public Memento Redo()
        {
            if(current + 1 < changes.Count)
            {
                var m = changes[++current];
                _balance = m.Balance;
                return m;
            }
            return null;
        }

        public override string ToString() => $"{nameof(_balance)}: {_balance}";
    }

    public class UndoAndRedo
    {
        public static void Start(string[] args)
        {
            var ba = new BankAccount(100);
            var m1 = ba.Deposit(50); // 150
            var m2 = ba.Deposit(25); // 175
            Console.WriteLine(ba);

            ba.Undo();
            Console.WriteLine($"Undo 1: {ba}");

            ba.Undo();
            Console.WriteLine($"Undo 2: {ba}");

            ba.Redo();
            Console.WriteLine($"Redo 1: {ba}");

            ba.Redo();
            Console.WriteLine($"Redo 2: {ba}");
        }
    }
}
