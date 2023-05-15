using HetDepot.Tours.Model;
using HetDepot.Views;
using HetDepot.Views.Interface;
using HetDepot.Views.Parts;

namespace HetDepot.Controllers
{
    public class GuideShowAndSelectTourController : Controller
    {



        public override void Execute()
        {

            /*
            //nextTour is nu de huidige rondleiding:
            var nextTour = Program.TourService.GetNextTour();
            if (nextTour == null)
            {
                var message = Program.SettingService.GetConsoleText("consoleGuideTourNoToursAvailable");

                new AlertView(message, AlertView.Info).Show();


                NextController = new ShowToursController();
                return;
            } 

            // Toon op console hudige rondleiding {volgende rondleiding}. guideConfirmTourCheckInStart is een string als user input geeft,anders null
            var guideConfirmTourCheckInStart = new InputView("Huidige rondleiding: " + (nextTour != null ? nextTour.ToString() : "Er zijn geen rondleidingen meer beschikbaar"), "Start deze rondleiding").ShowAndGetResult();
            //als input ja is, begin met inchecken bezoekers
            //Klik enter om te starten i.pv. ja intikken?
            if (guideConfirmTourCheckInStart == "ja")
            {
                NextController = new GuideStartTourAdmissionController(nextTour!);
            }
            else
            {
                NextController = new ShowToursController();
            }
            */

            var tours = Program.TourService.Tours;

            // Genereer lijst van rondleidingen met informatie over aantal reserveringen
            var tourList = tours.Select(tour => tour.ToListableItem(
                Program.SettingService.GetConsoleText(
                    tour.Reservations.Count <= 0 ? "consoleTourNoReservations" : (tour.Reservations.Count == 1 ? "consoleTourOneReservation" : "consoleTourReservations"),
                    new()
                    {
                        ["count"] = tour.Reservations.Count.ToString(),
                    }
                )
            )).ToList();

            // Extra optie "Inloggen als gids":
            tourList.Add(new ListViewExtraItem<Tour, Controller>(Program.SettingService.GetConsoleText("consoleBack"), () => new ShowToursController()));
            ListView<Tour> tourOverviewVisitorWithInterface = new(Program.SettingService.GetConsoleText("consoleGuideTourShowToursToStart"), tourList);

            Controller? otherController;
            Tour? selectedTour = tourOverviewVisitorWithInterface.ShowAndGetResult<Controller>(out otherController);
            NextController = otherController; // Alleen als extra optie gekozen is

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
