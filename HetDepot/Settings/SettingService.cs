using HetDepot.Errorlogging;
using HetDepot.Persistence;
using HetDepot.Settings.Model;

namespace HetDepot.Settings
{
	public class SettingService : ISettingService
	{
		private Setting _settings;
		private IRepository _repository;
		private IDepotErrorLogger _errorLogger;

		public SettingService(IRepository repository, IDepotErrorLogger errorLogger)
		{
			_repository = repository;
			_errorLogger = errorLogger;
			_settings = _repository.GetSettings();
		}

		public HashSet<String> GetTourTimes()
		{
			return _settings.TourTimes;
		}

		public int GetMaxTourReservations() => _settings.MaxReservationsPerTour;

		public string GetSettingValue(string name)
		{
			return "";
		}

		public string GetConsoleText(string name)
		{
			return _settings.ConsoleText[name];
		}
	}
}
