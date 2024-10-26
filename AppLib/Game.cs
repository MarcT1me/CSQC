using Engine.App;

namespace AppLib;

public class Game : App
{
    [Obsolete("Obsolete")]
    public Game() : base()
    {
        Engine.Time.Clock.Fps = 60;
    }

    public override void HandleEvent(int eventId)
    {
        Console.WriteLine($"Game Event {eventId}");
    }

    public override void Update()
    {
        Console.WriteLine(Clock.GetFps());
    }

    public override void Render()
    {
    }
}