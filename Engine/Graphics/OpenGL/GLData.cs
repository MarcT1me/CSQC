using Engine.Graphics.Window;
using OpenTK.Graphics.ES20;
using OpenTK.Mathematics;

namespace Engine.Graphics.OpenGL;

public struct GlData()
{
    // Static init info
    public static int NumberOfSamples = 8;
    public static int DepthBits = 24;
    public static Vector2i ApiVersions = new(3, 3);
    public static EnableCap[] EnableCaps = [EnableCap.DepthTest, EnableCap.Blend, EnableCap.CullFace];

    // scaling
    public Vector2i Position = default;
    public Vector2i Resolution = default;

    // field
    // public float Near = 0.00001f;
    // public float Far = 1000.0f;

    // other
    public Vector4 ClearColor = new Vector4(0.08f, 0.16f, 0.18f, 1.0f);
}