using HetDepot.People.Model;
using HetDepot.Tours.Model;
using HetDepot.Views;
using HetDepot.Views.Parts;

namespace HetDepot.Controllers
{
    public class ReservationRemoveController : Controller
    {
        private Tour _tour;
        private Visitor _visitor;
        private bool _forGroup;

        public ReservationRemoveController(Tour tour, Visitor visitor, bool forGroup = false)
        {
            _tour = tour;
            _visitor = visitor;
            _forGroup = forGroup;
        }

        public override void Execute()
        {
            NextController = new ShowToursController();

            if (_forGroup)
            {
                var message = Program.SettingService.GetConsoleText("consoleVisitorReservationAlreadyHavingOneForGroup");
                new AlertView(message, AlertView.Error).Show();
                NextController = new RequestAuthenticationController(_tour, true);
                return;
            }

            var question = Program.SettingService.GetConsoleText("consoleVisitorReservationAlreadyHavingOne");

            ListView<bool> replaceCurrentQuestion = new(question, new List<ListableItem<bool>>()
            {
                new ListViewItem<bool>("Ja", true),
                new ListViewItem<bool>("Nee", false),
            });
            bool cancelReservartion = (bool)replaceCurrentQuestion.ShowAndGetResult();

            if (!cancelReservartion)
            {
                var messageNotCancled = Program.SettingService
                    .GetConsoleText("consoleVisitorReservationChangeNotChanges", new()
                    {
                        ["time"] = _tour.GetTime(),
                    });

                new AlertView(messageNotCancled, AlertView.Success).Show();

                return;
            }

            Program.TourService.RemoveTourReservation(_tour, _visitor);

            var messageCancled = Program.SettingService.GetConsoleText("consoleVisitorReservationCancellationConfirmation");

            new AlertView(messageCancled, AlertView.Info).Show();
        }
    }
}
