using Engine.App;
using Engine.Data;
using Engine.Graphics.Window;
using OpenTK.Mathematics;

#pragma warning disable CS8500
namespace AppLib;

public static class Program
{
    public static void Main()
    {
        EngineData.IsRelease = false;

        WinData.Size = new Vector2i(720, 480);
        WinData.Title = EngineData.AppName;
        
        App.Mainloop<Game>();
    }
}