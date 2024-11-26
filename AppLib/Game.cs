using Engine.App;
using Engine.Event;
using Engine.Graphics.OpenGL;
using Engine.Graphics.Window;
using Engine.Time;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using SDL2;

namespace AppLib;

public class Game : App
{
    private Test.Test _test;

    protected override void PreInit()
    {
        base.PreInit();
        Clock.Fps = 0;
    }

    public Game() : base(
        new("Main", new WinData
        {
            Title = "Main Window",
            Size = new(800, 600)
        }, new GlData
        {
            ClearColor = new Vector4(0.1f, 0.5f, 0.4f, 1f)
        })
    )
    {
        GL.LoadBindings(new SdlBindingsContext());
        _test = new();
    }

    protected override void PostInit()
    {
        base.PostInit();
        _mainWindow.SetCurrent();
        _test.Update();
        _mainWindow.Show();
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
                }

                break;
        }
    }

    public override void Render()
    {
        _mainWindow.Render();
        _test.Render();
        _mainWindow.SwapBuffers();
    }
}