using SDL2;

namespace Engine.Time;

public static class Clock
{
    private static uint _fps;
    private static double _frameDelay;

    // public Clock fields
    public static uint Fps
    {
        get => _fps;
        set
        {
            if (value == 0)
            {
                _frameDelay = 0;
            }
            else
            {
                _fps = value;
                _frameDelay = 1000.0d / value;
            }
        }
    }

    public static double DeltaTime { get; private set; }
    public static double CurrentTime { get; private set; }

    public static void Initialise(float? fps = null)
    {
        if (fps.HasValue)
        {
            Fps = (uint)fps.Value;
        }

        CurrentTime = SDL.SDL_GetTicks();
    }

    public static double GetFps()
    {
        return 1.0d / DeltaTime;
    }

    public static void Tick()
    {
        UInt64 currentCounter = SDL.SDL_GetPerformanceCounter();
        UInt64 currentFrequency = SDL.SDL_GetPerformanceFrequency();
        double currentTime = (double)currentCounter / currentFrequency;
        double deltaTime = currentTime - CurrentTime;
        CurrentTime = currentTime;
        DeltaTime = deltaTime;

        if (deltaTime < _frameDelay)
        {
            double delay = _frameDelay - deltaTime;
            SDL.SDL_Delay((uint)delay);
        }
    }
}