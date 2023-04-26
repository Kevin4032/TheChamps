using HetDepot.Tours.Model;
using HetDepot.Views.Interface;
using HetDepot.Views;

namespace HetDepot.Controllers
{
	public class ShowToursController : Controller
	{
		public ShowToursController() : base()
		{
		}

		public override void Execute()
		{
			var tours = Program.TourService.Tours;

			//TODO: Opmerking Kevin: Als alle rondleidingen vol zitten, 'hangt' de interface
			ListView<Tour> tourOverviewVisitorWithInterface = new(Program.SettingService.GetConsoleText("consoleWelcome"), tours.ToList<IListableObject<Tour>>());
			Tour? selectedTour = tourOverviewVisitorWithInterface.ShowAndGetResult();

			if (selectedTour != null)
				NextController = new RequestAuthenticationController(selectedTour);
		}
	}
}
