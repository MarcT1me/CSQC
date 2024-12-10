using Engine.Data;
using Engine.Event;
using Engine.Graphics.DefaultMeshes;
using Engine.Graphics.OpenGL.Shaders;
using Engine.Graphics.OpenGL.Vertex;
using Engine.Graphics.Window;
using Engine.Objects;
using Engine.Objects.Tracer;
using OpenTK.Graphics.OpenGL;
using Engine.Graphics.OpenGL;
using Engine.Graphics.OpenGL.Buffer;

namespace AppLib.Test;

public class Test : QObject<QMeta>
{
    private static int _some;

    private readonly ShaderProgram _uvProgram;
    private readonly VertexArrayObject _quadVao;

    public Test()
    {
        _uvProgram = new(
            Path.Combine(QData.RootDirectory, "Data", "shaders"),
            "test"
        );
        QuadVbo quadVbo = new();
        _quadVao = new(
        [
            new VertexAttributeInfo("in_position", 3, typeof(float))
            /* indev */
            // new VertexAttributeInfo("in_normal", 3, typeof(float)),
            // new VertexAttributeInfo("in_texCoord", 2, typeof(float))
        ], _uvProgram, quadVbo);
        OpenGl.GlCheckError("Error Test initialize");
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
        _uvProgram.Use();
        _uvProgram.SetUniform(
            GL.Uniform2, "u_resolution", Window.Roster["Main"].QMeta.GlData.Resolution
        );
        
        int[] v = [0, 0];
        _uvProgram.GetUniform("u_resolution", v);
        Console.WriteLine("U_res: vec2(" + v[0] + ", " + v[1] + ")");

        /* indev */
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
        
        _uvProgram.Unuse();
    }

    public override void Render()
    {
        _quadVao.Draw();
    }

    public override void Dispose()
    {
        base.Dispose();
        _quadVao.Dispose();
    }
}