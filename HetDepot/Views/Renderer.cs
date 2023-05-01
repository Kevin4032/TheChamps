namespace HetDepot.Views;

public static class Renderer
{

    /**
	 * The render has all the rich console functions that are used within the views.
	 */

    public static int ConsoleWidth, ConsoleHeight, ConsoleLeft, ConsoleTop; // NOT Safe to use outside of Views i.e. in Models

    public static void ResetConsole(bool cursorVisible = true)
    {
        // Clears and resets the console to a default state

        Console.Clear();
        Console.ResetColor();
        Console.CursorVisible = cursorVisible;

        ConsoleWidth = Console.WindowWidth; // Width of console window
        ConsoleHeight = Console.WindowHeight; // Height of console window
        ConsoleLeft = Console.WindowLeft; // Horizontal scroll position of console window
        ConsoleTop = Console.WindowTop; // Vertical scroll position of console window
    }

    public static bool HasConsoleMoved()
    {
        // Has console size or scroll position changed?
        // This is useful to determine if the whole screen needs to be redrawn

        return ConsoleWidth != Console.WindowWidth || ConsoleHeight != Console.WindowHeight || ConsoleLeft != Console.WindowLeft || ConsoleTop != Console.WindowTop;
    }

    public static void ConsoleWrite(string content, int pos = 0, int maxWidth = 0, int align = 0, char padChar = ' ', ConsoleColor? color = null, ConsoleColor? background = null)
    {
        // Console.Write with more options, to force content to show up at a specific place on the screen
        // Parameters:
        // - content: the text content to write
        // - pos: the X position to write at
        // - maxWidth: the maximum width of the content (will be padded; set to 0 to use all available space)
        // - align: how to align the text (0 = left, 1 = center, 2 = right)
        // - padChar: the character to use for padding (defaults to space)
        // - color: the text color, or "foreground" color (defaults to the last used text color)
        // - background: the background color (defaults to the last used background color)

        if (maxWidth == 0)
            maxWidth = ConsoleWidth;

        if (maxWidth + pos > ConsoleWidth)
            maxWidth = ConsoleWidth - pos;

        if (maxWidth <= 0)
            return; // No room to write anything

        // Remove newlines, tabs, and other things that might mess things up when written to the console
        string filteredContent = "";
        foreach (char c in content)
            filteredContent += c < 32 ? ' ' : c;
        content = filteredContent;

        if (content.Length > maxWidth)
        {
            // Not enough space to write all this content
            content = maxWidth switch
            {
                <= 3 => content.Substring(0, maxWidth), // Not even enough space to write "..."
                _ => content.Substring(0, maxWidth - 3) + "...",
            };
        }
        else
        {
            // More than enough room, add padding to fill up the rest
            content = align switch
            {
                1 => content.PadLeft(maxWidth / 2, padChar).PadRight(maxWidth, padChar),
                2 => content.PadLeft(maxWidth, padChar),
                _ => content.PadRight(maxWidth, padChar),
            };
        }

        // Set colors
        Console.ForegroundColor = color ?? ConsoleColor.White;
        Console.BackgroundColor = background ?? ConsoleColor.Black;

        // Finally, write the content
        Console.SetCursorPosition(pos, Console.GetCursorPosition().Top);
        Console.Write(content);
        Console.ResetColor();
    }

    public static void ConsoleWrite(char padChar, int pos = 0, int maxWidth = 0)
    {
        // Overloading for simpler character padding
        ConsoleWrite("", pos, maxWidth, 0, padChar);
    }

    public static string? ConsoleWriteInput(string name)
    {
        Console.Write(name + " ");
        return Console.ReadLine();
    }

    public static void ConsoleNewline(bool force = false)
    {
        if (Console.GetCursorPosition().Left == 0 && !force)
            return; // Probably automatically wrapped to new line after filling up the last one

        Console.WriteLine();
    }

}
