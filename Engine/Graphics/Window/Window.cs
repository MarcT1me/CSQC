using System.ComponentModel;
using Engine.Data;
using Engine.Data.Files.Image;
using Engine.Objects;
using Engine.Objects.Tracer;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Common.Input;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Image = OpenTK.Windowing.Common.Input.Image;
// Engine
// other

namespace Engine.Graphics.Window;

public class Window : GameWindow
{
    [Obsolete("Obsolete")]
    public Window() : base(
        new GameWindowSettings(), new NativeWindowSettings
        {
            // Window
            Title = WinData.Title,
            Size = WinData.Size,
            Vsync = WinData.VSync,
            // OpenGl
            APIVersion = GLData.ApiVersions,
            API = ContextAPI.OpenGL,
            Profile = ContextProfile.Core,
            NumberOfSamples = GLData.NumberOfSamples,
            DepthBits = GLData.DepthBits,
            Flags = !EngineData.IsRelease
                ? ContextFlags.Debug
                : new Vector2(GLData.ApiVersions.Major, GLData.ApiVersions.Minor).Length
                  >
                  new Vector2(3, 3).Length
                    ? ContextFlags.ForwardCompatible
                    : ContextFlags.Default
        }
    )
    {
        var image = new ImageReader(
            @"Data\Assets\QuantumCore.png"
        ).ImageData;
        Icon = new WindowIcon(new Image(image.Size.X, image.Size.Y, image.Data));

        KeyDown += OnKeyDownCb;
    }

    private void OnKeyDownCb(KeyboardKeyEventArgs e)
    {
        if (e.Key == Keys.A) Console.WriteLine("A pressed");
    }

    protected override void OnUpdateFrame(FrameEventArgs frame)
    {
        if (KeyboardState.IsKeyDown(Keys.Escape))
        {
            Console.WriteLine("Escape");
            Close();
        }
        else if (KeyboardState.IsKeyDown(Keys.Enter))
        {
            QTracerAttribute<QObject>.HandleInstances();
        }
        else if (KeyboardState.IsKeyDown(Keys.Space))
        {
            var v = QObject.CallbackList["TestCall"].Invoke(null, "Hello Test!");
            Console.WriteLine(v);
        }
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        base.OnClosing(e);
        Console.WriteLine("Close");
        App.App.Running = false;
    }

    public override void Dispose()
    {
        base.Dispose();
        Console.WriteLine("Window dispose");
    }
}