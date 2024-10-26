using Engine.Data;
using Engine.Graphics.Window;
using Engine.Objects;
using Engine.Objects.Tracer;
using Engine.Time;

namespace Engine.App;

[QTracer<QObject>(TraceType.Scan)]
public abstract class App : QObject
{
    protected Window Window;

    public Clock Clock;

    [Obsolete("Obsolete")]
    public App() : base(EngineData.AppName)
    {
        QTracerAttribute<QObject>.HandleInstances();
        Window = new();
        Clock = new();
    }

    public static bool Running { get; set; } = true;

    protected void OnStart()
    {
        Console.WriteLine("OnStart");

        Window.Run();
    }

    protected void OnFailure(Exception exception)
    {
        Console.Error.WriteLine(exception.Message);
    }

    public void Run()
    {
        OnStart();

        while (Running)
        {
            HandleEvent(0);
            Update();
            Render();

            Clock.Tick();
        }
    }

    public override void Dispose()
    {
        Window.Dispose();
    }

    public static void Mainloop<T>() where T : App
    {
        T? app = null;
        while (Running)
        {
            Console.WriteLine("Mainloop iteration");
            try
            {
                app = Activator.CreateInstance<T>();
                app.Run();
            }
            catch (Exception e)
            {
                app?.OnFailure(e);
                throw;
            }
            finally
            {
                app?.Dispose();
            }
        }
    }
}