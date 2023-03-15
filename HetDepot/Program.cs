using HetDepot.Persistence;
using HetDepot.Settings;
using HetDepot.Registration;
using HetDepot.People;
using HetDepot.Tours.Model;
using HetDepot.Views;
using HetDepot.Views.Interface;
using HetDepot.Views.Parts;

namespace HetDepot
{
    internal class Program
    {
        static void Main(string[] args)
        {
        
            Console.WriteLine("Hello, World!");

            Console.WriteLine("Typ een naam om functies te testen (Kevin/Tom/Ruben):");
            string testName = Console.ReadLine() ?? "";

            switch (testName.ToLower())
            {

                case "tom":

                   
                    /*
                     * Hieronder ListView gemaakt. Deze ListView bied de mogelijkheid een selecteerbare lijst te
                     * tonen in de console. ListView heeft 2 parameters string Title en listViewItems.
                     * listViewItems moet een List zijn van classes die de IListableObject interface extenden of
                     * een lijst van directe ListItems.
                     * 
                     * Wat IListableObject mogelijk maakt is dat een model, in dit geval Tour, zonder transformatie
                     * gebruikt kan worden in een list. IListableObject verplicht de class een instance function
                     * ToListableItem() te hebben de instance convert naar een ListableItem.
                     */
                    
                    ListView tourOverviewVisitorWithInterface = new ListView("Welkom bij het depot", new List<IListableObject>()
                    {
                        new Tour(new DateTime(2023,3,8, 10, 00, 00), 3),
                        new Tour(new DateTime(2023,3,8, 10, 15, 00), 6),
                        new Tour(new DateTime(2023,3,8, 10, 30, 00), 0),
                        new Tour(new DateTime(2023,3,8, 10, 45, 00), 13),
                        new Tour(new DateTime(2023,3,8, 11, 00, 00), 0),
                        new Tour(new DateTime(2023,3,8, 11, 15, 00), 10),
                        new Tour(new DateTime(2023,3,8, 11, 30, 00), 1),
                        new Tour(new DateTime(2023,3,8, 12, 00, 00), 8),
                    });

                    /*
                     * Wanner de ListView is aangemaakt kan deze worden weergegeven en kan de keuze opgehaald worden
                     * via ShowAndGetResult(). Hier word de Value die terug word gegeven als object terug omgezet naar
                     * een Tour.
                     *
                     * De value is nooit iets anders dan Tour maar omdat er maar 1 return type kan zijn
                     * is deze object in ShowAndGetResult. Maar om te zorgen dat de compiler het nog snapt worden de value
                     * hier expliciet terug gezet naar Tour via de casting (Tour)
                     */
                    Tour selectedTourListItem = (Tour) tourOverviewVisitorWithInterface.ShowAndGetResult() ;

                    /*
                     * Hieronder word een InputView gemaakt deze lijkt op de list view alleen heeft deze in plaats van
                     * een list een input veld. InputView wilt 2 parameters (weer) Title en Message. Title word boven
                     * de input weergegeven en Message net voor het input veld. Bijvoorbeeld wanner Message "Uw code:" is.
                     * Zal de console Uw Code: schrijven en gelijk achter : kan de user dan iets invoeren.
                     * Er zit geen validatie in de inputview behalve het niet toelaten van lege inputs.
                     */
                    string? userCode = 
                        (new InputView("Reservering maken voor " + selectedTourListItem.GetTime(),
                        "Uw code:")).ShowAndGetResult();
                    
                    /*
                     * Hieronder weer de listview maar dan met een Subtitle en Directe ListViewItems 
                     */
                    ListView changeExisitingReservation = new ListView("Er is al een reservering voor vandaag. Wilt u doorgaan?","Als u doorgaat wordt de vorige reservering verwijderd.", new List<ListableItem>()
                    {
                        new ListViewItem("Ja", true, false, 1),
                        new ListViewItem("Nee", false, false, 1)
                    });

                    /*
                     * Hier word de Value die terug word gegeven als object terug omgezet naar een boolean
                     */
                    bool deletePrev = (bool) changeExisitingReservation.ShowAndGetResult();

                    /*
                     *
                     * Daarna wordt en 1 van de AlertViews aangeroepen. In tegendeel tot de List en -InputView
                     * geeft AlertView niks terug. Het enige dat deze doet is het weergeven van een message
                     * in het midden van de console met de gekozen ConsoleColor. AlertView heeft 3 constants voor
                     * kleuren maar in principe kan elke kleur gebruikt worden
                     * 
                     */
                    if (deletePrev)
                    {
                        (new AlertView("Uw reservering is aangemaakt", AlertView.Success)).Show();
                    }
                    else
                    {
                        (new AlertView("Uw reservering wordt behouden", AlertView.Info)).Show();
                    }
                    
                    break;
                
                case "kevin":
            
                    // Kevin's validatie tests
                    
                    //TODO: Invalid data bij de services		
                    var repository = new Repository(new DepotJson(), new Logger(new DepotJson()));
                    var peopleService = new PeopleService(repository);
                    var settingService = new SettingService(repository);
                    var registrationService = new RegistrationService(repository);

                    //Een van de punten waar ik mee worstel is 'simpel'
                    //het simpelste is een lijst met id's, die checken, geen objecten nalopen, alleen een lijst
                    //het kan ook in een tour object of elders.
                    //vervolgens ben ik geneigd weer veel extra te maken.
                    Console.WriteLine("===========================================");
                    Console.WriteLine("==      Registratie / controle tours     ==");
                    Console.WriteLine("===========================================");
                    Console.WriteLine("== Huidige reserveringen / aanmeldingen");
                    registrationService.TestShowAllRegistrations();
                    Console.WriteLine();
                    Console.WriteLine("== E0000000001 / E9900000000 geldig");
                    Console.WriteLine($"E0000000001 - {registrationService.HasTourReservation("E0000000001")}");
                    Console.WriteLine($"E9900000000 - {registrationService.HasTourReservation("E9900000000")}");
                    Console.WriteLine();
                    Console.WriteLine("== Toevoegen 2 reserveringen en bestaande verwijderen");
                    registrationService.AddTourReservation("E9900000000");
                    registrationService.AddTourReservation("E9910000000");
                    registrationService.RemoveTourReservation("E0000000001");
                    Console.WriteLine();
                    Console.WriteLine("== Nieuwe status reserveringen / aanmeldingen");
                    registrationService.TestShowAllRegistrations();
                    Console.WriteLine();
                    Console.WriteLine("== E0000000001 / E9900000000 geldig");
                    Console.WriteLine($"E0000000001 - {registrationService.HasTourReservation("E0000000001")}");
                    Console.WriteLine($"E9900000000 - {registrationService.HasTourReservation("E9900000000")}");
                    Console.WriteLine();



                    Console.WriteLine();

                    Console.WriteLine("===========================================");
                    Console.WriteLine("==                Settings               ==");
                    Console.WriteLine("===========================================");
                    Console.WriteLine($"Name - 'consoleVisitorLogonCodeInvalid', Value '{settingService.GetSettingValue("consoleVisitorLogonCodeInvalid")}'");
                    Console.WriteLine($"Name - 'maxPersonPerTour', Value '{settingService.GetSettingValue("maxPersonPerTour")}'");
                    Console.WriteLine($"Name - 'guideLunchbreakStart', Value '{settingService.GetSettingValue("guideLunchbreakStart")}'");
                    Console.WriteLine($"Name - 'guideLunchbreakEnd', Value '{settingService.GetSettingValue("guideLunchbreakEnd")}'");

                    Console.WriteLine();

                    Console.WriteLine("===========================================");
                    Console.WriteLine("==    Bezoekers, afdelingshoofd, gids    ==");
                    Console.WriteLine("===========================================");
                    Console.WriteLine($"Gids: - {peopleService.GetGuide().Id}");
                    Console.WriteLine($"Manager: - {peopleService.GetManager().Id}");

                    var visitors = peopleService.GetVisitors();
                    foreach (var visitor in visitors)
                    {
                        Console.WriteLine($"Bezoeker: - {visitor.Id}");
                    }

                    Console.WriteLine();

                    Console.WriteLine("===========================================");
                    Console.WriteLine("==                Errorlog               ==");
                    Console.WriteLine("===========================================");
                    repository.TestErrorlog();

                    var errorz = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "ExampleFile\\ExampleErrorlog.txt"));

                    foreach (var error in errorz)
                        Console.WriteLine(error);
                    
                    break;
                
                case "ruben":

                    // Ruben's user interface met rondleiding-tijden
                
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
                        Renderer.ResetConsole();
                        Console.WriteLine($"U heeft gekozen voor de rondleiding om {selectedTour} uur");
                        Console.WriteLine($"Bedankt en tot ziens bij Het Depot!");
                    }

                    break;
                
                default:
                    break;
            }
        }
    }
}