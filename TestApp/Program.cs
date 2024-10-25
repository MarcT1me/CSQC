using System.Reflection;

namespace TestApp;

internal abstract class Program
{
    private static void Main()
    {
        // Подписываемся на событие AssemblyResolve
        AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve!;

        string dllPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", "AppLib.dll");
        Console.WriteLine("Loading assembly " + dllPath);
        Assembly engineAppAssembly = Assembly.LoadFrom(dllPath);
        Type? programType = engineAppAssembly.GetType("AppLib.Program");
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