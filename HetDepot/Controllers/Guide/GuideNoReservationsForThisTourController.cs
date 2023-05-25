namespace HetDepot.Controllers;

class GuideNoReservationsForThisTourController : Controller
{


    public override void Execute()
    {
        // Wij komen op deze controller uit als gids rondleiding selecteert die geen reserveringen heeft
        // Prints: geen openstaande reserveringen voor deze rondleiding.

        Console.WriteLine(Program.SettingService.GetConsoleText("guideTourNoReservationsForTour"));
        // als gids h typt, gaan terug naar niewe instance van guidecontroller.
        Console.WriteLine("Druk \"h\" om door te gaan.");
        var pressedH = Console.ReadLine();
        if (pressedH == "H" || pressedH == "h")
        {
            NextController = new GuideShowAndSelectTourController();
        }

        // NextController = new SomeOtherController();
    }
}
