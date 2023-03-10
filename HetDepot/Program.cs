using HetDepot.Persistence;
using HetDepot.Settings;
using HetDepot.Registration;
using HetDepot.People;
using HetDepot.Tours.Model;
using HetDepot.Views;

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

                    // Tom's display tours
                    TourguideView.DisplayTours();
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
                        TourListView.ResetConsole();
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