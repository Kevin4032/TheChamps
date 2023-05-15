using HetDepot.Views.Interface;
using HetDepot.Views.Parts;

namespace HetDepot.Views;

public class BarGraphView
{

    /*
	 * A ListView instance can be created. On the creation it needs to be given listable items.
	 * With ShowAndGetResult() the list will be displayed in the console and the user selection will be returned
	 * by the function.
	 */

    private int _selectedItemIndex;
    private readonly string _title;
    private readonly string? _subtitle = "Scroll met pijltjes, ga terug met ESC";
    private readonly List<BarGraphPart> _barGraphParts;
    private readonly int _total;
    private readonly ConsoleColor firstColor;
    private readonly ConsoleColor secondColor;

    public BarGraphView(string title, int total, List<BarGraphPart> barGraphParts,
        ConsoleColor firstColor = ConsoleColor.Gray, ConsoleColor secondColor = ConsoleColor.DarkGray)
    {
        _title = title;
        _total = total;
        _barGraphParts = barGraphParts;
        this.firstColor = firstColor;
        this.secondColor = secondColor;
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
                if (null != _subtitle)
                {
                    Renderer.ConsoleWrite(_subtitle, 0, 0, 0, ' ', ConsoleColor.DarkGray);
                }
                Renderer.ConsoleNewline();
                Renderer.ConsoleWrite('=');
                Renderer.ConsoleNewline();


                firstSelectableItemPos = Console.GetCursorPosition().Top;

                if (itemOffset < 0)
                    itemOffset = 0;
                if (itemOffset >= _barGraphParts!.Count)
                    itemOffset = _barGraphParts.Count - 1;

                // List all ListViewItems
                for (int i = itemOffset; i < _barGraphParts.Count; i++)
                {
                    if (firstSelectableItemPos + i - itemOffset == Renderer.ConsoleHeight - 1)
                    {
                        // Reached max. number of lines on the screen
                        Renderer.ConsoleWrite("...", 0, 20);
                        break;
                    }
                    WriteListItem(_barGraphParts[i], i);
                }

                maxItemsOnScreen = Renderer.ConsoleHeight - firstSelectableItemPos - 1;
            }
            else if (previousSelection > -1)
            {

                // Selection has changed and console has not moved, redraw only the parts of the screen that have changed
                int cursorPos = Console.GetCursorPosition().Top;
                if (previousSelection + itemOffset >= 0 && previousSelection + itemOffset < _barGraphParts!.Count)
                {
                    Console.SetCursorPosition(0, firstSelectableItemPos + previousSelection);
                    int idx = previousSelection + itemOffset;
                    WriteListItem(_barGraphParts[idx], idx);
                }
                if (_selectedItemIndex + itemOffset >= 0 && _selectedItemIndex + itemOffset < _barGraphParts!.Count)
                {
                    Console.SetCursorPosition(0, firstSelectableItemPos + _selectedItemIndex);
                    int idx = _selectedItemIndex + itemOffset;
                    WriteListItem(_barGraphParts[idx], idx);
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

            if (_barGraphParts == null || _barGraphParts.Count == 0)
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

            if (_selectedItemIndex + itemOffset >= _barGraphParts.Count)
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
                redraw = itemOffset != maxItemsOnScreen * ((_barGraphParts.Count - 1) / maxItemsOnScreen);
                itemOffset = maxItemsOnScreen * ((_barGraphParts.Count - 1) / maxItemsOnScreen);
                _selectedItemIndex = _barGraphParts.Count - itemOffset - 1;
            }
        }
    }

    private void WriteListItem(BarGraphPart graphPart, int index = 0)
    {
        ConsoleColor color = ConsoleColor.White;
        ConsoleColor background = index % 2 == 0 ? firstColor : secondColor;

        int usedWidth = 0;
        int textWidth = 10;
        int barWidth = calculateBarWidth(graphPart.value, textWidth * 2);

        // Write label
        Renderer.ConsoleWrite(graphPart.Label, usedWidth, textWidth, 0, ' ', color, ConsoleColor.Black);
        usedWidth += textWidth;

        if (barWidth > 0)
        {
            // Write bar
            Renderer.ConsoleWrite("", usedWidth, barWidth, 0, ' ', color, background);
            usedWidth += barWidth;
        }

        // Write value
        Renderer.ConsoleWrite(graphPart.value.ToString(), usedWidth + (barWidth > 0 ? 1 : 0), textWidth, 0, ' ',
            ConsoleColor.White, ConsoleColor.Black);

        Renderer.ConsoleNewline();
    }

    private int calculateBarWidth(int part, int offset = 0)
    {
        if (part == 0)
            return 0;

        double partOfTotal = (double)part / _total;
        return (int)((Renderer.ConsoleWidth - offset) * partOfTotal);
    }

}
