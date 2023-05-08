using HetDepot.Controllers.General;
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

            var extraOptions = new List<ListableItem<Tour>>
            {
                new ListViewExtraItem<Tour,Controller>(Program.SettingService.GetConsoleText("consoleGuideLogin"),
                    () => new GuideController()),
                new ListViewExtraItem<Tour,Controller>(Program.SettingService.GetConsoleText("consoleHomeLoginAsManager"),
                    () => new ManagerController()),
            };

            ListView<Tour> tourOverviewVisitorWithInterface =
                new(Program.SettingService.GetConsoleText("consoleWelcome"), tourList, extraOptions);

            Controller? otherController;
            Tour? selectedTour = tourOverviewVisitorWithInterface.ShowAndGetResult<Controller>(out otherController);
            NextController = otherController; // Alleen als extra optie gekozen is

            if (selectedTour != null)
                NextController = new RequestAuthenticationController(selectedTour);
        }
    }
}
