using System.Drawing;
using System.Linq;
using HetDepot.Model;
using HetDepot.Service;

namespace HetDepot;

public static class UserInterface
{
    private const int TableWidth = 70;
    private static bool _hasStarted = false;

    public static void StartUi()
    {
        if (_hasStarted)
        {
            return;
        }

        DisplayMainMenu();
        _hasStarted = true;
    }

    private static void DisplayMainMenu()
    {
        var settingService = new SettingService();
        var tourService = new TourService(settingService);

        

        // Get an array of tours to display, should be moved to a .json structure
        var tours = tourService.GetTours();

        // Map the array of tours to a List of UiTableRows
        // With the time and the remaining spots as columns
        List<UiTableRow> mmRows = tours.Select(tour =>
            new UiTableRow(new []
            {
                tour.ToString(),
                tour.availableSpots + " " + (tour.availableSpots == 1 ? "plek" : "plekken")
            }, () => { DisplayOptionsForTour(tour); })).ToList();

        // Adds the "reservering wijzigen optie"
        mmRows.Add(new UiTableRow(new [] { "Een reservering wijzigen" }, () => {}));

        // Creates a UiTable instance with title "Het depot rondleidingen overzicht" and with the
        // rows (mmRows) created above
        UiTable mmTable = new(
            "Het Depot - Rondleidingen overzicht",
            mmRows, 
            // Defines a list with hidden options for the table
            new List<UiTableHiddenOption>
            {
                new (ConsoleKey.S, () =>
                {
                    Console.WriteLine("GIDS MENU");
                    return;
                })
            },
            // Defines a row that will be used as table header, this is optional and onSelect will never be called
            new UiTableRow(new [] { "Tijd", "Beschikbare plaatsen" }, () => { }));

        // Gives the created table object to the renderer
        DisplaySelectableTable(mmTable);
    }

    private static void DisplayTourRegistration(Tour tour)
    {
        Console.Clear();
        DisplayTitle("Inschrijven voor rondleiding om " + tour.GetTime());

        int codeLength = 10;
        Console.WriteLine();
        // TODO Implement max length with https://stackoverflow.com/a/5557919
        Console.Write(AlignCentre("Uw unieke code: ", TableWidth - codeLength, true));

        Console.CursorVisible = true;
        
        string? code = Console.ReadLine();

        if (string.IsNullOrEmpty(code))
        {
            DisplayOptionsForTour(tour);
        }
        
        // TODO Implement code validation and handling
        bool valid = false;
        if (valid)
        {
            
        }
        else
        {
            DisplayError("Unieke code was ongeldig probeer het nog eens");
            DisplayTourRegistration(tour);
        }
        
    }

    private static void DisplayError(string text)
    {
        Console.Clear();
        Console.CursorVisible = false;
        Console.BackgroundColor = ConsoleColor.Red;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine("\n" + AlignCentre(" ", TableWidth) + 
                          "\n" + AlignCentre(text, TableWidth) + 
                          "\n" + AlignCentre(" ", TableWidth));
        Console.ResetColor();
        Thread.Sleep(2000);
    }

    private static void DisplaySuccess(string text)
    {
        Console.Clear();
        Console.CursorVisible = false;
        Console.BackgroundColor = ConsoleColor.Green;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine("\n" + AlignCentre(text, TableWidth) + "\n");
        Console.ResetColor();
        Thread.Sleep(2000);
    }

    private static void DisplayOptionsForTour(Tour tour)
    {
        // Creates the 2 option rows for the tour overview
        UiTableRow[] tourRows =
        {
            new(new [] {"Aanmelden voor deze rondleiding"}, () =>
            {
                DisplayTourRegistration(tour);
            }),
            new(new [] {"Terug naar overzicht"}, () =>
            {
                DisplayMainMenu();
            }),
        };
        
        // Created the table for the tour overview with title "Rondleiding om {tour.GetTime()}"
        UiTable tourTable = new(
            "Rondleiding om " + tour.GetTime(),
            tourRows.ToList(), // Transforms the array to a list
            new List<UiTableHiddenOption>()); // Gives empty list for hidden options
        
        // Gives the created table object for Tour overview to the renderer
        DisplaySelectableTable(tourTable);
    }

