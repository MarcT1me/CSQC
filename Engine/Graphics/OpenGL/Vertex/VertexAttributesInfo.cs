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

    public static LinkedList<VertexAttributeInfo> Attributes { get; } = new();
    public static int Stride { get; private set; }

    static VertexAttributesInfo()
    {
        PostInit();
    }
    
    static void PostInit()
    {
        foreach (VertexAttributeInfo attr in Attributes)
        {
            Stride += attr.Count * attr.TypeSize;
        }
    }

    protected static void Add(VertexAttributeInfo attr)
    {
        Attributes.AddLast(attr);
    }
}
