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

    protected static readonly Dictionary<Type, VertexAttribPointerType> GLTypes = new()
    {
        { typeof(float), VertexAttribPointerType.Float },
        { typeof(double), VertexAttribPointerType.Double },
        { typeof(int), VertexAttribPointerType.Int },
        { typeof(uint), VertexAttribPointerType.UnsignedInt },
        { typeof(byte), VertexAttribPointerType.Byte }
    };

    protected static readonly Dictionary<Type, int> TypeSizes = new()
    {
        { typeof(float), sizeof(float) },
        { typeof(double), sizeof(double) },
        { typeof(int), sizeof(int) },
        { typeof(uint), sizeof(uint) },
        { typeof(byte), 1 }
    };

    public VertexAttributeInfo[] Attrs;

    public VertexArrayObject(VertexAttributeInfo[] attrs, ShaderProgram program, VertexBuffer buffer)
    {
        Attrs = attrs;
        Program = program;
        Buffer = buffer;
        VaoPtr = GL.GenVertexArray();
        GlCheckError();

        SetupVertexAttributes();
    }

    private void GlCheckError(string caption = "null")
    {
        ErrorCode error = GL.GetError();
        if (error != ErrorCode.NoError)
        {
            // Handle or log the error
            Console.WriteLine($"OpenGL Error: {error} - {caption}");
        }
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

            GL.VertexAttribPointer(
                index, attr.Count, GLTypes[attr.Type], false, attr.Count * TypeSizes[attr.Type], offset
            );
            GlCheckError($"Invalid attribute {attr.Name}");
            GL.EnableVertexAttribArray(index);
            GlCheckError($"Error enable attribute {attr.Name}");

            offset += attr.Count * TypeSizes[attr.Type];
        }

        Unbind();
    }

    public void Draw()
    {
        Program.Use(true);
        Bind();
        GL.DrawArrays(PrimitiveType.Triangles, 0, Buffer.indicesCount);
        Unbind();
        Program.Use(false);
    }

    void Dispose()
    {
        GL.DeleteVertexArray(VaoPtr);
        Buffer.Dispose();
    }
}