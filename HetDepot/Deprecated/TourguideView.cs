namespace HetDepot.Views;

using HetDepot.Tours.Model;

public static class TourguideView
{
    private static List<Tour> _tours; // Populated in updateToursFromStorage

    public static void DisplayTours()
    {
        // Make sure the class had the latest data
        updateToursFromStorage();
        
        Console.Clear();

        // Filter the tours without reservations
        List<Tour> tours = _tours.Where(t => t.Reservations.Count > 0).ToList(); 

        Console.WriteLine("\n-= Rondleiding overzicht =-\n");

        // List tour overview with numbers that the user can select
        for (int i = 0; i < tours.Count; i++)
        {
            Tour currentTour = tours[i];
            int currentOption = i + 1;

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"[{currentOption}]");
            Console.ResetColor();
            Console.Write($" {currentTour.GetTime()} {currentTour.Reservations} reserveringen");
            Console.Write("\n");
        }

        Tour? selectedTour = null;

        while (null == selectedTour)
        {
            Console.WriteLine();
            Console.Write("Welke rondleiding wilt u starten (geef het nummer op): ");
            string? selection = Console.ReadLine();

            int selectionIndex;

            // Check that input is not NULL
            if (null == selection)
            {
                displayConsoleError("Invoer is ongeldig. Selecteer het nummer van één rondleiding uit de lijst hierboven.");
                continue;
            }
            
            // Try converting selection to int
            try
            {
                selectionIndex = Int32.Parse(selection) - 1; 
            }
            catch (Exception e)
            {
                displayConsoleError("Invoer is ongeldig. Selecteer het nummer van één rondleiding uit de lijst hierboven.");
                continue;
            }

            // Check if selection exists
            if (selectionIndex < 0 || selectionIndex > tours.Count - 1)
            {
                displayConsoleError("Keuze is ongeldig. Selecteer een rondleiding uit de lijst hierboven.");
                continue;
            }

            // Find the selected tour and assign it to the selectedTour variable
            selectedTour = tours[selectionIndex];
        }

        Console.Clear();
        Console.WriteLine($"!!INFO!! Tour for {selectedTour} chosen");

        // TODO Handle tour selection
        
    }

    // Displays the given error message on the current line and clears the line after
    private static void displayConsoleError(string error)
    {
        Console.SetCursorPosition(0, Console.CursorTop - 1);
        ClearCurrentConsoleLine();
        
        Console.BackgroundColor = ConsoleColor.Red;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine(error);
        Console.ResetColor();
        Thread.Sleep(3000);
        
        Console.SetCursorPosition(0, Console.CursorTop - 1);
        ClearCurrentConsoleLine();
        Console.SetCursorPosition(0, Console.CursorTop - 1);
    }
    
    private static void ClearCurrentConsoleLine()
    {
        int currentLineCursor = Console.CursorTop;
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.Write(new string(' ', Console.WindowWidth)); 
        Console.SetCursorPosition(0, currentLineCursor);
    }

    private static void updateToursFromStorage()
    {
        // TODO Get tours from actual storage
        _tours = new List<Tour>()
        {
            new (new DateTime(2023,3,8, 10, 00, 00), 3),
            new (new DateTime(2023,3,8, 10, 15, 00), 6),
            new (new DateTime(2023,3,8, 10, 30, 00), 0),
            new (new DateTime(2023,3,8, 10, 45, 00), 13),
            new (new DateTime(2023,3,8, 11, 00, 00), 0),
            new (new DateTime(2023,3,8, 11, 15, 00), 10),
            new (new DateTime(2023,3,8, 11, 30, 00), 1),
            new (new DateTime(2023,3,8, 12, 00, 00), 8),
        };
    }
    
}