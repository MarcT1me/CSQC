using Engine.App;
using Engine.Event;
using SDL2;

namespace AppLib;

public class Game : App
{
    public override void HandleEvent(SdlEventArgs e)
    {
        switch (e.Event.type)
        {
            case SDL.SDL_EventType.SDL_QUIT:
                Running = false;
                break;
            case SDL.SDL_EventType.SDL_KEYDOWN:
                switch (e.Event.key.keysym.sym)
                {
                    case SDL.SDL_Keycode.SDLK_ESCAPE:
                        Running = false;
                        break;
                    case SDL.SDL_Keycode.SDLK_f:
                        Console.WriteLine("Fps: " + Engine.Time.Clock.GetFps());
                        Console.WriteLine("dt: " + Engine.Time.Clock.DeltaTime);
                        break;
                    case SDL.SDL_Keycode.SDLK_c:
                        CallbackList["TestCall"].Invoke(null, "Test");
                        break;
                    case SDL.SDL_Keycode.SDLK_h:
                        Window.Minimize();
                        break;
                }

                break;
        }
    }

    public override void Update()
    {
    }

    public override void Render()
    {
    }
}