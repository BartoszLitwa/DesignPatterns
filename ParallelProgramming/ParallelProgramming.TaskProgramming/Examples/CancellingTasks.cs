namespace ParallelProgramming.TaskProgramming.Examples.CancellingTasks
{
    public class CancellingTasks
    {
        public static void Start(string[] args)
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            token.Register(() =>
                {
                    Console.WriteLine("Cancellation has been request.");
                });

            var t = new Task(() =>
                {
                    try
                    {
                        int i = 0;
                        while (true)
                        {
                            //if (token.IsCancellationRequested)
                            //{
                            //    throw new OperationCanceledException();
                            //}
                            // If and throw
                            token.ThrowIfCancellationRequested();

                            Console.WriteLine($"{i++}\t");
                        }
                    }
                    catch (Exception ex)
                    {
                        
                    }
                },
                token);
            t.Start();

            Task.Factory.StartNew(() =>
                    {
                        token.WaitHandle.WaitOne();
                        Console.WriteLine("Wait handle released, cancelation was request");
                    });

            cts.Cancel();

            var planned = new CancellationTokenSource();
            var preventative = new CancellationTokenSource();
            var emergency = new CancellationTokenSource();

            var paranoid = CancellationTokenSource.CreateLinkedTokenSource(
                planned.Token,
                preventative.Token,
                emergency.Token);

            Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            int i = 0;
                            while(true)
                            {
                                paranoid.Token.ThrowIfCancellationRequested();
                                Console.WriteLine($"{i++}\t");
                                Thread.Sleep(10);
                            }
                        } catch(Exception ex)
                        {
                        }
                    },
                    paranoid.Token);

            Thread.Sleep(50);
            emergency.Cancel();

            Console.WriteLine("CancellingTasks done");
        }
    }
}
