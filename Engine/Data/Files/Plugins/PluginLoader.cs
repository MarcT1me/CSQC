using Engine.Data.Files.Resource;

namespace Engine.Data.Files.Plugins;

public class PluginLoader
{
    public PluginLoader(string directory)
    {
        if ((bool)QData.IsRelease!)
        {
            ResourceLoader.List["dll"].LoadEmbeddedResources(QAssembly.Curent);
        }
        else
        {
            foreach (string file in Directory.GetFiles(directory, "*.dll"))
                QAssembly.LoadFrom(file);
        }
    }
}