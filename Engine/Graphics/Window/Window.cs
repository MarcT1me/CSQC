using Engine.Event;
using Engine.Graphics.OpenGL;
using Engine.Objects;
using SDL2;
using OpenTK.Graphics.OpenGL;

namespace Engine.Graphics.Window;

using System;

public class Window : QObject<WinMeta>
{
    public static readonly object Lock = new();
    public static Dictionary<int, Window> Roster = new();

    private IntPtr _window;
    private readonly IntPtr _glContext;

    public IntPtr Get => _window;
    public int Id => (int)SDL.SDL_GetWindowID(_window);

    public static void InitialiseSdl()
    {
        SDL.SDL_Init(SDL.SDL_INIT_VIDEO);
    }

    public static void UnInitialiseSdl()
    {
        SDL.SDL_Quit();
    }

    public Window(string name, WinData winData, GlData glData)
    {
        _window = SDL.SDL_CreateWindow(
            winData.Title,
            winData.Position.X, winData.Position.Y,
            winData.Size.X, winData.Size.Y,
            winData.Flags | SDL.SDL_WindowFlags.SDL_WINDOW_OPENGL | SDL.SDL_WindowFlags.SDL_WINDOW_HIDDEN);
        if (_window == IntPtr.Zero)
        {
            throw new Exception("Failed to create window: " + SDL.SDL_GetError());
        }

        _glContext = SDL.SDL_GL_CreateContext(_window);
        if (_glContext == IntPtr.Zero)
        {
            throw new Exception("Failed to create OpenGL context: " + SDL.SDL_GetError());
        }

        if (glData.Resolution == default)
            glData.Resolution = winData.Resolution;
        QMeta = new WinMeta(id: name, index: Id, winData: winData, glData: glData);
        Roster.Add(Id, this);
        
        
    }

    public void Show() => SDL.SDL_ShowWindow(_window);
    public void Hide() => SDL.SDL_HideWindow(_window);
    public void Raise() => SDL.SDL_RaiseWindow(_window);
    public void Minimize() => SDL.SDL_MinimizeWindow(_window);
    public void Maximize() => SDL.SDL_MaximizeWindow(_window);
    public void SetOpacity(float opacity) => SDL.SDL_SetWindowOpacity(_window, opacity);

    public void SetCurrent() => SDL.SDL_GL_MakeCurrent(_window, _glContext);
    public void SwapBuffers() => SDL.SDL_GL_SwapWindow(_window);

    public void DeleteContext() => SDL.SDL_GL_DeleteContext(_glContext);

    public void Close() => SDL.SDL_DestroyWindow(_window);

    public void SetGl()
    {
        SetCurrent();
        OpenGl.SetGl(QMeta.GlData);
    }

    public override void Dispose()
    {
        if (QEventHandler.IsMultiThread && Roster.Count == 1 || _window == IntPtr.Zero) return;
        base.Dispose();
        Roster.Remove(Id);

        Close();
        DeleteContext();
        _window = IntPtr.Zero;
    }

    public override void HandleEvent(SdlEventArgs e)
    {
        switch (e.Event.window.windowEvent)
        {
            case (SDL.SDL_WindowEventID.SDL_WINDOWEVENT_FOCUS_LOST):
                SDL.SDL_StopTextInput();
                break;
            case (SDL.SDL_WindowEventID.SDL_WINDOWEVENT_FOCUS_GAINED):
                SDL.SDL_StartTextInput();
                break;
            case (SDL.SDL_WindowEventID.SDL_WINDOWEVENT_CLOSE):
                if (e.Event.window.windowID == Id)
                {
                    Dispose();
                }

                break;
        }
    }

    public override void Update()
    {
    }

    public override void Render()
    {
        SetCurrent();
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        SwapBuffers();
    }
}