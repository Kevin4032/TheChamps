using HetDepot.People.Model;
using HetDepot.Tours.Model;

namespace HetDepot.Controllers
{
	public class ValidateTourPickController : Controller
	{
		private Tour _tour;
		private Visitor _visitor;

		public ValidateTourPickController(Tour tour, Visitor visitor) : base()
		{ 
			_tour = tour;
			_visitor = visitor;
		}

		public override void Execute()
		{
			var visitorHasAdmission = _tourService.HasAdmission(_visitor);
			var visitorHasReservation = _tourService.HasReservation(_visitor);

			if (visitorHasAdmission)
				NextController = new ReservationDeclinedController();

			if (!visitorHasAdmission && visitorHasReservation)
			{
				var tourWithReservation = _tourService.GetReservation(_visitor);

				if (tourWithReservation.StartTime == _tour.StartTime)
					NextController = new ReservationRemoveController(_tour, _visitor);
				else
					NextController = new ReservationChangeController(_tour, _visitor);
			}

			if (!visitorHasAdmission && !visitorHasReservation)
				NextController = new ReservationCreateController(_tour, _visitor);
		}
	}
}
