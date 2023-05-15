namespace HetDepot.Views;

public class AlertView
{
    /*
     * The AlertView is a simple view that allows messages to be displayed for a few seconds and with a specific background
     * color. If the user presses any key, the view will be canceled early
     * (timeout adapted from: https://stackoverflow.com/questions/57615/how-to-add-a-timeout-to-console-readline)
     */

    private static Thread? _inputThread;
    private static AutoResetEvent? _getInput, _gotInput;
    private static ConsoleKeyInfo input;


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
        _getInput = new AutoResetEvent(false);
        _gotInput = new AutoResetEvent(false);
        _inputThread = new Thread(reader);
        _inputThread.IsBackground = true;
        _inputThread.Start();
    }

    private static void reader() {
        while (true) {
            _getInput!.WaitOne();
            input = Console.ReadKey();
            _gotInput!.Set();
        }
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
        Renderer.ConsoleNewline();
        Renderer.ConsoleWrite(_message, 0, 0, 1, ' ', ConsoleColor.White, _backgroundColor);
        Renderer.ConsoleNewline();
        Renderer.ConsoleWrite("", 0, 0, 1, ' ', ConsoleColor.White, _backgroundColor);
        Renderer.ConsoleNewline();
        _getInput!.Set();
        _gotInput!.WaitOne(sleepTime);
    }
}
