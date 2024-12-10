namespace Engine.Time;

public delegate void TimerEventHandler(object sender, TimerEventArgs e);

public class TimerEventArgs : EventArgs
{
    public bool Success;
}

public class Timer(double duration, TimerMode mode, string name)
{
    public static Dictionary<string, Timer> List { get; } = new();

    public double RemainingTime = duration;
    public double StartTime = Clock.CurrentTime;
    public event TimerEventHandler? OnEvent;
    public bool Running;

    public void Start()
    {
        List.TryAdd(name, this);
        StartTime = Clock.CurrentTime;
        RemainingTime = duration;
        Running = true;
    }

    public void Stop() => Running = false;

    public bool Update()
    {
        RemainingTime -= Clock.DeltaTime;

        if (Running)
        {
            bool condition;
            if (mode.HasFlag(TimerMode.Relative) || mode.HasFlag(TimerMode.Default))
                condition = RemainingTime <= 0;
            else if (mode.HasFlag(TimerMode.Absolute))
                condition =
                    Clock.CurrentTime - Clock.DeltaTime <= StartTime + duration + RemainingTime &&
                    Clock.CurrentTime + Clock.DeltaTime >= StartTime + duration + RemainingTime;
            else condition = false;

            if (condition)
            {
                Stop();
                if (mode.HasFlag(TimerMode.Causing))
                    OnEvent?.Invoke(this, new TimerEventArgs { Success = true });
                if (mode.HasFlag(TimerMode.Finite) || mode.HasFlag(TimerMode.Default))
                {
                    List.Remove(name);
                }
                else if (mode.HasFlag(TimerMode.Cyclical))
                {
                    Start();
                }

                return true;
            }
        }

        return false;
    }
}