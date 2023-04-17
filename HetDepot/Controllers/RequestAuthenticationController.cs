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
			var title = _settingService.GetConsoleText("consoleVisitorRequestCodeSelectedTour") + _tour.StartTime;
			var textToUser = _settingService.GetConsoleText("consoleLogonOpeningWelcome");

			var success = false;
			Person? person = null;
			string userCode;

			do
			{
				userCode = (new InputView(title, textToUser)).ShowAndGetResult() ?? "No input";

				if (userCode.ToLower() == "q")
				{
					success = true;
				}
				else
				{
					person = GetPerson(userCode);

					if (person != null)
						success = true;
					else
					{
						var errorMessage = _settingService.GetConsoleText("consoleVisitorLogonCodeInvalid");
						new AlertView(errorMessage, AlertView.Error).Show();
					}
				}
			}
			while (!success);

			if (userCode == "q")
				NextController = new ShowToursController();
			else if (person != null)
				NextController = new ValidateTourPickController(_tour, person);
		}

		private Person? GetPerson(string userCode)
		{
			Person? person = null;

			try
			{
				person = _peopleService.GetById(userCode);
			}
			catch (Exception ex)
			{
				_errorLogger.LogError($"{this.GetType()} - {ex.Message}");
			}

			return person;
		}
	}
}
