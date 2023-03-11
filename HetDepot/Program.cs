using HetDepot.JsonReader;
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

            var settingService = new SettingService();
			var registrationService = new RegistrationService();
			var validationService = new ValidationService(registrationService);
			var repository = new Repository(settingService, validationService);
			var peopleService = new PeopleService(repository);
			

			settingService.GetSettingValue("consoleVisitorLogonCodeInvalid");

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