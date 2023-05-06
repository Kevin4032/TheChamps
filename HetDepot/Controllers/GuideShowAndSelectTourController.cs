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

/*             {
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
            } */
            var tours = Program.TourService.Tours;

            var tourList = tours.ToList<IListableObject<Tour>>();

            // Extra optie "Inloggen als gids":
            var extraOptions = new List<ListableItem<Tour>>
            {
                //new ListViewExtraItem<Tour,Controller>(Program.SettingService.GetConsoleText("consoleGuideLogin"), () => new GuideShowAndSelectTourController()),
                //new ListViewExtraItem<Tour,Controller>(Program.SettingService.GetConsoleText("consoleGuideLogin"), () => new GuideController()),

            };

            //TODO: Opmerking Kevin: Als alle rondleidingen vol zitten, 'hangt' de interface
            ListView<Tour> tourOverviewVisitorWithInterface = new(Program.SettingService.GetConsoleText("consoleGuideTourShowToursToStart"), tourList, extraOptions);

            Controller? otherController;
            Tour? selectedTour = tourOverviewVisitorWithInterface.ShowAndGetResult<Controller>(out otherController);
            NextController = otherController; // Alleen als extra optie gekozen is

            if (selectedTour != null)
                NextController = new GuideStartTourAdmissionController(selectedTour);
        }
	}
}
