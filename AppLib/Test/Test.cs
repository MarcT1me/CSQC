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
    private readonly QuadVai _quadVai;
    private readonly QuadVbo _quadVbo;
    private readonly VertexArrayObject<int> _quadVao;

    public Test()
    {
        _uvProgram = new(
            Path.Combine(EngineData.RootDirectory, "Data", "shaders"),
            "test"
        );
        _quadVbo = new();
        _quadVai = new QuadVai();
        _quadVao = new(_quadVai, _uvProgram, _quadVbo);

        GL.LoadBindings(new SdlBindingsContext());
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
        _uvProgram.Use(false);
    }

    public override void Render()
    {
        _uvProgram.Use(true);
        _quadVao.Bind();
        GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
        _quadVao.Unbind();
        _uvProgram.Use(false);
    }
}

class QuadVai : VertexAttributesInfo
{
    public QuadVai()
    {
        Add(attr: new("in_position", 3, Float));
    }
}