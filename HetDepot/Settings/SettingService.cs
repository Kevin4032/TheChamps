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

		public string GetConsoleText(string name)
		{
			try
			{
				return _settings.ConsoleText[name];
			}
			catch (Exception e)
			{
				_errorLogger.LogError($"{this.GetType()} - Input [{name}] - {e.Message}");
			}

			return string.Empty; //TODO: nadenken of dit eigen erro moet worden
		}
	}
}
