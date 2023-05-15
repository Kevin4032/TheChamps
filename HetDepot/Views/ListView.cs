using HetDepot.Tours.Model;
using HetDepot.Views.Interface;
using HetDepot.Views.Parts;

namespace HetDepot.Views;

public class ListView<T>
{
    /*
	 * A ListView instance can be created. On the creation it needs to be given listable items.
	 * With ShowAndGetResult() the list will be displayed in the console and the user selection will be returned
	 * by the function.
	 */

    private int _selectedItemIndex;
    private readonly string _title;
    private string? _subtitle;
    private readonly List<ListableItem<T>> _listViewItems;
    private ListableItem<T>? _selectedListViewItem;

    public ListView(string title, List<ListableItem<T>> listViewItems)
    {
        _title = title;
        _listViewItems = listViewItems;
    }

    public ListView(string title, string subtitle, List<ListableItem<T>> listViewItems)
    {
        _title = title;
        _subtitle = subtitle;
        _listViewItems = listViewItems;
    }

    public ListView(string title, List<ListableItem<T>> listViewItems, List<ListableItem<T>>? extraOptions = null)
    {
        _title = title;
        _listViewItems = listViewItems;
        if (extraOptions != null) listViewItems!.AddRange(extraOptions);
    }

    public ListView(string title, List<IListableObject<T>> listViewObjects, List<ListableItem<T>>? extraOptions = null)
    {
        _title = title;

        List<ListableItem<T>> listViewItems = listViewObjects.Select(x => x.ToListableItem()).ToList();
        if (extraOptions != null) listViewItems!.AddRange(extraOptions);
        _listViewItems = listViewItems;
    }

    public ListView(string title, string subtitle, List<IListableObject<T>> listViewObjects,
        List<ListableItem<T>>? extraOptions = null)
    {
        _title = title;
        _subtitle = subtitle;

        List<ListableItem<T>> listViewItems = listViewObjects.Select(x => x.ToListableItem()).ToList();
        if (extraOptions != null) listViewItems!.AddRange(extraOptions);
        _listViewItems = listViewItems;
    }

