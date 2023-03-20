using HetDepot.People;
using HetDepot.Settings;
using HetDepot.Tours;

namespace HetDepot.Controllers
{
	public class CreateReservationController : Controller
	{
		private TourService _tourService;
		private PeopleService _peopleService;
		private SettingService _settingService;

		public CreateReservationController(
			TourService tourService
		,	PeopleService peopleService
		,	SettingService settingService
		)
		{
			_tourService = tourService;
			_peopleService = peopleService;
			_settingService = settingService;
			NextController = new RequestAuthenticationController();
		}

		public override void Execute()
		{
			var visitor = _peopleService.GetVisitorById("E0000000009");
			var tourtje = DateTime.Parse("2023-03-18T11:00:00.0000000+01:00");

			//if (_validationService.VisitorAllowedToMakeReservation(visitor))
			//{
			//	_registrationService.AddTourReservation(visitor.Id);
			//	_tourService.AddTourReservation(tourtje, visitor);
			//}
			//else
			//{
			//	Console.WriteLine(_settingService.GetSettingValue("consoleVisitorAlreadyHasTourAdmission"));
			//}

			//Extra check
			var tourEgt = _tourService.Tours.Where(t => t.StartTime == tourtje).FirstOrDefault();
			
			foreach (var bliebla in tourEgt.Reservations)
			{
				Console.WriteLine($"In de tour zit: {bliebla.Id}");
			}

		}
	}
}
