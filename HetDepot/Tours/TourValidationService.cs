using HetDepot.People.Model;
using HetDepot.Registration;
using HetDepot.Settings;
using HetDepot.Tours.Model;

namespace HetDepot.Tours
{
	public class TourValidationService
	{
		private RegistrationService _registrationService;
		private SettingService _settingService;

		public TourValidationService(RegistrationService registrationService, SettingService settingService)
		{
			_registrationService = registrationService;
			_settingService = settingService;
		}
		public TourServiceResult VisitorAllowedToMakeReservation(Visitor visitor)
		{
			var result = new TourServiceResult() { Success = !_registrationService.HasTourAdmission(visitor.Id) };

			if (result.Success)
				_registrationService.AddTourReservation(visitor.Id);
			else
				result.Message = _settingService.GetSettingValue("consoleVisitorAlreadyHasTourAdmission");

			return result;
		}
	}
}
