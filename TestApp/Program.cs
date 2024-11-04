using System.Reflection;
using System.Runtime.InteropServices;

namespace TestApp;

internal abstract class Program
{
    private static void Main()
    {
        AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve!;

        // Setting the library search directory
        string relativePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");

        Console.WriteLine("Loading assemblies from " + relativePath);
        // Hard loading of dependencies (publish)
        NativeLibrary.Load(Path.Combine(relativePath, "glfw3.dll"));
        NativeLibrary.Load(Path.Combine(relativePath, "SDL2.dll"));

        Assembly engineLib = Assembly.Load("Engine"); // load Engine
        Assembly appLib = Assembly.Load("AppLib"); // load App
        // set App in the Engine variables
        Type engineDataType = engineLib.GetType("Engine.Data.EngineData")!;
        engineDataType.GetField("RootDirectory")?.SetValue(null, AppDomain.CurrentDomain.BaseDirectory);
        engineDataType.GetField("AppLibAssembly")?.SetValue(null, appLib);
        engineDataType.GetField("AppName")?.SetValue(null, AppDomain.CurrentDomain.FriendlyName);
#if DEBUG
        engineDataType.GetField("IsRelease")?.SetValue(null, false);
#else
        engineDataType.GetField("IsRelease")?.SetValue(null, true);
#endif

        // invoke Main function
        Type? programType = appLib.GetType("AppLib.Program");
        MethodInfo? mainMethod = programType?.GetMethod("Main", BindingFlags.Public | BindingFlags.Static);
        mainMethod?.Invoke(null, null);
    }

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