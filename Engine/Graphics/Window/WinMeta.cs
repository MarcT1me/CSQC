using Engine.Graphics.OpenGL;
using Engine.Objects;

namespace Engine.Graphics.Window;

public readonly struct WinMeta(string id, int index, WinData winData, GlData glData) : IQMeta
{
    public string Id { get;} = id;
    public int Index { get; } = index;
    public WinData WinData { get; } = winData;
    public GlData GlData { get; } = glData;
}