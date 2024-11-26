using Engine.Data;
using Engine.Event;
using Engine.Graphics.DefaultMeshes;
using Engine.Graphics.OpenGL.Shaders;
using Engine.Graphics.OpenGL.Vertex;
using Engine.Graphics.Window;
using Engine.Objects;
using Engine.Objects.Tracer;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using Engine.App;

namespace AppLib.Test;

public class Test : QObject<QMeta>
{
    private static int _some;

    private readonly ShaderProgram _uvProgram;
    private readonly VertexArrayObject<int> _quadVao;

    public Test()
    {
        GL.LoadBindings(new SdlBindingsContext());
        _uvProgram = new(
            Path.Combine(EngineData.RootDirectory, "Data", "shaders"),
            "test"
        );
        QuadVbo quadVbo = new();
        var quadVai = new QuadVai();
        _quadVao = new(quadVai, _uvProgram, quadVbo);

        Console.WriteLine("Create Test Object");
    }

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
        _uvProgram.Use(true);
        Vector2 res = Window.Roster["Main"].QMeta.GlData.Resolution;
        Console.WriteLine(res);
        _uvProgram.SetUniform(GL.Uniform2, "u_resolution", res);
        float[] uRes = [0, 0];
        _uvProgram.GetUniform("u_resolution", uRes);
        Console.WriteLine($"{uRes[0]}, {uRes[1]}");
        _uvProgram.Use(false);
    }

    public override void Render()
    {
        _quadVao.Draw();
    }
}

class QuadVai : VertexAttributesInfo
{
    public QuadVai()
    {
        Add(attr: new("in_position", 3, Float));
    }
}