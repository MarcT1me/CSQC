namespace Engine.Objects;

public readonly struct QMeta(string? id) : IQMeta
{
    // just ID
    public string Id { get; } = id ?? Guid.NewGuid().ToString();
}