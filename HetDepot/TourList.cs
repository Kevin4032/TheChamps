namespace HetDepot;

class TourList
{
	// This class represents a "Tour list" screen that lists the available tours and allows the user to select one

	public static int ConsoleWidth, ConsoleHeight, ConsoleLeft, ConsoleTop;

	public List<string> Tours;
	public List<int> TourSpaces;
	public int SelectedTour; // Selected tour index (0 = first in list)
	public int SelectedItem; // Selected item index (0 = first on screen)
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
		SelectedItem = 0;
	}

	public void ShowScreen()
	{
		// Show the Tour List screen: Draw it on the console and wait for the user to press a key

		bool showScreen = true;
		bool redraw = true;
		int firstSelectableItemPos = 0; // Cursor position of first selectable item
		int previousSelection = -1; // Previously selected item (on this page)
		int maxItemsOnScreen = 0;
		int itemOffset = 0; // Amount of items to skip (shown on earlier pages)
		ConsoleKeyInfo pressedKey;

		while (showScreen)
		{

			// Decide if we need to redraw the whole screen
			if (redraw || HasConsoleMoved())
			{
				// Make sure we start with an empty console screen
				ResetConsole(false);
				Console.Title = Title;

				// Write the title and a line of '=' characters under it
				ConsoleWrite(Title);
				ConsoleNewline();
				ConsoleWrite('=');
				ConsoleNewline();

				firstSelectableItemPos = Console.GetCursorPosition().Top;
				if (itemOffset < 0)
					itemOffset = 0;
				if (itemOffset >= Tours.Count)
					itemOffset = Tours.Count - 1;

				// List all tours
				for (int i = itemOffset; i < Tours.Count; i++)
				{
					if (firstSelectableItemPos + i - itemOffset == ConsoleHeight - 1)
					{
						// Reached max. number of lines on the screen
						ConsoleWrite("...", 0, 10);
						ConsoleWrite("...", 10, ConsoleWidth - 11); // Console will scroll if screen is filled completely; prevent that
						break;
					}
					WriteTour(Tours[i], TourSpaces[i], SelectedItem + itemOffset == i);
				}
				maxItemsOnScreen = ConsoleHeight - firstSelectableItemPos - 1;
			}
			else if (previousSelection > -1)
			{
				// Selection has changed and console has not moved, redraw only the parts of the screen that have changed
				int cursorPos = Console.GetCursorPosition().Top;
				if (previousSelection + itemOffset >= 0 && previousSelection + itemOffset < Tours.Count)
				{
					Console.SetCursorPosition(0, firstSelectableItemPos + previousSelection);
					WriteTour(Tours[previousSelection + itemOffset], TourSpaces[previousSelection + itemOffset], false);
				}
				if (SelectedItem + itemOffset >= 0 && SelectedItem + itemOffset < Tours.Count)
				{
					Console.SetCursorPosition(0, firstSelectableItemPos + SelectedItem);
					WriteTour(Tours[SelectedItem + itemOffset], TourSpaces[SelectedItem + itemOffset], true);
				}
				Console.SetCursorPosition(0, cursorPos);
				previousSelection = -1;
			}

			// Console.Title = $"selectedItem: {SelectedItem}, itemOffset: {itemOffset}, maxItems {maxItemsOnScreen}, redraw: {(redraw || HasConsoleMoved() ? "TRUE" : "FALSE")}"; // Debug

			if (maxItemsOnScreen <= 0)
				return; // Forget it, there is no room to display anything at all

			// Wait for the user to press a key
			pressedKey = Console.ReadKey(true);
			redraw = false;

			switch (pressedKey.Key)
			{
				case ConsoleKey.Enter:
					SelectedTour = SelectedItem + itemOffset;
					if (SelectedTour < 0 || SelectedTour >= Tours.Count)
						break;
					if (TourSpaces[SelectedTour] > 0)
					{
						// We've selected a tour! Go to next screen
						showScreen = false;
					}
					break;
				case ConsoleKey.UpArrow:
					previousSelection = SelectedItem;
					SelectedItem--;
					if (SelectedItem < 0 && itemOffset > 0)
					{
						SelectedItem = maxItemsOnScreen - 1;
						itemOffset -= maxItemsOnScreen;
						redraw = true;
					}
					break;
				case ConsoleKey.DownArrow:
					previousSelection = SelectedItem;
					SelectedItem++;
					if (SelectedItem >= maxItemsOnScreen)
					{
						SelectedItem = 0;
						itemOffset += maxItemsOnScreen;
						redraw = true;
					}
					break;
			};

			if (SelectedItem + itemOffset >= Tours.Count)
			{
				// Wrap around to first item
				SelectedItem = 0;
				redraw = itemOffset != 0;
				itemOffset = 0;
			}

			if (SelectedItem >= maxItemsOnScreen)
				SelectedItem = maxItemsOnScreen - 1;

			if (SelectedItem < 0)
			{
				// Wrap around to last item
				redraw = itemOffset != maxItemsOnScreen * ((Tours.Count - 1) / maxItemsOnScreen);
				itemOffset = maxItemsOnScreen * ((Tours.Count - 1) / maxItemsOnScreen);
				SelectedItem = Tours.Count - itemOffset - 1;
			}
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

		if (spaces > 1)
		{
			ConsoleWrite($"{spaces} plaatsen vrij", 10, 0, 0, ' ', color, background);
		}
		else if (spaces == 1)
		{
			ConsoleWrite($"{spaces} plaats vrij", 10, 0, 0, ' ', color, background);
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

	public static void ConsoleNewline()
	{
		if (Console.GetCursorPosition().Left == 0)
			return; // Probably automatically wrapped to new line after filling up the last one

		Console.WriteLine();
	}


}