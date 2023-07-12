namespace ParallelProgramming.AsynchronousProgramming.Examples.TaskExamples;

public class TaskExamples
{
    public static async void Start(string[] args)
    {
        // double await is NOT a mistake Task<Task<int>>
        var t = await await Task.Factory.StartNew(async delegate
        {
            await Task.Delay(1000);
            return 123;
        });
    }
}