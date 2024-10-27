using SDL2;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Engine.Graphics.Window;

internal class SdlBindingsContext : IBindingsContext
{
    public IntPtr GetProcAddress(string procName)
    {
        return SDL.SDL_GL_GetProcAddress(procName);
    }
}

public class Window
{
    private readonly IntPtr _window;
    private readonly IntPtr _glContext;

    public IntPtr Get => _window;

    public static void InitialiseSdl()
    {
        SDL.SDL_Init(SDL.SDL_INIT_VIDEO);
    }

    public static void UnInitialiseSdl()
    {
        SDL.SDL_Quit();
    }

    public Window()
    {
        _window = SDL.SDL_CreateWindow(
            WinData.Title,
            WinData.Position.X, WinData.Position.Y,
            WinData.Size.X, WinData.Size.Y,
            WinData.Flags | SDL.SDL_WindowFlags.SDL_WINDOW_OPENGL);
        if (_window == IntPtr.Zero)
        {
            throw new Exception("Failed to create window: " + SDL.SDL_GetError());
        }

        _glContext = SDL.SDL_GL_CreateContext(_window);
        if (_glContext == IntPtr.Zero)
        {
            throw new Exception("Failed to create OpenGL context: " + SDL.SDL_GetError());
        }

        GL.LoadBindings(new SdlBindingsContext());
    }

    public void Show() => SDL.SDL_ShowWindow(_window);
    public void Hide() => SDL.SDL_HideWindow(_window);
    public void Raise() => SDL.SDL_RaiseWindow(_window);
    public void Minimize() => SDL.SDL_MinimizeWindow(_window);
    public void Maximize() => SDL.SDL_MaximizeWindow(_window);

    public void MakeCurrent() => SDL.SDL_GL_MakeCurrent(_window, _glContext);
    public void SwapBuffers() => SDL.SDL_GL_SwapWindow(_window);

    public void DeleteContext() => SDL.SDL_GL_DeleteContext(_glContext);
    public void Close() => SDL.SDL_DestroyWindow(_window);

    public void Dispose()
    {
        Close();
        DeleteContext();
    }
}