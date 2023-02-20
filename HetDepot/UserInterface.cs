using System.Drawing;

namespace HetDepot;

using System.Linq;

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
        // Get an array of tours to display, should be moved to a .json structure
        Tour[] tours =
        {
            new(new DateTime(2023, 02, 20, 11, 15, 00)),
            new(new DateTime(2023, 02, 20, 11, 30, 00)),
            new(new DateTime(2023, 02, 20, 11, 45, 00)),
            new(new DateTime(2023, 02, 20, 12, 00, 00)),
        };

        // Map the array of tours to a List of UiTableRows
        // With the time and the remaining spots as columns
        List<UiTableRow> mmRows = tours.Select(tour =>
            new UiTableRow(new []
            {
                tour.StartsAt.ToString("H:mm"),
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
        bool selectionMade = false; // Used to stop the loop when selection is made
        int optionsLength = table.Rows.Count; // Used for up and down functions

        while (!selectionMade)
        {
            Console.Clear(); // Clears the console
            DisplayTitle(table.Title); // Passes the table title to the display title methopd
            BuildTable(table, activeRow); // Builds the table for the first time and passes the table instance and the activeRow

            ConsoleKey key = Console.ReadKey().Key; // This reads the pressed key on the keyboard by the user

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
                    if (activeRow == optionsLength - 1)
                    {
                        activeRow = 0;
                        break;
                    }

                    activeRow++;
                    // After the key is pressed change activeRow and go to line 149
                    break;
                case ConsoleKey.UpArrow:
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

    private static void BuildTable(UiTable table, int activeRow)
    {
        // This function writes the table to the console and highlights the given active row
        
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