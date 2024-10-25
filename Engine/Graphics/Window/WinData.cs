using OpenTK.Mathematics;
using OpenTK.Windowing.Common;

namespace Engine.Graphics.Window;

public struct WinData
{
    // sizes
    public static Vector2i Size = new Vector2i(1600, 900);
    public static Vector2 ResolutionScaling;
    public static Vector2i Resolution;

    // orientation
    public static int Display;
    public static Vector2i Position;

    // other
    public static string Title = "QuantumGame";
    public static float Opacity = 1.0f;
    public static VSyncMode VSync = VSyncMode.Adaptive;
    public static bool FullScreen = false;
    public static int FrameRate = 60;

    public WinData()
    {
        ResolutionScaling = default;
        Display = default;
        Position = default;

        Update();
    }

    public void Update()
    {
        Resolution = (Vector2i)(Size * ResolutionScaling);
    }
}