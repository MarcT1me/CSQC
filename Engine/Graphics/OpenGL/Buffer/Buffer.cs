using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL;

namespace Engine.Graphics.OpenGL.Buffer;

public abstract unsafe class Buffer<T> where T : struct
{
    private readonly int _bufferPtr;
    protected T[] _data;

    protected Buffer()
    {
        _data = Array.Empty<T>();
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
        GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(_data.Length * Marshal.SizeOf(typeof(T))), _data.ToArray(),
            BufferUsageHint.StaticDraw);
        Unbind();
    }
    public T[] GetData()
    {
        return _data;
    }
}
