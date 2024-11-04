using Engine.Data;
using Engine.Threading;
using SDL2;

namespace Engine.Event;

public delegate void SdlEventHandler(SdlEventArgs e);

public class SdlEventArgs : EventArgs
{
    public SDL.SDL_Event Event;
}

public sealed class QEventHandler
{
    public static bool IsMultiThread = true;

    public static event SdlEventHandler? OnEvent;
    private static SDL.SDL_Event _e;

    private static void HandleEvent(QTask sender, QExtended args)
    {
        dynamic eventData = args;
        OnEvent?.Invoke(new SdlEventArgs { Event = eventData.Event });
        sender.Stop();
    }

    public static void HandleEvents()
    {
        while (SDL.SDL_PollEvent(out _e) != 0)
        {
            dynamic obj = new QExtended();
            obj.Event = _e;
            if (IsMultiThread)
                new QTask(HandleEvent).Start(obj);
            else 
                OnEvent?.Invoke(new SdlEventArgs { Event = _e });
        }
    }
}