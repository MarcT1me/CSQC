using IniParser;
using OpenTK.Mathematics;

namespace Engine.Data.Files.Localisation;

public class LangParser
{
    public static string? Directory = null;
    private readonly FileIniDataParser _parser;

    public LangParser()
    {
        _parser = new FileIniDataParser();
    }

    public LangData ParseFile(string language, string? filePath = null)
    {
        var data = _parser.ReadFile((filePath ?? Directory) + "\\" + language + ".ini");

        LangData localisation = new()
        {
            Range = new Vector2i
            {
                X = int.Parse(data["characters"]["first"]),
                Y = int.Parse(data["characters"]["final"])
            },
            phrase = new Dictionary<string, string>()
        };
        foreach (var phrase in data["translation"]) localisation.phrase.Add(phrase.KeyName, phrase.Value);

        return localisation;
    }
}