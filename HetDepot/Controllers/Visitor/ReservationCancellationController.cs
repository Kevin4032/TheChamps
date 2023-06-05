using HetDepot.People.Model;
using HetDepot.Tours.Model;
using HetDepot.Views;

namespace HetDepot.Controllers;

public class ReservationCancellationController : Controller
{
    private Visitor _visitor = default!;

    public override void Execute()
    {

        string visitorCode = (new InputView(
            Program.SettingService.GetConsoleText("visitorReservationCancellationTitle"),
            Program.SettingService.GetConsoleText("visitorEnterCode")
        )).ShowAndGetResult();

        try
        {
            _visitor = Program.PeopleService.GetVisitorById(visitorCode);
        }
        catch (NullReferenceException)
        {
            // Visitor not found
            (new AlertView(Program.SettingService.GetConsoleText("visitorLogonCodeInvalid"), AlertView.Error)).Show();
            NextController = this;
            return;
        }

        if (!Program.TourService.HasReservation(_visitor))
        {
            (new AlertView(Program.SettingService.GetConsoleText("visitorReservationCancellationNoReservation"), AlertView.Info)).Show();
            NextController = new ShowToursController();
            return;
        }


        if (Program.TourService.HasAdmission(_visitor))
        {
            (new AlertView(Program.SettingService.GetConsoleText("visitorReservationCancellationAlreadyUsed"), AlertView.Error)).Show();
            return;
        }

        Tour tour = Program.TourService.GetReservation(_visitor)!; // Can't be null, or "HasReservation" above would have returned false 
        Program.TourService.RemoveTourReservation(tour, _visitor);

        (new AlertView(Program.SettingService.GetConsoleText("visitorReservationCancellationConfirmation"), AlertView.Info)).Show();
    }
}
