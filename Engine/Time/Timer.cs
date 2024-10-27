namespace Engine.Time;

public delegate void TimerEventHandler(object sender, TimerEventArgs e);

public class TimerEventArgs : EventArgs
{
    public bool Success;
}

public class Timer(double duration, TimerMode mode)
{
    public static Dictionary<int, Timer> List { get; } = new();

    public double Duration = duration;
    public double RemainingTime = duration;
    public double StartTime = Clock.CurrentTime;
    public TimerMode Mode = mode;
    public event TimerEventHandler? OnEvent;
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
                    OnEvent?.Invoke(this, new TimerEventArgs { Success = true });
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