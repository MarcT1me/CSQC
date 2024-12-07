using Engine.Data;
using Engine.Event;
using Engine.Graphics.DefaultMeshes;
using Engine.Graphics.OpenGL.Shaders;
using Engine.Graphics.OpenGL.Vertex;
using Engine.Graphics.Window;
using Engine.Objects;
using Engine.Objects.Tracer;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using Engine.App;

namespace AppLib.Test;

public class Test : QObject<QMeta>
{
    private static int _some;

    private readonly ShaderProgram _uvProgram;
    private readonly VertexArrayObject _cubeVao;

    public Test()
    {
        GL.LoadBindings(new SdlBindingsContext());

        _uvProgram = new(
            Path.Combine(QData.RootDirectory, "Data", "shaders"),
            "test"
        );
        QuadVbo cubeVbo = new();
        _cubeVao = new(
        [
            new VertexAttributeInfo("in_position", 2, typeof(float)),
            // new VertexAttributeInfo("in_normal", 3, typeof(float)),
            // new VertexAttributeInfo("in_texCoord", 2, typeof(float))
        ], _uvProgram, cubeVbo);
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
        _uvProgram.SetUniform(
            GL.Uniform2, "u_resolution", Window.Roster["Main"].QMeta.GlData.Resolution
        );

        // Vector3 cameraPosition = new Vector3(0.0f, 1.0f, 0.0f);
        // Vector3 targetPosition = new Vector3(0.0f, 0.0f, 0.0f);
        // Vector3 upVector = new Vector3(0.0f, 1.0f, 0.0f);
        //
        // Matrix4 viewMatrix = Matrix4.LookAt(cameraPosition, targetPosition, upVector);
        //
        // float fieldOfView = MathHelper.PiOver4;
        // float aspectRatio = 16.0f / 9.0f;
        // float nearPlane = 0.1f;
        // float farPlane = 100.0f;
        //
        // Matrix4 projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(fieldOfView, aspectRatio, nearPlane, farPlane);

        // _uvProgram.SetUniform(
        //     GL.UniformMatrix4, "u_viewMatrix", false, ref viewMatrix
        // );
        // _uvProgram.SetUniform(
        //     GL.UniformMatrix4, "u_projectionMatrix", false, ref projectionMatrix
        // );
        
        _uvProgram.Use(false);
    }

    public override void Render()
    {
        _cubeVao.Draw();
    }
}