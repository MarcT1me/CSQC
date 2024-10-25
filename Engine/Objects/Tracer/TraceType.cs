namespace Engine.Objects.Tracer;

[Flags]
public enum TraceType
{
    Scan, // just adding

    // Methods
    Callback, // just Calling
    Bind, // Key Binds
    Node // Node Callback
}