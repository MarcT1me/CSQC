using Engine.Event;
using Engine.Graphics.OpenGL.Vertex;
using Engine.Objects;
using Engine.Objects.Tracer;

namespace AppLib.Test;

public class Test : QObject<QMeta>
{
    private static int _some;

    [QTracer<QObject<QMeta>>(TraceType.Callback)]
    public static int TestCall(string s)
    {
        Console.WriteLine("TestArg S: " + s + "\tTestCall: " + _some);
        _some++;
        return _some;
    }

    public override void HandleEvent(SdlEventArgs e)
    {
    }

    public override void Update()
    {
    }

    public override void Render()
    {
    }
}

[QTracer<VertexArrayObject<int>>(TraceType.Scan)]
class TestVAI : VertexAttributesInfo
{
    public TestVAI() 
    {
        Add(attr: new("test3bytes", 3, Byte));
    }
}
