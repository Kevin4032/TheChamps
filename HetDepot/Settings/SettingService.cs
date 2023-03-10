using HetDepot.JsonReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HetDepot.Settings
{
	public class SettingService
	{
		private List<Setting> _settings;

		public SettingService()
		{
			//TODO: IoC?
			Init();
		}

		public string GetSettingValue(string name)
		{
			return _settings.FirstOrDefault(s => s.Name == name)?.Value;
		}

		public void WriteSettings()
		{
			foreach (var setting in _settings) { Console.WriteLine($"{setting.Name} - {setting.Value}"); }
		}

		private void Init()
		{
			var localPath = Path.Combine(Directory.GetCurrentDirectory(), "ExampleFile\\ExampleSettings.json");

			Console.WriteLine($"Kevin: {localPath}");
			
			//TODO: Lege settings
			_settings = JsonHelper.ReadJson<List<Setting>>(localPath);

		}
	}
}
