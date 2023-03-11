using HetDepot.People.Model;
using HetDepot.Settings;
using HetDepot.Validation;
using System.Buffers;
using System.Data;
using System.IO;

namespace HetDepot.Persistence
{
	public class Repository
	{
		private IDepotDataReadWrite _depotDataReadWrite;
		private IDepotLogger _depotLogger;

		public Repository(IDepotDataReadWrite dataReadWrite, IDepotLogger depotLogger)
		{ 
			_depotDataReadWrite = dataReadWrite;
			_depotLogger = depotLogger;
		}

		public List<T> GetPeople<T>(string path) where T : Person
		{
			var people = _depotDataReadWrite.Read<List<T>>(path);
			var result = new List<T>();

			foreach (var person in people)
			{
				result.Add(person);
			}

			return result;
		}

		public Dictionary<string, string> GetSettings()
		{
			var settingsPath = Path.Combine(Directory.GetCurrentDirectory(), "ExampleFile\\ExampleSettings.json");

			//TODO: Lege settings
			var settings = DepotJson.Read<List<Setting>>(settingsPath);

			var result = new Dictionary<string, string>();

			foreach (var setting in settings)
			{
				result[setting.Name] = setting.Value;
			}

			return result;
		}

		public void WriteTourAdmission()
		{

		}
	}
}


/*
 		private List<T> AddToPeople<T>(string path) where T : Person
		{
			var result = new List<T>();
			var people = JsonHelper.Read<List<T>>(path);

			foreach (var person in people)
			{
				if (_validationService.ValidForAdministration(person))
					result.Add(person);
				else
					Console.WriteLine($"Lekker bezig {person}");
				//TODO: errors teruggeven / verschil in aangeleverd en ingelezen
			}

			return result;
		} 
*/