using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL;
using Engine.Graphics.OpenGL.Buffer;
using Engine.Graphics.OpenGL.Shaders;

namespace Engine.Graphics.OpenGL.Vertex;

public class VertexArrayObject
{
    // data of VAO
    protected readonly int VaoPtr;
    protected readonly ShaderProgram Program;
    protected readonly VertexBuffer Buffer;

    /* indev */
    // protected static readonly Dictionary<Type, VertexAttribPointerType> GlTypes = new()
    // {
    //     { typeof(float), VertexAttribPointerType.Float },
    //     { typeof(double), VertexAttribPointerType.Double },
    //     { typeof(int), VertexAttribPointerType.Int },
    //     { typeof(uint), VertexAttribPointerType.UnsignedInt },
    //     { typeof(byte), VertexAttribPointerType.Byte }
    // };

    public VertexAttributeInfo[] Attrs;

    public VertexArrayObject(VertexAttributeInfo[] attrs, ShaderProgram program, VertexBuffer buffer)
    {
        Attrs = attrs;
        Program = program;
        Buffer = buffer;
        VaoPtr = GL.GenVertexArray();
        OpenGl.GlCheckError("Error creating OpenGL vertex array");

        SetupVertexAttributes();
    }

    public void Bind()
    {
        GL.BindVertexArray(VaoPtr);
        OpenGl.GlCheckError("Error Binding OpenGL vertex array");
        Buffer.Bind();
    }

    public void Unbind()
    {
        Buffer.Unbind();
        GL.BindVertexArray(0);
    }

    private void SetupVertexAttributes()
    {
        Bind();

        int offset = 0;
        foreach (VertexAttributeInfo attr in Attrs)
        {
            int index = GL.GetAttribLocation(Program.ProgramPtr, attr.Name);
            if (index == -1)
            {
                Console.WriteLine($"Error: Attribute '{attr.Name}' not found in shader program.");
                return;
            }

            // int size = attr.Count * Marshal.SizeOf(attr.Type);
            
            GL.VertexAttribPointer(
                index: index,
                size: attr.Count,
                type: VertexAttribPointerType.Float, // GlTypes[attr.Type],
                normalized: false,
                stride: attr.Count * 4,
                offset: offset
            );
            OpenGl.GlCheckError($"Invalid attribute {attr.Name}");
            GL.EnableVertexAttribArray(index);
            OpenGl.GlCheckError($"Error enable attribute {attr.Name}");

            offset += attr.Count * 4;
        }

        Unbind();
    }

    public void Draw()
    {
        Program.Use();
        Bind();
        GL.DrawArrays(PrimitiveType.Triangles, 0, Buffer.IndicesCount);
        OpenGl.GlCheckError("Error Draw");
        Unbind();
        Program.Unuse();
    }

    public void Dispose()
    {
        GL.DeleteVertexArray(VaoPtr);
        Buffer.Dispose();
    }
}