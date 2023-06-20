namespace ParallelProgramming.TaskProgramming.Examples.CreatingAndStartingTasks
{
    public class CreatingAndStartingTasks
    {
        public static void Write(char c)
        {
            int i = 1000;
            while( i-- > 0 )
            {
                Console.Write(c);
            }
        }

        public static void Write(object o)
        {
            int i = 1000;
            while (i-- > 0)
            {
                Console.Write(o);
            }
        }

        public static int TextLength(object o)
        {
            Console.WriteLine($"\nTask with id {Task.CurrentId} processing object {o}...");
            return o!.ToString()!.Length;
        }

        public static void Start(string[] args)
        {
            //// Make a task and start
            //Task.Factory.StartNew(() => Write('.'));

            //// Create a task
            //var t = new Task(() => Write('?'));
            //t.Start(); // Run a task

            //Task t2 = new Task(Write, "hello");
            //t2.Start();
            //Task.Factory.StartNew(Write, 123);

            string text1 = "testing", text2 = "this";
            Task<int> task1 = new Task<int>(TextLength, text1);
            task1.Start();
            Task<int> task2 = Task.Factory.StartNew(TextLength, text2);

            Console.WriteLine($"Length of '{text1}' is {task1.Result}");
            Console.WriteLine($"Length of '{text2}' is {task2.Result}");

            Console.WriteLine("Tasks done!");
        }
    }
}
