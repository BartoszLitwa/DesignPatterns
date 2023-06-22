namespace ParallelProgramming.TaskProgramming.Examples.ExceptionHandling
{
    public class ExceptionHandling
    {
        public static void Start(string[] args)
        {
            try
            {
                Test();
            }
            catch (AggregateException ae)
            {
                foreach (var e in ae.InnerExceptions)
                    Console.WriteLine($"Handled outside {e.GetType()} from {e.Source}");
            }

            Console.WriteLine("ExceptionHandling done");
        }

        private static void Test()
        {
            var t = Task.Factory.StartNew(() =>
            {
                throw new InvalidOperationException("Can't do this") { Source = "t" };
            });

            var t2 = Task.Factory.StartNew(() =>
            {
                throw new AccessViolationException("Can't access this!") { Source = "t2" };
            });

            try
            {
                Task.WaitAll(t, t2);
            }
            catch (AggregateException ae)
            {
                //foreach (var e in ae.InnerExceptions)
                //    Console.WriteLine($"Exception {e.GetType()} from {e.Source}");

                ae.Handle((ex) =>
                {
                    if (ex is InvalidOperationException)
                    {
                        Console.WriteLine("Invalid op!");
                        return true; // Handled this exception
                    }
                    return false;
                });
            }
        }
    }
}
