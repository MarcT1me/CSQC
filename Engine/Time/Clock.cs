using SDL2;

namespace Engine.Time;

public class Clock
{
    private static uint _fps;
    private static float _frameDelay;

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
                _frameDelay = 1000.0f / value;
            }
        }
    }

    public static float DeltaTime { get; private set; }
    public static float LastTime { get; private set; }
    public static int CurrentTime { get; private set; }

    public Clock(float? fps = null)
    {
        if (fps.HasValue)
        {
            Fps = (uint)fps.Value;
        }

        LastTime = SDL.SDL_GetTicks();
    }

    public float GetFps()
    {
        return DeltaTime * Fps;
    }

    public void Tick()
    {
        CurrentTime = (int)SDL.SDL_GetTicks();
        uint deltaTime = (uint)(CurrentTime - LastTime);
        LastTime = CurrentTime;
        DeltaTime = deltaTime / 1000.0f;

        if (DeltaTime < _frameDelay)
        {
            uint delay = (uint)(_frameDelay - deltaTime);
            SDL.SDL_Delay(delay);
            deltaTime += delay;
            DeltaTime = deltaTime / 1000.0f;
        }
    }
}