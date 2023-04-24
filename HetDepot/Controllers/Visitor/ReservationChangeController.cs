using HetDepot.People.Model;
using HetDepot.Tours.Model;
using HetDepot.Views;

namespace HetDepot.Controllers
{
	public class ReservationChangeController : Controller
	{
		private Tour _tour;
		private Visitor _visitor;

		public ReservationChangeController(Tour tour, Visitor visitor) : base()
		{ 
			_tour = tour;
			_visitor = visitor;
		}
		public override void Execute()
		{
			var tourCurrentlySelected = _tourService.GetReservation(_visitor);

			_tourService.RemoveTourReservation(tourCurrentlySelected!, _visitor);
			_tourService.AddTourReservation(_tour, _visitor);

			var message = _settingService.GetConsoleText("consoleVisitorReservationChangeTourConfirmation").Replace("{tijdstipOud}", tourCurrentlySelected.StartTime.ToString()).Replace("{tijdstipNieuw}", _tour.StartTime.ToString());

			new AlertView(message, AlertView.Info).Show();

			NextController = new ReservationForGroupController(_tour);
		}
	}
}
