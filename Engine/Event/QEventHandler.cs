using SDL2;

namespace Engine.Event;

public delegate void SdlEventHandler(SdlEventArgs e);

public class SdlEventArgs : EventArgs
{
    public SDL.SDL_Event Event;
}

public sealed class QEventHandler()
{
    public static event SdlEventHandler? OnEvent;
    private static SDL.SDL_Event _e;

    public static void HandleEvents()
    {
        while (SDL.SDL_PollEvent(out _e) != 0)
        {
            OnEvent?.Invoke(new SdlEventArgs { Event = _e });
        }
    }
}