using HetDepot.People.Model;
using HetDepot.Tour;

namespace HetDepot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start lezen data");

            var settingService = new Settings.SettingService();
            var validationService = new Validation.ValidationService(settingService);
			var peopleService = new People.PeopleService(settingService, validationService);

			//var person1 = new Visitor("E0000000001");
			//var person2 = new Visitor("E000000000x");
			//var person3 = new Manager("D0001040700");
			//validationService.IsValid(person1);
			//validationService.IsValid(person2);
			//validationService.IsValid(person3);


			//settingService.WriteSettings();

			//Console.WriteLine(settingService.GetSettingValue("consoleVisitorLogonCodeInvalid"));

			//var visitors = peopleService.GetVisitor();

			//foreach (var visitor in visitors)
			//{
			//	Console.WriteLine($"{visitor.Id} - {visitor.GetType()}");
			//}

			//peopleService.WritePeople();
			//var guide = peopleService.GetGuide();


			//Console.WriteLine($"Guide: {guide.Id}");

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