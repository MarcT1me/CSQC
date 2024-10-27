using Engine.Data;
using Engine.Event;
using Engine.Graphics.OpenGL;
using Engine.Graphics.Window;
using Engine.Objects;
using Engine.Objects.Tracer;
using Engine.Time;
using Engine.Threading;
using OpenTK.Graphics.OpenGL;

namespace Engine.App;

public abstract class App : QObject
{
    protected Window Window;
    protected QEventHandler QEventHandler;
    private readonly QTask _task;

    protected App() : base(EngineData.AppName)
    {
        QTracerAttribute<QObject>.HandleInstances();

        Window.InitialiseSdl();
        OpenGl.Initialise();
        Clock.Initialise();

        Window = new();
        OpenGl.SetGl();

        QEventHandler = new();
        _task = new(HandleEvents);
    }

    public static bool Running { get; set; } = true;

    protected void OnStart()
    {
        Console.WriteLine("OnStart");
    }

    private void HandleEvents(QTask sender, QExtended? args)
    {
        Console.WriteLine("start");
        while (Running)
        {
        }

        sender.Join();
    }

    private void Run()
    {
        OnStart();

        _task.Start();
        
        while (Running)
        {
            QEventHandler.HandleEvents();
            Update();
            Render();

            Window.MakeCurrent();
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            Window.SwapBuffers();
            Clock.Tick();
        }
    }

    protected void OnFailure(Exception exception)
    {
        Console.Error.WriteLine(exception.Message);
    }

    public void Dispose()
    {
        Running = false;
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