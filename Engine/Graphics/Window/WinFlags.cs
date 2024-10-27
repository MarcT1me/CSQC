using SDL2;

namespace Engine.Graphics.Window;

public struct WinFlags
{
    public static SDL.SDL_WindowFlags Show = SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN;
    public static SDL.SDL_WindowFlags Hidden = SDL.SDL_WindowFlags.SDL_WINDOW_HIDDEN;
    public static SDL.SDL_WindowFlags Fullscreen = SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN;
    public static SDL.SDL_WindowFlags FullscreenDesktop = SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN_DESKTOP;
    public static SDL.SDL_WindowFlags Borderless = SDL.SDL_WindowFlags.SDL_WINDOW_BORDERLESS;
    public static SDL.SDL_WindowFlags Resizable = SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE;
    public static SDL.SDL_WindowFlags Minimized = SDL.SDL_WindowFlags.SDL_WINDOW_MINIMIZED;
    public static SDL.SDL_WindowFlags Maximized = SDL.SDL_WindowFlags.SDL_WINDOW_MAXIMIZED;
}