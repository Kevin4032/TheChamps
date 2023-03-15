namespace HetDepot.Controllers.Tests;

using HetDepot.Views;
using HetDepot.Tours.Model;

class RubensTestController : Controller
{
    /*
        Tom's code from the old Program.cs
    */

    public override void Execute()
    {
        try
        {
            Console.GetCursorPosition();
        }
        catch (System.IO.IOException)
        {
            // Voor degenen die Visual Studio Code niet goed ingesteld hebben!
            Console.WriteLine("No interactive console available");
            Console.WriteLine("If you see this in Visual Studio Code, set \"console\" to \"integratedTerminal\" and \"internalConsoleOptions\" to \"neverOpen\" in .vscode/launch.json");
            return;
        }

        List<Tour> tours = new()
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
        
        Tour? selectedTour = TourListView.SelectTour(tours);

        if (selectedTour != null)
        {
            TourListView.ResetConsole();
            Console.WriteLine($"U heeft gekozen voor de rondleiding om {selectedTour} uur");
            Console.WriteLine($"Bedankt en tot ziens bij Het Depot!");
        }
    }
}