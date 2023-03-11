using HetDepot.JsonReader;

namespace HetDepot.Settings
{
	public class SettingService
	{
		private List<Setting> _settings;

		public SettingService()
		{
			Init();
		}

		public string GetSettingValue(string name)
		{
			return _settings.FirstOrDefault(s => s.Name == name)?.Value;
		}

		private void Init()
		{
			var localPath = Path.Combine(Directory.GetCurrentDirectory(), "ExampleFile\\ExampleSettings.json");
			
			//TODO: Lege settings
			_settings = JsonHelper.Read<List<Setting>>(localPath);

		}
	}
}
