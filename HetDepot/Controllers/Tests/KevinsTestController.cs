namespace HetDepot.Controllers.Tests;

using HetDepot.Persistence;
using HetDepot.Settings;
using HetDepot.Registration;
using HetDepot.People;
using HetDepot.Errorlogging;
using HetDepot.Tours.Model;
using HetDepot.Tours;
using System.Text.Json;
using HetDepot.People.Model;

class KevinsTestController : Controller
{
    /*
        Kevin's code from the old Program.cs
    */

    private string _toursPath;

    public KevinsTestController()
    {
        _toursPath = Path.Combine(Directory.GetCurrentDirectory(), "ExampleFile\\ExampleTours.json");
	}

    public override void Execute()
    {
        //KevinDing();
        //EvenSchrijven();
        //KorteTest();
        //JsonPrutsen();
        Testje20230318();
    }


    private void Testje20230318()
    {
		var errorLoggerJson = new DepotErrorJson();
		var errorLogger = new DepotErrorLogger(errorLoggerJson);
		var repository = new Repository(new DepotJson(errorLogger), errorLogger, new DepotDataValidator());
		var peopleService = new PeopleService(repository, errorLogger);
		var settingService = new SettingService(repository, errorLogger);
		var registrationService = new RegistrationService(repository, errorLogger);
        var tourService = new TourService(repository, registrationService);
        var validationService = new ValidationService(registrationService);

		var controllert = new CreateReservationController(registrationService, tourService, peopleService, validationService, settingService);
        controllert.Execute();
    }

    private void KorteTest()
    {
        Console.WriteLine("In korte test - Start");

        var repo = new Repository(new DepotJson(new DepotErrorLogger(new DepotErrorJson())), new DepotErrorLogger(new DepotErrorJson()), new DepotDataValidator());
        var reg = new RegistrationService(repo, new DepotErrorLogger(new DepotErrorJson()));

        var ts = new TourService(repo, reg);
                            
        foreach (var tour in ts.Tours)
        {
            Console.WriteLine($"StartTime: {tour.StartTime} --==-- Gids: {tour.Guide.Id}");
        }


		Console.WriteLine("In korte test - Eind");
	}

    private void JsonPrutsen()
    {
        Console.WriteLine($"In json prutsen -- sart");
		var rawJson = File.ReadAllText(_toursPath);
		var result = JsonSerializer.Deserialize<List<TourJsonModel>>(rawJson);
		Console.WriteLine($"In json prutsen -- eind");
	}

    private void EvenSchrijven()
    {
        var schrijvert = new DepotJson(new DepotErrorLogger(new DepotErrorJson()));

        var toursTeSchrijven = GetTours();

        schrijvert.Write(_toursPath, toursTeSchrijven);
    }

    private List<Tour> GetTours()
    {
        var list = new List<Tour>();

        var t1 = new Tour(DateTime.Now, new Guide("D1234567890"), 13, new List<Visitor>(), new List<Visitor>());
		var t2 = new Tour(DateTime.Now, new Guide("D1234567890"), 13, new List<Visitor>(), new List<Visitor>());

        t1.AddReservation(new Visitor("E1234567890"));
		t1.AddAdmission(new Visitor("E1234567890"));

        t2.AddReservation(new Visitor("E0987654321"));
		t2.AddAdmission(new Visitor("E0987654321"));

		list.Add(t1);
		list.Add(t2);

		return list;
    }

    private static void KevinDing()
    {
        //TODO: Invalid data bij de services	
        var errorLoggerJson = new DepotErrorJson();	
        var errorLogger = new DepotErrorLogger(errorLoggerJson);
        var repository = new Repository(new DepotJson(errorLogger), errorLogger, new DepotDataValidator());
        var peopleService = new PeopleService(repository, errorLogger);
        var settingService = new SettingService(repository, errorLogger);
        var registrationService = new RegistrationService(repository, errorLogger);

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

    }
}