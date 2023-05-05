namespace HetDepot.Views;

public class AlertView
{
    /*
     * The AlertView is a simple view that allows messages to be displayed for 2 seconds and with a specific background
     * color.
     */

    public const ConsoleColor Success = ConsoleColor.DarkGreen;
    public const ConsoleColor Error = ConsoleColor.DarkRed;
    public const ConsoleColor Info = ConsoleColor.Blue;

    private const int WordsPerSecond = 2; // Based on 265 words per minute 
    private const int MinSleepTime = 2000;

    private readonly string _message;
    private readonly ConsoleColor _backgroundColor;


    public AlertView(string message, ConsoleColor backgroundColor)
    {
        _message = message;
        _backgroundColor = backgroundColor;
    }

    public void Show()
    {
        int wordCountForMessage = _message.Count(c => c == ' ');

        int sleepTime = wordCountForMessage / WordsPerSecond * 1000;

        if (sleepTime < MinSleepTime)
            sleepTime = MinSleepTime;

        Renderer.ResetConsole(false);
        Renderer.ConsoleNewline(true);
        Renderer.ConsoleWrite("", 0, 0, 1, ' ', ConsoleColor.White, _backgroundColor);
        Renderer.ConsoleWrite(_message, 0, 0, 1, ' ', ConsoleColor.White, _backgroundColor);
        Renderer.ConsoleWrite("", 0, 0, 1, ' ', ConsoleColor.White, _backgroundColor);
        Thread.Sleep(sleepTime);
    }
}
