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

            // Genereer lijst van rondleidingen met informatie over aantal vrije plaatsen
            var tourList = tours.Select(tour => tour.ToListableItem(
                () => Program.SettingService.GetConsoleText(
                    tour.FreeSpaces <= 0 ? "consoleTourNoFreeSpaces" : (tour.FreeSpaces == 1 ? "consoleTourOneFreeSpace" : "consoleTourFreeSpaces"),
                    new()
                    {
                        ["count"] = tour.FreeSpaces.ToString(),
                    }
                ),
                () => tour.FreeSpaces == 0 // Disabled (niet selecteerbaar) als er geen vrije plaatsen zijn
            )).ToList();

            // Extra optie "Inloggen als gids":
            tourList.Add(new ListViewExtraItem<Tour, Controller>(Program.SettingService.GetConsoleText("consoleGuideLogin"), () => new GuideController()));

            //TODO: Opmerking Kevin: Als alle rondleidingen vol zitten, 'hangt' de interface
            ListView<Tour> tourOverviewVisitorWithInterface = new(Program.SettingService.GetConsoleText("consoleWelcome"), tourList);

            Controller? otherController;
            Tour? selectedTour = tourOverviewVisitorWithInterface.ShowAndGetResult<Controller>(out otherController);
            NextController = otherController; // Alleen als extra optie gekozen is

            if (selectedTour != null)
                NextController = new RequestAuthenticationController(selectedTour);
        }
    }
}
