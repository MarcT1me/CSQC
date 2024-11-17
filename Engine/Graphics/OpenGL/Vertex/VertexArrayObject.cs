using OpenTK.Graphics.OpenGL;
using Engine.Graphics.OpenGL.Buffer;
using Engine.Graphics.OpenGL.Shaders;
using System.Collections.Generic;

namespace Engine.Graphics.OpenGL.Vertex;

public class VertexArrayObject<T> where T : struct
{
    // list of all VAI
    public static readonly Dictionary<string, Type> List = new();

    // data of VAO
    protected readonly int VaoPtr;
    protected readonly ShaderProgram Program;
    protected readonly VertexBuffer<T> Buffer;

    protected readonly static Dictionary<Type, VertexAttribPointerType> Types = new()
    {
        { typeof(float), VertexAttribPointerType.Float },
        { typeof(double), VertexAttribPointerType.Double },
        { typeof(int), VertexAttribPointerType.Int },
        { typeof(uint), VertexAttribPointerType.UnsignedInt },
        { typeof(byte), VertexAttribPointerType.Byte },
    };

    public readonly string Name;

    public VertexArrayObject(string name, ShaderProgram program, VertexBuffer<T> buffer)
    {
        Name = name;
        Program = program;
        Buffer = buffer;
        VaoPtr = GL.GenVertexArray();
    }

    public void Bind()
    {
        GL.BindVertexArray(VaoPtr);
        Buffer.Bind();
    }

    public void Unbind()
    {
        Buffer.Unbind();
        GL.BindVertexArray(0);
    }

    public void SetupVertexAttributes()
    {
        Bind();

        var attrsField = List[Name].GetField("Attributes")?.GetValue(null)! as LinkedList<VertexAttributeInfo>;
        VertexAttributeInfo[] attrs = attrsField!.ToArray();
        int stride = (int)List[Name].GetField("Stride")?.GetValue(null)!;

        int offset = 0;
        foreach (VertexAttributeInfo attr in attrs)
        {
            int index = GL.GetAttribLocation(Program.ProgramPtr, attr.Name);
            GL.VertexAttribPointer(index, attr.Count * attr.TypeSize, Types[typeof(T)], false, stride, offset);
            GL.EnableVertexAttribArray(index);
            offset += attr.Count * attr.TypeSize;
        }

        Unbind();
    }

    void Dispose()
    {
        GL.DeleteVertexArray(VaoPtr);
        Buffer.Dispose();
    }
}