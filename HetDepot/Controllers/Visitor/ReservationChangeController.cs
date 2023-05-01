using HetDepot.People.Model;
using HetDepot.Tours.Model;
using HetDepot.Views;

namespace HetDepot.Controllers
{
    public class ReservationChangeController : Controller
    {
        private Tour _tour;
        private Visitor _visitor;

        public ReservationChangeController(Tour tour, Visitor visitor) : base()
        {
            _tour = tour;
            _visitor = visitor;
        }
        public override void Execute()
        {
            var tourCurrentlySelected = Program.TourService.GetReservation(_visitor);

            if (tourCurrentlySelected == null || _tour == null)
                return; // Kan dit voorkomen? Zo ja, wanneer?

            Program.TourService.RemoveTourReservation(tourCurrentlySelected, _visitor);
            Program.TourService.AddTourReservation(_tour, _visitor);

            var message = Program.SettingService.GetConsoleText("consoleVisitorReservationChangeTourConfirmation", new()
            {
                ["currentTime"] = tourCurrentlySelected.StartTime.ToString(),
                ["newTime"] = _tour.StartTime.ToString(),
            });

            new AlertView(message, AlertView.Info).Show();

            NextController = new ReservationForGroupController(_tour);
        }
    }
}
