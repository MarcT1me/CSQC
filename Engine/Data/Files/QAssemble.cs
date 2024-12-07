using System.Reflection;

namespace Engine.Data.Files;

public static class QAssembly
{
    static QAssembly()
    {
        // AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve!;
    }
    
    public static Assembly LoadFrom(Stream stream)
    {
        byte[] assemblyData = new byte[stream.Length];
        _ = stream.Read(assemblyData, 0, assemblyData.Length);

        return LoadFrom(assemblyData);
    }
    
    public static Assembly LoadFrom(byte[] file) => Assembly.Load(file);

    public static Assembly LoadFrom(string file) => Assembly.Load(file);

    public static Assembly Curent => Assembly.GetExecutingAssembly();

    public static Assembly GetFrom(Type type) => type.Assembly;

    public static Assembly? GetFrom(string name) => AppDomain.CurrentDomain.GetAssemblies()
        .FirstOrDefault(a => a.GetName().Name == name);

    public static Assembly[] GetAll(string name) => AppDomain.CurrentDomain.GetAssemblies();

    private static Assembly? CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
    {
        string libPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");
        string assemblyName = new AssemblyName(args.Name).Name + ".dll";
        string assemblyPath = Path.Combine(libPath, assemblyName);
        if (File.Exists(assemblyPath))
            return Assembly.LoadFrom(assemblyPath);
        return null;
    }
}