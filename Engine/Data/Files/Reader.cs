namespace Engine.Data.Files;

public class Reader
{
    private readonly string _rootDirectory;

    protected Reader(string rootDirectory)
    {
        _rootDirectory = rootDirectory;
    }

    public string ReadTextFile(string path)
    {
        return File.ReadAllText(_rootDirectory + "\\" + path);
    }

    public Stream ReadStream(string path)
    {
        return File.OpenRead(_rootDirectory + "\\" + path);
    }
}