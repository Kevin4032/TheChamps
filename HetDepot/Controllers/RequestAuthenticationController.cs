using HetDepot.People.Model;
using HetDepot.Tours.Model;
using HetDepot.Views;

namespace HetDepot.Controllers
{
	public class RequestAuthenticationController : Controller
	{
		private Tour _tour;
		private Visitor _visitor;

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

			var ietsExtra = false;

			NextController = new CreateReservationController(visitor, _tour);
		}
	}
}
