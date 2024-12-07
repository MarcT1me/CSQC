namespace Engine.Graphics.OpenGL.Vertex;

public record struct VertexAttributeInfo(string Name, int Count, Type Type);

public class VertexAttributesList(VertexAttributeInfo[] attributes)
{
    public VertexAttributeInfo[] Attributes { get; } = attributes;
}
