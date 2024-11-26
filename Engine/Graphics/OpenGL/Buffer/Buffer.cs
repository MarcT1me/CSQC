using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL;

namespace Engine.Graphics.OpenGL.Buffer;

public abstract class Buffer<T> where T : struct
{
    private readonly int _bufferPtr;
    protected T[] Data;

    protected Buffer()
    {
        Data = [];
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

    protected void TransferData()
    {
        Bind();
        GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Data.Length * Marshal.SizeOf(typeof(T))), Data.ToArray(),
            BufferUsageHint.StaticDraw);
        Unbind();
    }
    public T[] GetData()
    {
        return Data;
    }
}
