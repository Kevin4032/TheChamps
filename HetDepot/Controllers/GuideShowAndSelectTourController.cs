using HetDepot.People.Model;
using HetDepot.Tours.Model;
using HetDepot.Views;

namespace HetDepot.Controllers
{
	public class GuideShowAndSelectTourController : Controller
	{
		private Tour _tour;
		private Visitor _visitor;
        private Guide _guide;

		public GuideShowAndSelectTourController(/* Tour tour, Visitor visitor, Guide guide */) : base()
		{ 
		/* 	_tour = tour;
			_visitor = visitor;
            _guide = guide; */
		}
		public override void Execute()
		{

            {
                //nextTour is nu de huidige rondleiding:
                var nextTour = _tourService.GetNextTour(); 

                // Toon op console hudige rondleiding {volgende rondleiding}. guideConfirmTourCheckInStart is een string als user input geeft,anders null
                var guideConfirmTourCheckInStart = new InputView("Huidige rondleiding: " + (nextTour != null ? nextTour.ToString() : "Er zijn geen rondleidingen meer beschikbaar"), "Start deze rondleiding").ShowAndGetResult();
                //als input ja is, begin met inchecken bezoekers
                //Klik enter om te starten i.pv. ja intikken?
                if (guideConfirmTourCheckInStart == "ja")
                {
                    NextController = new GuideStartTourAdmissionController();
                }
                else
                {
                    NextController = new ShowToursController();
                }
            }
        }
	}
}
