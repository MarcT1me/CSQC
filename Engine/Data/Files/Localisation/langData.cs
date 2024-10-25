using OpenTK.Mathematics;

namespace Engine.Data.Files.Localisation;

public struct LangData
{
    public Vector2i Range;

    // ReSharper disable once InconsistentNaming
    public Dictionary<string, string> phrase;
}