    private static void DisplaySelectableTable(UiTable table)
    {
        int activeRow = 0; // Used for highlighting row and getting selection
        int previousActiveRow = -1; // Used to know what row to redraw when selection changes
        bool selectionMade = false; // Used to stop the loop when selection is made
        bool redraw = true;
        int optionsLength = table.Rows.Count; // Used for up and down functions

        Console.CursorVisible = false;

        while (!selectionMade)
        {
            if (redraw) // Re-draw table only when necessary
            {
                if (previousActiveRow == -1)
                {
                    Console.Clear(); // Clears the console
                    DisplayTitle(table.Title); // Passes the table title to the display title method
                }
                BuildTable(table, activeRow, previousActiveRow); // Builds the table and passes the table instance and the activeRow and previousActiveRow (if any)
                redraw = false;
            }

            ConsoleKey key = Console.ReadKey(true).Key; // This reads the pressed key on the keyboard by the user

            // Checks if the key pressed exists in the hidden options, is so call the OnKey action
            UiTableHiddenOption? hiddenOption = table.HiddenOptions.Find(option => option.Key == key);

            if (null != hiddenOption)
            {
                hiddenOption.OnKey();
                return;
            }
            
            switch (key) // Handles the keypresses
            {
                case ConsoleKey.Enter:
                    selectionMade = true;
                    // After the selection is made exit the loop
                    break;
                case ConsoleKey.DownArrow:
                    previousActiveRow = activeRow;
                    redraw = true;
                    if (activeRow == optionsLength - 1)
                    {
                        activeRow = 0;
                        break;
                    }

                    activeRow++;
                    // After the key is pressed change activeRow and go to line 149
                    break;
                case ConsoleKey.UpArrow:
                    previousActiveRow = activeRow;
                    redraw = true;
                    if (activeRow == 0)
                    {
                        activeRow = optionsLength - 1;
                        break;
                    }

                    activeRow--;
                    // After the key is pressed change activeRow and go to line 149
                    break;
            }
        }

        // Find the selected option and call the onSelect action
        table.Rows[activeRow].OnSelect();
    }

    private static void DisplayTitle(string title)
    {
        Console.WriteLine("\n" + AlignCentre(title, TableWidth));
    }

    private static void BuildTable(UiTable table, int activeRow, int previousActiveRow = -1)
    {
        // This function writes the table to the console and highlights the given active row
        
        if (previousActiveRow != -1)
        {
            // The table was already drawn, draw only the parts that have changed
            int headerSize = table.Headers == null ? 3 : 5; // Number of lines that the header takes up (including the title)
            
            // Remember cursor position
            int cursorX, cursorY;
            (cursorX, cursorY) = Console.GetCursorPosition();

            // Redraw previous active row as inactive
            Console.SetCursorPosition(0, headerSize + previousActiveRow);
            PrintRow(table.Rows[previousActiveRow], false);

            // Redraw new active row as active
            Console.SetCursorPosition(0, headerSize + activeRow);
            PrintRow(table.Rows[activeRow], true);

            // Restore cursor position
            Console.SetCursorPosition(cursorX, cursorY);
            return;
        }

        if (table.Headers != null)
        {
            PrintLine();
            PrintRow(table.Headers, false);
        }

        PrintLine();
        for (int i = 0; i < table.Rows.Count; i++)
        {
            PrintRow(table.Rows[i], activeRow == i);
        }

        PrintLine();
        Console.WriteLine(AlignCentre("Navigeer met de pijltjes en selecteer met Enter\n", TableWidth));
    }

    private static void PrintLine()
    {
        Console.WriteLine(new string('-', TableWidth));
    }

    private static void PrintRow(UiTableRow uiTableRow, bool active)
    {
        string[] columns = uiTableRow.Columns;
        int width = (TableWidth - columns.Length) / columns.Length;
        string row = "|";

        foreach (string column in columns)
        {
            row += AlignCentre(column, width) + "|";
        }

        if (active)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
        }

        Console.WriteLine(row);
        Console.ResetColor();
    }

    private static string AlignCentre(string text, int width, bool onlyStart = false)
    {
        // Used for aligning text in the center of a col or row
        text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

        if (string.IsNullOrEmpty(text))
        {
            return new string(' ', width);
        }
        else
        {
            if (onlyStart)
            {
                return text.PadLeft(width - (width - text.Length) / 2);
            }
            return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
        }
    }

    private class UiTable
    {
        public readonly string Title;
        public readonly List<UiTableRow> Rows;
        public readonly List<UiTableHiddenOption> HiddenOptions;
        public readonly UiTableRow? Headers;

        public UiTable(string title, List<UiTableRow> rows, List<UiTableHiddenOption> hiddenOptions,
            UiTableRow? headers = null)
        {
            Title = title;
            Rows = rows;
            Headers = headers;
            HiddenOptions = hiddenOptions;
        }
    }

    private class UiTableRow
    {
        public readonly string[] Columns;
        public readonly Action OnSelect;

        public UiTableRow(string[] columns, Action onSelect)
        {
            Columns = columns;
            OnSelect = onSelect;
        }
    }

    private class UiTableHiddenOption
    {
        public readonly ConsoleKey Key;
        public readonly Action OnKey;

        public UiTableHiddenOption(ConsoleKey key, Action onKey)
        {
            Key = key;
            OnKey = onKey;
        }
    }
}