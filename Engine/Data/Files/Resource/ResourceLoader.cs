using System.Reflection;

namespace Engine.Data.Files.Resource;

public class ResourceLoader
{
    public static readonly Dictionary<string, object?> Resources = new();
    public static Dictionary<string, ResourceLoader> List = new()
    {
        {"dll", new(".dll", typeof(QAssembly))}
    };
    
    public string Extension;
    private Type _resourceType;
    private MethodInfo _loadMethod;
    
    public ResourceLoader(string extension, Type resourceType)
    {
        Extension = extension;
        var loadMethod = resourceType.GetMethod("FromStream", BindingFlags.Public | BindingFlags.Static);
        if (loadMethod == null)
            throw new Exception($"Not found resource load FromStream method in {resourceType.FullName}");
        _loadMethod = loadMethod;
        _resourceType = resourceType;
    }

    public void LoadEmbeddedResources(Assembly assembly)
    {
        string[] resNames = assembly.GetManifestResourceNames();

        foreach (string resName in resNames)
        {
            using (Stream? stream = assembly.GetManifestResourceStream(resName))
            {
                if (stream == null)
                {
                    Console.WriteLine($"Can't load {resName} resource stream");
                    continue;
                }

                if (resName.EndsWith(Extension, StringComparison.OrdinalIgnoreCase))
                {
                    Resources.Add($"{assembly.FullName}:{resName}", LoadFromStream(stream));
                }
            }
        }
    }

    public object? LoadFromStream(Stream stream)
    {
        return _loadMethod.Invoke(null, [stream]);
    }
}