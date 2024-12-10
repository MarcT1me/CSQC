using Engine.Event;
using Engine.Graphics.OpenGL;
using Engine.Graphics.OpenGL.Vertex;
using Engine.Graphics.Window;
using Engine.Objects;
using Engine.Objects.Tracer;
using Engine.Time;
using SDL2;
using OpenTK;
using OpenTK.Graphics.OpenGL;


namespace Engine.App;

public class SdlBindingsContext : IBindingsContext
{
    public IntPtr GetProcAddress(string procName)
    {
        return SDL.SDL_GL_GetProcAddress(procName);
    }
}

public abstract class App : QObject<QMeta>
{
    protected Window _mainWindow;
    
    protected virtual void PreInit()
    {
        Window.InitialiseSdl();
        OpenGl.Initialise();
        Clock.Initialise();
    }

    protected App(Window mainWindow)
    {
        GL.LoadBindings(new SdlBindingsContext());
        PreInit();
        _mainWindow = mainWindow;
    }

    public static bool Running { get; set; } = true;

    protected virtual void PostInit()
    {
        foreach (Window window in Window.Roster.Values)
        {
            window.SetGl();
            window.PostInit();
        }
    }

    public override void HandleEvent(SdlEventArgs e)
    {
        switch (e.Event.type)
        {
            case SDL.SDL_EventType.SDL_QUIT:
                Console.WriteLine("Quit");
                Running = false;
                break;
        }
    }

    public override void Update()
    {
        foreach (Window win in Window.Roster.Values) win.Update();
    }

    public override void Render()
    {
        foreach (Window win in Window.Roster.Values) win.Render();
    }

    private void Run()
    {
        PostInit();
        
        while (Running)
        {
            QEventHandler.HandleEvents();
            Update();
            Render();

            Clock.Tick();
        }
    }

    protected virtual void OnFailure(Exception exception)
    {
        Console.Error.WriteLine(exception.Message);
    }

    public override void Dispose()
    {
        base.Dispose();

        foreach (var win in Window.Roster.Values) win.Dispose();

        Window.UnInitialiseSdl();
    }

    public static void Mainloop<T>() where T : App
    {
        QTracerAttribute<QObject<QMeta>>.HandleInstances();

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