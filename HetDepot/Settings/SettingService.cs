using HetDepot.Persistence;

namespace HetDepot.Settings
{
	public class SettingService
	{
		private Dictionary<string, string> _settings;
		private Repository _repository;

		public SettingService(Repository repository)
		{
			_repository = repository;
			_settings = _repository.GetSettings();
		}

		public string GetSettingValue(string name)
		{
			return _settings[name];
		}
	}
}
