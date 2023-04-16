using HetDepot.People.Model;
using HetDepot.Tours.Model;
using HetDepot.Views;
using HetDepot.Views.Parts;

namespace HetDepot.Controllers;

public class ReservationForGroupController : Controller
{
    private Tour _tour;
    private Visitor _visitor; // TODO Add this visitor to the reservationAsGroupOwner

    public ReservationForGroupController(Tour tour, Visitor visitor) : base()
    {
        _tour = tour;
        _visitor = visitor;
    }

    public override void Execute()
    {
        // Update tour to have the latest data
        _tour = _tourService.getTourByStartTime(_tour.StartTime);
        
        int freeTourSpaces = _tour.FreeSpaces();

        if (freeTourSpaces <= 0)
        {
            NextController = new ShowToursController();
            return;
        }

        var groupReservationQuestion =
            new ListView(
                _settingService.GetConsoleText("consoleVisitorReservationForGroupQuestion"),
                _settingService.GetConsoleText("consoleVisitorReservationForGroupSubquestion").Replace("{FreeSpaces}", freeTourSpaces.ToString()),
                new List<ListableItem>()
                {
                    new ListViewItem("Ja", true),
                    new ListViewItem("Nee", false),
                }
            );
        bool anotherReservation = (bool)groupReservationQuestion.ShowAndGetResult();

        if (!anotherReservation)
        {
            NextController = new ShowToursController();
            return;
        }
        
        NextController = new RequestAuthenticationController(_tour, true);
    }
}