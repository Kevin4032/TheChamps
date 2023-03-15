namespace HetDepot.Views;

public class InputView
{

    public readonly string Title;
    public readonly string Message;


    public InputView(string title, string message)
    {
        Title = title;
        Message = message;
    }

    public string? ShowAndGetResult()
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
        Console.Title = Title;

        // Write the title and a line of '=' characters under it
        Renderer.ConsoleWrite(Title);
        Renderer.ConsoleNewline();
        Renderer.ConsoleWrite('=');
        Renderer.ConsoleNewline();
        return Renderer.ConsoleWriteInput(Message);
    }
    
}