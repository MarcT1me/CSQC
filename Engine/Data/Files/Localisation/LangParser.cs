using IniParser;
using IniParser.Model;
using OpenTK.Mathematics;

namespace Engine.Data.Files.Localisation;

public class LangParser
{
    private readonly FileIniDataParser _parser;
    public static string? Directory = null;

    public LangParser()
    {
        _parser = new FileIniDataParser();
    }

    public LangData ParseFile(string language, string? filePath = null)
    {
        IniData data = _parser.ReadFile((filePath ?? Directory) + "\\" + language + ".ini");

        LangData localisation = new()
        {
            Range = new Vector2i
            {
                X = int.Parse(data["characters"]["first"]),
                Y = int.Parse(data["characters"]["final"])
            },
            phrase = new()
        };
        foreach (KeyData phrase in data["translation"])
        {
            localisation.phrase.Add(phrase.KeyName, phrase.Value);
        }

        return localisation;
    }
}