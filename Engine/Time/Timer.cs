namespace Engine.Time;

public class TimerEventArgs(bool success) : EventArgs
{
    public bool Success => success;
}

public class Timer(float duration, TimerMode mode, EventHandler callback)
{
    public static Dictionary<int, Timer> List { get; } = new();

    public float Duration = duration;
    public float RemainingTime = duration;
    public float StartTime = Clock.CurrentTime;
    public TimerMode Mode = mode;
    public EventHandler Callback = callback;
    public bool Running;

    public void Start()
    {
        List.TryAdd(GetHashCode(), this);
        StartTime = Clock.CurrentTime;
        RemainingTime = Duration;
        Running = true;
    }

    public void Stop() => Running = false;

    public bool Update()
    {
        RemainingTime -= Clock.DeltaTime;

        if (Running)
        {
            bool condition;
            if (Mode.HasFlag(TimerMode.Relative) || Mode.HasFlag(TimerMode.Default))
                condition = RemainingTime <= 0;
            else if (Mode.HasFlag(TimerMode.Absolute))
                condition =
                    Clock.CurrentTime - Clock.DeltaTime <= StartTime + Duration + RemainingTime &&
                    Clock.CurrentTime + Clock.DeltaTime >= StartTime + Duration + RemainingTime;
            else condition = false;

            if (condition)
            {
                Stop();
                if (Mode.HasFlag(TimerMode.Causing))
                    Callback(this, new TimerEventArgs(true));
                if (Mode.HasFlag(TimerMode.Finite) || Mode.HasFlag(TimerMode.Default))
                {
                    List.Remove(GetHashCode());
                }
                else if (Mode.HasFlag(TimerMode.Cyclical))
                {
                    Start();
                }

                return true;
            }
        }

        return false;
    }
}