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
    private Window _mainWin;
    private Test.Test _test;

    protected override void PreInit()
    {
        base.PreInit();
        _mainWin = new("Main", new WinData()
        {
            Title = "Main Window",
            Size = new(800, 600),
        }, new GlData()
        {
            ClearColor = new Vector4(0.1f, 0.5f, 0.4f, 1f),
        });
        GL.LoadBindings(new SdlBindingsContext());
        Clock.Fps = 0;
    }

    protected override void PostInit()
    {
        base.PostInit();
        _mainWin.SetCurrent();
        _test = new();
        _test.Update();
        _mainWin.Show();
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
        _mainWin.Render();
        _test.Render();
        _mainWin.SwapBuffers();
    }
}