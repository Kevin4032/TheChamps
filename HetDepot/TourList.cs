namespace HetDepot;

class TourList
{
	// This class represents a "Tour list" screen that lists the available tours and allows the user to select one

	public static int ConsoleWidth, ConsoleHeight, ConsoleLeft, ConsoleTop;

	public List<string> Tours;
	public List<int> TourSpaces;
	public int SelectedTour; // Selected tour index
	public string Title = "Welkom bij Het Depot";


	public TourList()
	{
		// Constructor
		
		// Generate some test data (to be changed and probably moved elsewhere, but for now a list of times and a random list of free spaces will do)
		Tours = new()
		{
			"11:00",
			"11:20",
			"11:40",
			"12:00",
			"12:20",
			"12:40",
			"13:00",
			"13:20",
			"13:40",
			"14:00",
			"14:20",
			"14:40",
			"15:00",
			"15:20",
			"15:40",
			"16:00",
			"16:20",
			"16:40",
		};

		TourSpaces = new();
		Random r = new();
		while (TourSpaces.Count < Tours.Count)
			TourSpaces.Add(r.Next(13));

		SelectedTour = 0;
	}

	public void ShowScreen()
	{
		// Show the Tour List screen: Draw it on the console and wait for the user to press a key

		bool showScreen = true;
		bool redraw = true;
		int firstSelectableItemPos = 0; // Cursor position of first selectable item
		int previousSelection = -1; // Previously selected tour
		ConsoleKeyInfo pressedKey;

		while (showScreen)
		{

			// Decide if we need to redraw the whole screen
			if (redraw || HasConsoleMoved())
			{
				// Make sure we start with an empty console screen
				ResetConsole(false);

				// Write the title and a line of '=' characters under it
				ConsoleWrite(Title);
				ConsoleNewline();
				ConsoleWrite('=');
				ConsoleNewline();

				firstSelectableItemPos = Console.GetCursorPosition().Top;

				// List all tours
				for (int i = 0; i < Tours.Count; i++)
					WriteTour(Tours[i], TourSpaces[i], SelectedTour == i);
			}
			else if (previousSelection > -1)
			{
				// Selection has changed and console has not moved, redraw only the parts of the screen that have changed
				int cursorPos = Console.GetCursorPosition().Top;
				Console.SetCursorPosition(0, firstSelectableItemPos + previousSelection);
				WriteTour(Tours[previousSelection], TourSpaces[previousSelection], false);
				Console.SetCursorPosition(0, firstSelectableItemPos + SelectedTour);
				WriteTour(Tours[SelectedTour], TourSpaces[SelectedTour], true);
				Console.SetCursorPosition(0, cursorPos);
				previousSelection = -1;
			}

			// Wait for the user to press a key
			pressedKey = Console.ReadKey(true);
			redraw = false;

			switch (pressedKey.Key)
			{
				case ConsoleKey.Enter:
					if (TourSpaces[SelectedTour] > 0)
					{
						// We've selected a tour! Go to next screen
						showScreen = false;
					}
					break;
				case ConsoleKey.UpArrow:
					previousSelection = SelectedTour;
					SelectedTour = (SelectedTour <= 0 ? Tours.Count - 1 : SelectedTour - 1);
					break;
				case ConsoleKey.DownArrow:
					previousSelection = SelectedTour;
					SelectedTour = (SelectedTour >= Tours.Count - 1 ? 0 : SelectedTour + 1);
					break;
			};
		}
	}

	public static void WriteTour(string time, int spaces, bool selected = false)
	{
		ConsoleColor color = ConsoleColor.White;
		ConsoleColor errorColor = ConsoleColor.Red;
		ConsoleColor background = ConsoleColor.Black;

		if (selected)
		{
			color = ConsoleColor.Black;
			errorColor = ConsoleColor.DarkRed;
			background = ConsoleColor.White;
		}

		ConsoleWrite(time, 0, 10, 0, ' ', color, background);

		if (spaces > 0)
		{
			ConsoleWrite($"{spaces} plaatsen vrij", 10, 0, 0, ' ', color, background);
		}
		else
		{
			ConsoleWrite("VOL", 10, 0, 0, ' ', errorColor, background);
		}

		ConsoleNewline();
	}

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
				1 => content.PadLeft(maxWidth / 2, padChar).PadRight(maxWidth / 2 + maxWidth % 2, padChar),
				2 => content.PadLeft(maxWidth, padChar),
				_ => content.PadRight(maxWidth, padChar),
			};
		}

		// Set colors
		if (color != null)
			Console.ForegroundColor = (ConsoleColor)color;
		if (background != null)
			Console.BackgroundColor = (ConsoleColor)background;

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

	public static void ConsoleNewline()
	{
		if (Console.GetCursorPosition().Left == 0)
			return; // Probably automatically wrapped to new line after filling up the last one

		Console.WriteLine();
	}


}