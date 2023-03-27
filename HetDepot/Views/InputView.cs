namespace HetDepot.Views;

public class InputView
{
    
    /*
     * The input view displays a title and message in the console together with an console input.
     * The method ShowAndGetResult() displays the texts and the input in the console and returns the input given by
     * the user as a string. When the user tries to submit something blank the InputView wil show the input again
     * until the user gives an input.
     */

    private readonly string _title;
    private readonly string _message;


    public InputView(string title, string message)
    {
        _title = title;
        _message = message;
    }

    public string ShowAndGetResult()
    {
        string? result;

        do
        {
            result = RenderInputScreen();
        } while (string.IsNullOrEmpty(result));
        
        return result;
    }

    private string? RenderInputScreen()
    {
        // Make sure we start with an empty console screen
        Renderer.ResetConsole();
        Console.Title = _title;

        // Write the title and a line of '=' characters under it
        Renderer.ConsoleWrite(_title);
        Renderer.ConsoleNewline();
        Renderer.ConsoleWrite('=');
        Renderer.ConsoleNewline();
        return Renderer.ConsoleWriteInput(_message);
    }
    
}