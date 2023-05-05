using HetDepot.Tours.Model;
using HetDepot.Views;
using HetDepot.Views.Interface;
using HetDepot.Views.Parts;

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

			var tourList = tours.ToList<IListableObject<Tour>>();
			
			// Extra optie "Inloggen als gids":
			var extraOptions = new List<ListableItem<Tour>>
			{
				new ListViewExtraItem<Tour,Controller>(Program.SettingService.GetConsoleText("consoleGuideLogin"), () => new GuideController()),
			};

			//TODO: Opmerking Kevin: Als alle rondleidingen vol zitten, 'hangt' de interface
			ListView<Tour> tourOverviewVisitorWithInterface = new(Program.SettingService.GetConsoleText("consoleWelcome"), tourList, extraOptions);

			Controller? otherController;
			Tour? selectedTour = tourOverviewVisitorWithInterface.ShowAndGetResult<Controller>(out otherController);
			NextController = otherController; // Alleen als extra optie gekozen is
			
			if (selectedTour != null)
				NextController = new RequestAuthenticationController(selectedTour);
		}
	}
}
