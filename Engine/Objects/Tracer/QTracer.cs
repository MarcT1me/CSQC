using System.Diagnostics;
using System.Reflection;
using Engine.Data;

namespace Engine.Objects.Tracer;

public delegate object? Callback(string? id, params object[] args);

[AttributeUsage(AttributeTargets.All)]
public sealed class QTracerAttribute<T>(TraceType traceType) : Attribute where T : class
{
    private void Handle(Type @class)
    {
        // if Flag not allow
        Debug.Assert(traceType.HasFlag(TraceType.Scan), "traceType does not match the Scan");

        var list = typeof(T).GetField(
            "List", BindingFlags.Public | BindingFlags.Static
        )?.GetValue(null) as Dictionary<string, Type>;

        // if already exist
        if ((bool)list?.TryGetValue(@class.Name, out var _)) return;

        list.Add(@class.Name, @class);
    }

    private void Handle(Type @class, MethodInfo method)
    {
        Debug.Assert(traceType.HasFlag(TraceType.Callback), "traceType does not match the Callback");

        // getting Callback List from T
        var list = typeof(T).GetField(
            "CallbackList",
            BindingFlags.Public | BindingFlags.Static
        )?.GetValue(@class) as Dictionary<string, Callback>;

        // if already exist
        if ((bool)list?.TryGetValue(method.Name, out var _)) return;

        // adding Invoker
        list.Add(
            method.Name,
            (id, args) =>
                method.Invoke(
                    method.IsStatic ? @class : null,
                    BindingFlags.InvokeMethod |
                    (method.IsStatic ? BindingFlags.Static : BindingFlags.Instance) |
                    (method.IsPublic ? BindingFlags.Public : BindingFlags.NonPublic),
                    null,
                    args,
                    null
                ) // Lambda Callback
        );
    }

    public static void HandleInstances()
    {
        var types = EngineData.AppLibAssembly.GetTypes(); // get all classes

        foreach (var @class in types)
        {
            // get class attributes
            var classAttributes = @class.GetCustomAttributes(typeof(QTracerAttribute<T>), false);

            foreach (var attr in
                     classAttributes) ((QTracerAttribute<T>)attr).Handle(@class); // Handle as class instance

            foreach (var method in @class.GetMethods())
            {
                // get method attributes
                var methodAttributes = method.GetCustomAttributes(typeof(QTracerAttribute<T>), false);

                foreach (var attr in
                         methodAttributes) ((QTracerAttribute<T>)attr).Handle(@class, method); // Handle as Callback
            }
        }
    }
}