using Engine.Data;

namespace Engine.Threading;

public class QTask(QTask.TaskDelegate callback)
{
    public delegate void TaskDelegate(QTask sender, QExtended? args);

    public Task? Task { get; private set; }

    public void Start(QExtended? args = null)
    {
        Task = Task.Run(() => callback(this, args));
    }

    public void Join()
    {
        Task?.Wait();
        Task = null;
    }

    public void Stop()
    {
        if (Task is { IsCanceled: false })
            Task.Dispose();
        Task = null;
    }
}