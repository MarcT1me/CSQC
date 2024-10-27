using OpenTK.Mathematics;
using SDL2;

namespace Engine.Graphics.Window;

public struct WinData
{
    // sizes
    private static Vector2i _size = new(1600, 900);
    private static Vector2 _resolutionScaling;

    public static Vector2i Size
    {
        get => _size;
        set
        {
            _size = value;
            Resolution = (Vector2i)(Size * _resolutionScaling);
        }
    }

    public static Vector2i ResolutionScaling
    {
        get => _size;
        set
        {
            _resolutionScaling = value;
            Resolution = (Size * value);
        }
    }

    public static Vector2i Resolution;

    // orientation
    public static Vector2i Position = new(SDL.SDL_WINDOWPOS_CENTERED);

    // other
    public static string Title = "QuantumGame";
    public static SDL.SDL_WindowFlags Flags;
}