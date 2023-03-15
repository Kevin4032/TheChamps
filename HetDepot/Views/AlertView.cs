namespace HetDepot.Views;

public class AlertView
{
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
        Renderer.ConsoleWrite(_message, 0, Renderer.ConsoleWidth, 1, ' ', _backgroundColor);
        Thread.Sleep(SleepTime);
    }
    
}