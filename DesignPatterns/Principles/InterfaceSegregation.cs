using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.SOLID.Principles
{
    public class InterfaceSegregation
    {
        public class Document
        {
            
        }

        public interface IPrinter
        {
            void Print(Document d);
        }

        public interface IScanner
        {
            void Scan(Document d);
        }

        public interface IMultiFunctionDevice : IPrinter, IScanner
        {

        }

        public class MutliFunctionPrinter : IMultiFunctionDevice
        {
            private readonly IPrinter _printer;
            private readonly IScanner _scanner;

            public MutliFunctionPrinter(IPrinter printer, IScanner scanner)
            {
                _printer = printer;
                _scanner = scanner;
            }

            // Decorator pattern
            public void Print(Document d) => _printer.Print(d);

            public void Scan(Document d) => _scanner.Scan(d);
        }

        public class OldFashionedPrinter : IPrinter
        {
            public void Print(Document d)
            {
                throw new NotImplementedException();
            }
        }

        public class Photocopier : IPrinter, IScanner
        {
            public void Print(Document d)
            {
                throw new NotImplementedException();
            }

            public void Scan(Document d)
            {
                throw new NotImplementedException();
            }
        }
        
        public static void Start(string[] args)
        {
        }
    }
}
