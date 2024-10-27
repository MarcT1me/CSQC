using Engine.Data.Files;
using OpenTK.Graphics.OpenGL;

namespace Engine.Graphics.Shaders;

public class Shader : Reader
{
    public Shader(ShaderType shaderType, string rootDirectory, string path) : base(rootDirectory)
    {
        ShaderPtr = GL.CreateShader(shaderType);

        var source = ReadTextFile(path);
        GL.ShaderSource(ShaderPtr, source);

        GL.CompileShader(ShaderPtr);
    }

    public int ShaderPtr { get; }

    public ShaderCreateStatus CompilationStatus()
    {
        return new ShaderCreateStatus(ShaderPtr);
    }

    public void AttachTo(int shaderProgram)
    {
        GL.AttachShader(shaderProgram, ShaderPtr);
    }

    public void Dispose()
    {
        GL.DeleteShader(ShaderPtr);
    }
}