using Engine.Data;
using Engine.Graphics.Window;
using Engine.Objects;
using Engine.Objects.Tracer;

namespace Engine.App;

[QTracer<QObject>(TraceType.Scan)]
public abstract class App : QObject
{
    public static bool Running { get; set; } = true;

    protected Window Window;

    // public Clock Clock;

    [Obsolete("Obsolete")]
    public App() : base(id: EngineData.AppName)
    {
        QTracerAttribute<QObject>.HandleInstances();
        this.Window = new();
    }

    protected void OnStart()
    {
        Console.WriteLine("OnStart");

        this.Window.Run();
    }

    protected void OnFailure(Exception exception)
    {
        Console.Error.WriteLine(exception.Message);
    }

    public void Run()
    {
        OnStart();

        while (App.Running)
        {
            this.HandleEvent(1);
            this.Update(1);
            this.Render();
        }
    }

    public override void Dispose()
    {
        this.Window.Dispose();
    }

    public static void Mainloop<T>() where T : App
    {
        T? app = null;
        while (App.Running)
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