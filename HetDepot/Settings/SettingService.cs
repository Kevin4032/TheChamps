using HetDepot.Errorlogging;
using HetDepot.Persistence;
using HetDepot.Settings.Model;

namespace HetDepot.Settings
{
	public class SettingService : ISettingService
	{
		private Setting _settings;
		private Repository _repository;
		private IDepotErrorLogger _errorLogger;

		public SettingService(Repository repository, IDepotErrorLogger errorLogger)
		{
			_repository = repository;
			_settings = _repository.GetSettings();
			_errorLogger = errorLogger;
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
