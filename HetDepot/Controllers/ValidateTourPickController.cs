using HetDepot.Tours.Model;

namespace HetDepot.Controllers
{
	public class ValidateTourPickController : Controller
	{
		private Tour _tour;

		public ValidateTourPickController(Tour tour) : base()
		{ 
			_tour = tour;
		}

		public override void Execute()
		{
			//if (_validationService.VisitorAllowedToMakeReservation(visitor))
			//{
			//	_registrationService.AddTourReservation(visitor.Id);
			//	_tourService.AddTourReservation(tourtje, visitor);
			//}
			//else
			//{
			//	Console.WriteLine(_settingService.GetSettingValue("consoleVisitorAlreadyHasTourAdmission"));
			//}
		}
	}
}
