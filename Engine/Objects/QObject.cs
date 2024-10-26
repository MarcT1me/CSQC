using Engine.Objects.Tracer;

namespace Engine.Objects;

public abstract class QObject
{
    public static readonly Dictionary<string, Type> List = [];
    public static readonly Dictionary<string, Callback> CallbackList = [];

    public IQMeta QMeta;

    public QObject(string? id = null)
    {
        QMeta = new QMeta(id);
    }

    public abstract void HandleEvent(int eventId);
    public abstract void Update();
    public abstract void Render();

    public virtual void Dispose()
    {
    }
}