namespace Engine.Time;

[Flags]
public enum TimerMode
{
    Default,

    // List
    Cyclical,
    Finite,

    // Type 
    Causing,

    // Time duration
    Relative,
    Absolute,
}