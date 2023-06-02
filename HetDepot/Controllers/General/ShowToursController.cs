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
            var tours = Program.TourService.GetOpenTours();

            // Generate list of tours with info on number of free spaces
            var tourList = tours.Select(tour => tour.ToListableItem(
                Program.SettingService.GetConsoleText(
                    tour.FreeSpaces <= 0 ? "tourNoFreeSpaces" : (tour.FreeSpaces == 1 ? "tourOneFreeSpace" : "tourFreeSpaces"),
                    new()
                    {
                        ["count"] = tour.FreeSpaces.ToString(),
                    }
                ),
                tour.FreeSpaces == 0 // Disabled (not selectable) when there are no free spaces
            )).ToList();

            // Extra options ("Reservering Annuleren", "Inloggen als gids", "Inloggen als afdelingshoofd")
            tourList.Add(new ListViewExtraItem<Tour, Controller>(Program.SettingService.GetConsoleText("homeCancelReservation"), () => new ReservationCancellationController()));
            tourList.Add(new ListViewExtraItem<Tour, Controller>(Program.SettingService.GetConsoleText("homeLoginAsGuide"), () => new GuideController()));
            tourList.Add(new ListViewExtraItem<Tour, Controller>(Program.SettingService.GetConsoleText("homeLoginAsManager"), () => new ManagerController()));

            //TODO: Opmerking Kevin: Als alle rondleidingen vol zitten, 'hangt' de interface
            ListView<Tour> tourOverviewVisitorWithInterface = new(
                Program.SettingService.GetConsoleText("welcome"),
                Program.SettingService.GetConsoleText("welcomeInstructions"),
                tourList
            );

            Controller? otherController;
            Tour? selectedTour = tourOverviewVisitorWithInterface.ShowAndGetResult<Controller>(out otherController);
            NextController = otherController; // Only set if an extra option is selected

            if (selectedTour != null)
                NextController = new RequestAuthenticationController(selectedTour);
        }
    }
}
