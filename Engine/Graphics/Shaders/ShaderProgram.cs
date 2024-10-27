using OpenTK.Graphics.OpenGL;

namespace Engine.Graphics.Shaders;

public delegate void UniformSetter<T>(int location, T value) where T : struct;

public delegate void UniformMSetter<T>(int location, bool transpose, ref T value) where T : struct;

public class ShaderProgram
{
    private readonly Shader _fragShader;
    private readonly Shader _vertShader;

    public ShaderProgram(string rootDirectory, string path)
    {
        // vertex shader handling
        _vertShader = new Shader(ShaderType.VertexShader, rootDirectory, path + ".vert");
        var vertCreateStatus = _vertShader.CompilationStatus();
        if (!vertCreateStatus.Successfully)
            vertCreateStatus.WriteLog();
        // fragment shader handling
        _fragShader = new Shader(ShaderType.FragmentShader, rootDirectory, path + ".frag");
        var fragCreateStatus = _fragShader.CompilationStatus();
        if (!fragCreateStatus.Successfully)
            fragCreateStatus.WriteLog();
        // program itself
        ProgramPtr = GL.CreateProgram();
        _vertShader.AttachTo(ProgramPtr);
        _fragShader.AttachTo(ProgramPtr);

        GL.LinkProgram(ProgramPtr);
        var programLinkStatus = new ProgramLinkStatus(ProgramPtr);
        if (!programLinkStatus.Successfully)
            programLinkStatus.WriteLog();
    }

    public int ProgramPtr { get; }

    public void Dispose()
    {
        GL.DeleteProgram(ProgramPtr);
        _vertShader.Dispose();
        _fragShader.Dispose();
    }

    public void Use()
    {
        GL.UseProgram(ProgramPtr);
    }

    public void Use(bool use)
    {
        GL.UseProgram(use ? ProgramPtr : 0);
    }

    public void SetUniform<T>(UniformSetter<T> setter, string uName, T uValue) where T : struct
    {
        var loc = GL.GetUniformLocation(ProgramPtr, uName);
        setter(loc, uValue);
    }

    public void SetUniform<T>(UniformMSetter<T> setter, string uName, bool transpose, ref T uValue) where T : struct
    {
        var loc = GL.GetUniformLocation(ProgramPtr, uName);
        setter(loc, transpose, ref uValue);
    }

    public void GetUniform(string uName, double[] values)
    {
        var loc = GL.GetUniformLocation(ProgramPtr, uName);
        GL.GetUniform(ProgramPtr, loc, values);
    }

    public void GetUniform(string uName, int[] values)
    {
        var loc = GL.GetUniformLocation(ProgramPtr, uName);
        GL.GetUniform(ProgramPtr, loc, values);
    }

    public void GetUniform(string uName, float[] values)
    {
        var loc = GL.GetUniformLocation(ProgramPtr, uName);
        GL.GetUniform(ProgramPtr, loc, values);
    }
}