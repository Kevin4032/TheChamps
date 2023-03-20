using HetDepot.Errorlogging;
using HetDepot.Persistence;

namespace HetDepot.Settings
{
	public class SettingService
	{
		private Dictionary<string, string> _settings;
		private Repository _repository;
		private IDepotErrorLogger _errorLogger;

		public SettingService(Repository repository, IDepotErrorLogger errorLogger)
		{
			_repository = repository;
			_settings = _repository.GetSettings();
			_errorLogger = errorLogger;
		}

		public string GetSettingValue(string name)
		{
			return _settings[name];
		}
	}
}
