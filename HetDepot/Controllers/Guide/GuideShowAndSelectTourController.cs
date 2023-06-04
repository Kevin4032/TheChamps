using HetDepot.Tours.Model;
using HetDepot.Views;
using HetDepot.Views.Parts;

namespace HetDepot.Controllers
{
    public class GuideShowAndSelectTourController : Controller
    {

        public override void Execute()
        {
            var tours = Program.TourService.GetOpenTours();

            // Genereer lijst van rondleidingen met informatie over aantal reserveringen
            var tourList = tours.Select(tour => tour.ToListableItem(
                Program.SettingService.GetConsoleText(
                    tour.Reservations.Count <= 0 ? "tourNoReservations" : (tour.Reservations.Count == 1 ? "tourOneReservation" : "tourReservations"),
                    new()
                    {
                        ["count"] = tour.Reservations.Count.ToString(),
                    }
                )
            )).ToList();

            // Extra optie "Inloggen als gids":
            tourList.Add(new ListViewExtraItem<Tour, Controller>(Program.SettingService.GetConsoleText("back"), () => new ShowToursController()));
            ListView<Tour> tourOverviewVisitorWithInterface = new(Program.SettingService.GetConsoleText("guideTourShowToursToStart"), tourList);

            Controller? otherController;
            Tour? selectedTour = tourOverviewVisitorWithInterface.ShowAndGetResult<Controller>(out otherController);
            NextController = otherController; // Only set if an extra option is selected

            if (selectedTour == null)
                return;

            //Controleer of selectedTour meer dan nul reserveringen heeft:
            if (selectedTour.Reservations.Count == 0)
            {
                NextController = new GuideNoReservationsForThisTourController();
            }
            else
            {
                NextController = new GuideStartTourAdmissionController(selectedTour);
            }

        }

    }
}
