using HetDepot.People.Model;
using HetDepot.Tours.Model;

namespace HetDepot.Controllers
{
    public class ValidateTourPickController : Controller
    {
        private Tour _tour;
        private Person _person;
        private bool _forGroup;

        public ValidateTourPickController(Tour tour, Person person, bool forGroup = false) : base()
        {
            _tour = tour;
            _person = person;
            _forGroup = forGroup;
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
                    {
                        NextController = new ReservationRemoveController(_tour, _visitor, _forGroup);
                    }
                    else
                    {
                        NextController = new ReservationChangeController(_tour, _visitor, _forGroup);
                    }
                }

                if (!visitorHasAdmission && !visitorHasReservation)
                    NextController = new ReservationCreateController(_tour, _visitor);
            }
        }
    }
}
