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
        // _toursPath = Path.Combine(Directory.GetCurrentDirectory(), "ExampleFile", "ExampleTours.json");
	}

    public override void Execute()
    {
		// Testje20230326_001();
        //Testje20230320_001();
        //Testje20230320_002();
        //Testje20230320_003();
        //Testje20230320_004();
        //Testje20230320_005();
        //KevinDing();
        //EvenSchrijven();
        //KorteTest();
        //JsonPrutsen();
        //Testje20230318();
    }

	private void Testje20230326_001()
	{
		var showTours = new ShowToursController();
		showTours.Execute();
	}

	private void Testje20230320_005()
	{
		var errorLoggerJson = new DepotErrorJson();
		var errorLogger = new DepotErrorLogger(errorLoggerJson);
		var repository = new Repository(new DepotJson(errorLogger), errorLogger, new DepotDataValidator());
		var peopleService = new PeopleService(repository, errorLogger);
		var settingService = new SettingService(repository, errorLogger);
		var tourService = new TourService(repository, errorLogger);

		var tours = tourService.Tours;

		Console.WriteLine($"Tours: {tours.Count}");

		foreach (var tour in tours)
		{
			Console.WriteLine($"Tour: {tour.StartTime}");
			foreach (var visitor in tour.Reservations)
			{
				Console.WriteLine($"Reservation: {visitor.Id}");
			}
		}

	}

	private void Testje20230320_004()
	{
		var errorLoggerJson = new DepotErrorJson();
		var errorLogger = new DepotErrorLogger(errorLoggerJson);
		var repository = new Repository(new DepotJson(errorLogger), errorLogger, new DepotDataValidator());
		var peopleService = new PeopleService(repository, errorLogger);
		var settingService = new SettingService(repository, errorLogger);
		var tourService = new TourService(repository, errorLogger);

		var visitors = peopleService.GetVisitors();

        //Tours is leeg,bewust.
        var tours = tourService.Tours;

        Console.WriteLine($"Tours: {tours.Count}");

        foreach ( var visitor in visitors )
        {
            Console.WriteLine($"Visitor: {visitor.Id}");
        }

        foreach ( var tour in tours )
        {
            Console.WriteLine($"Tour: {tour.StartTime}");
			foreach ( var visitor in tour.Reservations )
            {
                Console.WriteLine($"Reservation: {visitor.Id}");
            }
		}

        //tourService.WriteTourData();

		Console.WriteLine($"TWEEEDE RODNE");

		tours[0].AddReservation(visitors.ElementAt(0));

		foreach (var tour in tours)
		{
			Console.WriteLine($"Tour: {tour.StartTime}");
			foreach (var visitor in tour.Reservations)
			{
				Console.WriteLine($"Reservation: {visitor.Id}");
			}
		}

		//tourService.WriteTourData();
	}

	private void Testje20230320_003()
	{
		var errorLoggerJson = new DepotErrorJson();
		var errorLogger = new DepotErrorLogger(errorLoggerJson);
		var repository = new Repository(new DepotJson(errorLogger), errorLogger, new DepotDataValidator());
        var set = repository.GetSettings();

        Console.WriteLine($"MaxRes stouR{set.MaxReservationsPerTour}");

	}

	private void Testje20230320_002()
	{
        var set = new Settings.Model.Setting(ExampleConsole(), ExampleTourTime(), 13);

		var errorLoggerJson = new DepotErrorJson();
		var errorLogger = new DepotErrorLogger(errorLoggerJson);
		var s = new DepotJson(errorLogger);

        s.Write("ExampleFile\\Kevintest.json", set);
	}

	private void Testje20230320_001()
	{
		var errorLoggerJson = new DepotErrorJson();
		var errorLogger = new DepotErrorLogger(errorLoggerJson);
		var repository = new Repository(new DepotJson(errorLogger), errorLogger, new DepotDataValidator());
		var peopleService = new PeopleService(repository, errorLogger);
		var settingService = new SettingService(repository, errorLogger);

		var tourService = new TourService(repository, errorLogger);

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
        var tourService = new TourService(repository, errorLogger);


		var tourtje = tourService.Tours.FirstOrDefault(t => t.StartTime == DateTime.Parse("2023-03-18T11:00:00.0000000+01:00"));
		//var controllert = new CreateReservationController("E0000000009", DateTime.Parse("2023-03-18T11:00:00.0000000+01:00"));
		if (tourtje != null)
		{
			var controllert = new ReservationCreateController(tourtje, new Visitor("E0000000009"));
			controllert.Execute();
		}
    }

    private void KorteTest()
    {
        Console.WriteLine("In korte test - Start");

        var repo = new Repository(new DepotJson(new DepotErrorLogger(new DepotErrorJson())), new DepotErrorLogger(new DepotErrorJson()), new DepotDataValidator());
        var setting = new SettingService(repo, new DepotErrorLogger(new DepotErrorJson()));
		var peopleService = new PeopleService(repo, new DepotErrorLogger(new DepotErrorJson()));

        var ts = new TourService(repo, new DepotErrorLogger(new DepotErrorJson()));
                            
        foreach (var tour in ts.Tours)
        {
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

        var t1 = new Tour(DateTime.Now, 13, new List<Visitor>(), new List<Visitor>());
		var t2 = new Tour(DateTime.Now, 13, new List<Visitor>(), new List<Visitor>());

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

        var tourService = new TourService(repository, errorLogger);

        

		//tourService.VoorTestEnDemoDoeleinden();

		var t1 = DateTime.Parse("2023-03-18T11:00:00.0000000+01:00");
        var t2 = DateTime.Parse("2023-03-18T12:00:00.0000000+01:00");

        var visitorNew1 = new Visitor("K0000000001");
		var visitorNew2 = new Visitor("K0000000002");
		var visitorNew3 = new Visitor("K0000000003");
		var visitorBestaan1 = new Visitor("E0987654321");

		//Console.WriteLine($"Visitornew {tourService.HasAdmission(visitorNew1)}");
		//Console.WriteLine($"viistorbestaand {tourService.HasAdmission(visitorBestaan1)}");


		//Console.WriteLine($"TOURTAKEN? {visitorNew1.TourTaken}");
		var tourtje = tourService.Tours.FirstOrDefault(t => t.StartTime == DateTime.Parse("2023-03-18T11:00:00.0000000+01:00"));

		if (tourtje != null)
		{
			tourService.AddTourAdmission(tourtje, visitorNew1);
			tourService.AddTourAdmission(tourtje, visitorNew2);
			tourService.AddTourReservation(tourtje, visitorNew3);
		}

        //tourService.VoorTestEnDemoDoeleinden();


        Console.WriteLine($"Schrijven met nieuwe entries, check file op disk");
        //tourService.WriteTourData();


		Console.WriteLine("===========================================");
        Console.WriteLine("==                Settings               ==");
        Console.WriteLine("===========================================");
        //Console.WriteLine($"Name - 'consoleVisitorLogonCodeInvalid', Value '{settingService.GetSettingValue("consoleVisitorLogonCodeInvalid")}'");
        //Console.WriteLine($"Name - 'maxPersonPerTour', Value '{settingService.GetSettingValue("maxPersonPerTour")}'");
        //Console.WriteLine($"Name - 'guideLunchbreakStart', Value '{settingService.GetSettingValue("guideLunchbreakStart")}'");
        //Console.WriteLine($"Name - 'guideLunchbreakEnd', Value '{settingService.GetSettingValue("guideLunchbreakEnd")}'");

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

        var errorz = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "ExampleFile", "ExampleErrorlog.txt"));

        foreach (var error in errorz)
            Console.WriteLine(error);

    }

	private Dictionary<string, string> ExampleConsole()
	{
		return new Dictionary<string, string>()
			{
				{ "consoleLogonOpeningWelcome", "Meld u aan op de console" }
			,   {"consoleVisitorReservationMaking", "U kunt een reservering maken door een tijdstip te selecteren."}
			,   {"consoleVisitorReservationAlreadyHavingOne", "U heeft al een reservering. Als u een ander tijdstip selecteert, wordt uw reservering gewijzigd. Als u uw huidige reservering selecteert, wordt deze geannuleerd."}
			,   {"consoleVisitorLogonCodeInvalid", "De code is niet geldig. Controleer uw code en probeer het nog eens."}
			,   {"consoleVisitorReservationGroupOption", "Aanmelden groep"}
			,   {"consoleVisitorReservationNoMoreTours", "Er zijn geen rondleidingen meer beschikbaar"}
			,   {"consoleVisitorReservationConfirmation", "Uw inschrijving is bevestigd"}
			,   {"consoleVisitorReservationGroupStart", "U wordt gevraagd om alle unieke codes in te voeren. De maximale grootte is {instelling maximale grootte rondleiding}. Als u klaar bent, voert u in \u201Cgereed\u201D"}
			,   {"consoleVisitorReservationGroupEnd", "De grootte van uw gezelschap is {groepsgrootte}"}
			,   {"consoleVisitorReservationMaximumForTour", "Het maximum aantal deelnemers is bereikt."}
			,   {"consoleVisitorReservationCancellationRequestionConfirmation", "Wilt u deze rondleiding annuleren? Ja/Nee."}
			,   {"consoleVisitorReservationCancellationConfirmation", "Reservering geannuleerd"}
			,   {"consoleVisitorReservationChangeTourConfirmation", "U bent uitgeschreven voor {tijdstip}\u201D. Uw nieuwe rondleiding start om {tijdstip}"}
			,   {"consoleGuideTourVisitorValidationStart", "U kunt de rondleiding starten."}
			,   {"consoleGuideLogonCodeInvalid", "Uw code is niet geldig. Werkt u hier wel?"}
			,   {"consoleGuideTourCurrent", "Huidige rondleiding {tijdstip n}"}
			,   {"consoleGuideTourStart", "Rondleiding starten?"}
			,   {"consoleGuideTourNoToursAvailable", "Er zijn geen rondleidingen meer beschikbaar"}
			,   {"consoleGuideTourVisitorValidationStarted", "Er zijn { aantal reserveringen}"}
			,   {"consoleGuideTourVisitorValidationVisitorValidated", "{aantal} van {reserveringen} aangemeld."}
			,   {"consoleGuideTourVisitorValidationVisitorNextVisitor", "Volgende aanmelding"}
			,   {"consoleGuideTourVisitorAddWithoutReservationOption", "Aanmelden zonder reservering"}
			,   {"consoleGuideTourVisitorTourStartOption", "Starten rondleiding"}
			,   {"consoleGuideTourAllReservationsValidated", "Alle deelnemers zijn aangemeld."}
			,   {"consoleGuideTourVisitorAddWithoutReservationConfirmation", "Bezoeker toegevoegd. {aantal} van {reserveringen} aangemeld."}
			,   {"consoleManagerTicketsLoaded", "Met succes entreebewijzen geladen"}
			,   {"consoleManagerShowOptions", "Laad entreebewijzen voor de dag\nLaad instellingen\nBekijk rondleidinggegevens."}
			,   {"consoleManagerLogonCodeInvalid", "Uw code is niet geldig."}
			,   {"consoleVisitorAlreadyHasTourAdmission", "U heeft al deelgenomen aan een rondleiding vandaag. Morgen weer een kans."}
			};
	}
	private HashSet<string> ExampleTourTime()
	{
		return new HashSet<string> { "11:00", "11:20", "11:40", "12:00", "12:20", "12:40", "13:00", "13:20", "13:40", "14:00" };
	}
}