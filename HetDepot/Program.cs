namespace HetDepot;

using HetDepot.Controllers;
using HetDepot.Errorlogging;
using HetDepot.People;
using HetDepot.Persistence;
using HetDepot.Settings;
using HetDepot.Tours;

internal class Program
{
    /*
        Main Program class. This should only serve as a bootstrap to a controller, and be changed as little as possible.
        When creating new functionality, create new controllers instead and modify existing ones to point to the new code.
    */

    public static Controller? CurrentController { get; private set; }

    public static readonly IDepotErrorLogger ErrorLogger;
    public static readonly ITourService TourService;
    public static readonly IPeopleService PeopleService;
    public static readonly ISettingService SettingService;

    private static Controller? _createDefaultController()
    {
        // This creates an instance of the default controller (the "home screen")
        // It's the first screen to be shown and the program returns to it if a controller does not provide a different controller to run next
        return new DefaultController();
    }

    static Program()
    {
        ErrorLogger = new DepotErrorLogger(new DepotErrorJson());
        Repository repository = null;
        
        try
        {
            repository = new Repository(new DepotJson(ErrorLogger), ErrorLogger, new DepotDataValidator());
        }
        catch (Exception ex)
        {
            LogError(ex);
        }

        // Het idee van Services is toch dat ze overal beschikbaar zijn? Daarom hier naartoe verplaatst vanuit Controller (Ruben)
        SettingService = new SettingService(repository, ErrorLogger);

        try
        {
            PeopleService = new PeopleService(repository, ErrorLogger);
        }
        catch (Exception ex)
        {
            LogError(ex);
        }

        // Controle dat data correct is
        CheckProperInit();

        TourService = new TourService(repository, ErrorLogger);
    }

    private static void LogError(Exception ex)
    {
        Console.WriteLine(ex.Message);
        Console.WriteLine("Zie 'Errorlog.txt' en los het probleem op");
        Environment.Exit(1);
    }

    private static void CheckProperInit()
    {
        CheckVisitors();
        CheckGuide();
        CheckManager();
        ManagerAndGuideSameId();
    }

    private static void CheckVisitors()
    {
        try
        {
            var visitorCount = Program.PeopleService.GetVisitors().Count;
            if (visitorCount == 0)
            {
                throw new Exception("Geen bezoekers gevonden, zie ExampleVisitors.json");
            }
        }
        catch (Exception ex)
        {
            LogError(ex);
        }
    }

    private static void CheckGuide()
    {
        try
        {
            var guide = Program.PeopleService.GetGuide();
        }
        catch (Exception ex)
        {
            LogError(ex);
        }
    }

    private static void CheckManager()
    {
        try
        {
            var guide = Program.PeopleService.GetManager();
        }
        catch (Exception ex)
        {
            LogError(ex);
        }
    }

    public static void Main(string[] args)
    {
        // Start by creating a default controller (see _createDefaultController above)
        CurrentController = _createDefaultController();

        // Basic program loop
        // If there is a default controller, the program should always return to it and never end
        while (CurrentController != null)
        {
            // Execute the current controller
            CurrentController.Execute();

            // Set up the next controller (the "NextController" that was set by the controller that just executed, or else the default controller if NextController is null)
            CurrentController = Controller.NextController ?? _createDefaultController();
            Controller.ResetNextController(); // Reset NextController to null
        }
    }

    private static void ManagerAndGuideSameId()
    {
        var guide = PeopleService.GetGuide();
        var manager = PeopleService.GetManager();

        if (guide.Equals(manager))
        {
            ErrorLogger.LogError("ID van Gids en Afdelingshoofd is gelijk. Zie ExampleManager.json / ExampleGuide.json");
            Console.WriteLine("ID van Gids en Afdelingshoofd is gelijk.");
            Console.WriteLine("Zie 'Errorlog.txt' en los het probleem op.");
            Environment.Exit(1);
        }
    }
}
