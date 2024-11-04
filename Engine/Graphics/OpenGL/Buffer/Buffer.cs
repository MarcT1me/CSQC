using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL;

namespace Engine.Graphics.OpenGL.Buffer;

public abstract class Buffer<T> where T : struct
{
    private readonly int _bufferPtr;

    protected Buffer()
    {
        _bufferPtr = GL.GenBuffer();
    }

    public void Bind()
    {
        GL.BindBuffer(BufferTarget.ArrayBuffer, _bufferPtr);
    }

    public void Unbind()
    {
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
    }

    public void Dispose()
    {
        GL.DeleteBuffer(_bufferPtr);
    }

    protected void TransferData(ref T[] data)
    {
        Bind();
        GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(data.Length * Marshal.SizeOf(typeof(T))), data.ToArray(),
            BufferUsageHint.StaticDraw);
        Unbind();
    }
}
