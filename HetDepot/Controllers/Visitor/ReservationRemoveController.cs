using HetDepot.People.Model;
using HetDepot.Tours.Model;
using HetDepot.Views;

namespace HetDepot.Controllers
{
    public class ReservationRemoveController : Controller
    {
        private Tour _tour;
        private Visitor _visitor;

        public ReservationRemoveController(Tour tour, Visitor visitor)
        {
            _tour = tour;
            _visitor = visitor;
        }

        public override void Execute()
        {
            Program.TourService.RemoveTourReservation(_tour, _visitor);

            var message = Program.SettingService.GetConsoleText("consoleVisitorReservationCancellationConfirmation");

            new AlertView(message, AlertView.Info).Show();

            NextController = new ShowToursController();
        }
    }
}
