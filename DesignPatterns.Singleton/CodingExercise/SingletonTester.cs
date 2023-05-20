using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Singleton.CodingExercise
{
    public class SingletonTester
    {
        private static Lazy<SingletonTester> _instance 
            = new Lazy<SingletonTester>(() => new SingletonTester());

        public static SingletonTester Instance => _instance.Value;

        public static bool IsSingleton(Func<object> func)
        {
            var ob1 = func();
            var ob2 = func();

            return ob1 == ob2;
        }

        public static void Start(string[] args)
        {
            Console.WriteLine(SingletonTester.IsSingleton(() => SingletonTester.Instance));
            Console.WriteLine(SingletonTester.IsSingleton(() => new SingletonTester()));
        }
    }
}
