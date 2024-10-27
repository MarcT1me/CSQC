using Engine.Graphics.Window;
using OpenTK.Mathematics;

namespace Engine.Graphics.OpenGL;

public struct GlData
{
    // scaling
    public static Vector2i Resolution => WinData.Resolution;
    public static Vector2i Position = default;
    public static int NumberOfSamples = 8;
    public static readonly int DepthBits = 24;

    // other
    public static readonly Vector2i ApiVersions = new(3, 3);
    // public static float Near = 0.00001f;
    // public static float Far = 1000.0f;
}