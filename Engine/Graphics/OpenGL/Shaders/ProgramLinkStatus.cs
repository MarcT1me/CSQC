using OpenTK.Graphics.OpenGL;

namespace Engine.Graphics.OpenGL.Shaders;

public readonly struct ProgramLinkStatus
{
    public readonly bool Successfully;
    public readonly string Log;
    public readonly int Status;

    public ProgramLinkStatus(int programPtr)
    {
        GL.GetProgram(programPtr, GetProgramParameterName.LinkStatus, out Status);
        Successfully = Status == 1;
        Log = !Successfully ? GL.GetShaderInfoLog(programPtr) : string.Empty;
    }

    public void WriteLog()
    {
        Console.WriteLine($"TestApp link status: {Status}");
        Console.WriteLine(Log);
    }
}