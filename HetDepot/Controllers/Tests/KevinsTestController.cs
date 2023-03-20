using HetDepot.Persistence;
using HetDepot.Settings;
using HetDepot.People;
using HetDepot.Errorlogging;
using HetDepot.Tours.Model;
using HetDepot.Tours;
using System.Text.Json;
using HetDepot.People.Model;

namespace HetDepot.Controllers.Tests;

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
        Testje20230320_001();
        //KevinDing();
        //EvenSchrijven();
        //KorteTest();
        //JsonPrutsen();
        //Testje20230318();
    }

	private void Testje20230320_001()
	{
		var errorLoggerJson = new DepotErrorJson();
		var errorLogger = new DepotErrorLogger(errorLoggerJson);
		var repository = new Repository(new DepotJson(errorLogger), errorLogger, new DepotDataValidator());
		var peopleService = new PeopleService(repository, errorLogger);
		var settingService = new SettingService(repository, errorLogger);

		var tourService = new TourService(repository, settingService);

        var visitors = peopleService.GetVisitors();

        foreach (var visitor in visitors)
        {
            Console.WriteLine(visitor.Id);
        }
	}

	private void Testje20230318()
    {
		var errorLoggerJson = new DepotErrorJson();
		var errorLogger = new DepotErrorLogger(errorLoggerJson);
		var repository = new Repository(new DepotJson(errorLogger), errorLogger, new DepotDataValidator());
		var peopleService = new PeopleService(repository, errorLogger);
		var settingService = new SettingService(repository, errorLogger);
        var tourService = new TourService(repository, settingService);

		var controllert = new CreateReservationController(tourService, peopleService, settingService);
        controllert.Execute();
    }

    private void KorteTest()
    {
        Console.WriteLine("In korte test - Start");

        var repo = new Repository(new DepotJson(new DepotErrorLogger(new DepotErrorJson())), new DepotErrorLogger(new DepotErrorJson()), new DepotDataValidator());
        var setting = new SettingService(repo, new DepotErrorLogger(new DepotErrorJson()));

        var ts = new TourService(repo, setting);
                            
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
        var errorLoggerJson = new DepotErrorJson();	
        var errorLogger = new DepotErrorLogger(errorLoggerJson);
        var repository = new Repository(new DepotJson(errorLogger), errorLogger, new DepotDataValidator());
        var peopleService = new PeopleService(repository, errorLogger);
        var settingService = new SettingService(repository, errorLogger);

        var tourService = new TourService(repository, settingService);

        

		tourService.VoorTestEnDemoDoeleinden();

		var t1 = DateTime.Parse("2023-03-18T11:00:00.0000000+01:00");
        var t2 = DateTime.Parse("2023-03-18T12:00:00.0000000+01:00");

        var visitorNew1 = new Visitor("K0000000001");
		var visitorNew2 = new Visitor("K0000000002");
		var visitorNew3 = new Visitor("K0000000003");
		var visitorBestaan1 = new Visitor("E0987654321");

        Console.WriteLine($"Visitornew {tourService.HasAdmission(visitorNew1)}");
        Console.WriteLine($"viistorbestaand {tourService.HasAdmission(visitorBestaan1)}");


		//Console.WriteLine($"TOURTAKEN? {visitorNew1.TourTaken}");

		tourService.AddTourAdmission(t1, visitorNew1);
		tourService.AddTourAdmission(t2, visitorNew2);
        tourService.AddTourReservation(t2, visitorNew3);

        tourService.VoorTestEnDemoDoeleinden();


        Console.WriteLine($"Schrijven met nieuwe entries, check file op disk");
        tourService.WriteTourData();


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