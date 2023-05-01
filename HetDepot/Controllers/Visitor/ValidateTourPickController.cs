using HetDepot.People.Model;
using HetDepot.Tours.Model;

namespace HetDepot.Controllers
{
    public class ValidateTourPickController : Controller
    {
        private Tour _tour;
        private Person _person;

        public ValidateTourPickController(Tour tour, Person person) : base()
        {
            _tour = tour;
            _person = person;
        }

        public override void Execute()
        {
            if (_person is Visitor)
            {
                var _visitor = (Visitor)_person;
                var visitorHasAdmission = Program.TourService.HasAdmission(_visitor);
                var visitorHasReservation = Program.TourService.HasReservation(_visitor);

                if (visitorHasAdmission)
                    NextController = new ReservationDeclinedController();

                if (!visitorHasAdmission && visitorHasReservation)
                {
                    var tourWithReservation = Program.TourService.GetReservation(_visitor);

                    if (tourWithReservation!.StartTime == _tour.StartTime)
                        NextController = new ReservationRemoveController(_tour, _visitor);
                    else
                        NextController = new ReservationChangeController(_tour, _visitor);
                }

                if (!visitorHasAdmission && !visitorHasReservation)
                    NextController = new ReservationCreateController(_tour, _visitor);
            }
        }
    }
}
