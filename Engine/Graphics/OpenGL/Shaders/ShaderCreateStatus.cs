using OpenTK.Graphics.OpenGL;

namespace Engine.Graphics.OpenGL.Shaders;

public readonly struct ShaderCreateStatus
{
    public readonly bool Successfully;
    public readonly string Log;
    public readonly int Status;

    public ShaderCreateStatus(int shaderPtr)
    {
        GL.GetShader(shaderPtr, ShaderParameter.CompileStatus, out Status);
        Successfully = Status == 1;
        Log = !Successfully ? GL.GetShaderInfoLog(shaderPtr) : string.Empty;
    }

    public void WriteLog()
    {
        Console.WriteLine($"Shader compile status: {Status}");
        Console.WriteLine(Log);
    }
}