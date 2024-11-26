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

    protected static readonly Dictionary<Type, VertexAttribPointerType> Types = new()
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
        foreach (VertexAttributeInfo attr in Attrs.Attributes)
        {
            int index = GL.GetAttribLocation(Program.ProgramPtr, attr.Name);
            if (index == -1)
            {
                Console.WriteLine($"Error: Attribute '{attr.Name}' not found in shader program.");
                return;
            }
            
            GL.VertexAttribPointer(index, attr.Count * attr.TypeSize, Types[typeof(T)], false, attr.Count * attr.TypeSize, offset);
            GlCheckError($"Invalid attribute {attr.Name}");
            GL.EnableVertexAttribArray(index);
            GlCheckError($"Error enable attribute {attr.Name}");

            offset += attr.Count * attr.TypeSize;
        }

        Unbind();
    }

    public void Draw()
    {
        Program.Use(true);
        Bind();
        GL.DrawArrays(PrimitiveType.Triangles, 0, Buffer.indecesCount);
        Unbind();
        Program.Use(false);
    }

    void Dispose()
    {
        GL.DeleteVertexArray(VaoPtr);
        Buffer.Dispose();
    }
}