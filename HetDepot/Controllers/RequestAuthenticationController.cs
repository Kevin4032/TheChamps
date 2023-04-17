using HetDepot.People.Model;
using HetDepot.Tours.Model;
using HetDepot.Views;

namespace HetDepot.Controllers
{
	public class RequestAuthenticationController : Controller
	{
		private Tour _tour;

		public RequestAuthenticationController(Tour tour) : base() 
		{ 
			_tour = tour;
		}

		public override void Execute()
		{
			var title = Program.SettingService.GetConsoleText("consoleVisitorRequestCodeSelectedTour", new() {
				["time"] = _tour.StartTime.ToString(),
			});
			var textToUser = Program.SettingService.GetConsoleText("consoleLogonOpeningWelcome");

			Person? person = null;
			string userCode;

			while (person == null)
			{
				userCode = (new InputView(title, textToUser)).ShowAndGetResult() ?? "";

				if (userCode.ToLower() == "q")
					break;
				
				person = userCode == "" ? null : GetPerson(userCode);

				if (person == null)
				{
					var errorMessage = Program.SettingService.GetConsoleText("consoleVisitorLogonCodeInvalid");
					new AlertView(errorMessage, AlertView.Error).Show();
				}
			}

			NextController = person == null ? new ShowToursController() : new ValidateTourPickController(_tour, person);
		}

		private Person? GetPerson(string userCode)
		{
			Person? person = null;

			try
			{
				person = Program.PeopleService.GetById(userCode);
			}
			catch (Exception ex)
			{
				Program.ErrorLogger.LogError($"{this.GetType()} - {ex.Message}");
			}

			return person;
		}
	}
}
