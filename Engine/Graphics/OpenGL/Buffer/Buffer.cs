using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL;

namespace Engine.Graphics.OpenGL.Buffer;

public abstract class Buffer
{
    private readonly int _bufferPtr;
    protected object[] Data;

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
        List<byte> byteList = new();

        foreach (object obj in Data)
        {
            int size = Marshal.SizeOf(obj.GetType());

            List<byte> buffer = new();
            if (obj is float f)
                buffer.AddRange(BitConverter.GetBytes(f));
            if (obj is double d)
                buffer.AddRange(BitConverter.GetBytes(d));
            else if (obj is int i)
                buffer.AddRange(BitConverter.GetBytes(i));
            else if (obj is uint u)
                buffer.AddRange(BitConverter.GetBytes(u));
            else
                throw new ArgumentException("Unsupported data type in Data array.");

            byteList.AddRange(buffer);
        }

        Bind();
        GL.BufferData(
            BufferTarget.ArrayBuffer, (IntPtr)byteList.Count, byteList.ToArray(), BufferUsageHint.StaticDraw
        );
        Unbind();
    }

    public object[] GetData()
    {
        return Data;
    }
}