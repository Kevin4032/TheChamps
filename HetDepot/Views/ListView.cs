using HetDepot.Views.Interface;
using HetDepot.Views.Parts;

namespace HetDepot.Views;

public class ListView
{
    private int SelectedItemIndex;
    public string Title;
    public string? Subtitle;
    public static List<ListableItem> ListViewItems;

    public ListView(string title, List<ListableItem> listViewItems)
    {
	    Title = title;
	    ListViewItems = listViewItems;
    }

    public ListView(string title, string subtitle, List<ListableItem> listViewItems)
    {
	    Title = title;
	    Subtitle = subtitle;
	    ListViewItems = listViewItems;
    }

    public ListView(string title, List<IListableObject> listViewItems)
    {
	    Title = title;
	    ListViewItems = listViewItems.Select(x => x.ToListableItem()).ToList();
    }

    public ListView(string title, string subtitle ,List<IListableObject> listViewItems)
    {
	    Title = title;
	    Subtitle = subtitle;
	    ListViewItems = listViewItems.Select(x => x.ToListableItem()).ToList();
    }
    
    public object ShowAndGetResult()
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
			if (redraw || Renderer.HasConsoleMoved())
			{
				// Make sure we start with an empty console screen
				Renderer.ResetConsole(false);
				Console.Title = Title;

				// Write the title and a line of '=' characters under it
				Renderer.ConsoleWrite(Title);
				if (null != Subtitle)
				{
					Renderer.ConsoleWrite(Subtitle, 0,0,0,' ', ConsoleColor.Gray);
				}
				Renderer.ConsoleNewline();
				Renderer.ConsoleWrite('=');
				Renderer.ConsoleNewline();


				firstSelectableItemPos = Console.GetCursorPosition().Top;

				if (itemOffset < 0)
					itemOffset = 0;
				if (itemOffset >= ListViewItems.Count)
					itemOffset = ListViewItems.Count - 1;

				// List all ListViewItems
				for (int i = itemOffset; i < ListViewItems.Count; i++)
				{
					if (firstSelectableItemPos + i - itemOffset == Renderer.ConsoleHeight - 1)
					{
						// Reached max. number of lines on the screen
						Renderer.ConsoleWrite("...", 0, 20);
						break;
					}
					WriteListItem(ListViewItems[i], SelectedItemIndex + itemOffset == i);
				}
				
				maxItemsOnScreen = Renderer.ConsoleHeight - firstSelectableItemPos - 1;
			}
			else if (previousSelection > -1)
			{

				// Selection has changed and console has not moved, redraw only the parts of the screen that have changed
				int cursorPos = Console.GetCursorPosition().Top;
				if (previousSelection + itemOffset >= 0 && previousSelection + itemOffset < ListViewItems.Count)
				{
					Console.SetCursorPosition(0, firstSelectableItemPos + previousSelection);
					WriteListItem(ListViewItems[previousSelection + itemOffset], false);
				}
				if (SelectedItemIndex + itemOffset >= 0 && SelectedItemIndex + itemOffset < ListViewItems.Count)
				{
					Console.SetCursorPosition(0, firstSelectableItemPos + SelectedItemIndex);
					WriteListItem(ListViewItems[SelectedItemIndex + itemOffset], true);
				}
				Console.SetCursorPosition(0, cursorPos);
				previousSelection = -1;
			}

			// Console.Title = $"selectedItem: {SelectedItem}, itemOffset: {itemOffset}, maxItems {maxItemsOnScreen}, redraw: {(redraw || HasConsoleMoved() ? "TRUE" : "FALSE")}"; // Debug

			if (maxItemsOnScreen <= 0)
				continue; // Forget it, there is no room to display anything at all

			// Wait for the user to press a key
			pressedKey = Console.ReadKey(true);
			redraw = false;

			if (ListViewItems == null || ListViewItems.Count == 0)
			{
				// Nothing to choose from, just redraw at every button press
				redraw = true;
				continue;
			}

			switch (pressedKey.Key)
			{
				case ConsoleKey.Enter:
					SelectedItemIndex += itemOffset;
					if (SelectedItemIndex < 0 || SelectedItemIndex >= ListViewItems.Count)
						break;

					ListableItem selectedListViewItem = ListViewItems[SelectedItemIndex];

					if (selectedListViewItem.Disabled)
					{
						continue;
					}

					return selectedListViewItem.Value;
				case ConsoleKey.UpArrow:
					previousSelection = SelectedItemIndex;
					SelectedItemIndex--;
					if (SelectedItemIndex < 0 && itemOffset > 0)
					{
						SelectedItemIndex = maxItemsOnScreen - 1;
						itemOffset -= maxItemsOnScreen;
						redraw = true;
					}
					break;
				case ConsoleKey.DownArrow:
					previousSelection = SelectedItemIndex;
					SelectedItemIndex++;
					if (SelectedItemIndex >= maxItemsOnScreen)
					{
						SelectedItemIndex = 0;
						itemOffset += maxItemsOnScreen;
						redraw = true;
					}
					break;
			};

			if (SelectedItemIndex + itemOffset >= ListViewItems.Count)
			{
				// Wrap around to first item
				SelectedItemIndex = 0;
				redraw = itemOffset != 0;
				itemOffset = 0;
			}

			if (SelectedItemIndex >= maxItemsOnScreen)
				SelectedItemIndex = maxItemsOnScreen - 1;

			if (SelectedItemIndex < 0)
			{
				// Wrap around to last item
				redraw = itemOffset != maxItemsOnScreen * ((ListViewItems.Count - 1) / maxItemsOnScreen);
				itemOffset = maxItemsOnScreen * ((ListViewItems.Count - 1) / maxItemsOnScreen);
				SelectedItemIndex = ListViewItems.Count - itemOffset - 1;
			}
		}

		return null;
	}

	private static void WriteListItem(ListableItem listableItem, bool selected = false)
	{
		ConsoleColor color = ConsoleColor.White;
		ConsoleColor background = ConsoleColor.Black;

		if (listableItem.Disabled)
		{
			color = ConsoleColor.DarkRed;
		}
		
		if (selected)
		{
			color = ConsoleColor.Black;
			background = ConsoleColor.White;
		}

		int usedWidth = 0;

		foreach (ListViewItemPart textPart in listableItem.GetTextParts())
		{
			int width = (textPart.Width ?? Renderer.ConsoleWidth) - usedWidth;
			Renderer.ConsoleWrite( textPart.Text, usedWidth, width, listableItem.TextAlignment, ' ', color, background);
			usedWidth += width;
		}

		Renderer.ConsoleNewline();
	}

}