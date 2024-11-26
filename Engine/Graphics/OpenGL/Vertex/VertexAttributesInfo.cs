using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Platform.Windows;

namespace Engine.Graphics.OpenGL.Vertex;

public record struct VertexAttributeInfo(string Name, int Count, int TypeSize);

public abstract class VertexAttributesInfo
{
    // Статические размеры
    protected static readonly int Float = sizeof(float);
    protected static readonly int Double = sizeof(double);
    protected static readonly int Int = sizeof(int);
    protected static readonly int UnsignedInt = sizeof(uint);
    protected static readonly int Byte = 1;

    public LinkedList<VertexAttributeInfo> Attributes { get; } = new();

    protected void Add(VertexAttributeInfo attr)
    {
        Attributes.AddLast(attr);
    }
}
