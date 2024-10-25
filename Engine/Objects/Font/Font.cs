using OpenTK.Mathematics;
// engine
using Engine.Data.Files.Localisation;
using Engine.Graphics.Fonts.FreeType;

#pragma warning disable CS8500

namespace Engine.Graphics.Fonts;

public class Font : FreeTypeFont
{
    public static Dictionary<string, Font> Fonts = new(); // list of Fonts
    public static Dictionary<string, FontData> List = new(); // list of Text lines
    public readonly string Name; // Font name
    public static string Directory = @"C:\Windows\Fonts"; // current Font directory

    public Font(string name, uint fontSize, string font, LangData lang, string? path = null)
        : base(fontSize, $"{path ?? Directory}\\" + font + ".ttf", lang.Range)
    {
        Name = name;
        Fonts.Add(Name, this);
    }

    public void AddText(
        string name, string text, Vector2i pos,
        Vector4? color = null, Vector2? dir = null, string? lang = null)
    {
        FontData data = new FontData
        {
            Font = Name,
            Text = text,
            Pos = pos,
            Color = color ?? Vector4.One,
            Dir = dir ?? Vector2.Zero,
            Lang = lang ?? "Eng"
        };
        List.Add(name, data);
    }

    public void AddText(
        string name, LangData localisation, Vector2i pos,
        Vector4? color = null, Vector2? dir = null, string? lang = null)
    {
        AddText(name, localisation.phrase[name], pos, color, dir, lang);
    }

    public void AddPhrase(
        string name, LangData localisation, Vector2i pos, object[] args,
        Vector4? color = null, Vector2? dir = null, string? lang = null
    )
    {
        string text = String.Format(localisation.phrase[name], args);
        AddText(name, text, pos, color, dir, lang);
    }

    public void DelText(string name)
    {
        // deleting the specified text line
        List.Remove(name);
    }

    public new void Dispose()
    {
        Fonts.Remove(Name); // dell Font
        foreach (KeyValuePair<string, FontData> fontData in List)
        {
            // deleting lines of text that use the current font
            if (fontData.Value.Font == Name)
                List.Remove(fontData.Key);
        }

        base.Dispose();
    }

    public void Render(string name)
    {
        // rendering the text of the specified name
        RenderText(List[name].Text, List[name].Pos, List[name].Dir);
    }
}