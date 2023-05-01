using HetDepot.Tours.Model;
using HetDepot.Views;
using HetDepot.Views.Interface;

namespace HetDepot.Controllers
{
    public class ShowToursController : Controller
    {
        public ShowToursController() : base()
        {
        }

        public override void Execute()
        {
            var tours = Program.TourService.Tours;

            //TODO: Opmerking Kevin: Als alle rondleidingen vol zitten, 'hangt' de interface
            ListView tourOverviewVisitorWithInterface = new ListView(Program.SettingService.GetConsoleText("consoleWelcome"), tours.ToList<IListableObject>());
            Tour selectedTour = (Tour)tourOverviewVisitorWithInterface.ShowAndGetResult();

            NextController = new RequestAuthenticationController(selectedTour);
        }
    }
}
