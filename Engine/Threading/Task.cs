namespace Engine.Threading;

public class Task(Task.TaskDelegate callback)
{
    public delegate void TaskDelegate(object? sender, params object[] args);

    public Thread? Thread;

    public void Start(params object[] args)
    {
        Thread = new(() => callback(this, args));
        Thread.Start();
    }

    public void Join()
    {
        Thread?.Join();
        Thread = null;
    }

    [Obsolete("Obsolete")]
    public void Stop()
    {
        Thread?.Abort();
        Thread = null;
    }
}