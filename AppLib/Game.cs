using Engine.App;
using Engine.Event;
using Engine.Graphics.OpenGL;
using Engine.Graphics.Window;
using Engine.Time;
using OpenTK.Mathematics;
using SDL2;

namespace AppLib;

public class Game : App
{
    private bool _show = true;

    private Window _mainWin;
    private Window _secondWin;

    protected override void PreInit()
    {
        base.PreInit();
        Clock.Fps = 0;

        _mainWin = new("Main", new WinData()
        {
            Title = "Main Window",
            Size = new(800, 600),
        }, new GlData()
        {
            ClearColor = new Vector4(1f, 0f, 0f, 1f),
        });
        _secondWin = new("Second", new WinData()
        {
            Title = "Second Window",
            Size = new(800, 600),
            Opacity = 0.5f,
        }, new GlData()
        {
            ClearColor = new Vector4(0f, 1f, 0f, 1f),
        });
    }

    public override void HandleEvent(SdlEventArgs e)
    {
        base.HandleEvent(e);
        switch (e.Event.type)
        {
            case SDL.SDL_EventType.SDL_KEYDOWN:
                switch (e.Event.key.keysym.sym)
                {
                    case SDL.SDL_Keycode.SDLK_ESCAPE:
                        Running = false;
                        break;
                    case SDL.SDL_Keycode.SDLK_f:
                        Console.WriteLine("Fps: " + Clock.GetFps());
                        Console.WriteLine("dt: " + Clock.DeltaTime);
                        break;
                    case SDL.SDL_Keycode.SDLK_c:
                        CallbackList["TestCall"].Invoke(null, "Test");
                        break;
                    case SDL.SDL_Keycode.SDLK_h:
                        if (e.Event.window.windowID == _mainWin.Id)
                        {
                            _show = !_show;
                        }

                        if (_show)
                            _secondWin.Maximize();
                        else
                            _secondWin.Minimize();
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