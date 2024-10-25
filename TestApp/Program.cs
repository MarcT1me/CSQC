using System.Reflection;

namespace TestApp
{
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("Loading assembly " + AppDomain.CurrentDomain.BaseDirectory + "AppCode.dll");
            
            string dllPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", "AppCode.dll");

            Assembly engineAppAssembly = Assembly.LoadFrom(dllPath);

            Type programType = engineAppAssembly.GetType("AppCode.Program");

            MethodInfo mainMethod = programType.GetMethod("Main", BindingFlags.Public | BindingFlags.Static);

            mainMethod.Invoke(null, null);
        }
    }
}