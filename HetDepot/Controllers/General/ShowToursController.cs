using HetDepot.Controllers.General;
using HetDepot.Tours.Model;
using HetDepot.Views.Interface;
using HetDepot.Views;
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

			ListView tourOverviewVisitorWithInterface = new ListView(Program.SettingService.GetConsoleText("consoleWelcome"), 
				tours.ToList<IListableObject>(), new List<ListableItem>()
				{
					new ListViewItem(Program.SettingService.GetConsoleText("consoleHomeLoginAsGuide"), "guide"),
					new ListViewItem(Program.SettingService.GetConsoleText("consoleHomeLoginAsManager"), "manager")
				});
			object selectedTour = tourOverviewVisitorWithInterface.ShowAndGetResult();

			if (selectedTour is Tour)
			{
				NextController = new RequestAuthenticationController((Tour)selectedTour);
				return;
			}
			
			if ((string)selectedTour == "guide")
			{
				NextController = new GuideController();
				return;
			}

			if ((string)selectedTour == "manager")
			{
				NextController = new ManagerController();
				return;
			}
		}
	}
}
