using OpenTK.Mathematics;

namespace Engine.Graphics.Window;

public struct GLData
{
    // scaling
    public static Vector2i Resolution;
    public static Vector2i Position;
    public static int NumberOfSamples = 8;
    public static readonly int DepthBits = 24;

    // other
    public static readonly Version ApiVersions = new(3, 3);
    public static float Near = 0.00001f;
    public static float Far = 1000.0f;

    public GLData()
    {
        Position = default;
    }

    public void Update()
    {
        Resolution = WinData.Size;
    }
}