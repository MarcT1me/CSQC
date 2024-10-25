using System.Reflection;
using System.Runtime.InteropServices;

namespace TestApp;

internal abstract class Program
{
    private static void Main()
    {
        // Подписываемся на событие AssemblyResolve
        AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve!;

        // Устанавливаем директорию поиска библиотек
        string relativePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");

        Console.WriteLine("Loading assemblies from " + relativePath);
        NativeLibrary.Load(Path.Combine(relativePath, "glfw3.dll")); // load GLFW from bin (publish)

        Assembly engineLib = Assembly.Load("Engine"); // load Engine
        Assembly appLib = Assembly.Load("AppLib"); // load App
        // set App in the Engine variables
        Type? engineDataType = engineLib.GetType("Engine.Data.EngineData")!;
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
        // Путь к папке с библиотеками
        string libPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");

        // Имя запрашиваемой сборки
        string assemblyName = new AssemblyName(args.Name).Name + ".dll";

        // Путь к сборке
        string assemblyPath = Path.Combine(libPath, assemblyName);

        // Проверяем, существует ли файл
        if (File.Exists(assemblyPath))
        {
            // Загружаем сборку
            return Assembly.LoadFrom(assemblyPath);
        }

        // Если сборка не найдена, возвращаем null
        return null;
    }
}