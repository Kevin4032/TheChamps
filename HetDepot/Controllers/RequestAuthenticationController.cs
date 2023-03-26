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

			var userCode = (new InputView(title,textToUser)).ShowAndGetResult() ?? "No input";
			var visitor = _peopleService.GetVisitorById(userCode);

			NextController = new ValidateTourPickController(_tour, visitor);
		}
	}
}
