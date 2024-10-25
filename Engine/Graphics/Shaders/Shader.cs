// ReSharper disable MemberCanBePrivate.Global

using OpenTK.Graphics.OpenGL;
using Engine.Data.Files;

// file namespace
namespace Engine.Graphics.Shaders;

public class Shader : Reader
{
    public int ShaderPtr { get; }

    public Shader(ShaderType shaderType, string rootDirectory, string path) : base(rootDirectory)
    {
        ShaderPtr = GL.CreateShader(shaderType);

        var source = ReadTextFile(path);
        GL.ShaderSource(ShaderPtr, source);

        GL.CompileShader(ShaderPtr);
    }

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