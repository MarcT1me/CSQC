using Engine.App;
using Engine.Event;

namespace AppLib;

public static class Program
{
    public static void Main()
    {
        QEventHandler.IsMultiThread = false;
        App.Mainloop<Game>();
    }
}