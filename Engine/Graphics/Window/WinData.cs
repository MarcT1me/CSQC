using OpenTK.Mathematics;
using SDL2;

namespace Engine.Graphics.Window;

public struct WinData
{
    // sizes
    private Vector2i _size = new(1600, 900);
    private Vector2 _resolutionScaling;

    public Vector2i Size
    {
        get => _size;
        set
        {
            _size = value;
            Resolution = (Vector2i)(Size * _resolutionScaling);
        }
    }

    public Vector2i ResolutionScaling
    {
        get => _size;
        set
        {
            _resolutionScaling = value;
            Resolution = (Size * value);
        }
    }

    public Vector2i Resolution;

    // orientation
    public Vector2i Position = new(SDL.SDL_WINDOWPOS_CENTERED);

    // other
    public string Title = "QuantumGame";
    public SDL.SDL_WindowFlags Flags;
    public float Opacity = 1.0f;

    public WinData()
    {
        _resolutionScaling = default;
        Resolution = default;
        Flags = (SDL.SDL_WindowFlags)0;
    }
}