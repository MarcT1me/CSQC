using Engine.App;
using Engine.Data;
using Engine.Graphics.Window;
using Engine.Time;
using OpenTK.Mathematics;

namespace AppLib;

public static class Program
{
    public static void Main()
    {
        Clock.Fps = 0;

        WinData.Size = new Vector2i(720, 480);
        WinData.Title = EngineData.AppName;

        App.Mainloop<Game>();
    }
}