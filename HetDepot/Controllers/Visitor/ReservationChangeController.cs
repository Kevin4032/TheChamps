using HetDepot.People.Model;
using HetDepot.Tours.Model;
using HetDepot.Views;
using HetDepot.Views.Parts;

namespace HetDepot.Controllers
{
    public class ReservationChangeController : Controller
    {
        private Tour _tour;
        private Visitor _visitor;
        private bool _forGroup;

        public ReservationChangeController(Tour tour, Visitor visitor, bool forGroup = false) : base()
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

            ListView replaceCurrentQuestion = new ListView(question, subquestion, new List<ListableItem>()
            {
                new ListViewItem("Ja", true),
                new ListViewItem("Nee", false),
            });
            bool replacePrev = (bool)replaceCurrentQuestion.ShowAndGetResult();

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
            
            NextController = new ReservationForGroupController(_tour);
        }
    }
}