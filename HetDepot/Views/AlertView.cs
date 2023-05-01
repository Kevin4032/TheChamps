namespace HetDepot.Views;

public class AlertView
{

    /*
     * The AlertView is a simple view that allows messages to be displayed for 2 seconds and with a specific background
     * color.
     */

    public const ConsoleColor Success = ConsoleColor.Green;
    public const ConsoleColor Error = ConsoleColor.DarkRed;
    public const ConsoleColor Info = ConsoleColor.Blue;

    private const int SleepTime = 2000;

    private readonly string _message;
    private readonly ConsoleColor _backgroundColor;


    public AlertView(string message, ConsoleColor backgroundColor)
    {
        _message = message;
        _backgroundColor = backgroundColor;
    }

    public void Show()
    {
        Renderer.ResetConsole(false);
        Renderer.ConsoleNewline(true);
        Renderer.ConsoleWrite(_message, 0, 0, 1, ' ', _backgroundColor);
        Thread.Sleep(SleepTime);
    }

}
