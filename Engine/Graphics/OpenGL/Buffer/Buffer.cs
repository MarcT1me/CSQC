using OpenTK.Graphics.OpenGL;

namespace Engine.Graphics.OpenGL.Buffer;

public abstract class Buffer
{
    private readonly int _bufferPtr = GL.GenBuffer();
    protected float[] Data = [];
    public float[] GetData() => Data;

    /* indev */
    // private readonly Dictionary<TypeCode, Func<IConvertible, byte[]>> _typeCodeToBytes = new()
    // {
    //     // integers
    //     { TypeCode.Byte, b => [b.ToByte(null)] },
    //     { TypeCode.Int16, i => BitConverter.GetBytes(i.ToInt16(null)) },
    //     { TypeCode.Int32, i => BitConverter.GetBytes(i.ToInt32(null)) },
    //     { TypeCode.Int64, i => BitConverter.GetBytes(i.ToInt64(null)) },
    //     // unsigned integers
    //     { TypeCode.UInt16, u => BitConverter.GetBytes(u.ToUInt16(null)) },
    //     { TypeCode.UInt32, u => BitConverter.GetBytes(u.ToUInt32(null)) },
    //     { TypeCode.UInt64, u => BitConverter.GetBytes(u.ToUInt64(null)) },
    //     // floating point
    //     { TypeCode.Single, f => BitConverter.GetBytes(f.ToSingle(null)) },
    //     { TypeCode.Double, f => BitConverter.GetBytes(f.ToDouble(null)) }
    // };

    public void Bind()
    {
        GL.BindBuffer(BufferTarget.ArrayBuffer, _bufferPtr);
        OpenGl.GlCheckError("Error Binding Buffer");
    }

    public void Unbind()
    {
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
    }

    protected void TransferData()
    {
        /* indev */
        // List<byte> byteList = new();
        //
        // foreach (var obj in Data)
        // {
        //     if (obj is IConvertible convertible)
        //     {
        //         if (_typeCodeToBytes.TryGetValue(convertible.GetTypeCode(), out var converter))
        //         {
        //             var bytes = converter(convertible);
        //             if (BitConverter.IsLittleEndian)
        //                 Array.Reverse(bytes);
        //             byteList.AddRange(bytes);
        //         }
        //         else
        //         {
        //             throw new ArgumentException($"Unsupported data type: {obj.GetType()}");
        //         }
        //     }
        //     else
        //     {
        //         throw new ArgumentException($"Object is not IConvertible: {obj.GetType()}");
        //     }
        // }

        Bind();
        GL.BufferData(
            BufferTarget.ArrayBuffer,
            Data.Length, // (IntPtr)byteList.Count, 
            Data, // byteList.ToArray(), 
            BufferUsageHint.StaticDraw
        );
        OpenGl.GlCheckError("Error Transfer Data");
        Unbind();
    }

    public void Dispose()
    {
        GL.DeleteBuffer(_bufferPtr);
    }
}