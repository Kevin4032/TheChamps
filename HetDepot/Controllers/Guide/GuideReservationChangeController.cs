using HetDepot.People.Model;
using HetDepot.Tours.Model;
using HetDepot.Views;
using HetDepot.Views.Parts;

namespace HetDepot.Controllers
{
    public class GuideReservationChangeController : Controller
    {
        private Tour _tour;
        private Visitor _visitor;
        private bool _forGroup;

        public GuideReservationChangeController(Tour tour, Visitor visitor, bool forGroup = false) : base()
        {
            _tour = tour;
            _visitor = visitor;
            _forGroup = forGroup;
        }

        public override void Execute()
        {
            var tourCurrentlySelected = Program.TourService.GetReservation(_visitor);

            if (tourCurrentlySelected == null || _tour == null)
                return; // Kan dit voorkomen? Zo ja, wanneer?

            var question = Program.SettingService
                .GetConsoleText(_forGroup ? "consoleVisitorReservationChangeTourInfoForGroup" :
                    "consoleVisitorReservationChangeTourInfo", new()
                    {
                        ["prevTime"] = tourCurrentlySelected.GetTime(),
                    });

            var subquestion = Program.SettingService
                .GetConsoleText("consoleVisitorReservationCancellationRequestionConfirmation");

            ListView<bool> replaceCurrentQuestion = new(question, subquestion, new List<ListableItem<bool>>()
            {
                new ListViewItem<bool>("Ja", true),
                new ListViewItem<bool>("Nee", false),
            });
            bool replacePrev = replaceCurrentQuestion.ShowAndGetResult();

            NextController = new ShowToursController();

            if (!replacePrev)
            {
                var messageNotChanged = Program.SettingService
                    .GetConsoleText("consoleVisitorReservationChangeNotChanges", new()
                    {
                        ["time"] = tourCurrentlySelected.GetTime(),
                    });
                new AlertView(messageNotChanged, AlertView.Success).Show();
                return;
            }

            Program.TourService.RemoveTourReservation(tourCurrentlySelected, _visitor);
            Program.TourService.AddTourReservation(_tour, _visitor);

            var messageChanged = Program.SettingService.GetConsoleText(
                _forGroup ? "consoleVisitorReservationChangeTourConfirmationForGroup" :
                    "consoleVisitorReservationChangeTourConfirmation", new()
                    {
                        ["currentTime"] = tourCurrentlySelected.GetTime(),
                        ["newTime"] = _tour.GetTime(),
                    });

            new AlertView(messageChanged, AlertView.Success).Show();
            //gaat terug naar de gids aanmelden bezoekers controller.
            NextController = new GuideStartTourAdmissionController(_tour);
        }
    }
}
