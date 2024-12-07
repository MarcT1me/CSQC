using System.Reflection;

namespace Engine.Data;

public struct QData
{
    public static string RootDirectory = "";
    public static Assembly AppLibAssembly;

    public static bool IsRelease;
    public static string AppName;
}