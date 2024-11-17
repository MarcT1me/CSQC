using OpenTK.Graphics.OpenGL;
using Engine.Graphics.OpenGL.Buffer;
using Engine.Graphics.OpenGL.Shaders;

namespace Engine.Graphics.OpenGL.Vertex;

public class VertexArrayObject<T> where T : struct
{
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

    public VertexAttributesInfo Attrs;

    public VertexArrayObject(VertexAttributesInfo attrs, ShaderProgram program, VertexBuffer<T> buffer)
    {
        Attrs = attrs;
        Program = program;
        Buffer = buffer;
        VaoPtr = GL.GenVertexArray();
        SetupVertexAttributes();
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

        LinkedList<VertexAttributeInfo> attrsField = Attrs.Attributes;
        VertexAttributeInfo[] attrs = attrsField.ToArray();
        int stride = Attrs.Stride;

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