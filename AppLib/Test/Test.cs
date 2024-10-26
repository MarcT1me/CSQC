using Engine.App;
using Engine.Objects;
using Engine.Objects.Tracer;

namespace AppLib.Test;

[QTracer<App>(TraceType.Scan)]
public class Test : QObject
{
    private static int _some;

    [QTracer<QObject>(TraceType.Callback)]
    public static int TestCall(string s)
    {
        Console.WriteLine("TestArg S: " + s + "\tTestCall: " + _some);
        _some++;
        return _some;
    }

    public override void HandleEvent(int eventId)
    {
    }

    public override void Update()
    {
    }

    public override void Render()
    {
    }
}