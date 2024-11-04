using Engine.Event;
using Engine.Objects.Tracer;

namespace Engine.Objects;

public abstract class QObject<T> where T : struct
{
    public static readonly Dictionary<string, Type> List = [];
    public static readonly Dictionary<string, Callback> CallbackList = [];

    public T QMeta;

    public QObject()
    {
        QEventHandler.OnEvent += HandleEvent;
    }

    public abstract void HandleEvent(SdlEventArgs e);
    public abstract void Update();
    public abstract void Render();

    public virtual void Dispose()
    {
        QEventHandler.OnEvent -= HandleEvent;
    }
}