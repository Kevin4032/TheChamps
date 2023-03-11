using HetDepot.Persistence;
using HetDepot.Validation;
using HetDepot.Settings;
using HetDepot.Registration;
using HetDepot.People;

namespace HetDepot
{
    internal class Program
    {
        static void Main(string[] args)
        {
			//TODO: Invalid data meldingen in systeemsettings.
			//TODO: 			
			var repository = new Repository(new DepotJson(), new Logger());
			var peopleService = new PeopleService(repository);
			var settingService = new SettingService(repository);
			var registrationService = new RegistrationService(repository);


			Console.WriteLine(settingService.GetSettingValue("consoleVisitorLogonCodeInvalid"));
			Console.WriteLine(settingService.GetSettingValue("maxPersonPerTour"));
			Console.WriteLine(settingService.GetSettingValue("guideLunchbreakStart"));
			Console.WriteLine(settingService.GetSettingValue("guideLunchbreakEnd"));

			Console.WriteLine($"Gids: - {peopleService.GetGuide().Id}");
			Console.WriteLine($"Manager: - {peopleService.GetManager().Id}");

			var visitors = peopleService.GetVisitors();
			foreach (var visitor in visitors)
			{ 
				Console.WriteLine(visitor.Id);
			}

			//Onderstaand geeft dictionary error
			//Console.WriteLine(settingService.GetSettingValue("consoleVisitorLogonCodeIasdffdsnvalid"));

		}
    }
}

/*
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

			TourList tours = new();
			tours.ShowScreen();

			TourList.ResetConsole();
			Console.WriteLine($"U heeft gekozen voor de rondleiding om {tours.Tours[tours.SelectedTour]} uur");
			Console.WriteLine($"Bedankt en tot ziens bij Het Depot!");
*/