    public T? ShowAndGetResult()
    {
        // Show the Tour List screen: Draw it on the console and wait for the user to press a key
        bool redraw = true;
        int firstSelectableItemPos = 0; // Cursor position of first selectable item
        int previousSelection = -1; // Previously selected item (on this page)
        int maxItemsOnScreen = 0;
        int itemOffset = 0; // Amount of items to skip (shown on earlier pages)
        ConsoleKeyInfo pressedKey;

        while (true) // Loop until the code inside of this loop breaks the loop
        {
            // Decide if we need to redraw the whole screen
            if (redraw || Renderer.HasConsoleMoved())
            {
                // Make sure we start with an empty console screen
                Renderer.ResetConsole(false);
                Console.Title = _title;

                // Write the title and a line of '=' characters under it
                Renderer.ConsoleWrite(_title);
                if (null != _subtitle)
                {
                    Renderer.ConsoleWrite(_subtitle, 0, 0, 0, ' ', ConsoleColor.Gray);
                }

                Renderer.ConsoleNewline();
                Renderer.ConsoleWrite('=');
                Renderer.ConsoleNewline();


                firstSelectableItemPos = Console.GetCursorPosition().Top;

                if (itemOffset < 0)
                    itemOffset = 0;
                if (itemOffset >= _listViewItems!.Count)
                    itemOffset = _listViewItems.Count - 1;

                // List all ListViewItems
                for (int i = itemOffset; i < _listViewItems.Count; i++)
                {
                    if (firstSelectableItemPos + i - itemOffset == Renderer.ConsoleHeight - 1)
                    {
                        // Reached max. number of lines on the screen
                        Renderer.ConsoleWrite("...", 0, 20);
                        break;
                    }

                    WriteListItem(_listViewItems[i], _selectedItemIndex + itemOffset == i);
                }

                maxItemsOnScreen = Renderer.ConsoleHeight - firstSelectableItemPos - 1;
            }
            else if (previousSelection > -1)
            {
                // Selection has changed and console has not moved, redraw only the parts of the screen that have changed
                int cursorPos = Console.GetCursorPosition().Top;
                if (previousSelection + itemOffset >= 0 && previousSelection + itemOffset < _listViewItems!.Count)
                {
                    Console.SetCursorPosition(0, firstSelectableItemPos + previousSelection);
                    WriteListItem(_listViewItems[previousSelection + itemOffset]);
                }

                if (_selectedItemIndex + itemOffset >= 0 && _selectedItemIndex + itemOffset < _listViewItems!.Count)
                {
                    Console.SetCursorPosition(0, firstSelectableItemPos + _selectedItemIndex);
                    WriteListItem(_listViewItems[_selectedItemIndex + itemOffset], true);
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

            if (_listViewItems == null || _listViewItems.Count == 0)
            {
                // Nothing to choose from, just redraw at every button press
                redraw = true;
                continue;
            }

            switch (pressedKey.Key)
            {
                case ConsoleKey.Enter:
                    _selectedItemIndex += itemOffset;
                    if (_selectedItemIndex < 0 || _selectedItemIndex >= _listViewItems.Count)
                        break;

                    _selectedListViewItem = _listViewItems[_selectedItemIndex];

                    if (_selectedListViewItem.Disabled)
                    {
                        continue;
                    }

                    return _selectedListViewItem.Value;
                case ConsoleKey.UpArrow:
                    previousSelection = _selectedItemIndex;
                    _selectedItemIndex--;
                    if (_selectedItemIndex < 0 && itemOffset > 0)
                    {
                        _selectedItemIndex = maxItemsOnScreen - 1;
                        itemOffset -= maxItemsOnScreen;
                        redraw = true;
                    }

                    break;
                case ConsoleKey.DownArrow:
                    previousSelection = _selectedItemIndex;
                    _selectedItemIndex++;
                    if (_selectedItemIndex >= maxItemsOnScreen)
                    {
                        _selectedItemIndex = 0;
                        itemOffset += maxItemsOnScreen;
                        redraw = true;
                    }

                    break;
            }

            if (_selectedItemIndex + itemOffset >= _listViewItems.Count)
            {
                // Wrap around to first item
                _selectedItemIndex = 0;
                redraw = itemOffset != 0;
                itemOffset = 0;
            }

            if (_selectedItemIndex >= maxItemsOnScreen)
                _selectedItemIndex = maxItemsOnScreen - 1;

            if (_selectedItemIndex < 0)
            {
                // Wrap around to last item
                redraw = itemOffset != maxItemsOnScreen * ((_listViewItems.Count - 1) / maxItemsOnScreen);
                itemOffset = maxItemsOnScreen * ((_listViewItems.Count - 1) / maxItemsOnScreen);
                _selectedItemIndex = _listViewItems.Count - itemOffset - 1;
            }
        }
    }

    public void Show()
    {
        // Show the Tour List screen: Draw it on the console and wait for the user to press a key
        bool redraw = true;
        int firstSelectableItemPos = 0; // Cursor position of first selectable item
        int previousSelection = -1; // Previously selected item (on this page)
        int maxItemsOnScreen = 0;
        int itemOffset = 0; // Amount of items to skip (shown on earlier pages)
        ConsoleKeyInfo pressedKey;

        while (true) // Loop until the code inside of this loop breaks the loop
        {
            // Decide if we need to redraw the whole screen
            if (redraw || Renderer.HasConsoleMoved())
            {
                // Make sure we start with an empty console screen
                Renderer.ResetConsole(false);
                Console.Title = _title;

                // Write the title and a line of '=' characters under it
                Renderer.ConsoleWrite(_title);

                if (_subtitle == null)
                {
                    _subtitle = "Scroll met pijltjes, ga terug met ESC";
                }

                Renderer.ConsoleWrite(_subtitle, 0, 0, 0, ' ', ConsoleColor.DarkGray);
                Renderer.ConsoleNewline();
                Renderer.ConsoleWrite('=');
                Renderer.ConsoleNewline();


                firstSelectableItemPos = Console.GetCursorPosition().Top;

                if (itemOffset < 0)
                    itemOffset = 0;
                if (itemOffset >= _listViewItems!.Count)
                    itemOffset = _listViewItems.Count - 1;

                // List all ListViewItems
                for (int i = itemOffset; i < _listViewItems.Count; i++)
                {
                    if (firstSelectableItemPos + i - itemOffset == Renderer.ConsoleHeight - 1)
                    {
                        // Reached max. number of lines on the screen
                        Renderer.ConsoleWrite("...", 0, 20);
                        break;
                    }

                    WriteListItem(_listViewItems[i]);
                }

                maxItemsOnScreen = Renderer.ConsoleHeight - firstSelectableItemPos - 1;
            }
            else if (previousSelection > -1)
            {
                // Selection has changed and console has not moved, redraw only the parts of the screen that have changed
                int cursorPos = Console.GetCursorPosition().Top;
                if (previousSelection + itemOffset >= 0 && previousSelection + itemOffset < _listViewItems!.Count)
                {
                    Console.SetCursorPosition(0, firstSelectableItemPos + previousSelection);
                    WriteListItem(_listViewItems[previousSelection + itemOffset]);
                }

                if (_selectedItemIndex + itemOffset >= 0 && _selectedItemIndex + itemOffset < _listViewItems!.Count)
                {
                    Console.SetCursorPosition(0, firstSelectableItemPos + _selectedItemIndex);
                    WriteListItem(_listViewItems[_selectedItemIndex + itemOffset]);
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

            if (_listViewItems == null || _listViewItems.Count == 0)
            {
                // Nothing to choose from, just redraw at every button press
                redraw = true;
                continue;
            }

            switch (pressedKey.Key)
            {
                case ConsoleKey.Escape:
                    return;
                case ConsoleKey.UpArrow:
                    previousSelection = _selectedItemIndex;
                    _selectedItemIndex -= maxItemsOnScreen;
                    if (_selectedItemIndex < 0 && itemOffset > 0)
                    {
                        _selectedItemIndex = maxItemsOnScreen - 1;
                        itemOffset -= maxItemsOnScreen;
                        redraw = true;
                    }

                    break;
                case ConsoleKey.DownArrow:
                    previousSelection = _selectedItemIndex;
                    _selectedItemIndex += maxItemsOnScreen;
                    if (_selectedItemIndex >= maxItemsOnScreen)
                    {
                        _selectedItemIndex = 0;
                        itemOffset += maxItemsOnScreen;
                        redraw = true;
                    }

                    break;
            }

            if (_selectedItemIndex + itemOffset >= _listViewItems.Count)
            {
                // Wrap around to first item
                _selectedItemIndex = 0;
                redraw = itemOffset != 0;
                itemOffset = 0;
            }

            if (_selectedItemIndex >= maxItemsOnScreen)
                _selectedItemIndex = maxItemsOnScreen - 1;

            if (_selectedItemIndex < 0)
            {
                // Wrap around to last item
                redraw = itemOffset != maxItemsOnScreen * ((_listViewItems.Count - 1) / maxItemsOnScreen);
                itemOffset = maxItemsOnScreen * ((_listViewItems.Count - 1) / maxItemsOnScreen);
                _selectedItemIndex = _listViewItems.Count - itemOffset - 1;
            }
        }
    }

    public T? ShowAndGetResult<TExtra>(out TExtra? extraResult)
    {
        T? result = ShowAndGetResult();
        extraResult = (_selectedListViewItem is IListableExtraItem<TExtra> extra)
            ? extra.GetExtraItem()
            : default(TExtra);
        return result;
    }

    private static void WriteListItem(ListableItem<T> listableItem, bool selected = false)
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
            Renderer.ConsoleWrite(textPart.Text, usedWidth, width, listableItem.TextAlignment, ' ', color, background);
            usedWidth += width;
        }

        Renderer.ConsoleNewline();
    }
}
