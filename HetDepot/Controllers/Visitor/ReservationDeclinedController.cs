using HetDepot.Views;

namespace HetDepot.Controllers
{
    public class ReservationDeclinedController : Controller
    {
        public ReservationDeclinedController() : base()
        {
        }

        public override void Execute()
        {
            var message = Program.SettingService.GetConsoleText("visitorAlreadyHasTourAdmission");

            new AlertView(message, AlertView.Info).Show();

            NextController = new ShowToursController();
        }
    }
}
